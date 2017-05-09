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
* @description Controller responsável por exibir os detalhes
*/

(function () {

    'use strict';

    angular
        .module('specialty')
        .controller('SpecialtyDetailController', SpecialtyDetailController);

    SpecialtyDetailController.$inject = [
        '$stateParams',
        '$state',
        'totvs.app-notification.Service',
        'i18nFilter',
		'specialtyFactory'
    ];

	function SpecialtyDetailController(
        $stateParams, $state, notification, i18nFilter, specialtyFactory) {

		// *********************************************************************************
		// *** Variables
		// *********************************************************************************

		var self = this;

        // *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

        self.specialty = {};
        self.loadRecord = loadRecord;
        self.applyEdit = applyEdit;
        self.remove = remove;

        init();

        // *********************************************************************************
		// *** Controller Initialize
		// *********************************************************************************

        function init(cacheController) {
            if ($stateParams) {
                self.loadRecord($stateParams.id || $stateParams);
            } else {
                $state.go('specialties.start');
            }

		}

		// *********************************************************************************
		// *** Functions
		// *********************************************************************************

		function loadRecord(id) {

			specialtyFactory.getRecord(id, function (specialty) {
				if (specialty){
                    if(specialty.id)
					    self.specialty = specialty;
                    if(specialty.data && specialty.data.id)
					    self.specialty = specialty.data;
                    if(specialty.result && specialty.result.id)
					    self.specialty = specialty.result;
				} else {
                    notification.notify({
                        type: 'warning',
                        title: '404',
                        detail: 'Registro "' + id + '" não encontrado. Você será redirecionado a lista de registros!'
                    });

                    $state.go('specialties.start');
                }
			});
		}

        function applyEdit(newValue, field) {
            var update = {};

            if (newValue !== self.specialty[field]) {
                update[field] = newValue;
                specialtyFactory.updateRecord(self.specialty.id, update);
            }
        }

		function remove() {
            notification.question({
				title: 'l-question',
				text: i18nFilter('l-confirm-delete-operation'),
				cancelLabel: 'l-no',
				confirmLabel: 'l-yes',
				callback: function (isPositiveResult) {
					if (isPositiveResult) {
						specialtyFactory.deleteRecord(self.specialty.id, function (result) {
							if (result) {
                                $state.go('specialties.start');
							}
						});
					}
				}
			});
		}
	};

}());
