webpackJsonp(["organizationUnit.module"],{

/***/ "../../../../../src/app/organizationUnit/form-organizationUnit/form-organizationUnit.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "form div[class^=\"thf-\"] {\r\n    margin-bottom: 22px;\r\n}\r\n\r\nform div[class^=\"thf-field\"] {\r\n    margin-bottom: 0px;\r\n}", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/organizationUnit/form-organizationUnit/form-organizationUnit.component.html":
/***/ (function(module, exports) {

module.exports = "<thf-page-edit  \r\n        t-title=\"Cadastro de Organização\">\r\n  <form #form=\"ngForm\" >\r\n          \r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n      <thf-input\r\n          class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n          t-label=\"Nome da Organização\"\r\n          t-help=\"Informe o nome da Organização\"\r\n          t-required\r\n          t-clean\r\n          t-maxlength=\"128\"\r\n          name=\"name\"\r\n          t-focus\r\n          [(ngModel)]=this.data.name>\r\n      </thf-input>\r\n    </div>\r\n    \r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" >\r\n      <thf-input\r\n          class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n          t-label=\"Nome de Exibição\"\r\n          t-help=\"Informe o nome de exibição da Organização\"\r\n          t-required\r\n          t-clean\r\n          t-maxlength=\"128\"\r\n          name=\"displayName\"\r\n          [(ngModel)]=this.data.displayName>\r\n      </thf-input>\r\n    </div>\r\n\r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\" *ngIf=\"this.authService.isHostUser()\">\r\n      <thf-lookup\r\n        class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n        name=\"tenantId\"\r\n        t-field-value=\"id\"\r\n        t-field-label=\"name\"\r\n        t-label=\"Tenant\"\r\n        t-help=\"Informe o tenant da Organização\"\r\n        t-placeholder=\"Nenhum\"\r\n        t-title=\"Selecione um tenant\"\r\n        [t-filter-service]=\"tenantThfLookupService\"\r\n        [t-columns]=\"tenantColumns\"\r\n        [(ngModel)]=\"this.data.tenantId\">\r\n      </thf-lookup>\r\n    </div>\r\n\r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\"  *ngIf=\"!this.isUpdate\">\r\n      <thf-lookup\r\n        class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\"\r\n        name=\"parentId\"\r\n        t-field-value=\"id\"\r\n        t-field-label=\"displayName\"\r\n        t-label=\"Organização Pai\"\r\n        t-help=\"Informe a Organização Pai, se nenhum for selecionado então a Organização não possuirá pai\"\r\n        t-placeholder=\"Nenhum\"\r\n        t-title=\"Selecione uma Organização Pai\"\r\n        [t-filter-service]=\"organizationThfLookupService\"\r\n        [t-columns]=\"organizationColumns\"\r\n        [(ngModel)]=\"this.data.parentId\">\r\n      </thf-lookup>\r\n    </div>\r\n\r\n    <div class=\"thf-xl-12 thf-lg-12 thf-md-12 thf-sm-12\"  *ngIf=\"this.isUpdate && this.parentName\">\r\n      <div class=\"thf-xl-6 thf-lg-6 thf-md-12 thf-sm-12\">\r\n        <div class=\"thf-field-container\"> \r\n          <div class=\"thf-field-container-title\" style=\"margin-bottom: 5px;\"> \r\n            <span class=\"thf-field-title\">Nome da Organização Pai</span>\r\n          </div> \r\n          <div class=\"thf-field-help\" style=\"margin-bottom: 6px;\">Nome da Organização Pai</div>\r\n          <div class=\"thf-field-container-input\"> \r\n            <span class=\"thf-input\">{{ this.parentName }}</span>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </div>\r\n\r\n  </form>\r\n</thf-page-edit>"

/***/ }),

/***/ "../../../../../src/app/organizationUnit/form-organizationUnit/form-organizationUnit.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return FormOrganizationComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__utils_hub_crud_component_edit__ = __webpack_require__("../../../../../src/app/utils/hub-crud-component-edit.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__organizationUnit_service__ = __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_router__ = __webpack_require__("../../../router/esm5/router.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__utils_hub_api__ = __webpack_require__("../../../../../src/app/utils/hub-api.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__tenant_tenant_service__ = __webpack_require__("../../../../../src/app/tenant/tenant.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__auth_auth_service__ = __webpack_require__("../../../../../src/app/auth/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__tenant_tenant_thflookup_service__ = __webpack_require__("../../../../../src/app/tenant/tenant.thflookup.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__organizationUnit_thflookup_service__ = __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.thflookup.service.ts");
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









var FormOrganizationComponent = /** @class */ (function (_super) {
    __extends(FormOrganizationComponent, _super);
    /**
     * O Contrutor para inicialização
     */
    function FormOrganizationComponent(activatedRoute, router, dataService, tenantThfLookupService, organizationThfLookupService, tenantService, authService) {
        var _this = _super.call(this, activatedRoute, router, dataService, __WEBPACK_IMPORTED_MODULE_1__organizationUnit_service__["a" /* OrganizationData */]) || this;
        _this.activatedRoute = activatedRoute;
        _this.router = router;
        _this.dataService = dataService;
        _this.tenantThfLookupService = tenantThfLookupService;
        _this.organizationThfLookupService = organizationThfLookupService;
        _this.tenantService = tenantService;
        _this.authService = authService;
        _this.tenantColumns = [
            { column: 'tenantName', label: 'Nome', type: 'string' },
            { column: 'name', label: 'Nome de Exibição', type: 'string', fieldLabel: true }
        ];
        _this.organizationColumns = [
            { column: 'name', label: 'Nome', type: 'string' },
            { column: 'displayName', label: 'Nome de Exibição', type: 'string', fieldLabel: true }
        ];
        _this.beforeEditing.subscribe(function (data) {
            _this.tenantId = data.tenantId;
            if (!authService.isHostUser()) {
                tenantService.getQuery(new __WEBPACK_IMPORTED_MODULE_4__utils_hub_api__["a" /* HubApiQueryString */]({
                    page: 1,
                    pageSize: 1
                })).subscribe(function (success) {
                    if (success && success.items)
                        _this.data.tenantId = success.items[0].id;
                });
            }
            if (_this.isUpdate) {
                dataService.get(data.parentId)
                    .subscribe(function (success) {
                    if (success)
                        _this.parentName = success.displayName;
                });
            }
            _this.onChangedTenant();
        });
        return _this;
    }
    FormOrganizationComponent.prototype.ngAfterContentChecked = function () {
        if (this.data.tenantId &&
            this.tenantId !== this.data.tenantId) {
            this.tenantId = this.data.tenantId;
            this.onChangedTenant();
        }
    };
    FormOrganizationComponent.prototype.onChangedTenant = function () {
        this.data.parentId = 0;
        this.organizationThfLookupService.tenantId = this.data.tenantId;
    };
    FormOrganizationComponent = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_2__angular_core__["n" /* Component */])({
            selector: 'app-form-organization',
            template: __webpack_require__("../../../../../src/app/organizationUnit/form-organizationUnit/form-organizationUnit.component.html"),
            styles: [__webpack_require__("../../../../../src/app/organizationUnit/form-organizationUnit/form-organizationUnit.component.css")]
        })
        /**
         * @description
         * A classe responsável pelas ações do formulário de organization
         * @extends HubCrudComponentForm
         */
        ,
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_3__angular_router__["a" /* ActivatedRoute */],
            __WEBPACK_IMPORTED_MODULE_3__angular_router__["d" /* Router */],
            __WEBPACK_IMPORTED_MODULE_1__organizationUnit_service__["b" /* OrganizationService */],
            __WEBPACK_IMPORTED_MODULE_7__tenant_tenant_thflookup_service__["a" /* TenantThfLookupService */],
            __WEBPACK_IMPORTED_MODULE_8__organizationUnit_thflookup_service__["a" /* OrganizationThfLookupService */],
            __WEBPACK_IMPORTED_MODULE_5__tenant_tenant_service__["b" /* TenantService */],
            __WEBPACK_IMPORTED_MODULE_6__auth_auth_service__["a" /* AuthService */]])
    ], FormOrganizationComponent);
    return FormOrganizationComponent;
}(__WEBPACK_IMPORTED_MODULE_0__utils_hub_crud_component_edit__["a" /* HubCrudComponentForm */]));



/***/ }),

/***/ "../../../../../src/app/organizationUnit/organizationUnit-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OrganizationRoutingModule; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__form_organizationUnit_form_organizationUnit_component__ = __webpack_require__("../../../../../src/app/organizationUnit/form-organizationUnit/form-organizationUnit.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__organizationUnit_component__ = __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_router__ = __webpack_require__("../../../router/esm5/router.js");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var organizationRoutes = [
    { path: '', component: __WEBPACK_IMPORTED_MODULE_1__organizationUnit_component__["a" /* OrganizationComponent */] },
    { path: 'new', component: __WEBPACK_IMPORTED_MODULE_0__form_organizationUnit_form_organizationUnit_component__["a" /* FormOrganizationComponent */] },
    { path: 'edit/:id', component: __WEBPACK_IMPORTED_MODULE_0__form_organizationUnit_form_organizationUnit_component__["a" /* FormOrganizationComponent */] }
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
        Object(__WEBPACK_IMPORTED_MODULE_2__angular_core__["J" /* NgModule */])({
            imports: [__WEBPACK_IMPORTED_MODULE_3__angular_router__["e" /* RouterModule */].forChild(organizationRoutes)],
            exports: [__WEBPACK_IMPORTED_MODULE_3__angular_router__["e" /* RouterModule */]]
        })
        /**
         * @description
         * A classe de roteamento do modulo de organization
         */
    ], OrganizationRoutingModule);
    return OrganizationRoutingModule;
}());



/***/ }),

/***/ "../../../../../src/app/organizationUnit/organizationUnit.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/organizationUnit/organizationUnit.component.html":
/***/ (function(module, exports) {

module.exports = "<thf-page-list\r\n  t-title=\"Organizações\"\r\n  [t-filter]=\"thfFilter\"\r\n  [t-actions]=\"thfActions\">\r\n\r\n  <thf-table\r\n      [t-items]=\"dataService.items\"\r\n      [t-columns]=\"dataService.getColumns()\"\r\n      [t-actions]=\"thfListActions\"\r\n      [t-sort]=\"true\"\r\n      [t-show-more-disabled]=\"!dataService.hasNext\"\r\n      (t-show-more)=\"dataService.more()\">\r\n  </thf-table>\r\n</thf-page-list>\r\n\r\n<thf-modal\r\n  [t-title]=\"thfAdvancedSearchName\"\r\n  [t-primary-action]=\"advancedSearchPrimaryAction\"\r\n  [t-secondary-action]=\"advancedSearchSecondaryAction\">\r\n\r\n  <form #form=\"ngForm\" >\r\n      \r\n      <div class=\"thf-xl-6 thf-lg-6 thf-md-6 thf-sm-12\">\r\n        <thf-input\r\n            t-label=\"Nome da Organização\"\r\n            t-clean\r\n            t-maxlength=\"128\"\r\n            name=\"name\"\r\n            [(ngModel)]=organizationAdvancedSearch.name>\r\n        </thf-input>\r\n      </div>\r\n            \r\n      <div class=\"thf-xl-6 thf-lg-6 thf-md-6 thf-sm-12\">\r\n        <thf-input\r\n            t-label=\"Nome de Exibição\"\r\n            t-clean\r\n            t-maxlength=\"128\"\r\n            name=\"displayName\"\r\n            [(ngModel)]=organizationAdvancedSearch.displayName>\r\n        </thf-input>\r\n      </div>\r\n            \r\n      <div class=\"thf-xl-6 thf-lg-6 thf-md-6 thf-sm-12\">\r\n        <thf-select\r\n            t-label=\"Nome da Organização Pai\"\r\n            t-clean\r\n            t-maxlength=\"128\"\r\n            name=\"parentId\"\r\n            [t-options]=\"organizations\"\r\n            [(ngModel)]=organizationAdvancedSearch.parentId>\r\n        </thf-select>\r\n      </div>\r\n      \r\n  </form>\r\n</thf-modal>"

/***/ }),

/***/ "../../../../../src/app/organizationUnit/organizationUnit.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OrganizationComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__utils_hub_crud_component_list__ = __webpack_require__("../../../../../src/app/utils/hub-crud-component-list.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_router__ = __webpack_require__("../../../router/esm5/router.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__totvs_thf_ui_components_thf_modal_thf_modal_component__ = __webpack_require__("../../../../@totvs/thf-ui/components/thf-modal/thf-modal.component.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__organizationUnit_service__ = __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.service.ts");
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
 * Component da tela de consulta de Organization, baseado no modelo abstrato HubCrudComponentList.
 * @extends HubCrudComponentList
 */
var OrganizationComponent = /** @class */ (function (_super) {
    __extends(OrganizationComponent, _super);
    /**
     * Construtor para inicializar a classe.
     * @param router Referência para a rota, permitindo navegação entre páginas.
     * @param dataService Serviço de dados de organization.
     */
    function OrganizationComponent(router, dataService) {
        var _this = _super.call(this, router, dataService, __WEBPACK_IMPORTED_MODULE_4__organizationUnit_service__["a" /* OrganizationData */]) || this;
        _this.router = router;
        _this.dataService = dataService;
        /**
         * Objeto que representa os campos da busca avançada.
         */
        _this.organizationAdvancedSearch = {
            name: '',
            displayName: '',
            parentId: 0
        };
        /**
         * Array de objetos do tipo ThfPageAction, descrevendo as ações dos botões da página de lista.
         */
        _this.thfActions = [
            { label: 'Incluir', url: _this.dataService.serviceName + "/new", icon: 'thf-icon-plus' }
        ];
        _this.organizations = [{ label: "Selecione um pai", value: 0 }];
        return _this;
        // dataService.getQuery(
        //   new HubApiQueryString({
        //     page: 1,
        //     pageSize: 50
        //   })
        // ).subscribe((success) => {
        //   if (success && success.items) {
        //     for (let org of success.items) {
        //       this.organizations.push({ label: org.name, value: org.id });
        //     }
        //   }
        // });
    }
    /**
     * Método responsável por criar os filtros para que a consulta seja realizada da maneira correta.
     * @param isAdvancedSearch Indica se o filtro é de busca avançada (true) ou de busca rápida (false).
     */
    OrganizationComponent.prototype.setFilters = function (isAdvancedSearch) {
        if (isAdvancedSearch === void 0) { isAdvancedSearch = false; }
        this.dataService.filter = new Array();
        if (this.quickSearch && !isAdvancedSearch) {
            this.dataService.filter.push({ key: 'name', value: this.quickSearch });
        }
        else {
            this.dataService.filter.push({ key: 'name', value: this.organizationAdvancedSearch.name });
            this.dataService.filter.push({ key: 'displayName', value: this.organizationAdvancedSearch.displayName });
            this.dataService.filter.push({ key: 'parentId', value: this.organizationAdvancedSearch.parentId });
        }
    };
    __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_1__angular_core__["_10" /* ViewChild */])(__WEBPACK_IMPORTED_MODULE_3__totvs_thf_ui_components_thf_modal_thf_modal_component__["a" /* ThfModalComponent */]),
        __metadata("design:type", __WEBPACK_IMPORTED_MODULE_3__totvs_thf_ui_components_thf_modal_thf_modal_component__["a" /* ThfModalComponent */])
    ], OrganizationComponent.prototype, "thfAdvancedSearch", void 0);
    OrganizationComponent = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_1__angular_core__["n" /* Component */])({
            selector: 'app-organization',
            template: __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.component.html"),
            styles: [__webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.component.css")]
        }),
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_2__angular_router__["d" /* Router */], __WEBPACK_IMPORTED_MODULE_4__organizationUnit_service__["b" /* OrganizationService */]])
    ], OrganizationComponent);
    return OrganizationComponent;
}(__WEBPACK_IMPORTED_MODULE_0__utils_hub_crud_component_list__["a" /* HubCrudComponentList */]));



/***/ }),

/***/ "../../../../../src/app/organizationUnit/organizationUnit.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "OrganizationModule", function() { return OrganizationModule; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__organizationUnit_routing_module__ = __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__totvs_thf_ui_thf_module__ = __webpack_require__("../../../../@totvs/thf-ui/thf.module.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_common__ = __webpack_require__("../../../common/esm5/common.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_forms__ = __webpack_require__("../../../forms/esm5/forms.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_common_http__ = __webpack_require__("../../../common/esm5/http.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__organizationUnit_component__ = __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__form_organizationUnit_form_organizationUnit_component__ = __webpack_require__("../../../../../src/app/organizationUnit/form-organizationUnit/form-organizationUnit.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__organizationUnit_service__ = __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__auth_auth_service__ = __webpack_require__("../../../../../src/app/auth/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__auth_token_interceptor__ = __webpack_require__("../../../../../src/app/auth/token.interceptor.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__tenant_tenant_service__ = __webpack_require__("../../../../../src/app/tenant/tenant.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__tenant_tenant_thflookup_service__ = __webpack_require__("../../../../../src/app/tenant/tenant.thflookup.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__organizationUnit_thflookup_service__ = __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.thflookup.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};














var OrganizationModule = /** @class */ (function () {
    /**
     * @description
     * Classe principal do modulo de organization
     */
    function OrganizationModule() {
    }
    OrganizationModule = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_2__angular_core__["J" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_3__angular_common__["b" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_1__totvs_thf_ui_thf_module__["a" /* ThfModule */],
                __WEBPACK_IMPORTED_MODULE_0__organizationUnit_routing_module__["a" /* OrganizationRoutingModule */],
                __WEBPACK_IMPORTED_MODULE_4__angular_forms__["d" /* FormsModule */],
                __WEBPACK_IMPORTED_MODULE_5__angular_common_http__["c" /* HttpClientModule */]
            ],
            declarations: [
                __WEBPACK_IMPORTED_MODULE_6__organizationUnit_component__["a" /* OrganizationComponent */],
                __WEBPACK_IMPORTED_MODULE_7__form_organizationUnit_form_organizationUnit_component__["a" /* FormOrganizationComponent */]
            ],
            providers: [
                __WEBPACK_IMPORTED_MODULE_8__organizationUnit_service__["b" /* OrganizationService */],
                __WEBPACK_IMPORTED_MODULE_13__organizationUnit_thflookup_service__["a" /* OrganizationThfLookupService */],
                __WEBPACK_IMPORTED_MODULE_9__auth_auth_service__["a" /* AuthService */],
                __WEBPACK_IMPORTED_MODULE_11__tenant_tenant_service__["b" /* TenantService */],
                __WEBPACK_IMPORTED_MODULE_12__tenant_tenant_thflookup_service__["a" /* TenantThfLookupService */],
                {
                    provide: __WEBPACK_IMPORTED_MODULE_5__angular_common_http__["a" /* HTTP_INTERCEPTORS */],
                    useClass: __WEBPACK_IMPORTED_MODULE_10__auth_token_interceptor__["a" /* TokenInterceptor */],
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



/***/ }),

/***/ "../../../../../src/app/organizationUnit/organizationUnit.thflookup.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OrganizationThfLookupService; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__organizationUnit_service__ = __webpack_require__("../../../../../src/app/organizationUnit/organizationUnit.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__utils_hub_api__ = __webpack_require__("../../../../../src/app/utils/hub-api.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var OrganizationThfLookupService = /** @class */ (function () {
    function OrganizationThfLookupService(organizationService) {
        this.organizationService = organizationService;
    }
    OrganizationThfLookupService.prototype.getFilteredData = function (filter, page, pageSize) {
        return this.organizationService.getQueryByTenant(new __WEBPACK_IMPORTED_MODULE_2__utils_hub_api__["a" /* HubApiQueryString */]({
            page: page,
            pageSize: pageSize,
            filter: [{ key: "name", value: filter }]
        }), this.tenantId);
    };
    OrganizationThfLookupService.prototype.getObjectByValue = function (value) {
        return this.organizationService.get(value);
    };
    OrganizationThfLookupService = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["B" /* Injectable */])(),
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__organizationUnit_service__["b" /* OrganizationService */]])
    ], OrganizationThfLookupService);
    return OrganizationThfLookupService;
}());



/***/ })

});
//# sourceMappingURL=organizationUnit.module.chunk.js.map