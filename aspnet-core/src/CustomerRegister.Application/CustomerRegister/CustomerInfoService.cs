using CustomerRegister.CustomerRegister;
using CustomerRegister.Dtos;
using CustomerRegister.Register;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.TextTemplating;

namespace CustomerRegister
{
    //public class CustomerInfoService : CrudAppService<CustomerInfo, CustomerInfoDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCustomerDto>
    //{
    //    public CustomerInfoService(IRepository<CustomerInfo, Guid> repository) : base(repository) { }
    //}

    public class CustomerInfoService : CustomerRegisterAppService, ICustomerInfoServices
    {
        private readonly IRepository<CustomerInfo, Guid> _repository;
        private readonly IEmailSender _emailSender;
        private readonly ITemplateRenderer _templateRenderer;
        private readonly IConfiguration _configuration;

        public CustomerInfoService(IRepository<CustomerInfo, Guid> repository, IEmailSender emailSender, ITemplateRenderer templateRenderer, IConfiguration configuration)
        {
            _repository = repository;
            _emailSender = emailSender;
            _templateRenderer = templateRenderer;
            _configuration = configuration;

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

        public async Task<CustomerInfoDto> CreateAsync(CreateUpdateCustomerDto input)
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
                //EmailBody = "HI " + input.CustomerName +
                //", This is the reminder for you that your course " +
                //input.CourseName + ", is on " + input.StartDate,
                EmailTo = input.CustomerEmail,
                //EmailBody = await _templateRenderer.RenderAsync(
                //    StandardEmailTemplates.Message, new
                //    {
                //        message = "HI " + input.CustomerName + ", This is the reminder for you that your course " + input.CourseName + ", is on " + input.StartDate,
                //    }
                //    ),
                EmailBody= "HI " + input.CustomerName + ", This is the reminder for you that your course " + input.CourseName + ", is on " + input.StartDate,
                EmailSubject = "Subject",

            };

            //SendEmail(emailData);
            await SendEmailAsync(emailData);

            return ObjectMapper.Map<CustomerInfo, CustomerInfoDto>(customer);
        }

        //public bool SendEmail(EmailData emailData)
        //{
        //    try
        //    {
        //        MimeMessage emailMessage = new();

        //        CustomerInfo info = new();

        //        MailboxAddress emailFrom = new(CustomerRegisterConsts.Name, CustomerRegisterConsts.EmailId);
        //        emailMessage.From.Add(emailFrom);

        //        //MailboxAddress emailTo = new(emailData.EmailToName, emailData.EmailToId);
        //        MailboxAddress emailTo = new(emailData.EmailToName, emailData.EmailToId);
        //        emailMessage.To.Add(emailTo);

        //        emailMessage.Subject = emailData.EmailSubject;

        //        BodyBuilder emailBodyBuilder = new()
        //        {
        //            TextBody = emailData.EmailBody
        //        };
        //        emailMessage.Body = emailBodyBuilder.ToMessageBody();


        //        SmtpClient emailClient = new();

        //        emailClient.Connect(CustomerRegisterConsts.Host, CustomerRegisterConsts.Port, CustomerRegisterConsts.UseSSL);
        //        emailClient.Authenticate(CustomerRegisterConsts.EmailId, CustomerRegisterConsts.Password);
        //        emailClient.Send(emailMessage);
        //        emailClient.Disconnect(true);
        //        emailClient.Dispose();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log Exception Details
        //        return false;
        //    }
        //}

        public async Task SendEmailAsync(EmailData emailData)
        {

            try
            {
                var apiKey = _configuration.GetRequiredSection("EmailSettings").GetValue<string>("SendGridAPIKey");
                var client = new SendGridClient(apiKey);
                var FromEmail = _configuration.GetRequiredSection("EmailSettings").GetValue<string>("FromEmail");
                var EmailBody = emailData.EmailBody;

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(FromEmail),
                    Subject = "Plain Text Email",
                    PlainTextContent = EmailBody
                };

                msg.AddTo(emailData.EmailTo);

                var response = await client.SendEmailAsync(msg);

                string message = response.IsSuccessStatusCode ? "Email Send" : "Email Sending Failed..";
                //return Ok(message);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }

        }
    }
}