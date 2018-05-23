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
var hub_crud_component_list_1 = require("./../utils/hub-crud-component-list");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var thf_modal_component_1 = require("@totvs/thf-ui/components/thf-modal/thf-modal.component");
var tenant_service_1 = require("./tenant.service");
var auth_service_1 = require("../auth/auth.service");
/**
 * @description
 * Component da tela de consulta de Tenant, baseado no modelo abstrato HubCrudComponentList.
 * @extends HubCrudComponentList
 */
var TenantComponent = /** @class */ (function (_super) {
    __extends(TenantComponent, _super);
    /**
     * Construtor para inicializar a classe.
     * @param router Referência para a rota, permitindo navegação entre páginas.
     * @param dataService Serviço de dados de tenant.
     */
    function TenantComponent(router, dataService, authService) {
        var _this = _super.call(this, router, dataService, tenant_service_1.TenantData) || this;
        _this.router = router;
        _this.dataService = dataService;
        _this.authService = authService;
        /**
         * thfListActions: atributo com as ações dos registros lista de dados
         */
        _this.tenantListActions = [
            { action: 'update', label: 'Editar' }
        ];
        /**
         * Objeto que representa os campos da busca avançada.
         */
        _this.tenantAdvancedSearch = {
            tenantName: '',
            name: '',
            // connectionString: '',
            isActiveList: [{ value: 'isActive', label: 'Ativo' }],
            isActiveResponse: []
        };
        /**
         * Array de objetos do tipo ThfPageAction, descrevendo as ações dos botões da página de lista.
         */
        _this.thfActions = [];
        _this.authService.onCompleteAuthentication().subscribe(function () {
            if (_this.authService.isHostUser()) {
                _this.thfActions = [
                    { label: 'Incluir', url: _this.dataService.serviceName + "/new", icon: 'thf-icon-plus' }
                ];
            }
        });
        return _this;
    }
    /**
     * Método responsável por criar os filtros para que a consulta seja realizada da maneira correta.
     * @param isAdvancedSearch Indica se o filtro é de busca avançada (true) ou de busca rápida (false).
     */
    TenantComponent.prototype.setFilters = function (isAdvancedSearch) {
        if (isAdvancedSearch === void 0) { isAdvancedSearch = false; }
        this.dataService.filter = new Array();
        if (this.quickSearch && !isAdvancedSearch) {
            this.dataService.filter.push({ key: 'tenantName', value: this.quickSearch });
        }
        else {
            this.dataService.filter.push({ key: 'tenantName', value: this.tenantAdvancedSearch.tenantName });
            this.dataService.filter.push({ key: 'name', value: this.tenantAdvancedSearch.name });
            // this.dataService.filter.push({ key: 'connectionString', value: this.tenantAdvancedSearch.connectionString });
            this.dataService.filter.push({ key: 'isActive', value: this.tenantAdvancedSearch.isActiveResponse.length > 0 });
        }
    };
    __decorate([
        core_1.ViewChild(thf_modal_component_1.ThfModalComponent),
        __metadata("design:type", thf_modal_component_1.ThfModalComponent)
    ], TenantComponent.prototype, "thfAdvancedSearch", void 0);
    TenantComponent = __decorate([
        core_1.Component({
            selector: 'app-tenant',
            templateUrl: './tenant.component.html',
            styleUrls: ['./tenant.component.css']
        }),
        __metadata("design:paramtypes", [router_1.Router, tenant_service_1.TenantService, auth_service_1.AuthService])
    ], TenantComponent);
    return TenantComponent;
}(hub_crud_component_list_1.HubCrudComponentList));
exports.TenantComponent = TenantComponent;
//# sourceMappingURL=tenant.component.js.map