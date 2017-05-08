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

    professionalFactory.$inject = ['$totvsresource'];

    function professionalFactory($totvsresource) {

        var hostname = (currentEnviroment === enviroment.DEVELOPMENT) ? "localhost" : "ec2-35-165-157-186.us-west-2.compute.amazonaws.com",
            url = 'http://' + hostname + ':5050/api/professional/:professionalId/:code',
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
            return this.TOTVSQuery(parameters, callback);
        }

        function getRecord(professionalId, code, callback) {
            return this.TOTVSGet({ professionalId: professionalId, code: code }, callback);
        }

        function saveRecord(model, callback) {
            return this.TOTVSSave({}, model, callback);
        }

        function updateRecord(professionalId, code, model, callback) {
            return this.TOTVSUpdate({ professionalId: professionalId, code: code }, model, callback);
        }

        function deleteRecord(professionalId, code, callback) {
            return this.TOTVSRemove({ professionalId: professionalId, code: code }, callback);
        }
    }


    // Registra a AngularJS Factory para customização do httpInterceptor padrão do AngularJS.
    angular
        .module('professional')
        .factory('professionalHttpInterceptor', appHTTPInterceptors);

    appHTTPInterceptors.$inject = ['$q'];

    function appHTTPInterceptors($q) {
        return {
            request: function (config) {
                return config || $q.when(config);
            },
            requestError: function (rejection) {
                return rejection;
            },
            response: function (response) {
                if (response.data.result
                    && response.data.result.data)
                    response.data = response.data.result.data;

                if (response.data.data)
                    response.data = response.data.data;

                return response;
            },
            responseError: function (rejection) {
                return rejection;
            }
        };
    }

}());