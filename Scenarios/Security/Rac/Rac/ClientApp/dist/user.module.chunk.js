webpackJsonp(["user.module"],{

/***/ "../../../../../src/app/user/form-user/form-user.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(true);
// imports


// module
exports.push([module.i, ".checkbox-group {\r\n    height: 102px;\r\n}\r\n\r\nform div[class^=\"thf-\"] {\r\n    margin-bottom: 22px;\r\n}", "", {"version":3,"sources":["D:/git/Tnf-Rac/src/Tnf.Rac.Host/ClientApp/src/app/user/form-user/D:/git/Tnf-Rac/src/Tnf.Rac.Host/ClientApp/src/app/user/form-user/form-user.component.css"],"names":[],"mappings":"AAAA;IACI,cAAc;CACjB;;AAED;IACI,oBAAoB;CACvB","file":"form-user.component.css","sourcesContent":[".checkbox-group {\r\n    height: 102px;\r\n}\r\n\r\nform div[class^=\"thf-\"] {\r\n    margin-bottom: 22px;\r\n}"],"sourceRoot":""}]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/user/form-user/form-user.component.html":
/***/ (function(module, exports) {

module.exports = "<thf-page-edit  \r\n        t-title=\"Cadastro de Usuário\">\r\n  <form #form=\"ngForm\" >\r\n      \r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n      <thf-login\r\n          class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n          t-label=\"Login\"\r\n          t-help=\"Informe o login do Usuário\"\r\n          t-required\r\n          t-clean\r\n          t-maxlength=\"128\"\r\n          t-focus\r\n          name=\"userName\"\r\n          [(ngModel)]=this.data.userName>\r\n      </thf-login>\r\n    </div>\r\n          \r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n      <thf-input\r\n          class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n          t-label=\"Email\"\r\n          t-help=\"Informe o email do Usuário\"\r\n          t-required\r\n          t-clean\r\n          t-maxlength=\"128\"\r\n          name=\"emailAddress\"\r\n          [t-pattern]=\"this.emailPattern\"\r\n          [(ngModel)]=this.data.emailAddress>\r\n      </thf-input>\r\n    </div>\r\n\r\n    <div *ngIf=\"!this.isUpdate\">\r\n      <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n        <thf-password\r\n            class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n            t-label=\"Senha\"\r\n            t-help=\"Informe a senha do Usuário\"\r\n            t-error-pattern=\"Senhas diferentes\"\r\n            t-required\r\n            t-maxlength=\"128\"\r\n            type=\"password\"\r\n            name=\"password\"\r\n            [t-pattern]=\"getPasswordPattern()\"\r\n            [(ngModel)]=this.rawPassword>\r\n        </thf-password>\r\n      </div>\r\n\r\n      <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n        <thf-password\r\n            class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n            t-label=\"Repetir a Senha\"\r\n            t-help=\"Repita a senha novamente\"\r\n            t-error-pattern=\"Senhas diferentes\"\r\n            t-required\r\n            t-maxlength=\"128\"\r\n            type=\"password\"\r\n            name=\"passwordReplay\"\r\n            [t-pattern]=\"getPasswordPattern()\"\r\n            [(ngModel)]=this.passwordReplay>\r\n        </thf-password>\r\n      </div>\r\n    </div>\r\n          \r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n      <thf-input\r\n          class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n          t-label=\"Nome\"\r\n          t-help=\"Informe o nome do Usuário\"\r\n          t-required\r\n          t-clean\r\n          t-maxlength=\"128\"\r\n          name=\"name\"\r\n          [(ngModel)]=this.data.name>\r\n      </thf-input>\r\n    </div>\r\n          \r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n      <thf-input\r\n          class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n          t-label=\"Sobrenome\"\r\n          t-help=\"Informe o sobrenome do Usuário\"\r\n          t-required\r\n          t-clean\r\n          t-maxlength=\"128\"\r\n          name=\"surname\"\r\n          [(ngModel)]=this.data.surname>\r\n      </thf-input>\r\n    </div>\r\n          \r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n      <thf-input\r\n          class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n          t-label=\"Telefone\"\r\n          t-help=\"Informe o telefone do Usuário\"\r\n          t-clean\r\n          t-maxlength=\"128\"\r\n          name=\"phoneNumber\"\r\n          [t-mask]=\"phoneMask\"\r\n          [(ngModel)]=this.data.phoneNumber>\r\n      </thf-input>\r\n    </div>\r\n\r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" *ngIf=\"this.authService.isHostUser()\">\r\n      <thf-lookup\r\n        class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n        name=\"tenantId\"\r\n        t-field-value=\"id\"\r\n        t-field-label=\"name\"\r\n        t-label=\"Tenant\"\r\n        t-help=\"Informe o tenant do Usuário\"\r\n        t-placeholder=\"Nenhum\"\r\n        t-title=\"Selecione um tenant\"\r\n        [t-filter-service]=\"tenantThfLookupService\"\r\n        [t-columns]=\"tenantColumns\"\r\n        [(ngModel)]=\"this.data.tenantId\">\r\n      </thf-lookup>\r\n    </div>\r\n                \r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n      <thf-checkbox-group\r\n          class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n          name=\"isActive\"\r\n          t-label=\"Ativo\"\r\n          t-help=\"Informe se o Usuário está ativo ou não\"\r\n          [t-options]=\"this.isActiveList\"\r\n          (t-change)=\"this.data.isActive = this.isActiveResponse.length > 0\"\r\n          [(ngModel)]=this.isActiveResponse>\r\n      </thf-checkbox-group>\r\n    </div>\r\n    \r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n      <thf-multiselect\r\n        class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n        name=\"roles\"\r\n        t-label=\"Roles\"\r\n        t-help=\"Selecione as roles desse Usuário\"\r\n        [(ngModel)]=\"this.selectedRoles\"\r\n        [t-options]=\"this.roles\">\r\n      </thf-multiselect>\r\n    </div>\r\n    \r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n      <thf-multiselect\r\n        class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n        name=\"organizations\"\r\n        t-label=\"Organizações\"\r\n        t-help=\"Selecione as organizações desse Usuário\"\r\n        [(ngModel)]=\"this.selectedOrganizations\"\r\n        [t-options]=\"this.organizations\">\r\n      </thf-multiselect>\r\n    </div>\r\n  </form>\r\n</thf-page-edit>"

/***/ }),

/***/ "../../../../../src/app/user/form-user/form-user.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return FormUserComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__utils_hub_crud_component_edit__ = __webpack_require__("../../../../../src/app/utils/hub-crud-component-edit.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__user_service__ = __webpack_require__("../../../../../src/app/user/user.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_router__ = __webpack_require__("../../../router/esm5/router.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__utils_hub_api__ = __webpack_require__("../../../../../src/app/utils/hub-api.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__organizationUnit_organizationUnit_service__ = __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__role_role_service__ = __webpack_require__("../../../../../src/app/role/role.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__tenant_tenant_service__ = __webpack_require__("../../../../../src/app/tenant/tenant.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__auth_auth_service__ = __webpack_require__("../../../../../src/app/auth/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__tenant_tenant_thflookup_service__ = __webpack_require__("../../../../../src/app/tenant/tenant.thflookup.service.ts");
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










var FormUserComponent = /** @class */ (function (_super) {
    __extends(FormUserComponent, _super);
    /**
     * O Contrutor para inicialização
     */
    function FormUserComponent(activatedRoute, router, dataService, tenantThfLookupService, organizationService, roleService, tenantService, authService) {
        var _this = _super.call(this, activatedRoute, router, dataService, __WEBPACK_IMPORTED_MODULE_1__user_service__["a" /* UserData */]) || this;
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
                tenantService.getQuery(new __WEBPACK_IMPORTED_MODULE_4__utils_hub_api__["a" /* HubApiQueryString */]({
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
        var query = new __WEBPACK_IMPORTED_MODULE_4__utils_hub_api__["a" /* HubApiQueryString */]({
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
        var query = new __WEBPACK_IMPORTED_MODULE_4__utils_hub_api__["a" /* HubApiQueryString */]({
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
        Object(__WEBPACK_IMPORTED_MODULE_2__angular_core__["n" /* Component */])({
            selector: 'app-form-user',
            template: __webpack_require__("../../../../../src/app/user/form-user/form-user.component.html"),
            styles: [__webpack_require__("../../../../../src/app/user/form-user/form-user.component.css")]
        })
        /**
         * @description
         * A classe responsável pelas ações do formulário de user
         * @extends HubCrudComponentForm
         */
        ,
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_3__angular_router__["a" /* ActivatedRoute */],
            __WEBPACK_IMPORTED_MODULE_3__angular_router__["d" /* Router */],
            __WEBPACK_IMPORTED_MODULE_1__user_service__["b" /* UserService */],
            __WEBPACK_IMPORTED_MODULE_9__tenant_tenant_thflookup_service__["a" /* TenantThfLookupService */],
            __WEBPACK_IMPORTED_MODULE_5__organizationUnit_organizationUnit_service__["b" /* OrganizationService */],
            __WEBPACK_IMPORTED_MODULE_6__role_role_service__["b" /* RoleService */],
            __WEBPACK_IMPORTED_MODULE_7__tenant_tenant_service__["b" /* TenantService */],
            __WEBPACK_IMPORTED_MODULE_8__auth_auth_service__["a" /* AuthService */]])
    ], FormUserComponent);
    return FormUserComponent;
}(__WEBPACK_IMPORTED_MODULE_0__utils_hub_crud_component_edit__["a" /* HubCrudComponentForm */]));



/***/ }),

/***/ "../../../../../src/app/user/user-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UserRoutingModule; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__form_user_form_user_component__ = __webpack_require__("../../../../../src/app/user/form-user/form-user.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__user_component__ = __webpack_require__("../../../../../src/app/user/user.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_router__ = __webpack_require__("../../../router/esm5/router.js");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var userRoutes = [
    { path: '', component: __WEBPACK_IMPORTED_MODULE_1__user_component__["a" /* UserComponent */] },
    { path: 'new', component: __WEBPACK_IMPORTED_MODULE_0__form_user_form_user_component__["a" /* FormUserComponent */] },
    { path: 'edit/:id', component: __WEBPACK_IMPORTED_MODULE_0__form_user_form_user_component__["a" /* FormUserComponent */] }
];
/**
 * @description
 * Módulo responsável por gerenciar as rotas do módulo de user
 */
var UserRoutingModule = /** @class */ (function () {
    /**
     * @description
     * A classe de roteamento do modulo de user
     */
    function UserRoutingModule() {
    }
    UserRoutingModule = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_2__angular_core__["J" /* NgModule */])({
            imports: [__WEBPACK_IMPORTED_MODULE_3__angular_router__["e" /* RouterModule */].forChild(userRoutes)],
            exports: [__WEBPACK_IMPORTED_MODULE_3__angular_router__["e" /* RouterModule */]]
        })
        /**
         * @description
         * A classe de roteamento do modulo de user
         */
    ], UserRoutingModule);
    return UserRoutingModule;
}());



/***/ }),

/***/ "../../../../../src/app/user/user.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(true);
// imports


// module
exports.push([module.i, "", "", {"version":3,"sources":[],"names":[],"mappings":"","file":"user.component.css","sourceRoot":""}]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/user/user.component.html":
/***/ (function(module, exports) {

module.exports = "<thf-page-list\r\n  t-title=\"Usuários\"\r\n  [t-filter]=\"thfFilter\"\r\n  [t-actions]=\"thfActions\">\r\n\r\n  <thf-table\r\n      [t-items]=\"dataService.items\"\r\n      [t-columns]=\"dataService.getColumns()\"\r\n      [t-actions]=\"thfListActions\"\r\n      [t-sort]=\"true\"\r\n      [t-show-more-disabled]=\"!dataService.hasNext\"\r\n      (t-show-more)=\"dataService.more()\">\r\n  </thf-table>\r\n</thf-page-list>\r\n\r\n<thf-modal\r\n  [t-title]=\"thfAdvancedSearchName\"\r\n  [t-primary-action]=\"advancedSearchPrimaryAction\"\r\n  [t-secondary-action]=\"advancedSearchSecondaryAction\">\r\n\r\n  <form #form=\"ngForm\" >\r\n      \r\n      <div class=\"thf-xl-6 thf-lg-6 thf-md-6 thf-sm-12\">\r\n        <thf-input\r\n            t-label=\"Login\"\r\n            t-clean\r\n            t-maxlength=\"128\"\r\n            name=\"userName\"\r\n            [(ngModel)]=userAdvancedSearch.userName>\r\n        </thf-input>\r\n      </div>\r\n            \r\n      <div class=\"thf-xl-6 thf-lg-6 thf-md-6 thf-sm-12\">\r\n        <thf-input\r\n            t-label=\"Email\"\r\n            t-clean\r\n            t-maxlength=\"128\"\r\n            name=\"emailAddress\"\r\n            [(ngModel)]=userAdvancedSearch.emailAddress>\r\n        </thf-input>\r\n      </div>\r\n            \r\n      <div class=\"thf-xl-6 thf-lg-6 thf-md-6 thf-sm-12\">\r\n        <thf-input\r\n            t-label=\"Nome\"\r\n            t-clean\r\n            t-maxlength=\"128\"\r\n            name=\"fullName\"\r\n            [(ngModel)]=userAdvancedSearch.fullName>\r\n        </thf-input>\r\n      </div>\r\n            \r\n      <div class=\"thf-xl-6 thf-lg-6 thf-md-6 thf-sm-12\">\r\n        <thf-input\r\n            t-label=\"Telefone\"\r\n            t-clean\r\n            t-maxlength=\"128\"\r\n            name=\"phoneNumber\"\r\n            t-mask=\"customMask\"\r\n            [(ngModel)]=userAdvancedSearch.phoneNumber>\r\n        </thf-input>\r\n      </div>\r\n                  \r\n      <div class=\"thf-xl-6 thf-lg-6 thf-md-6 thf-sm-12\">\r\n        <thf-checkbox-group\r\n            name=\"isActive\"\r\n            [t-options]=\"userAdvancedSearch.isActiveList\"\r\n            [(ngModel)]=userAdvancedSearch.isActiveResponse>\r\n        </thf-checkbox-group>\r\n      </div>\r\n      \r\n  </form>\r\n</thf-modal>"

/***/ }),

/***/ "../../../../../src/app/user/user.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UserComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__utils_hub_crud_component_list__ = __webpack_require__("../../../../../src/app/utils/hub-crud-component-list.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_router__ = __webpack_require__("../../../router/esm5/router.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__totvs_thf_ui_components_thf_modal_thf_modal_component__ = __webpack_require__("../../../../@totvs/thf-ui/components/thf-modal/thf-modal.component.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__user_service__ = __webpack_require__("../../../../../src/app/user/user.service.ts");
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





/**
 * @description
 * Component da tela de consulta de User, baseado no modelo abstrato HubCrudComponentList.
 * @extends HubCrudComponentList
 */
var UserComponent = /** @class */ (function (_super) {
    __extends(UserComponent, _super);
    /**
     * Construtor para inicializar a classe.
     * @param router Referência para a rota, permitindo navegação entre páginas.
     * @param dataService Serviço de dados de user.
     */
    function UserComponent(router, dataService) {
        var _this = _super.call(this, router, dataService, __WEBPACK_IMPORTED_MODULE_4__user_service__["a" /* UserData */]) || this;
        _this.router = router;
        _this.dataService = dataService;
        _this.customMask = '(99) 99999-9999';
        /**
         * Objeto que representa os campos da busca avançada.
         */
        _this.userAdvancedSearch = {
            userName: '',
            emailAddress: '',
            fullName: '',
            phoneNumber: '',
            isActiveList: [{ value: 'isActive', label: 'Ativo' }],
            isActiveResponse: []
        };
        /**
         * Array de objetos do tipo ThfPageAction, descrevendo as ações dos botões da página de lista.
         */
        _this.thfActions = [
            { label: 'Incluir', url: _this.dataService.serviceName + "/new", icon: 'thf-icon-plus' }
        ];
        return _this;
    }
    /**
     * Método responsável por criar os filtros para que a consulta seja realizada da maneira correta.
     * @param isAdvancedSearch Indica se o filtro é de busca avançada (true) ou de busca rápida (false).
     */
    UserComponent.prototype.setFilters = function (isAdvancedSearch) {
        if (isAdvancedSearch === void 0) { isAdvancedSearch = false; }
        this.dataService.filter = new Array();
        if (this.quickSearch && !isAdvancedSearch) {
            this.dataService.filter.push({ key: 'fullName', value: this.quickSearch });
        }
        else {
            this.dataService.filter.push({ key: 'userName', value: this.userAdvancedSearch.userName });
            this.dataService.filter.push({ key: 'emailAddress', value: this.userAdvancedSearch.emailAddress });
            this.dataService.filter.push({ key: 'fullName', value: this.userAdvancedSearch.fullName });
            this.dataService.filter.push({ key: 'phoneNumber', value: this.userAdvancedSearch.phoneNumber });
            this.dataService.filter.push({ key: 'isActive', value: this.userAdvancedSearch.isActiveResponse.length > 0 });
        }
    };
    __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_1__angular_core__["_10" /* ViewChild */])(__WEBPACK_IMPORTED_MODULE_3__totvs_thf_ui_components_thf_modal_thf_modal_component__["a" /* ThfModalComponent */]),
        __metadata("design:type", __WEBPACK_IMPORTED_MODULE_3__totvs_thf_ui_components_thf_modal_thf_modal_component__["a" /* ThfModalComponent */])
    ], UserComponent.prototype, "thfAdvancedSearch", void 0);
    UserComponent = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_1__angular_core__["n" /* Component */])({
            selector: 'app-user',
            template: __webpack_require__("../../../../../src/app/user/user.component.html"),
            styles: [__webpack_require__("../../../../../src/app/user/user.component.css")]
        }),
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_2__angular_router__["d" /* Router */], __WEBPACK_IMPORTED_MODULE_4__user_service__["b" /* UserService */]])
    ], UserComponent);
    return UserComponent;
}(__WEBPACK_IMPORTED_MODULE_0__utils_hub_crud_component_list__["a" /* HubCrudComponentList */]));



/***/ }),

/***/ "../../../../../src/app/user/user.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "UserModule", function() { return UserModule; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__user_routing_module__ = __webpack_require__("../../../../../src/app/user/user-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__totvs_thf_ui_thf_module__ = __webpack_require__("../../../../@totvs/thf-ui/thf.module.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_common__ = __webpack_require__("../../../common/esm5/common.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_forms__ = __webpack_require__("../../../forms/esm5/forms.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_common_http__ = __webpack_require__("../../../common/esm5/http.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__user_component__ = __webpack_require__("../../../../../src/app/user/user.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__form_user_form_user_component__ = __webpack_require__("../../../../../src/app/user/form-user/form-user.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__user_service__ = __webpack_require__("../../../../../src/app/user/user.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__organizationUnit_organizationUnit_service__ = __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__role_role_service__ = __webpack_require__("../../../../../src/app/role/role.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__tenant_tenant_service__ = __webpack_require__("../../../../../src/app/tenant/tenant.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__auth_auth_service__ = __webpack_require__("../../../../../src/app/auth/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__auth_token_interceptor__ = __webpack_require__("../../../../../src/app/auth/token.interceptor.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_14__tenant_tenant_thflookup_service__ = __webpack_require__("../../../../../src/app/tenant/tenant.thflookup.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};















var UserModule = /** @class */ (function () {
    /**
     * @description
     * Classe principal do modulo de user
     */
    function UserModule() {
    }
    UserModule = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_2__angular_core__["J" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_3__angular_common__["b" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_1__totvs_thf_ui_thf_module__["a" /* ThfModule */],
                __WEBPACK_IMPORTED_MODULE_0__user_routing_module__["a" /* UserRoutingModule */],
                __WEBPACK_IMPORTED_MODULE_4__angular_forms__["d" /* FormsModule */],
                __WEBPACK_IMPORTED_MODULE_5__angular_common_http__["c" /* HttpClientModule */]
            ],
            declarations: [
                __WEBPACK_IMPORTED_MODULE_6__user_component__["a" /* UserComponent */],
                __WEBPACK_IMPORTED_MODULE_7__form_user_form_user_component__["a" /* FormUserComponent */]
            ],
            providers: [
                __WEBPACK_IMPORTED_MODULE_8__user_service__["b" /* UserService */],
                __WEBPACK_IMPORTED_MODULE_10__role_role_service__["b" /* RoleService */],
                __WEBPACK_IMPORTED_MODULE_9__organizationUnit_organizationUnit_service__["b" /* OrganizationService */],
                __WEBPACK_IMPORTED_MODULE_11__tenant_tenant_service__["b" /* TenantService */],
                __WEBPACK_IMPORTED_MODULE_12__auth_auth_service__["a" /* AuthService */],
                __WEBPACK_IMPORTED_MODULE_14__tenant_tenant_thflookup_service__["a" /* TenantThfLookupService */],
                {
                    provide: __WEBPACK_IMPORTED_MODULE_5__angular_common_http__["a" /* HTTP_INTERCEPTORS */],
                    useClass: __WEBPACK_IMPORTED_MODULE_13__auth_token_interceptor__["a" /* TokenInterceptor */],
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



/***/ }),

/***/ "../../../../../src/app/user/user.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return UserService; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UserData; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__utils_hub_entity__ = __webpack_require__("../../../../../src/app/utils/hub-entity.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common_http__ = __webpack_require__("../../../common/esm5/http.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__utils_hub_crud_service__ = __webpack_require__("../../../../../src/app/utils/hub-crud-service.ts");
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




var UserService = /** @class */ (function (_super) {
    __extends(UserService, _super);
    /**
     * Construtor para inicializar a classe.
     * @param http Serviço HTTP que permite realizar as requisições para o servidor.
     */
    function UserService(http) {
        var _this = _super.call(this, http, UserData) || this;
        /** serviceName: define o nome do serviço para comunicação */
        _this.serviceName = 'user';
        /**
         * Atributo com as colunas apresentadas na tabela da lista.
         */
        _this.columns = [
            { column: 'tenantName', label: 'Nome do Tenant' },
            { column: 'fullName', label: 'Nome' },
            { column: 'userName', label: 'Login' },
            { column: 'emailAddress', label: 'Email' },
            { column: 'isActive', label: 'Ativo', type: 'check' }
        ];
        return _this;
    }
    UserService = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_2__angular_core__["B" /* Injectable */])()
        /**
         * @description
         * Essa classe permite fazer a configuração do serviço REST
         * @extends HubCRUDService
         */
        ,
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__angular_common_http__["b" /* HttpClient */]])
    ], UserService);
    return UserService;
}(__WEBPACK_IMPORTED_MODULE_3__utils_hub_crud_service__["a" /* HubCRUDService */]));

/**
 * @description
 * A classe representa a entidade da tabela user e os atributos
 * são equivalentes aos campos da tabela
 * @extends HubEntity
 */
var UserData = /** @class */ (function (_super) {
    __extends(UserData, _super);
    /**
     * Método construtor para inicializar a classe users.
     * @param Objeto representando a users.
     */
    function UserData(obj) {
        if (obj === void 0) { obj = { id: undefined, userName: '', emailAddress: '', name: '', surname: '', phoneNumber: '', password: '', isActive: true, roles: [], organizations: [], tenantId: 0 }; }
        var _this = _super.call(this) || this;
        _this.userName = '';
        _this.emailAddress = '';
        _this.name = '';
        _this.surname = '';
        _this.phoneNumber = '';
        _this.isActive = false;
        _this.password = '';
        _this.tenantId = 0;
        _this.roles = [];
        _this.organizations = [];
        _this.id = obj.id;
        _this.userName = obj.userName;
        _this.emailAddress = obj.emailAddress;
        _this.name = obj.name;
        _this.surname = obj.surname;
        _this.phoneNumber = obj.phoneNumber;
        _this.password = obj.password;
        _this.isActive = obj.isActive;
        _this.roles = obj.roles;
        _this.organizations = obj.organizations;
        _this.tenantId = obj.tenantId;
        return _this;
    }
    return UserData;
}(__WEBPACK_IMPORTED_MODULE_0__utils_hub_entity__["a" /* HubEntity */]));



/***/ })

});
//# sourceMappingURL=user.module.chunk.js.map