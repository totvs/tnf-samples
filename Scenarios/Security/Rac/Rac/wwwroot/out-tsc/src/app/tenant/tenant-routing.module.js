"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var form_tenant_component_1 = require("./form-tenant/form-tenant.component");
var tenant_component_1 = require("./tenant.component");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var tenantRoutes = [
    { path: '', component: tenant_component_1.TenantComponent },
    { path: 'new', component: form_tenant_component_1.FormTenantComponent },
    { path: 'edit/:id', component: form_tenant_component_1.FormTenantComponent }
];
/**
 * @description
 * Módulo responsável por gerenciar as rotas do módulo de tenant
 */
var TenantRoutingModule = /** @class */ (function () {
    /**
     * @description
     * A classe de roteamento do modulo de tenant
     */
    function TenantRoutingModule() {
    }
    TenantRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forChild(tenantRoutes)],
            exports: [router_1.RouterModule]
        })
        /**
         * @description
         * A classe de roteamento do modulo de tenant
         */
    ], TenantRoutingModule);
    return TenantRoutingModule;
}());
exports.TenantRoutingModule = TenantRoutingModule;
//# sourceMappingURL=tenant-routing.module.js.map