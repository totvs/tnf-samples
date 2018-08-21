import { Host } from "../app/utils/host";

export const environment = {
    production: true,

    authorityEndPoint: `http://${Host.GetTenantName()}.rac.totvs.com.br/totvs.rac`,
    applicationEndPoint: `http://${Host.GetTenantName()}.localhost:5055`,
};
