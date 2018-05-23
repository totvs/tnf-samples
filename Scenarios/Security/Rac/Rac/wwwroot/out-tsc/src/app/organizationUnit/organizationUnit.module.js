"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var organizationUnit_routing_module_1 = require("./organizationUnit-routing.module");
var thf_module_1 = require("@totvs/thf-ui/thf.module");
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/common/http");
var organizationUnit_component_1 = require("./organizationUnit.component");
var form_organizationUnit_component_1 = require("./form-organizationUnit/form-organizationUnit.component");
var organizationUnit_service_1 = require("./organizationUnit.service");
var auth_service_1 = require("../auth/auth.service");
var token_interceptor_1 = require("../auth/token.interceptor");
var tenant_service_1 = require("../tenant/tenant.service");
var tenant_thflookup_service_1 = require("../tenant/tenant.thflookup.service");
var organizationUnit_thflookup_service_1 = require("./organizationUnit.thflookup.service");
var OrganizationModule = /** @class */ (function () {
    /**
     * @description
     * Classe principal do modulo de organization
     */
    function OrganizationModule() {
    }
    OrganizationModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                thf_module_1.ThfModule,
                organizationUnit_routing_module_1.OrganizationRoutingModule,
                forms_1.FormsModule,
                http_1.HttpClientModule
            ],
            declarations: [
                organizationUnit_component_1.OrganizationComponent,
                form_organizationUnit_component_1.FormOrganizationComponent
            ],
            providers: [
                organizationUnit_service_1.OrganizationService,
                organizationUnit_thflookup_service_1.OrganizationThfLookupService,
                auth_service_1.AuthService,
                tenant_service_1.TenantService,
                tenant_thflookup_service_1.TenantThfLookupService,
                {
                    provide: http_1.HTTP_INTERCEPTORS,
                    useClass: token_interceptor_1.TokenInterceptor,
                    multi: true
                }
            ]
        })
        /**
         * @description
         * Classe principal do modulo de organization
         */
    ], OrganizationModule);
    return OrganizationModule;
}());
exports.OrganizationModule = OrganizationModule;
//# sourceMappingURL=organizationUnit.module.js.map