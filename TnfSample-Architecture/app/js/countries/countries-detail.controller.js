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
* @description Controller responsável por exibir os detalhes
*/

(function () {

    'use strict';

    angular
        .module('country')
        .controller('CountryDetailController', CountryDetailController);

    CountryDetailController.$inject = [
        '$stateParams',
        '$state',
        'totvs.app-notification.Service',
        'i18nFilter',
		'countryFactory'
    ];

	function CountryDetailController(
        $stateParams, $state, notification, i18nFilter, countryFactory) {

		// *********************************************************************************
		// *** Variables
		// *********************************************************************************

		var self = this;

        // *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

        self.country = {};
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
                $state.go('countries.start');
            }

		}

		// *********************************************************************************
		// *** Functions
		// *********************************************************************************

		function loadRecord(id) {

			countryFactory.getRecord(id, function (country) {
				if (country){
                    if(country.id)
					    self.country = country;
                    if(country.data && country.data.id)
					    self.country = country.data;
                    if(country.result && country.result.id)
					    self.country = country.result;
				} else {
                    notification.notify({
                        type: 'warning',
                        title: '404',
                        detail: 'Registro "' + id + '" não encontrado. Você será redirecionado a lista de registros!'
                    });

                    $state.go('countries.start');
                }
			});
		}

        function applyEdit(newValue, field) {
            var update = {};

            if (newValue !== self.country[field]) {
                update[field] = newValue;
                countryFactory.updateRecord(self.country.id, update);
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
						countryFactory.deleteRecord(self.country.id, function (result) {
							if (result) {
                                $state.go('countries.start');
							}
						});
					}
				}
			});
		}
	};

}());
