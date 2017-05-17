/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsApp
* @name totvsAppConfig
* @object config
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @requires totvs-app.module
*
* @dependencies
*
* @description Main config
*/

(function () {

    'use strict';

    angular
        .module('totvsApp')
        .factory('NotifyFactory', NotifyFactory);

    NotifyFactory.$inject = ['totvs.app-notification.Service'];

    function NotifyFactory(totvsNotification){
		var self = this;

        self.showNotifications = showNotifications;
        self.encapsulateCallback = encapsulateCallback;

        return self;
        
        function showNotifications(notifications) {
            angular.forEach(notifications, function (value) {
                totvsNotification.notify({
                    type: 'error',
                    title: 'Erro',
                    detail: value.message
                });
            });
        }

        function encapsulateCallback(callback){
            var _callback = callback;
            return function (result) {
                if (result.notifications)
                    self.showNotifications(result.notifications);
                _callback(result);
            }
        }
    }

    angular
        .module('totvsApp')
        .factory('EnviromentFactory', EnviromentFactory);

    function EnviromentFactory(){
		var self = this;

        self.enviroment = {
            DEVELOPMENT: 0,
            RELEASE: 1
        };
        
        self.currentEnviroment = (window.location.hostname.indexOf("amazon") === -1) ?
                self.enviroment.DEVELOPMENT :
                self.enviroment.RELEASE;
        
        self.hostname = (currentEnviroment === enviroment.DEVELOPMENT) ? "localhost" : "ec2-35-165-157-186.us-west-2.compute.amazonaws.com";
            
        self.apiurl = 'http://' + TnfHostName + ':5050/';

        return self;
    }

}());
