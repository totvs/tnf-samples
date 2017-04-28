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

    presidentFactory.$inject = ['$totvsresource'];

    function presidentFactory($totvsresource) {

        var hostname = (window.location.hostname.indexOf("amazon") === -1) ? "localhost:62114" : "ec2-35-165-157-186.us-west-2.compute.amazonaws.com:5000",
            url = 'http://' + hostname + '/api/white-house/:id', 
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

        function getRecord(id, callback) {
            return this.TOTVSGet({id: id}, callback);
        }

        function saveRecord(model, callback) {
            return this.TOTVSSave({}, model, callback);
        }

        function updateRecord(id, model, callback) {
            return this.TOTVSUpdate({id: id}, model, callback);
        }

        function deleteRecord(id, callback) {
            return this.TOTVSRemove({id: id}, callback);
        }
    }

 
    // Registra a AngularJS Factory para customização do httpInterceptor padrão do AngularJS.
    angular        
        .module('president')
        .factory('totvsHttpInterceptor', appHTTPInterceptors);

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
                if(response.data.result
                   && response.data.result.data)
                    response.data = response.data.result.data;

                if(response.data.data)
                    response.data = response.data.data;

                return response;
            },
            responseError: function (rejection) {
                return rejection;
            }
        };
    }

}());