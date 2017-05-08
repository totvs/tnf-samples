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
        .config(totvsAppConfig);

    totvsAppConfig.$inject = ['$httpProvider'];

    function totvsAppConfig($httpProvider, TOTVSProfileProvider) {

        $httpProvider.interceptors.push(
            'totvsHttpInterceptor',
            'presidentHttpInterceptor',
            'countryHttpInterceptor',
            'professionalHttpInterceptor');

        //TotvsI18nProvider.setBaseContext('/');

    }

}());
