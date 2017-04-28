/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsMenu
* @name totvsMenuProcesses
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

    'use strict';

    angular
        .module('totvsMenu')
        .factory('totvsMenuProcesses', totvsMenuProcesses);

    totvsMenuProcesses.$inject = [];

    function totvsMenuProcesses() {

        var menuProcesses = [],
            processes = {},
            factory = {
                getMenuProcesses: getMenuProcesses,
                getProgramProcesses: getProgramProcesses
            };

        return factory;

        function getMenuProcesses(callback) {
            // TODO: Get list of server
            if (menuProcesses.length === 0) {
                menuProcesses = GenerateMenuProcesses();
            }

            callback(menuProcesses);
        }

        function getProgramProcesses(id, callback) {
            // TODO: Get list of server
            if (!processes[id]) {
                processes[id] = GenerateProcesses(id);
            }

            callback(processes[id]);
        }

    }

    // ********************************************************************
    // Function - Simulation
    // ********************************************************************

    function GenerateMenuProcesses() {
        var i,
            max = 0,
            menuProcesses = [];

        // max = Math.floor((Math.random() * 10) + 5);

        for (i = 1; i <= max; i++) {
            menuProcesses.push(new MenuProcess(
                i,
                i,
                'Menu Processo #' + i
            ));
        }

        return menuProcesses;
    }

    function MenuProcess(id, seq, process) {
        this.id = id || 1;
        this.seq = seq || 1;
        this.process = process || '[process]';
    }

    function GenerateProcesses(id) {
        var i,
            max = 0,
            processes = [],
            module = 1;

        max = Math.floor((Math.random() * 20) + 1);

        for (i = 1; i <= max; i++) {
            module = Math.floor((Math.random() * 3) + 1);

            processes.push(new Process(
                Boolean(Math.floor(Math.random() * 2)),
                'Programa Processo #' + i,
                'Processo #' + id + '.' + i,
                'Modulo #' + id,
                '/'
            ));
        }

        return processes;
    }

    function Process(favorite, program, description, module, url) {
        this.favorite = favorite;
        this.program = program || '[program]';
        this.description = description || '[description]';
        this.module = module || '[module]';
        this.url = url || '/';
        this.type = 0;
    }

}());
