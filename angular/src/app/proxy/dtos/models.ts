import type { EmailTemplateTypes } from '../email-templates/email-template-types.enum';
import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdateCustomerDto {
  customerName: string;
  customerEmail?: string;
  startDate?: string;
  endDate?: string;
  emailTemplateType?: EmailTemplateTypes;
  courseName: string;
}

export interface CustomerInfoDto extends AuditedEntityDto<string> {
  customerName?: string;
  customerEmail?: string;
  startDate?: string;
  endDate?: string;
  courseName?: string;
}

export interface GetCustomerDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface SendAllEmailDto {
  emails: string[];
}

export interface TemplateInfoDto extends AuditedEntityDto<string> {
  templateData?: string;
  templateName?: string;
}
