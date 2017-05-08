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
		'professionalFactory'
    ];

	function ProfessionalEditController(
        $scope, $stateParams, $state, $window, notification, i18nFilter, professionalFactory) {

		// *********************************************************************************
		// *** Variables
		// *********************************************************************************

		var self = this;

        // *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

        self.professional = {};
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
                angular.forEach(cacheController, function(value, property) {
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
                        detail: 'Registro "' + id + '" não encontrado, mas você pode inserir um novo registro. =P'
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
			if (self.professional.id) {
				professionalFactory.updateRecord(self.professional.id, self.professional, function (result) {
					$state.go('professionals.detail', {id: self.professional.id});
				});
			} else {
				professionalFactory.saveRecord([self.professional], function (result) {
					$state.go('professionals.start');
				});
			}
		}

        function saveNew() {
			if (self.professional.id) {
				professionalFactory.updateRecord(self.professional.id, self.professional, function (result) {
					$state.go('professional.new');
				});
			} else {
				professionalFactory.saveRecord([self.professional], function (result) {
					setTimeout(function() {
                    	$state.go($state.current, {}, {reload: true});
					}, 5000);
				});
			}
		}
	}
}());
