/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsCountry
* @name TotvsCountryListController
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
		.module('country')
		.controller('CountryEditController', CountryEditController);

	CountryEditController.$inject = [
		'$scope',
		'$stateParams',
		'$state',
		'$window',
		'totvs.app-notification.Service',
		'i18nFilter',
		'countryFactory'
	];

	function CountryEditController(
		$scope, $stateParams, $state, $window, notification, i18nFilter, countryFactory) {

		// *********************************************************************************
		// *** Variables
		// *********************************************************************************

		var self = this;

		// *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

		self.country = {};
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
			countryFactory.getRecord(id, function (country) {
				if (country) {
					if (country.id)
						self.country = country;
					if (country.data && country.data.id)
						self.country = country.data;
					if (country.result && country.result.id)
						self.country = country.result;
				} else {
					notification.notify({
						type: 'warning',
						title: '404',
						detail: 'Registro "' + id + '" não encontrado, mas você pode inserir um novo registro. =P'
					});

					$state.go('countries.new');
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
			if (self.country.id) {
				countryFactory.updateRecord(self.country.id, self.country, function (result) {
					$state.go('countries.detail', { id: self.country.id });
				});
			} else {
				countryFactory.saveRecord(self.country, function (result) {
					$state.go('countries.start');
				});
			}
		}

		function saveNew() {
			if (self.country.id) {
				countryFactory.updateRecord(self.country.id, self.country, function (result) {
					$state.go('countries.new');
				});
			} else {
				countryFactory.saveRecord(self.country, function (result) {
					$state.go($state.current, {}, { reload: true });
				});
			}
		}
	}
}());
