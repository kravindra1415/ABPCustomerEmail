using CustomerRegister.Dtos;
using CustomerRegister.Register;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;
using Volo.Abp.TextTemplating;

namespace CustomerRegister.Templates
{
    public class TemplateService : CustomerRegisterAppService
    {
        private readonly IRepository<TemplateInfo, Guid> _repository;
        private readonly IConfiguration _configuration;
        private readonly ITemplateRenderer _templateRender;
        private readonly IEmailSender _emailSender;

        public TemplateService(IRepository<TemplateInfo, Guid> repository, IEmailSender emailSender, IConfiguration configuration, ITemplateRenderer templateRender)
        {
            _repository = repository;
            _configuration = configuration;
            _templateRender = templateRender;
            _emailSender = emailSender;
        }

        public async Task<PagedResultDto<TemplateInfoDto>> GetTemplates(GetCustomerDto input)
        {
            var template = await _repository.GetListAsync();

            var totalCount = input.Filter == null
            ? await _repository.CountAsync()
            : await _repository.CountAsync(a => a.TemplateName.Contains(input.Filter));


            return new PagedResultDto<TemplateInfoDto>(
                totalCount, ObjectMapper.Map<List<TemplateInfo>, List<TemplateInfoDto>>(template)
           );
        }

        //public async Task<List<TemplateInfoDto>> GetTemplateNameAsync()
        //{
        //    var coursename = await _repository.GetListAsync();
        //    return coursename.Select(item => new TemplateInfoDto
        //    {
        //        TemplateName = item.TemplateName
        //    }).ToList();
        //}

        public async Task<TemplateInfoDto> CreateTempAsync(TemplateInfo input)
        {
            var tempInfo = new TemplateInfo()
            {
                TemplateName = input.TemplateName,
                TemplateData = input.TemplateData,

            };

            await _repository.InsertAsync(tempInfo);
            return ObjectMapper.Map<TemplateInfo, TemplateInfoDto>(tempInfo);
        }

        //public async Task SendEmailAsync(EmailData emailData, TemplateInfo info)
        //{
        //    //var lastRecord = await _repository.LastOrDefaultAsync();
        //        try
        //        {
        //            var body = await _templateRender.RenderAsync(EmailTemplateConst.Registration, new
        //            {
        //                name = info.CustomerName,
        //                course = info.CourseName,
        //                startdate = info.StartDate,
        //                enddate = info.EndDate,
        //                status = "active"
        //            });

        //            await _emailSender.SendAsync(
        //               emailData.EmailToId,     // target email address
        //               emailData.EmailSubject = "Registration Mail",  // subject
        //               body  // email body 
        //           );

        //        }
        //        catch (Exception ex)
        //        {
        //            var msg = ex.Message;
        //            throw;
        //        }
        //}
    }
}