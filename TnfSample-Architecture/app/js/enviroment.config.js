var DynamicEnvironment = (function () {
    'use strict';

    var self = {};

    self.enum = {
        DEVELOPMENT: 0,
        RELEASE: 1
    };

    self.currentEnviroment = (window.location.hostname.indexOf("amazon") === -1) ?
        self.enum.DEVELOPMENT :
        self.enum.RELEASE;

    self.hostname = (self.currentEnviroment === self.enum.DEVELOPMENT) ? "localhost" : "ec2-35-165-157-186.us-west-2.compute.amazonaws.com";

    self.apiurl = 'http://' + self.hostname + ':5050/';

    return self;
}());
