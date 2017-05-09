/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module specialty
* @object module
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @dependencies
*
* @description Modulo specialty
*/

(function () {
    'use strict';

    angular
        .module('specialty')
        .config(specialtyRouteConfig);

    specialtyRouteConfig.$inject = ['$stateProvider'];

    function specialtyRouteConfig($stateProvider) {
        var title = 'Specialty';

        $stateProvider.state('specialties', {
            abstract: true,
            template: '<ui-view/>'

        }).state('specialties.start', {
            url: '/specialties',
            controller: 'SpecialtyListController',
            controllerAs: 'controller',
            templateUrl: 'js/specialties/specialties-list.view.html',
            title: title
            
        }).state('specialties.detail', {
            url: '/specialties/detail/:id',
            controller: 'SpecialtyDetailController',
            controllerAs: 'controller',
            templateUrl: 'js/specialties/specialties-detail.view.html',
            title: title

        }).state('specialties.new', {
            url: '/specialties/new',
            controller: 'SpecialtyEditController',
            controllerAs: 'controller',
            templateUrl: 'js/specialties/specialties-edit.view.html',
            title: title

        }).state('specialties.edit', {
            url: '/specialties/edit/:id',
            controller: 'SpecialtyEditController',
            controllerAs: 'controller',
            templateUrl: 'js/specialties/specialties-edit.view.html',
            title: title

        });
    }

}());
