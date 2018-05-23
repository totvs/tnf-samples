"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var role_routing_module_1 = require("./role-routing.module");
var thf_module_1 = require("@totvs/thf-ui/thf.module");
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/common/http");
var role_component_1 = require("./role.component");
var form_role_component_1 = require("./form-role/form-role.component");
var role_service_1 = require("./role.service");
var ngx_treeview_1 = require("ngx-treeview");
var auth_service_1 = require("../auth/auth.service");
var token_interceptor_1 = require("../auth/token.interceptor");
var tenant_service_1 = require("../tenant/tenant.service");
var tenant_thflookup_service_1 = require("../tenant/tenant.thflookup.service");
var RoleModule = /** @class */ (function () {
    /**
     * @description
     * Classe principal do modulo de role
     */
    function RoleModule() {
    }
    RoleModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                thf_module_1.ThfModule,
                role_routing_module_1.RoleRoutingModule,
                forms_1.FormsModule,
                http_1.HttpClientModule,
                ngx_treeview_1.TreeviewModule.forRoot()
            ],
            declarations: [
                role_component_1.RoleComponent,
                form_role_component_1.FormRoleComponent
            ],
            providers: [
                role_service_1.RoleService,
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
         * Classe principal do modulo de role
         */
    ], RoleModule);
    return RoleModule;
}());
exports.RoleModule = RoleModule;
//# sourceMappingURL=role.module.js.map