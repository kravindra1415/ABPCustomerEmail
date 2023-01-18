using CustomerRegister.CustomerRegister;
using CustomerRegister.Dtos;
using CustomerRegister.EmailTemplates;
using CustomerRegister.Register;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;
using Volo.Abp.TextTemplating;

namespace CustomerRegister
{
    public class CustomerInfoService : CustomerRegisterAppService, ICustomerInfoServices
    {
        private readonly IRepository<CustomerInfo, Guid> _repository;
        private readonly IRepository<TemplateInfo, Guid> _tempRepository;
        private readonly IConfiguration _configuration;
        private readonly ITemplateRenderer _templateRender;
        private readonly IEmailSender _emailSender;

        public CustomerInfoService(IRepository<CustomerInfo, Guid> repository,
            IConfiguration configuration, IRepository<TemplateInfo,
                Guid> tempRepository,
            ITemplateRenderer templateRender,
            IEmailSender emailSender
            )
        {
            _repository = repository;
            _configuration = configuration;
            _tempRepository = tempRepository;
            _templateRender = templateRender;
            _emailSender = emailSender;
        }

        public async Task<PagedResultDto<CustomerInfoDto>> GetCustomers(GetCustomerDto input)
        {
            var customer = await _repository.GetListAsync();

            var totalCount = input.Filter == null
            ? await _repository.CountAsync()
            : await _repository.CountAsync(a => a.CustomerName.Contains(input.Filter));

            //var customerDto = ObjectMapper.Map<CustomerInfoDto, CustomerInfo>();

            return new PagedResultDto<CustomerInfoDto>(
                totalCount,
            ObjectMapper.Map<List<CustomerInfo>, List<CustomerInfoDto>>(customer));
        }

        //public async Task SendEmailAll(List<string> emails)
        //{
        //    var getAllData = await _repository.GetListAsync();

        //    foreach (var emailItem in emails)
        //    {
        //        try
        //        {

        //            var getAll = await _repository.GetListAsync(a => a.CustomerEmail == emailItem);

        //            foreach (var customerData in getAll)
        //            {
        //                var customer = new CustomerInfo
        //                {
        //                    CustomerName = customerData.CustomerName,
        //                    CustomerEmail = customerData.CustomerEmail,
        //                    StartDate = customerData.StartDate,
        //                    EndDate = customerData.EndDate,
        //                    CourseName = customerData.CourseName
        //                };

        //                var body = await _templateRender.RenderAsync(EmailTemplateConst.Registration, new
        //                {
        //                    name = customer.CustomerName,
        //                    course = customer.CourseName,
        //                    startdate = customer.StartDate,
        //                    enddate = customer.EndDate,
        //                    status = "active"
        //                });

        //                await _emailSender.SendAsync(
        //                   customer.CustomerEmail,     // target email address
        //                   "Registration Mail",  // subject
        //                   body  // email body 
        //               );

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            var msg = ex.Message;
        //            throw;
        //        }
        //    }
        //}

        public async Task SendEmailAll(SendAllEmailDto input,string emailTemplateName)
        {
            string body;
            try
            {
                // var email = new List<string>();
                foreach (var emailItem in input.Emails)
                {
                    var getAll = await _repository.GetListAsync(a => a.CustomerEmail == emailItem);

                    foreach (var customerData in getAll)
                    {
                        var customer = new CustomerInfo
                        {
                            CustomerName = customerData.CustomerName,
                            CustomerEmail = customerData.CustomerEmail,
                            StartDate = customerData.StartDate,
                            EndDate = customerData.EndDate,
                            CourseName = customerData.CourseName
                        };

                        //var body = await _templateRender.RenderAsync(EmailTemplateConst.Registration, new
                        //{
                        //    name = customer.CustomerName,
                        //    course = customer.CourseName,
                        //    startdate = customer.StartDate,
                        //    enddate = customer.EndDate,
                        //    status = "active"
                        //});

                        body = body = (await _tempRepository.FirstAsync(x => x.TemplateName == emailTemplateName)).TemplateData;

                        body = body.Replace("{{name}}",customer.CustomerName);
                        body = body.Replace("{{course}}",customer.CourseName);
                        body = body.Replace("{{startdate}}", customer.StartDate.ToString());
                        body = body.Replace("{{enddate}}", customer.EndDate.ToString());

                        await _emailSender.SendAsync(
                           customer.CustomerEmail,     // target email address
                           emailTemplateName,  // subject
                           body  // email body 
                       );
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }

        public async Task<List<string>> GetAllTemplates()
        {
            var dd = (await _tempRepository.GetQueryableAsync()).Select(x => x.TemplateName).ToList();
            return dd;
        }

        public async Task<string> GetEmailTemplate(string emailTemplateName)
        {
            string body;
            body = (await _tempRepository.FirstAsync(x => x.TemplateName == emailTemplateName)).TemplateData;
            return body;
        }

        public async Task<CustomerInfoDto> CreateAsync(CreateUpdateCustomerDto input)
        {
            try
            {
                var customer = new CustomerInfo
                {
                    CustomerName = input.CustomerName,
                    CustomerEmail = input.CustomerEmail,
                    StartDate = input.StartDate,
                    EndDate = input.EndDate,
                    CourseName = input.CourseName
                };

                await _repository.InsertAsync(customer);

                EmailData emailData = new()
                {
                    EmailToName = input.CustomerName,
                    EmailToId = input.CustomerEmail,
                    //EmailSubject = "Confirmation Mail",
                    EmailFrom = _configuration.GetSection("Settings").GetValue<string>("Abp.Mailing.DefaultFromAddress")
                };

                await SendEmailAsync(emailData, customer, input.EmailTemplateType.GetValueOrDefault());

                return ObjectMapper.Map<CustomerInfo, CustomerInfoDto>(customer);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }

        public async Task SendEmailAsync(EmailData emailData, CustomerInfo info, EmailTemplateTypes emailTemplate)
        {

            if (EmailTemplateTypes.Registration == emailTemplate)
            {
                try
                {
                    var body = await _templateRender.RenderAsync(EmailTemplateConst.Registration, new
                    {
                        name = info.CustomerName,
                        course = info.CourseName,
                        startdate = info.StartDate,
                        enddate = info.EndDate,
                        status = "active"
                    });

                    await _emailSender.SendAsync(
                       emailData.EmailToId,     // target email address
                       emailData.EmailSubject = "Registration Mail",  // subject
                       body  // email body 
                   );

                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    throw;
                }
            }
            else if (EmailTemplateTypes.Confirmation == emailTemplate)
            {
                try
                {
                    var body = await _templateRender.RenderAsync(EmailTemplateConst.Confirmation, new
                    {
                        name = info.CustomerName,
                        course = info.CourseName,
                        startdate = info.StartDate,
                        status = "active"
                    });

                    await _emailSender.SendAsync(
                       emailData.EmailToId,     // target email address
                       emailData.EmailSubject = "Confirmation Mail",  // subject
                       body  // email body 
                   );

                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    throw;
                }
            }
        }

        public async Task<List<TemplateInfoDto>> GetTemplateNameAsync()
        {
            var coursename = await _tempRepository.GetListAsync();
            return coursename.Select(item => new TemplateInfoDto
            {
                TemplateName = item.TemplateName
            }).ToList();
        }

        //public async Task SendEmailAsync(EmailData emailData, CustomerInfo info)
        //{
        //    try
        //    {


        //        var apiKey = _configuration.GetSection("EmailSetting").GetValue<string>("SendGridAPIKey");
        //        var tempPath = _configuration.GetSection("EmailSetting").GetValue<string>("TemplatePath");
        //        var client = new SendGridClient(apiKey);

        //        var data = await _tempRepository.GetListAsync();
        //        //var emailTempData = await _tempRepository.GetAsync(x => x.TemplateName + ".tpl" == "EmailTemplate.tpl");

        //        var emailBody = emailData.EmailBody;
        //        var body = "";
        //        var tempData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, tempPath);
        //        //var tempData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, emailTempData.ToString());

        //        using (StreamReader reader = new(tempData))
        //        {
        //            body = reader.ReadToEnd();
        //        }

        //        body = body.Replace("{{name}}", info.CustomerName);
        //        body = body.Replace("{{course}}", info.CourseName);
        //        body = body.Replace("{{startdate}}", info.StartDate.ToString());
        //        body = body.Replace("{{enddate}}", info.StartDate.ToString());
        //        body = body.Replace("{{status}}", "active");

        //        var msg = new SendGridMessage()
        //        {
        //            From = new EmailAddress(emailData.EmailFrom),
        //            Subject = "Plain Text Email",
        //            //PlainTextContent = emailBody
        //            // HtmlContent = EmailHTMLTemplate(info)
        //            HtmlContent = body
        //        };

        //        msg.AddTo(emailData.EmailToId);

        //        var response = await client.SendEmailAsync(msg);

        //        string message = response.IsSuccessStatusCode ? "Email Send" : "Email Sending Failed..";
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message;
        //        throw;
        //    }
        //}


        //public async Task SendEmailAsync(EmailData emailData, CustomerInfo info)
        //{
        //    try
        //    {

        //        string body = await _templateRender.RenderAsync(EmailTemplateConst.Registration, new
        //        {
        //            name = "aman"
        //        });

        //var apiKey = _configuration.GetSection("EmailSetting").GetValue<string>("SendGridAPIKey");
        ////var tempPath = _configuration.GetSection("EmailSetting").GetValue<string>("TemplatePath");

        //var client = new SendGridClient(apiKey);


        //var dataAll = await _tempRepository.GetListAsync();
        //var emailTempData = await _tempRepository.GetAsync(x => x.TemplateName + ".tpl" == "EmailTemplate.tpl");
        //var getTemplateName = emailTempData.TemplateName;

        //var tempPath = _configuration.GetSection("EmailSetting").GetValue<string>("TemplatePath") + getTemplateName + ".tpl";

        //var emailBody = emailData.EmailBody;
        //var body = "";
        //var tempData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, tempPath);
        ////var tempData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, emailTempData.ToString());

        //using (StreamReader reader = new(tempData))
        //{
        //    body = reader.ReadToEnd();
        //}

        //body = body.Replace("{{name}}", info.CustomerName);
        //body = body.Replace("{{course}}", info.CourseName);
        //body = body.Replace("{{startdate}}", info.StartDate.ToString());
        //body = body.Replace("{{enddate}}", info.StartDate.ToString());
        //body = body.Replace("{{status}}", "active");

        //var msg = new SendGridMessage()
        //{
        //    From = new EmailAddress(emailData.EmailFrom),
        //    Subject = "Reminder Mail",
        //    //PlainTextContent = emailBody
        //    // HtmlContent = EmailHTMLTemplate(info)
        //    HtmlContent = body
        //};

        //msg.AddTo(emailData.EmailToId);

        //var response = await client.SendEmailAsync(msg);

        //string message = response.IsSuccessStatusCode ? "Email Send" : "Email Sending Failed..";
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message;
        //        throw;
        //    }
        //}

        //private string EmailHTMLTemplate(CustomerInfo customerInfo)
        //{
        //    var htmlMessage = @"<html><head>
        //                <meta charset='utf-8' />
        //                <title></title>
        //                <style>
        //                @mixin media() {
        //                @media (min-width: 768px) {
        //                @content;
        //                }
        //                }

        //                body, html {
        //                font-family: 'Vollkorn', serif;
        //                font-weight: 400;
        //                line-height: 1.3;
        //                font-size: 16px;
        //                }

        //                .siteTitle {
        //                display: block;
        //                font-weight: 900;
        //                font-size: 30px;
        //                margin: 20px 0;

        //                @include media {
        //                font-size: 60px;
        //                }
        //                }

        //                header,
        //                main,
        //                footer {
        //                max-width: 960px;
        //                margin: 0 auto;
        //                }

        //                .card {
        //                height: 400px;
        //                position: relative;
        //                padding: 20px;
        //                box-sizing: border-box;
        //                display: flex;
        //                align-items: flex-end;
        //                text-decoration: none;
        //                border: 4px solid #b0215e;
        //                margin-bottom: 20px;
        //                //background-image: url('https://baylorlariat.com/wp-content/uploads/2018/02/Iron-Man-Movie_Poster_2008.jpg');
        //                background-size: cover;

        //                @include media {
        //                height: 500px;
        //                }
        //                }

        //                .inner {
        //                height: 50%;
        //                display: flex;
        //                flex-direction: column;
        //                justify-content: center;
        //                align-items: center; 
        //                background: white;
        //                box-sizing: border-box;
        //                padding: 40px;

        //                @include media {
        //                width: 50%;
        //                height: 100%;
        //                }
        //                }

        //                .title {
        //                font-size: 24px;
        //                color: black;  
        //                text-align: center;
        //                font-weight: 700;
        //                color: #181818;
        //                text-shadow: 0px 2px 2px #a6f8d5;
        //                position: relative;
        //                margin: 0 0 20px 0;

        //                @include media {
        //                font-size: 30px;
        //                }
        //                }
        //                </style>
        //                </head>
        //                <body>
        //                <div  class='card'>
        //                <div class='inner'>
        //                <h1><b>Reminder</b></h1>

        //                <span>
        //                    <h4>HI <span style='font-size: 18px;'>{{name}}</span>, </h4>
        //                    <h2></h2>
        //                    <h4>This is the reminder for you that your course <span style='font-size: 18px;'>{{course}}</span>, </h4>
        //                    <!-- <h2>{{course}}</h2> -->
        //                    <h4>is on <span style='font-size: 18px;'>{{startdate}}</span></h4>
        //                    <!-- <h2>{{startdate}}</h2> -->
        //                    <time class='subtitle'>Reminder<time>
        //                </span>

        //                </div>
        //                </div>
        //                </body>
        //                </html>";

        //    htmlMessage = htmlMessage.Replace("{{name}}", customerInfo.CustomerName);
        //    htmlMessage = htmlMessage.Replace("{{course}}", customerInfo.CourseName);
        //    htmlMessage = htmlMessage.Replace("{{startdate}}", customerInfo.StartDate.ToString());


        //    return htmlMessage;

        //}

    }
}

//"EmailSettings": {
//  "EmailId": "kravindra141516@gmail.com",
//  "Name": "Support - Pro Code Guide",
//  "Password": "jnzmdzzqphbixocu",
//  "Host": "smtp.gmail.com",
//  "Port": 465,
//  "UseSSL": true
//},


//var abc = await _tempRepository.GetListAsync();
//var xyz = abc.Select(item => new TemplateInfoDto
//{
//    TemplateName = item.TemplateName
//}).ToList();

//var aaa = xyz.FirstOrDefault(a => a.TemplateName == EmailTemplateTypes.Registration.ToString());

//if (emailData.EmailSubject == "Reminder Mail")