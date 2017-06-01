/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsProfessional
* @name TotvsProfessionalListController
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
        .module('professional')
        .factory('professionalFactory', professionalFactory);

    professionalFactory.$inject = ['$totvsresource', 'NotifyFactory', 'TNF_ENVIROMENT'];

    function professionalFactory($totvsresource, NotifyFactory, TNF_ENVIROMENT) {

        var url = TNF_ENVIROMENT.apiurl + 'api/professional/:professionalId/:code',
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

        function getRecord(professionalId, code, callback) {
            var callback = NotifyFactory.encapsulateCallback(callback);

            return this.TOTVSGet({ professionalId: professionalId, code: code, expand: "professionalSpecialties.specialty" }, callback);
        }

        function saveRecord(model, callback) {
            var callback = NotifyFactory.encapsulateCallback(callback);

            return this.TOTVSSave({}, model, callback);
        }

        function updateRecord(professionalId, code, model, callback) {
            var callback = NotifyFactory.encapsulateCallback(callback);

            return this.TOTVSUpdate({ professionalId: professionalId, code: code }, model, callback);
        }

        function deleteRecord(professionalId, code, callback) {
            var callback = NotifyFactory.encapsulateCallback(callback);

            return this.TOTVSRemove({ professionalId: professionalId, code: code }, callback);
        }
    }

}());