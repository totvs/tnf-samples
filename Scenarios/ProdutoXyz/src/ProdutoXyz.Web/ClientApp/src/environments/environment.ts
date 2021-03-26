import { Host } from "../app/utils/host";

export const environment = {
    production: false,

    authorityEndPoint: `https://{tenant}.rac.dev.totvs.app/totvs.rac`,
    applicationEndPoint: Host.getHost(),
};

