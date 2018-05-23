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
var role_service_1 = require("./../role.service");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var hub_api_1 = require("../../utils/hub-api");
var ngx_treeview_1 = require("ngx-treeview");
var role_treeview_i18n_1 = require("./role-treeview-i18n");
var tenant_service_1 = require("../../tenant/tenant.service");
var auth_service_1 = require("../../auth/auth.service");
var tenant_thflookup_service_1 = require("../../tenant/tenant.thflookup.service");
var FormRoleComponent = /** @class */ (function (_super) {
    __extends(FormRoleComponent, _super);
    /**
     * O Contrutor para inicialização
     */
    function FormRoleComponent(activatedRoute, router, dataService, tenantThfLookupService, tenantService, authService) {
        var _this = _super.call(this, activatedRoute, router, dataService, role_service_1.RoleData) || this;
        _this.activatedRoute = activatedRoute;
        _this.router = router;
        _this.dataService = dataService;
        _this.tenantThfLookupService = tenantThfLookupService;
        _this.tenantService = tenantService;
        _this.authService = authService;
        _this.isDefaultList = [{ value: 'isDefault', label: 'Padrão' }];
        _this.isDefaultResponse = [];
        _this.tenantColumns = [
            { column: 'tenantName', label: 'Nome', type: 'string' },
            { column: 'name', label: 'Nome de Exibição', type: 'string', fieldLabel: true }
        ];
        _this.config = ngx_treeview_1.TreeviewConfig.create({
            hasAllCheckBox: true,
            hasFilter: true,
            hasCollapseExpand: true,
            decoupleChildFromParent: false,
            maxHeight: 350
        });
        _this.reduceToTreeview = function (parent, el, index, array) {
            var value = parent.name ? parent.name + "." + el : el;
            var treeItem = (parent.item.children || []).find(function (p) { return p.text === el; });
            if (!treeItem) {
                treeItem = new ngx_treeview_1.TreeviewItem({ text: el, value: "", checked: _this.data.permissions.includes(value) });
                parent.item.children ?
                    parent.item.children.push(treeItem) :
                    parent.item.children = [treeItem];
            }
            if (index === array.length - 1)
                treeItem.value = value;
            return { item: treeItem, name: value };
        };
        dataService.addExpand("permissions");
        _this.isDefaultResponse = [];
        _this.beforeEditing.subscribe(function (data) {
            _this.tenantId = data.tenantId;
            _this.isDefaultResponse = _this.data.isDefault ? ['isDefault'] : [];
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
    FormRoleComponent.prototype.ngAfterViewInit = function () {
        this.toggleClassName('ngx-treeview input[type=text]', 'thf-input');
    };
    FormRoleComponent.prototype.ngAfterContentChecked = function () {
        this.toggleClassName('ngx-treeview input[type=checkbox]', 'thf-checkbox-group-input');
        this.toggleClassName('ngx-treeview input[type=checkbox]+label', 'thf-checkbox-group-label');
        this.toggleClassName('ngx-treeview input[type=checkbox]+label', 'thf-clickable');
        this.toggleClassName('ngx-treeview input[type=checkbox]', 'thf-checkbox-group-input-checked', 'ng-reflect-model');
        if (this.data.tenantId &&
            this.tenantId !== this.data.tenantId) {
            this.tenantId = this.data.tenantId;
            this.onChangedTenant();
        }
    };
    FormRoleComponent.prototype.toggleClassName = function (el, className, removeAttribute) {
        [].forEach.call(document.querySelectorAll(el), function (e) {
            if (removeAttribute && (!e.getAttribute(removeAttribute) || e.getAttribute(removeAttribute) === "false"))
                e.classList.remove(className);
            else
                e.classList.add(className);
        });
    };
    FormRoleComponent.prototype.onFeaturesChange = function (selectedFeatures) {
        if (this.features.length > 0) {
            this.childModified = true;
            this.data.permissions = selectedFeatures;
        }
    };
    FormRoleComponent.prototype.createTreeview = function (list) {
        var _this = this;
        var listTree = [];
        var treeParent = { item: { children: listTree } };
        var splittedLists = list.map(function (p) { return p.split('.'); });
        splittedLists.forEach(function (l) { return l.reduce(_this.reduceToTreeview, treeParent); });
        return listTree;
    };
    FormRoleComponent.prototype.onChangedTenant = function () {
        var _this = this;
        this.dataService.getFeaturesByTenant(this.data.tenantId).subscribe(function (success) {
            if (success) {
                _this.features = _this.createTreeview(success);
            }
        });
    };
    FormRoleComponent = __decorate([
        core_1.Component({
            selector: 'app-form-role',
            templateUrl: './form-role.component.html',
            styleUrls: ['./form-role.component.css'],
            providers: [
                { provide: ngx_treeview_1.TreeviewI18n, useClass: role_treeview_i18n_1.RoleTreeviewI18n }
            ]
        })
        /**
         * @description
         * A classe responsável pelas ações do formulário de role
         * @extends HubCrudComponentForm
         */
        ,
        __metadata("design:paramtypes", [router_1.ActivatedRoute,
            router_1.Router,
            role_service_1.RoleService,
            tenant_thflookup_service_1.TenantThfLookupService,
            tenant_service_1.TenantService,
            auth_service_1.AuthService])
    ], FormRoleComponent);
    return FormRoleComponent;
}(hub_crud_component_edit_1.HubCrudComponentForm));
exports.FormRoleComponent = FormRoleComponent;
//# sourceMappingURL=form-role.component.js.map