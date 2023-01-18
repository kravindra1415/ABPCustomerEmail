import { mapEnumToOptions } from '@abp/ng.core';

export enum EmailTemplateTypes {
  Registration = 1,
  Confirmation = 2,
}

export const emailTemplateTypesOptions = mapEnumToOptions(EmailTemplateTypes);
