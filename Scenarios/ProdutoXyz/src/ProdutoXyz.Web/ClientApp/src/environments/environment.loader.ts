import { environment } from './environment';
import { Host } from '../app/utils/host';

const TENANT_PATTERN = "{tenant}"

export async function loadEnvironment() {
    var envJson = await getEnvironmentJson();

    if (envJson) {
        replaceAuthoriryEndpoint(envJson);

        for (var propertyName in envJson) {
            environment[propertyName] = envJson[propertyName];
        }
    }

    return envJson;
}

function replaceAuthoriryEndpoint(envJson: any) {
    envJson.authorityEndPoint = tryReplaceTenantPattern(envJson.authorityEndPoint);
}

function getEnvironmentJson() {
    return new Promise<any>((resolve, reject) => {
        var httpRequest = new XMLHttpRequest();
        httpRequest.open("Get", "./api/environment", true);

        httpRequest.onload = function () {
            if (httpRequest.status === 200) {
                resolve(JSON.parse(httpRequest.responseText));
            } else {
                resolve(null);
            }
        };

        httpRequest.send();
    });
}

function tryReplaceTenantPattern(url: string): string {
    if (url && hasTenantPattern(url)) {
        var tenantName = Host.getTenantName();
        if (tenantName) {
            url = repalceTenantPattern(url, tenantName);
        } else {
            throw `Unable to find the tenantName on the host ${Host.getTenantName()}`;
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
