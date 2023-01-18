import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { GetCustomerDto, TemplateInfoDto } from '../dtos/models';
import type { TemplateInfo } from '../register/models';

@Injectable({
  providedIn: 'root',
})
export class TemplateService {
  apiName = 'Default';
  

  createTemp = (input: TemplateInfo) =>
    this.restService.request<any, TemplateInfoDto>({
      method: 'POST',
      url: '/api/app/template/temp',
      body: input,
    },
    { apiName: this.apiName });
  

  getTemplatesByInput = (input: GetCustomerDto) =>
    this.restService.request<any, PagedResultDto<TemplateInfoDto>>({
      method: 'GET',
      url: '/api/app/template/templates',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
