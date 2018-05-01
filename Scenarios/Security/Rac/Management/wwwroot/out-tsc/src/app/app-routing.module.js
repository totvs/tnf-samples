"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var auth_guard_1 = require("./auth/auth.guard");
var auth_callback_component_1 = require("./auth/callback/auth-callback.component");
var home_component_1 = require("./home/home.component");
var routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'home',
        component: home_component_1.HomeComponent,
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'tenant',
        loadChildren: 'app/tenant/tenant.module#TenantModule',
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'organizationUnit',
        loadChildren: 'app/organizationUnit/organizationUnit.module#OrganizationModule',
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'user',
        loadChildren: 'app/user/user.module#UserModule',
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'role',
        loadChildren: 'app/role/role.module#RoleModule',
        canActivate: [auth_guard_1.AuthGuard]
    },
    {
        path: 'auth-callback',
        component: auth_callback_component_1.AuthCallbackComponent
    }
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forRoot(routes)],
            exports: [router_1.RouterModule]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());
exports.AppRoutingModule = AppRoutingModule;
//# sourceMappingURL=app-routing.module.js.map