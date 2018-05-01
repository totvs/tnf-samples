"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var tenant_routing_module_1 = require("./tenant-routing.module");
var thf_module_1 = require("@totvs/thf-ui/thf.module");
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/common/http");
var tenant_component_1 = require("./tenant.component");
var form_tenant_component_1 = require("./form-tenant/form-tenant.component");
var tenant_service_1 = require("./tenant.service");
var auth_service_1 = require("../auth/auth.service");
var token_interceptor_1 = require("../auth/token.interceptor");
var tenant_thflookup_service_1 = require("./tenant.thflookup.service");
var TenantModule = /** @class */ (function () {
    /**
     * @description
     * Classe principal do modulo de tenant
     */
    function TenantModule() {
    }
    TenantModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                thf_module_1.ThfModule,
                tenant_routing_module_1.TenantRoutingModule,
                forms_1.FormsModule,
                http_1.HttpClientModule
            ],
            declarations: [
                tenant_component_1.TenantComponent,
                form_tenant_component_1.FormTenantComponent
            ],
            providers: [
                tenant_thflookup_service_1.TenantThfLookupService,
                tenant_service_1.TenantService,
                auth_service_1.AuthService,
                {
                    provide: http_1.HTTP_INTERCEPTORS,
                    useClass: token_interceptor_1.TokenInterceptor,
                    multi: true
                }
            ]
        })
        /**
         * @description
         * Classe principal do modulo de tenant
         */
    ], TenantModule);
    return TenantModule;
}());
exports.TenantModule = TenantModule;
//# sourceMappingURL=tenant.module.js.map