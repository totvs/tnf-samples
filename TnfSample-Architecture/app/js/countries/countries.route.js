/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module country
* @object module
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @dependencies
*
* @description Modulo country
*/

(function () {
    'use strict';

    angular
        .module('country')
        .config(countryRouteConfig);

    countryRouteConfig.$inject = ['$stateProvider'];

    function countryRouteConfig($stateProvider) {
        var title = 'Country';

        $stateProvider.state('countries', {
            abstract: true,
            template: '<ui-view/>'

        }).state('countries.start', {
            url: '/countries',
            controller: 'CountryListController',
            controllerAs: 'controller',
            templateUrl: 'js/countries/countries-list.view.html',
            title: title
            
        }).state('countries.detail', {
            url: '/countries/detail/:id',
            controller: 'CountryDetailController',
            controllerAs: 'controller',
            templateUrl: 'js/countries/countries-detail.view.html',
            title: title

        }).state('countries.new', {
            url: '/countries/new',
            controller: 'CountryEditController',
            controllerAs: 'controller',
            templateUrl: 'js/countries/countries-edit.view.html',
            title: title

        }).state('countries.edit', {
            url: '/countries/edit/:id',
            controller: 'CountryEditController',
            controllerAs: 'controller',
            templateUrl: 'js/countries/countries-edit.view.html',
            title: title

        });
    }

}());
