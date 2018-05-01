"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var form_organizationUnit_component_1 = require("./form-organizationUnit/form-organizationUnit.component");
var organizationUnit_component_1 = require("./organizationUnit.component");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var organizationRoutes = [
    { path: '', component: organizationUnit_component_1.OrganizationComponent },
    { path: 'new', component: form_organizationUnit_component_1.FormOrganizationComponent },
    { path: 'edit/:id', component: form_organizationUnit_component_1.FormOrganizationComponent }
];
/**
 * @description
 * Módulo responsável por gerenciar as rotas do módulo de organization
 */
var OrganizationRoutingModule = /** @class */ (function () {
    /**
     * @description
     * A classe de roteamento do modulo de organization
     */
    function OrganizationRoutingModule() {
    }
    OrganizationRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forChild(organizationRoutes)],
            exports: [router_1.RouterModule]
        })
        /**
         * @description
         * A classe de roteamento do modulo de organization
         */
    ], OrganizationRoutingModule);
    return OrganizationRoutingModule;
}());
exports.OrganizationRoutingModule = OrganizationRoutingModule;
//# sourceMappingURL=organizationUnit-routing.module.js.map