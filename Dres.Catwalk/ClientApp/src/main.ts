import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { API_BASE_URL } from './app/api-client/services';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href.replace(/\/$/, '');
}

const providers = [
  { provide: API_BASE_URL, useFactory: getBaseUrl, deps: [] }
];

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.error(err));
