import { Host } from "../app/utils/host";

export const environment = {
    production: true,

    authorityEndPoint: `https://{tenant}.rac.dev.totvs.app/totvs.rac`,
    applicationEndPoint: Host.getHost(),
};
