/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsMenu
* @name TotvsMenuController
* @object controller
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
        .controller('TotvsMenuController', TotvsMenuController);

    TotvsMenuController.$inject = [
        '$location',
        '$scope',
        '$timeout',
        'i18nFilter',
        'totvsMenuConstant',
        'totvsMenuRecents',
        'totvsMenuFavorites',
        'totvsMenuPrograms',
        'totvsMenuProcesses',
        'TotvsDesktopSidebar'
    ];

    function TotvsMenuController($location, $scope, $timeout, i18nFilter,
        totvsMenuConstant, totvsMenuRecents, totvsMenuFavorites, totvsMenuPrograms, totvsMenuProcesses, TotvsDesktopSidebar) {

        // *********************************************************************************
		// *** Variables
		// *********************************************************************************

        var self = this,
            elementApps = $('#apps'),
            elementPrcs = $('#prcs');

        // *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

        self.srcLogo = 'assets/img/totvs.png'; // TODO: Factory for logo
        self.selectedMenuGroup = undefined;
        self.selectedApplication = undefined;
        self.selectedModule = undefined;
        self.selectedMenuItem = undefined;
        self.applications = [];
        self.programs = [];
        self.totalPrograms = [-1, -1, -1, -1];
        self.menuHeaderTitle = '';
        self.getRecents = getRecents;
        self.getFavorites = getFavorites;
        self.loadMenu = loadMenu;
        self.loadModules = loadModules;
        self.selectModule = selectModule;
        self.selectProcess = selectProcess;
        self.selectTipProgram = selectTipProgram;
        self.openMenuProgram = openMenuProgram;

        // *********************************************************************************
		// *** Controller Initialize
		// *********************************************************************************

        function init(cacheController) {
            // Função para o menu responsivo
            TotvsDesktopSidebar.init();

            if (cacheController) {

                angular.forEach(cacheController, function(value, property) {
                    self[property] = value;
                });

                if (self.selectedMenuGroup === totvsMenuConstant.menuGroups.APPLICATIONS) {
                    loadMenu('apps');

                    loadModules(self.selectedApplication);
                } else if (self.selectedMenuGroup === totvsMenuConstant.menuGroups.PROCESSES) {
                    loadMenu('prcs');
                }

                TotvsDesktopSidebar.open();
            } else {
                // Load constants
                self.menuGroups = totvsMenuConstant.menuGroups;

                getFavorites();

                // Load List Menu Applications
                totvsMenuPrograms.getMenuApplications(function (data) {
                    self.applications = data;
                });

                // Load List Menu Processes
                totvsMenuProcesses.getMenuProcesses(function (data) {
                    self.processes = data;
                });
            }
        }

        // *********************************************************************************
		// *** Events Listners
		// *********************************************************************************

        $scope.$on('$totvsViewServiceInit', function (event, cacheController) {
            init(cacheController);
        });

        // *********************************************************************************
		// *** Functions
		// *********************************************************************************

        function initVariables(menuGroup, tipProgram, headerTitle) {
            self.selectedModule = undefined;
            self.selectedMenuItem = undefined;
            self.programs = [];
            self.totalPrograms = [-1, -1, -1, -1];

            self.selectedMenuGroup = menuGroup;
            self.selectedTipProgram = tipProgram;
            self.menuHeaderTitle = headerTitle;
        }

        function getRecents() {
            initVariables(totvsMenuConstant.menuGroups.RECENTS, 4, i18nFilter('recents'));

            totvsMenuRecents.getProgramRecents(function (data) {
                self.programs = angular.copy(data);
                updateTotalPrograms();
                TotvsDesktopSidebar.close();
            });
        }

        function getFavorites() {
            initVariables(totvsMenuConstant.menuGroups.FAVORITES, 4, i18nFilter('favorites'));

            totvsMenuFavorites.getProgramFavorites(function (data) {
                self.programs = angular.copy(data);
                updateTotalPrograms();
                TotvsDesktopSidebar.close();
            });
        }

        function loadMenu(id) {

            if (id === 'apps') {
                self.selectedMenuGroup = totvsMenuConstant.menuGroups.APPLICATIONS;

                elementPrcs.next().slideUp();
                elementApps.next().slideDown();
            } else {
                self.selectedMenuGroup = totvsMenuConstant.menuGroups.PROCESSES;

                elementApps.next().slideUp();
                elementPrcs.next().slideDown();
            }

        }

        function loadModules(app) {
            self.selectedApplication = app;

            if (!app) {
                return;
            }

            self.applications.forEach(function (application) {
                if (application.id !== app.id) {
                    $('#' + application.id).next().slideUp();
                }
            });

            $timeout(function () {
                $('#' + app.id).next().slideDown();
            });
        }

        function selectModule(module) {
            initVariables(totvsMenuConstant.menuGroups.APPLICATIONS, 4, i18nFilter('applications') +
                        ': ' + module.module);

            totvsMenuPrograms.getProgramApplications(module.seq, function (data) {
                $location.url(data[0].url);
                // self.programs = angular.copy(data);
                // updateTotalPrograms();

                // self.selectedModule = module;
                // TotvsDesktopSidebar.close();
            });
        }

        function selectProcess(prcs) {

            if (!prcs.id) {
                return;
            }

            initVariables(totvsMenuConstant.menuGroups.PROCESSES, undefined, i18nFilter('processes') +
                        ': ' + prcs.process);

            self.selectedProcess = undefined;
            self.selectedProcess = prcs;

            totvsMenuProcesses.getProgramProcesses(prcs.id, function (data) {
                self.programs = angular.copy(data);
                TotvsDesktopSidebar.close();
            });

        }

        function selectTipProgram(program) {
            self.selectedTipProgram = program;
        }

        function updateTotalPrograms() {
            self.totalPrograms = [0, 0, 0, 0];

            self.programs.forEach(function (program) {
                if (program.type >= 1 && program.type <= 4) {
                    self.totalPrograms[program.type - 1]++;
                }
            });
        }

        function openMenuProgram(program) {
            if (!program) {
                return;
            }

            $location.url(program.url);
        }

    }

}());
