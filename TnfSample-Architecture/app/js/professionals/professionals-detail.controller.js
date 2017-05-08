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
* @description Controller responsável por exibir os detalhes
*/

(function () {

    'use strict';

    angular
        .module('professional')
        .controller('ProfessionalDetailController', ProfessionalDetailController);

    ProfessionalDetailController.$inject = [
        '$stateParams',
        '$state',
        'totvs.app-notification.Service',
        'i18nFilter',
		'professionalFactory'
    ];

	function ProfessionalDetailController(
        $stateParams, $state, notification, i18nFilter, professionalFactory) {

		// *********************************************************************************
		// *** Variables
		// *********************************************************************************

		var self = this;

        // *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

        self.professional = {};
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
                $state.go('professionals.start');
            }

		}

		// *********************************************************************************
		// *** Functions
		// *********************************************************************************

		function loadRecord(id) {

			professionalFactory.getRecord(id, function (professional) {
				if (professional){
                    if(professional.id)
					    self.professional = professional;
                    if(professional.data && professional.data.id)
					    self.professional = professional.data;
                    if(professional.result && professional.result.id)
					    self.professional = professional.result;
				} else {
                    notification.notify({
                        type: 'warning',
                        title: '404',
                        detail: 'Registro "' + id + '" não encontrado. Você será redirecionado a lista de registros!'
                    });

                    $state.go('professionals.start');
                }
			});
		}

        function applyEdit(newValue, field) {
            var update = {};

            if (newValue !== self.professional[field]) {
                update[field] = newValue;
                professionalFactory.updateRecord(self.professional.id, update);
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
						professionalFactory.deleteRecord(self.professional.id, function (result) {
							if (result) {
                                $state.go('professionals.start');
							}
						});
					}
				}
			});
		}
	};

}());
