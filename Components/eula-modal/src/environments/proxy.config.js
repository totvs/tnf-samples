const BASE_TARGET = 'https://api-fluig.dev.totvs.app';
const BASE_TARGET_COMMUNICATION = 'https://communication.dev.totvs.app';
const BASE_BUCKET_TARGET = 'https://s3.amazonaws.com/pages-myfluig-qa-us-east-1';

const proxy = [
    {
        context: '/static',
        target: BASE_BUCKET_TARGET,
        changeOrigin: true,
    },
    {
        context: ['/manager/api', '/accounts'],
        target: BASE_TARGET,
        changeOrigin: true,
    },
    {
        context: '/core/api/v2/notification',
        target: BASE_TARGET_COMMUNICATION,
        changeOrigin: true
    }
];

module.exports = proxy;