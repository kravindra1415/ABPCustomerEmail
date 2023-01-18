import type { CreateUpdateCustomerDto, CustomerInfoDto, GetCustomerDto, SendAllEmailDto, TemplateInfoDto } from './dtos/models';
import type { EmailTemplateTypes } from './email-templates/email-template-types.enum';
import type { CustomerInfo, EmailData } from './register/models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CustomerInfoService {
  apiName = 'Default';
  

  create = (input: CreateUpdateCustomerDto) =>
    this.restService.request<any, CustomerInfoDto>({
      method: 'POST',
      url: '/api/app/customer-info',
      body: input,
    },
    { apiName: this.apiName });
  

  getAllTemplates = () =>
    this.restService.request<any, string[]>({
      method: 'GET',
      url: '/api/app/customer-info/templates',
    },
    { apiName: this.apiName });
  

  getCustomersByInput = (input: GetCustomerDto) =>
    this.restService.request<any, PagedResultDto<CustomerInfoDto>>({
      method: 'GET',
      url: '/api/app/customer-info/customers',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getEmailTemplateByEmailTemplateName = (emailTemplateName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/customer-info/email-template',
      params: { emailTemplateName },
    },
    { apiName: this.apiName });
  

  getTemplateName = () =>
    this.restService.request<any, TemplateInfoDto[]>({
      method: 'GET',
      url: '/api/app/customer-info/template-name',
    },
    { apiName: this.apiName });
  

  sendEmail = (emailData: EmailData, info: CustomerInfo, emailTemplate: EmailTemplateTypes) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/customer-info/send-email',
      params: { emailTemplate },
      body: info,
    },
    { apiName: this.apiName });
  

  sendEmailAllByInputAndEmailTemplateName = (input: SendAllEmailDto, emailTemplateName: string) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/customer-info/send-email-all',
      params: { emailTemplateName },
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
