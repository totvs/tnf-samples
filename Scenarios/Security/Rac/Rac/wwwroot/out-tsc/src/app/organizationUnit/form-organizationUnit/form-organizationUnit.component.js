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
var organizationUnit_service_1 = require("./../organizationUnit.service");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var hub_api_1 = require("../../utils/hub-api");
var tenant_service_1 = require("../../tenant/tenant.service");
var auth_service_1 = require("../../auth/auth.service");
var tenant_thflookup_service_1 = require("../../tenant/tenant.thflookup.service");
var organizationUnit_thflookup_service_1 = require("../organizationUnit.thflookup.service");
var FormOrganizationComponent = /** @class */ (function (_super) {
    __extends(FormOrganizationComponent, _super);
    /**
     * O Contrutor para inicialização
     */
    function FormOrganizationComponent(activatedRoute, router, dataService, tenantThfLookupService, organizationThfLookupService, tenantService, authService) {
        var _this = _super.call(this, activatedRoute, router, dataService, organizationUnit_service_1.OrganizationData) || this;
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
                tenantService.getQuery(new hub_api_1.HubApiQueryString({
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
        core_1.Component({
            selector: 'app-form-organization',
            templateUrl: './form-organizationUnit.component.html',
            styleUrls: ['./form-organizationUnit.component.css']
        })
        /**
         * @description
         * A classe responsável pelas ações do formulário de organization
         * @extends HubCrudComponentForm
         */
        ,
        __metadata("design:paramtypes", [router_1.ActivatedRoute,
            router_1.Router,
            organizationUnit_service_1.OrganizationService,
            tenant_thflookup_service_1.TenantThfLookupService,
            organizationUnit_thflookup_service_1.OrganizationThfLookupService,
            tenant_service_1.TenantService,
            auth_service_1.AuthService])
    ], FormOrganizationComponent);
    return FormOrganizationComponent;
}(hub_crud_component_edit_1.HubCrudComponentForm));
exports.FormOrganizationComponent = FormOrganizationComponent;
//# sourceMappingURL=form-organizationUnit.component.js.map