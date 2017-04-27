/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module president
* @name PresidentSearchController
* @object controller
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @requires
*
* @dependencies
*
* @description Controller responsável por pesquisar os registros
*/

(function () {

    'use strict';

    angular
        .module('president')
        .controller('PresidentSearchController', PresidentSearchController);

    PresidentSearchController.$inject = ['$modalInstance', 'data'];

    function PresidentSearchController($modalInstance, data) {

        // *********************************************************************************
		// *** Variables
		// *********************************************************************************

        var self = this;

        // *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

        self.advancedSearch = angular.copy(data); // copia os dados da pesquisa anterior para o controller
        self.search = search;
        self.close = close;

        // *********************************************************************************
		// *** Functions
		// *********************************************************************************

        function search() {
            $modalInstance.close(self.advancedSearch);
        }

        function close() {
            $modalInstance.dismiss();
        }
    }

}());
