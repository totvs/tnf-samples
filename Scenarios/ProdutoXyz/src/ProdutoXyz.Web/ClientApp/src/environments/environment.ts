import { Host } from "../app/utils/host";

export const environment = {
    production: false,

    authorityEndPoint: `https://localhost:5000`,
    applicationEndPoint: Host.getHost(),
};

