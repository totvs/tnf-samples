/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsMenu
* @name totvsMenuConstant
* @object factory
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @requires totvs-menu.module
*
* @dependencies
*
* @description
*/

(function () {

    angular
        .module('totvsMenu')
        .constant('totvsMenuConstant', {
            menuGroups: {
                RECENTS: "recents",
                FAVORITES: "favorites",
                APPLICATIONS: "application",
                PROCESSES: "processes"
            }
        });

}());
