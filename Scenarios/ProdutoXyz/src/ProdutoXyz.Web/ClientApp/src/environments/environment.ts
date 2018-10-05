import { Host } from "../app/utils/host";

export const environment = {
    production: false,

    authorityEndPoint: `http://${Host.GetTenantName()}.rac.totvs.com.br/totvs.rac`,
    applicationEndPoint: `http://${Host.GetTenantName()}.localhost:5055`,
};

