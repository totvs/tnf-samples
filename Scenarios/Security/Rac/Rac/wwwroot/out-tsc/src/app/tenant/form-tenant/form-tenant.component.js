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
var tenant_service_1 = require("./../tenant.service");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var FormTenantComponent = /** @class */ (function (_super) {
    __extends(FormTenantComponent, _super);
    /**
     * O Contrutor para inicialização
     */
    function FormTenantComponent(activatedRoute, router, dataService) {
        var _this = _super.call(this, activatedRoute, router, dataService, tenant_service_1.TenantData) || this;
        _this.activatedRoute = activatedRoute;
        _this.router = router;
        _this.dataService = dataService;
        _this.isActiveList = [{ value: 'isActive', label: 'Ativo' }];
        _this.isActiveResponse = [];
        _this.isActiveResponse = [];
        _this.beforeEditing.subscribe(function (data) {
            _this.isActiveResponse = data.isActive ? ['isActive'] : [];
        });
        return _this;
    }
    ;
    FormTenantComponent = __decorate([
        core_1.Component({
            selector: 'app-form-tenant',
            templateUrl: './form-tenant.component.html',
            styleUrls: ['./form-tenant.component.css']
        })
        /**
         * @description
         * A classe responsável pelas ações do formulário de tenant
         * @extends HubCrudComponentForm
         */
        ,
        __metadata("design:paramtypes", [router_1.ActivatedRoute,
            router_1.Router,
            tenant_service_1.TenantService])
    ], FormTenantComponent);
    return FormTenantComponent;
}(hub_crud_component_edit_1.HubCrudComponentForm));
exports.FormTenantComponent = FormTenantComponent;
//# sourceMappingURL=form-tenant.component.js.map