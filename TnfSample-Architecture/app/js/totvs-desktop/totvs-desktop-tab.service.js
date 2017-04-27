/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsDesktop
* @name TotvsDesktopTabService
* @object service
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @requires
*
* @dependencies
*
* @description
*/

(function () {

    'use strict';

    angular
        .module('totvsDesktop')
        .service('TotvsDesktopTabService', TotvsDesktopTabService);

    TotvsDesktopTabService.$inject = [];

    function TotvsDesktopTabService() {

        // *********************************************************************************
		// *** Private Variables
		// *********************************************************************************

        var tabs = [];

        // *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

        // Adiciona uma view com controller
        this.addTab = addTab;
        this.getTabByName = getTabByName;
        this.deactivateAll = deactivateAll;
        this.getAll = getAll;

        // *********************************************************************************
		// *** Functions
		// *********************************************************************************

        function addTab(name, state, params, view, onSelect, onClose) {

            var index,
                tab = {
                    active: true,
                    name: name,
                    state: state,
                    params: params,
                    view: view,
                    onSelect: onSelect,
                    onClose: onClose
                };

            deactivateAll();

            index = tabs.indexOf(getTabByName(name));

            if (index === -1) {
                tabs.push(tab);
            } else {
                tabs[index] = tab;
            }

            return getAll();

        }

        function getTabByName(name) {

            var i,
                len = tabs.length;

            for (i = 0; i < len; i++) {
                if (tabs[i].name.toLowerCase() === name.toLowerCase()) {
                    return tabs[i];
                }
            }
        }

        function deactivateAll() {
            tabs.forEach(function(tab) {
                tab.active = false;
            });
        }

        function getAll() {
            return tabs;
        }

    }

}());
