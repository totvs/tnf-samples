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
* @description Controller responsável por editar o registro
*/

(function () {

	'use strict';

	angular
		.module('professional')
		.controller('ProfessionalEditController', ProfessionalEditController);

	ProfessionalEditController.$inject = [
		'$scope',
		'$stateParams',
		'$state',
		'$window',
		'totvs.app-notification.Service',
		'i18nFilter',
		'professionalFactory',
		'specialtyFactory'
	];

	function ProfessionalEditController(
		$scope, $stateParams, $state, $window, notification, i18nFilter, professionalFactory, specialtyFactory) {

		// *********************************************************************************
		// *** Variables
		// *********************************************************************************

		var self = this;

		// *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

		self.professional = { _expandables: [] };
		self.cancel = cancel;
		self.save = save;
		self.saveNew = saveNew;
		self.specialties = [];

		// *********************************************************************************
		// *** Controller Initialize
		// *********************************************************************************

		function init(cacheController) {

			if (!cacheController) {
				if ($stateParams && $stateParams.professionalId) {
					loadRecord($stateParams.professionalId, $stateParams.code);
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


		specialtyFactory.findRecords({ pageSize: 100 }, function (result) {
			if (result)
				self.specialties = result;
		});

		function setSpecialties() {
			self.professional.specialties = self.professional.specialties || [];
			self.professional.specialties = self.professional.specialties.map(s => self.specialties.filter(x => x.id === s.id)[0]);
		};

		function loadRecord(professionalId, code) {
			professionalFactory.getRecord(professionalId, code, function (professional) {
				if (professional) {
					if (professional.professionalId)
						self.professional = professional;
					if (professional.data && professional.data.professionalId)
						self.professional = professional.data;
					if (professional.result && professional.result.professionalId)
						self.professional = professional.result;

					self.professional.specialties = self.professional.specialties.map(function (s) { return s.id; });
				} else {
					notification.notify({
						type: 'warning',
						title: '404',
						detail: 'Registro "' + professionalId + '", "' + code + '" não encontrado, mas você pode inserir um novo registro. =P'
					});

					$state.go('professionals.new');
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
			setSpecialties();
			if (self.professional.professionalId) {
				professionalFactory.updateRecord(self.professional.professionalId, self.professional.code, self.professional, function (result) {
					if (!result.messages)
						$state.go('professionals.detail', { professionalId: self.professional.professionalId, code: self.professional.code }, { reload: true });
				});
			} else {
				professionalFactory.saveRecord(self.professional, function (result) {
					if (!result.messages)
						$state.go('professionals.start', {}, { reload: true });
				});
			}
		}

		function saveNew() {
			setSpecialties();
			if (self.professional.professionalId) {
				professionalFactory.updateRecord(self.professional.professionalId, self.professional.code, self.professional, function (result) {
					if (!result.messages)
						$state.go('professional.new', {}, { reload: true });
				});
			} else {
				professionalFactory.saveRecord(self.professional, function (result) {
					if (!result.messages)
						$state.go($state.current, {}, { reload: true });
				});
			}
		}
	}
}());
