import { hubMessage } from "../app.component";

export class Host {

    static GetHost() {

        var url = document.getElementsByTagName('base')[0].href;

        url = url.replace(/\/$/, "");

        return url;
    }

    static GetTenantName() {

        var tenantName = location.hostname.substring(0, location.hostname.indexOf('.'));
        if (tenantName.length > 0)
            return tenantName;

        throw new Error('Invalid Tenant Name');
    }
}
