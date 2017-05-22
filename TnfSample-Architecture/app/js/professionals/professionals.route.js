/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module professional
* @object module
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @dependencies
*
* @description Modulo professional
*/

(function () {
    'use strict';

    angular
        .module('professional')
        .config(professionalRouteConfig);

    professionalRouteConfig.$inject = ['$stateProvider', 'TNF_ENVIROMENT'];

    function professionalRouteConfig($stateProvider, TNF_ENVIROMENT) {
        var title = (TNF_ENVIROMENT.currentEnviroment === TNF_ENVIROMENT.enum.DEVELOPMENT) ? 'Professionals' : 'Professional';

        $stateProvider.state('professionals', {
            abstract: true,
            template: '<ui-view/>'

        }).state('professionals.start', {
            url: '/professionals',
            controller: 'ProfessionalListController',
            controllerAs: 'controller',
            templateUrl: 'js/professionals/professionals-list.view.html',
            title: title
            
        }).state('professionals.detail', {
            url: '/professionals/detail/:professionalId/:code',
            controller: 'ProfessionalDetailController',
            controllerAs: 'controller',
            templateUrl: 'js/professionals/professionals-detail.view.html',
            title: title

        }).state('professionals.new', {
            url: '/professionals/new',
            controller: 'ProfessionalEditController',
            controllerAs: 'controller',
            templateUrl: 'js/professionals/professionals-edit.view.html',
            title: title

        }).state('professionals.edit', {
            url: '/professionals/edit/:professionalId/:code',
            controller: 'ProfessionalEditController',
            controllerAs: 'controller',
            templateUrl: 'js/professionals/professionals-edit.view.html',
            title: title

        });
    }

}());
