import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'CustomerRegister',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44380/',
    redirectUri: baseUrl,
    clientId: 'CustomerRegister_App',
    responseType: 'code',
    scope: 'offline_access CustomerRegister',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44380',
      rootNamespace: 'CustomerRegister',
    },
  },
} as Environment;
