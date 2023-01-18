import type { AuditedAggregateRoot } from '../volo/abp/domain/entities/auditing/models';

export interface CustomerInfo extends AuditedAggregateRoot<string> {
  customerName?: string;
  customerEmail?: string;
  startDate?: string;
  endDate?: string;
  courseName?: string;
}

export interface EmailData {
  emailToId?: string;
  emailToName?: string;
  emailSubject?: string;
  emailBody?: string;
  emailFrom?: string;
}

export interface TemplateInfo extends AuditedAggregateRoot<string> {
  templateData?: string;
  templateName?: string;
}
