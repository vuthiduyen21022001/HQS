import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44330/',
  redirectUri: baseUrl,
  clientId: 'Inventory_App',
  responseType: 'code',
  scope: 'offline_access Inventory',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Inventory',
    logoUrl: '',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44330',
      rootNamespace: 'HQSOFT.eBiz.Inventory',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
    Inventory: {
      url: 'https://localhost:44382',
      rootNamespace: 'HQSOFT.eBiz.Inventory',
    },
  },
} as Environment;
