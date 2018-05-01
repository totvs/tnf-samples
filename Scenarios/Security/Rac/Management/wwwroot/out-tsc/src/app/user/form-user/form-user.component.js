"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var hub_crud_component_edit_1 = require("./../../utils/hub-crud-component-edit");
var user_service_1 = require("./../user.service");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var hub_api_1 = require("../../utils/hub-api");
var organizationUnit_service_1 = require("../../organizationUnit/organizationUnit.service");
var role_service_1 = require("../../role/role.service");
var tenant_service_1 = require("../../tenant/tenant.service");
var auth_service_1 = require("../../auth/auth.service");
var tenant_thflookup_service_1 = require("../../tenant/tenant.thflookup.service");
var FormUserComponent = /** @class */ (function (_super) {
    __extends(FormUserComponent, _super);
    /**
     * O Contrutor para inicialização
     */
    function FormUserComponent(activatedRoute, router, dataService, tenantThfLookupService, organizationService, roleService, tenantService, authService) {
        var _this = _super.call(this, activatedRoute, router, dataService, user_service_1.UserData) || this;
        _this.activatedRoute = activatedRoute;
        _this.router = router;
        _this.dataService = dataService;
        _this.tenantThfLookupService = tenantThfLookupService;
        _this.organizationService = organizationService;
        _this.roleService = roleService;
        _this.tenantService = tenantService;
        _this.authService = authService;
        _this.isActiveList = [{ value: 'isActive', label: 'Ativo' }];
        _this.isActiveResponse = [];
        _this.passwordReplay = '';
        _this.selectedRoles = [];
        _this.selectedOrganizations = [];
        _this.canFilterByTenant = true;
        _this.tenantColumns = [
            { column: 'tenantName', label: 'Nome', type: 'string' },
            { column: 'name', label: 'Nome de Exibição', type: 'string', fieldLabel: true }
        ];
        _this.phoneMask = '(99) 99999-9999';
        _this.emailPattern = /^([\w-\.]*)@([\w-]+)\.([a-z]{1,6}(?:\.[a-z]{1,6})?)$/;
        _this.fillRolesAndOrganizations = function (data) {
            data.organizations = _this.selectedOrganizations;
            data.roles = _this.selectedRoles;
            if (data.tenantId === 0)
                data.tenantId = null;
        };
        dataService.addExpand("organizations.organizationUnit");
        dataService.addExpand("roles.role");
        _this.roles = [];
        _this.organizations = [];
        _this.beforeEditing.subscribe(function (data) {
            _this.tenantId = data.tenantId;
            _this.isActiveResponse = data.isActive ? ['isActive'] : [];
            _this.beforePersisting.subscribe(_this.fillRolesAndOrganizations);
            if (!authService.isHostUser()) {
                tenantService.getQuery(new hub_api_1.HubApiQueryString({
                    page: 1,
                    pageSize: 1
                })).subscribe(function (success) {
                    if (success && success.items)
                        _this.data.tenantId = success.items[0].id;
                });
            }
            _this.onChangedTenant();
        });
        return _this;
    }
    Object.defineProperty(FormUserComponent.prototype, "rawPassword", {
        get: function () {
            return this._rawPassword;
        },
        set: function (newPassword) {
            this._rawPassword = newPassword;
            this.data.password = btoa(newPassword);
        },
        enumerable: true,
        configurable: true
    });
    FormUserComponent.prototype.getPasswordPattern = function () {
        return "\\b" + this.rawPassword + "\\b";
    };
    FormUserComponent.prototype.ngAfterContentChecked = function () {
        if (this.data.tenantId &&
            this.tenantId !== this.data.tenantId) {
            this.tenantId = this.data.tenantId;
            this.onChangedTenant();
        }
    };
    FormUserComponent.prototype.onChangedTenant = function () {
        if (this.canFilterByTenant) {
            this.canFilterByTenant = false;
            this.filteredOrganizationByTenantId();
            this.filteredRolesByTenantId();
        }
    };
    FormUserComponent.prototype.filteredOrganizationByTenantId = function () {
        var _this = this;
        this.organizations = [];
        var query = new hub_api_1.HubApiQueryString({
            page: 1,
            pageSize: 100
        });
        this.organizationService.getQueryByTenant(query, this.data.tenantId).subscribe(function (success) {
            if (success && success.items) {
                for (var _i = 0, _a = success.items; _i < _a.length; _i++) {
                    var org = _a[_i];
                    _this.organizations.push({ value: org.id, label: org.displayName });
                }
            }
            _this.canFilterByTenant = true;
            _this.selectedOrganizations = _this.data.organizations;
        });
    };
    FormUserComponent.prototype.filteredRolesByTenantId = function () {
        var _this = this;
        this.roles = [];
        var query = new hub_api_1.HubApiQueryString({
            page: 1,
            pageSize: 100
        });
        this.roleService.getQueryByTenant(query, this.data.tenantId).subscribe(function (success) {
            if (success && success.items) {
                for (var _i = 0, _a = success.items; _i < _a.length; _i++) {
                    var role = _a[_i];
                    _this.roles.push({ value: role.id, label: role.displayName });
                }
            }
            _this.canFilterByTenant = true;
            _this.selectedRoles = _this.data.roles;
        });
    };
    FormUserComponent = __decorate([
        core_1.Component({
            selector: 'app-form-user',
            templateUrl: './form-user.component.html',
            styleUrls: ['./form-user.component.css']
        })
        /**
         * @description
         * A classe responsável pelas ações do formulário de user
         * @extends HubCrudComponentForm
         */
        ,
        __metadata("design:paramtypes", [router_1.ActivatedRoute,
            router_1.Router,
            user_service_1.UserService,
            tenant_thflookup_service_1.TenantThfLookupService,
            organizationUnit_service_1.OrganizationService,
            role_service_1.RoleService,
            tenant_service_1.TenantService,
            auth_service_1.AuthService])
    ], FormUserComponent);
    return FormUserComponent;
}(hub_crud_component_edit_1.HubCrudComponentForm));
exports.FormUserComponent = FormUserComponent;
//# sourceMappingURL=form-user.component.js.map