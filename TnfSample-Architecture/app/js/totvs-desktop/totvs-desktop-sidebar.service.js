/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsDesktop
* @name TotvsDesktoSidebar
* @object service
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @requires
*
* @dependencies
*
* @description
*/
(function () {

    'use strict';

    angular
        .module('totvsDesktop')
        .service('TotvsDesktopSidebar', TotvsDesktopSidebar);

    TotvsDesktopSidebar.$inject = [];

    function TotvsDesktopSidebar() {
        var isOpened;

        this.init = init;
        this.open = openMenu;
        this.close = closeMenu;

        function init() {
            if (window.innerWidth < 768) {
				var menuw = document.getElementById('menu-workspace'),
                    body = $('body'),
                    btbHome = '.btn-home';

				isOpened = false;
                
                body.off('click', btbHome);
				body.on('click', btbHome, openMenu);
				menuw.addEventListener('click', closeMenu);
            }
        }

        function openMenu() {
            if (isOpened !== undefined && !isOpened) {
                var menuLateral = document.getElementById('menu-lateral');
                if (menuLateral) {
                    menuLateral.style.width = '250px';
                    document.getElementById('menu-workspace').style.marginLeft = '260px';
                    document.getElementById('menu-desktop').style.overflow = 'hidden';
                    isOpened = !isOpened;
                }
            } else { closeMenu(); }
        }

        function closeMenu() {
            if (isOpened !== undefined && isOpened) {
                var menuLateral = document.getElementById('menu-lateral');
                if (menuLateral) {
                    menuLateral.style.width = '0';
                    document.getElementById('menu-workspace').style.marginLeft = '0';
                    document.getElementById('menu-desktop').style.overflow = 'auto';
                    isOpened = !isOpened;
                }
            }
        }
	}
}());
