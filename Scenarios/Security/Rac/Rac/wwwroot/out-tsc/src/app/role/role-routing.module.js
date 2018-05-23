"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var form_role_component_1 = require("./form-role/form-role.component");
var role_component_1 = require("./role.component");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var roleRoutes = [
    { path: '', component: role_component_1.RoleComponent },
    { path: 'new', component: form_role_component_1.FormRoleComponent },
    { path: 'edit/:id', component: form_role_component_1.FormRoleComponent }
];
/**
 * @description
 * Módulo responsável por gerenciar as rotas do módulo de role
 */
var RoleRoutingModule = /** @class */ (function () {
    /**
     * @description
     * A classe de roteamento do modulo de role
     */
    function RoleRoutingModule() {
    }
    RoleRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forChild(roleRoutes)],
            exports: [router_1.RouterModule]
        })
        /**
         * @description
         * A classe de roteamento do modulo de role
         */
    ], RoleRoutingModule);
    return RoleRoutingModule;
}());
exports.RoleRoutingModule = RoleRoutingModule;
//# sourceMappingURL=role-routing.module.js.map