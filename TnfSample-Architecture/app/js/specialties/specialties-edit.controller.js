/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsSpecialty
* @name TotvsSpecialtyListController
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
		.module('specialty')
		.controller('SpecialtyEditController', SpecialtyEditController);

	SpecialtyEditController.$inject = [
		'$scope',
		'$stateParams',
		'$state',
		'$window',
		'totvs.app-notification.Service',
		'i18nFilter',
		'specialtyFactory'
	];

	function SpecialtyEditController(
		$scope, $stateParams, $state, $window, notification, i18nFilter, specialtyFactory) {

		// *********************************************************************************
		// *** Variables
		// *********************************************************************************

		var self = this;

		// *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

		self.specialty = {};
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
			specialtyFactory.getRecord(id, function (specialty) {
				if (specialty) {
					if (specialty.id)
						self.specialty = specialty;
				} else {
					notification.notify({
						type: 'warning',
						title: '404',
						detail: 'Registro "' + id + '" não encontrado, mas você pode inserir um novo registro. =P'
					});

					$state.go('specialties.new');
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
			if (self.specialty.id) {
				specialtyFactory.updateRecord(self.specialty.id, self.specialty, function (result) {
					if (!result.messages)
						$state.go('specialties.detail', { id: self.specialty.id }, { reload: true });
				});
			} else {
				specialtyFactory.saveRecord(self.specialty, function (result) {
					if (!result.messages)
						$state.go('specialties.start', {}, { reload: true });
				});
			}
		}

		function saveNew() {
			if (self.specialty.id) {
				specialtyFactory.updateRecord(self.specialty.id, self.specialty, function (result) {
					if (!result.messages)
						$state.go('specialties.new', {}, { reload: true });
				});
			} else {
				specialtyFactory.saveRecord(self.specialty, function (result) {
					if (!result.messages)
						$state.go($state.current, {}, { reload: true });
				});
			}
		}
	}
}());
