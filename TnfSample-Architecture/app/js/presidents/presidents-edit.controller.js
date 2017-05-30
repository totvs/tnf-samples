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
* @description Controller responsável por editar o registro
*/

(function () {

	'use strict';

	angular
		.module('president')
		.controller('PresidentEditController', PresidentEditController);

	PresidentEditController.$inject = [
		'$scope',
		'$stateParams',
		'$state',
		'$window',
		'totvs.app-notification.Service',
		'i18nFilter',
		'presidentFactory'
	];

	function PresidentEditController(
		$scope, $stateParams, $state, $window, notification, i18nFilter, presidentFactory) {

		// *********************************************************************************
		// *** Variables
		// *********************************************************************************

		var self = this;

		// *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

		self.president = {};
		self.cancel = cancel;
		self.save = save;
		self.saveNew = saveNew;

		// *********************************************************************************
		// *** Controller Initialize
		// *********************************************************************************

		function init(cacheController) {

			if (!cacheController) {
				if ($stateParams && $stateParams.id) {
					loadRecord($stateParams.id);
				}
			} else {
				// Buscando dados iniciais do "cache"
				angular.forEach(cacheController, function (value, property) {
					self[property] = value;
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

		function loadRecord(id) {
			presidentFactory.getRecord(id, function (president) {
				if (president) {
					if (president.id)
						self.president = president;
					if (president.data && president.data.id)
						self.president = president.data;
					if (president.result && president.result.id)
						self.president = president.result;
				} else {
					notification.notify({
						type: 'warning',
						title: '404',
						detail: 'Registro "' + id + '" não encontrado, mas você pode inserir um novo registro. =P'
					});

					$state.go('presidents.new');
				}
			});
		}

		function cancel() {
			notification.question({
				title: 'l-question',
				text: i18nFilter('l-cancel-operation'),
				cancelLabel: 'l-no',
				confirmLabel: 'l-yes',
				callback: function (isPositiveResult) {
					if (isPositiveResult) {
						$window.history.back();
					}
				}
			});
		}

		function save() {
			if (self.president.id) {
				presidentFactory.updateRecord(self.president.id, self.president, function (result) {
					if (!result.messages)
						$state.go('presidents.detail', { id: self.president.id }, { reload: true });
				});
			} else {
				presidentFactory.saveRecord(self.president, function (result) {
					if (!result.messages)
						$state.go('presidents.start', {}, { reload: true });
				});
			}
		}

		function saveNew() {
			if (self.president.id) {
				presidentFactory.updateRecord(self.president.id, self.president, function (result) {
					if (!result.messages)
						$state.go('president.new', {}, { reload: true });
				});
			} else {
				presidentFactory.saveRecord(self.president, function (result) {
					if (!result.messages)
						$state.go($state.current, {}, { reload: true });
				});
			}
		}
	}
}());
