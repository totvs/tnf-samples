/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module president
* @object module
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @dependencies
*
* @description Modulo president
*/

(function () {
    'use strict';

    angular
        .module('president')
        .config(presidentRouteConfig);

    presidentRouteConfig.$inject = ['$stateProvider'];

    function presidentRouteConfig($stateProvider) {
        var title = (currentEnviroment === enviroment.DEVELOPMENT) ? 'Presidents' : 'President';

        $stateProvider.state('presidents', {
            abstract: true,
            template: '<ui-view/>'

        }).state('presidents.start', {
            url: '/presidents',
            controller: 'PresidentListController',
            controllerAs: 'controller',
            templateUrl: 'js/presidents/presidents-list.view.html',
            title: title
            
        }).state('presidents.detail', {
            url: '/presidents/detail/:id',
            controller: 'PresidentDetailController',
            controllerAs: 'controller',
            templateUrl: 'js/presidents/presidents-detail.view.html',
            title: title

        }).state('presidents.new', {
            url: '/presidents/new',
            controller: 'PresidentEditController',
            controllerAs: 'controller',
            templateUrl: 'js/presidents/presidents-edit.view.html',
            title: title

        }).state('presidents.edit', {
            url: '/presidents/edit/:id',
            controller: 'PresidentEditController',
            controllerAs: 'controller',
            templateUrl: 'js/presidents/presidents-edit.view.html',
            title: title

        });
    }

}());
