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
                if (response.data.items)
                    response.data = response.data.items;

                return response;
            },
            responseError: function (response) {
                return response;
            }
        };
    }

}());
