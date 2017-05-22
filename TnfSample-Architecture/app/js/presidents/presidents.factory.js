/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsPresident
* @name TotvsPresidentListController
* @object controller
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @requires
*
* @dependencies
*
* @description Responsável por interagir com o REST, buscando e enviado informações para o servidor (back-end)
*/

(function () {

    'use strict';

    angular
        .module('president')
        .factory('presidentFactory', presidentFactory);

    presidentFactory.$inject = ['$totvsresource', 'NotifyFactory', 'TNF_ENVIROMENT'];

    function presidentFactory($totvsresource, NotifyFactory, TNF_ENVIROMENT) {

        var url = TNF_ENVIROMENT.apiurl + 'api/white-house/:id', 
            factory;

        factory = $totvsresource.REST(url, {}, {});

        factory.findRecords = findRecords; // Busca todos os registros
        factory.getRecord = getRecord; // Busca um registro específico
        factory.saveRecord = saveRecord; // Salva um novo registro
        factory.updateRecord = updateRecord; // Atualiza um registro
        factory.deleteRecord = deleteRecord; // Exclui um registro

        return factory;

        // *********************************************************************************
		// *** Functions
		// *********************************************************************************

        function findRecords(parameters, callback) {
            var callback = NotifyFactory.encapsulateCallback(callback);

            return this.TOTVSQuery(parameters, callback);
        }

        function getRecord(id, callback) {
            var callback = NotifyFactory.encapsulateCallback(callback);

            return this.TOTVSGet({ id: id }, callback);
        }

        function saveRecord(model, callback) {
            var callback = NotifyFactory.encapsulateCallback(callback);

            return this.TOTVSSave({}, model, callback);
        }

        function updateRecord(id, model, callback) {
            var callback = NotifyFactory.encapsulateCallback(callback);

            return this.TOTVSUpdate({ id: id }, model, callback);
        }

        function deleteRecord(id, callback) {
            var callback = NotifyFactory.encapsulateCallback(callback);

            return this.TOTVSRemove({ id: id }, callback);
        }
    }

}());