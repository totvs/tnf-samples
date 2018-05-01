"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var user_routing_module_1 = require("./user-routing.module");
var thf_module_1 = require("@totvs/thf-ui/thf.module");
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/common/http");
var user_component_1 = require("./user.component");
var form_user_component_1 = require("./form-user/form-user.component");
var user_service_1 = require("./user.service");
var organizationUnit_service_1 = require("../organizationUnit/organizationUnit.service");
var role_service_1 = require("../role/role.service");
var tenant_service_1 = require("../tenant/tenant.service");
var auth_service_1 = require("../auth/auth.service");
var token_interceptor_1 = require("../auth/token.interceptor");
var tenant_thflookup_service_1 = require("../tenant/tenant.thflookup.service");
var UserModule = /** @class */ (function () {
    /**
     * @description
     * Classe principal do modulo de user
     */
    function UserModule() {
    }
    UserModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                thf_module_1.ThfModule,
                user_routing_module_1.UserRoutingModule,
                forms_1.FormsModule,
                http_1.HttpClientModule
            ],
            declarations: [
                user_component_1.UserComponent,
                form_user_component_1.FormUserComponent
            ],
            providers: [
                user_service_1.UserService,
                role_service_1.RoleService,
                organizationUnit_service_1.OrganizationService,
                tenant_service_1.TenantService,
                auth_service_1.AuthService,
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
         * Classe principal do modulo de user
         */
    ], UserModule);
    return UserModule;
}());
exports.UserModule = UserModule;
//# sourceMappingURL=user.module.js.map