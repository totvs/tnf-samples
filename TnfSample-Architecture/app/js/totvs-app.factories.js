/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsApp
* @name totvsAppConfig
* @object config
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @requires totvs-app.module
*
* @dependencies
*
* @description Main config
*/

(function () {

    'use strict';

    angular
        .module('totvsApp')
        .factory('NotifyFactory', NotifyFactory);

    NotifyFactory.$inject = ['totvs.app-notification.Service'];

    function NotifyFactory(totvsNotification) {
        var self = this;

        self.showNotifications = showNotifications;
        self.encapsulateCallback = encapsulateCallback;

        return self;

        function showNotifications(notifications) {
            angular.forEach(notifications, function (value) {
                totvsNotification.notify({
                    type: 'error',
                    title: value.message,
                    detail: value.detailedMessage
                });
            });
        }

        function encapsulateCallback(callback) {
            return callback;
            // var _callback = callback;
            // return function (result) {
            //     if (result.message)
            //         self.showNotifications([result]);
            //     if (result.Details)
            //         self.showNotifications(result.Details);
            //     _callback(result);
            // }
        }
    }

}());
