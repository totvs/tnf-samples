/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsDesktop
* @name TotvsDesktopViewService
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
        .service('TotvsDesktopViewService', TotvsDesktopViewService);

    TotvsDesktopViewService.$inject = ['$rootScope', '$timeout'];

    function TotvsDesktopViewService($rootScope, $timeout) {

        // *********************************************************************************
		// *** Private Variables
		// *********************************************************************************

        var views = {};

        // *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

        // Adiciona uma view com controller
        this.addView = addView;
        // Adiciona uma view baseado no state
        this.addViewState = addViewState;
        // Pega uma view
        this.getView = getView;
        // Pega uma view baseado no state
        this.getViewState = getViewState;
        // Pega todas as views
        this.getAllViews = getAllViews;
        // Pega controller da view
        this.getViewController = getViewController;
        // Pega controller da view baseado no state
        this.getViewControllerState = getViewControllerState;
        // Remove view pelo nome
        this.removeViewByName = removeViewByName;
        // Pega o nome da view baseado no state
        this.getViewNameCurrentState = getViewNameCurrentState;

        // *********************************************************************************
		// *** Functions
		// *********************************************************************************

        function addView(viewName, controllerName, controllerInstance, url) {

            if (!views[viewName]) {
                views[viewName] = {};
            }

            views[viewName].controllerActive = controllerName;
            views[viewName][controllerName] = {
                url: url,
                controller: getOnlyProperties(controllerInstance)
            };

        }

        function addViewState(state, url) {
            var viewName;

            if (state.$current) {
                viewName = getViewNameState(state.$current.locals);
            }

            if (viewName && state.$current.locals[viewName].$scope) {
                addView(
                    viewName.replace('@', ''),
                    state.current.controller,
                    state.$current.locals[viewName].$scope[state.current.controllerAs],
                    url);
            }

        }

        function getView(viewName) {
            return views[viewName] || undefined;
        }

        function getViewState(state, url) {
            var viewName;

            if (state.$current) {
                viewName = getViewNameState(state.$current.locals);
            }

            return getView(viewName.replace('@', ''));
        }

        function getViewController(viewName, controllerName, url) {
            var view = views[viewName] ? views[viewName][controllerName] : undefined;

            if (!view) {
                return undefined;
            }

            if (!url) {
                return view;
            }

            return url === view.url ? view : undefined;
        }

        function getViewControllerState(state, url) {
            var viewName;

            if (state.$current) {
                viewName = getViewNameState(state.$current.locals);
            }

            return getViewController(viewName.replace('@', ''), state.current.controller, url);
        }

        function getAllViews () {
            return angular.copy(views);
        }

        function removeViewByName(viewName) {
            if (views[viewName]) {
                delete views[viewName];
            }
        }

        function getViewNameCurrentState(state) {
            if (state.$current) {
                return getViewNameState(state.$current.locals).replace('@', '');
            }
        }

        // *********************************************************************************
		// *** Private Functions
		// *********************************************************************************

        function getOnlyProperties(controllerInstance) {
            var properties = {};

            angular.forEach(controllerInstance, function (value, property) {
                if (property.charAt(0) !== '$' && property !== 'this' && !angular.isFunction(value)) {
                    properties[property] = value;
                }
            });

            return angular.copy(properties);
        }

        function getViewNameState(currentState) {
            var name;

            angular.forEach(currentState, function (value, property) {
                if (property.charAt(0) === '@' && angular.isObject(value)) {
                    name = property;
                }
            });

            return name;
        }
    }

}());
