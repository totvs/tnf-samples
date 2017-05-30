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
* @description Controller responsável por exibir os detalhes
*/

(function () {

    'use strict';

    angular
        .module('president')
        .controller('PresidentDetailController', PresidentDetailController);

    PresidentDetailController.$inject = [
        '$stateParams',
        '$state',
        'totvs.app-notification.Service',
        'i18nFilter',
		'presidentFactory'
    ];

	function PresidentDetailController(
        $stateParams, $state, notification, i18nFilter, presidentFactory) {

		// *********************************************************************************
		// *** Variables
		// *********************************************************************************

		var self = this;

        // *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

        self.president = { _expandables: [] };
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
                $state.go('presidents.start');
            }

		}

		// *********************************************************************************
		// *** Functions
		// *********************************************************************************

		function loadRecord(id) {

			presidentFactory.getRecord(id, function (president) {
				if (president){
                    if(president.id)
					    self.president = president;
                    if(president.data && president.data.id)
					    self.president = president.data;
                    if(president.result && president.result.id)
					    self.president = president.result;
				} else {
                    notification.notify({
                        type: 'warning',
                        title: '404',
                        detail: 'Registro "' + id + '" não encontrado. Você será redirecionado a lista de registros!'
                    });

                    $state.go('presidents.start');
                }
			});
		}

        function applyEdit(newValue, field) {
            var update = {};

            if (newValue !== self.president[field]) {
                update[field] = newValue;
                presidentFactory.updateRecord(self.president.id, update);
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
						presidentFactory.deleteRecord(self.president.id, function (result) {
							if (result) {
                                $state.go('presidents.start');
							}
						});
					}
				}
			});
		}
	};

}());
