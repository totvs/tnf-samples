import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { loadEnvironment } from './environments/environment.loader';
import { Host } from './app/utils/host';

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}

const TENANT_PATTERN = "{tenant}"

const providers = [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
    enableProdMode();
}

environment.authorityEndPoint = tryReplaceTenantPattern(environment.authorityEndPoint);

platformBrowserDynamic(providers).bootstrapModule(AppModule)
    .catch(err => console.log(err));

function tryReplaceTenantPattern(url: string): string {
    if (url && hasTenantPattern(url)) {
        var tenantName = Host.getTenantName();
        if (tenantName) {
            url = repalceTenantPattern(url, tenantName);
        } else {
            throw `Unable to find the tenantName on the host ${Host.getHost()}`;
        }
    }

    return url;
}

function hasTenantPattern(url: string): boolean {
    return url.includes(TENANT_PATTERN);
}

function repalceTenantPattern(url: string, tenantName: string): string {
    return url.replace(TENANT_PATTERN, tenantName);
}
