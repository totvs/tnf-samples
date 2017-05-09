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
        .factory('appHTTPInterceptor', appHTTPInterceptor);

    // Registra a AngularJS Factory para customização do httpInterceptor padrão do AngularJS.
    appHTTPInterceptor.$inject = ['$q'];

    function appHTTPInterceptor($rootScope, $q) {
        return {
            request: function (config) {
                return config || $q.when(config);
            },
            requestError: function (rejection) {
                return rejection;
            },
            response: function (response) {
                if (response.data.result) {
                    var notifications = response.data.result.notifications || response.data.result.Notifications,
                        success = response.data.result.success;

                    response.data = response.data.result.data || response.data.result.items || response.data.result || {};
                    response.data.notifications = notifications;
                    response.data.success = success;
                }

                return response;
            },
            responseError: function (rejection) {
                return rejection;
            }
        };
    }

}());
