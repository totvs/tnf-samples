export class Host {

    static getHost() {

        var url = document.getElementsByTagName('base')[0].href;

        url = url.replace(/\/$/, "");

        return url;
    }

    static getTenantName() {

        var tenantName = location.hostname.substring(0, location.hostname.indexOf('.'));
        if (tenantName.length > 0)
            return tenantName;

        throw new Error('Invalid Tenant Name');
    }
}
