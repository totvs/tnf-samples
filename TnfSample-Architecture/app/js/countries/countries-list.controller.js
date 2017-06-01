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
* @description Controller responsável por listar os registros
*/

(function () {

    'use strict';

    angular
        .module('country')
        .controller('CountryListController', CountryListController);

    CountryListController.$inject = [
        '$scope',
        '$modal',
        'totvs.app-notification.Service',
        'dateFilter',
        'i18nFilter',
		'countryFactory'
    ];

    function CountryListController($scope, $modal, notification, dateFilter, i18nFilter, countryFactory) {

        // *********************************************************************************
		// *** Variables
		// *********************************************************************************

        var self = this,
            advancedSearch = {};

        // *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

        self.records = [];
		self.recordsCount = 0;
        self.disclaimers = [];
        self.searchText = '';
        self.search = search;
        self.loadRecords = loadRecords;
        self.openAdvancedSearch = openAdvancedSearch;
        self.removeDisclaimer = removeDisclaimer;
        self.applyEdit = applyEdit;
        self.onRemove = onRemove;

        // *********************************************************************************
		// *** Controller Initialize
		// *********************************************************************************

		function init(cacheController) {
            if (!cacheController) {
                // Carregando pela primeira vez
                loadRecords(false);
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

        function loadRecords(isMore) {
			var parameters = {},
                start = 0;

            // paginação
			self.recordsCount = undefined;

			if (isMore) {
				start = parseInt(self.records.length / 2) + 1;
			} else {
				self.records = [];
			}

            // pesquisa
            if (self.disclaimers.length > 0) {
                angular.forEach(self.disclaimers, function (disclaimer) {
                    if (disclaimer.property && disclaimer.value)
                        parameters[disclaimer.property] = disclaimer.value;
                });
            }

            parameters.page = start;
            parameters.pageSize = 10;

			countryFactory.findRecords(parameters, function (result) {
				if (result) {
					angular.forEach(result, function (value) {
                        
						if (value && value.$length) {
							self.recordsCount = value.$length;
						}

						self.records.push(value);
					});

                    self.recordsCount = self.recordsCount || result.length;
				} else {
                    self.records = [];
                    self.recordsCount = 0;
                }
			});
		}

        function search() {
            self.disclaimers = [];

            if (self.searchText) {
                addDisclaimer('name', self.searchText, 'Pesquisa: ' + self.searchText);
            }

            self.loadRecords(false);
		}

        function openAdvancedSearch() {
			var modalInstance = $modal.open({
                templateUrl: '/js/countries/countries-search.view.html',
                controller: 'CountrySearchController as controller',
                size: 'md',
                resolve: {
                    data: function () {
                        return angular.copy(advancedSearch);
                    }
                }
            });

            modalInstance.result.then(function (params) {
                advancedSearch = angular.copy(params)

                addDisclaimers();
            });
		}

        function addDisclaimer(property, value, label) {
            self.disclaimers.push({
                property: property,
                value: value,
                title: label
            });
        }

        function addDisclaimers() {

            var filter = angular.copy(advancedSearch);

            removeDisclaimers();

            if (filter.name) {
                addDisclaimer('name', filter.name, 'Nome igual a "' + filter.name + '"');
            }

            self.loadRecords(false);
        }

        function removeDisclaimer(disclaimer) {
            // pesquisa e remove o disclaimer do array
            var index = self.disclaimers.indexOf(disclaimer);

            if (index !== -1) {
                self.disclaimers.splice(index, 1);
            }

            if (disclaimer.property === 'name') {
                self.searchText = '';
            }

            self.loadRecords();
        }

        function removeDisclaimers() {
            self.disclaimers = [];
        }

        function applyEdit(newValue, field, country) {
            var update = {};

            if (newValue !== country[field]) {
                update[field] = newValue;

                countryFactory.updateRecord(country.id, update);
            }
        }

		function onRemove(record) {
            notification.question({
				title: 'l-question',
				text: i18nFilter('l-confirm-delete-operation'),
				cancelLabel: 'l-no',
				confirmLabel: 'l-yes',
				callback: function (isPositiveResult) {
                    var index;

					if (isPositiveResult) {
						countryFactory.deleteRecord(record.id, function (result) {
							if (result) {

								index = self.records.indexOf(record);

								if (index !== -1) {
									self.records.splice(index, 1);
									self.recordsCount -= 1;
								}
							}
						});
					}
				}
			});
		}
	};

}());
