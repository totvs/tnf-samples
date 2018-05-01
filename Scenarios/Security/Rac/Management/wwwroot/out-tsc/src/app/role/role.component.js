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
var role_service_1 = require("./role.service");
/**
 * @description
 * Component da tela de consulta de Role, baseado no modelo abstrato HubCrudComponentList.
 * @extends HubCrudComponentList
 */
var RoleComponent = /** @class */ (function (_super) {
    __extends(RoleComponent, _super);
    /**
     * Construtor para inicializar a classe.
     * @param router Referência para a rota, permitindo navegação entre páginas.
     * @param dataService Serviço de dados de role.
     */
    function RoleComponent(router, dataService) {
        var _this = _super.call(this, router, dataService, role_service_1.RoleData) || this;
        _this.router = router;
        _this.dataService = dataService;
        /**
         * Objeto que representa os campos da busca avançada.
         */
        _this.roleAdvancedSearch = {
            name: '',
            displayName: '',
            isDefaultList: [{ value: 'isDefault', label: 'Padrão' }],
            isDefaultResponse: []
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
    RoleComponent.prototype.setFilters = function (isAdvancedSearch) {
        if (isAdvancedSearch === void 0) { isAdvancedSearch = false; }
        this.dataService.filter = new Array();
        if (this.quickSearch && !isAdvancedSearch) {
            this.dataService.filter.push({ key: 'name', value: this.quickSearch });
        }
        else {
            this.dataService.filter.push({ key: 'name', value: this.roleAdvancedSearch.name });
            this.dataService.filter.push({ key: 'displayName', value: this.roleAdvancedSearch.displayName });
            this.dataService.filter.push({ key: 'isDefault', value: this.roleAdvancedSearch.isDefaultResponse.length > 0 });
        }
    };
    __decorate([
        core_1.ViewChild(thf_modal_component_1.ThfModalComponent),
        __metadata("design:type", thf_modal_component_1.ThfModalComponent)
    ], RoleComponent.prototype, "thfAdvancedSearch", void 0);
    RoleComponent = __decorate([
        core_1.Component({
            selector: 'app-role',
            templateUrl: './role.component.html',
            styleUrls: ['./role.component.css']
        }),
        __metadata("design:paramtypes", [router_1.Router, role_service_1.RoleService])
    ], RoleComponent);
    return RoleComponent;
}(hub_crud_component_list_1.HubCrudComponentList));
exports.RoleComponent = RoleComponent;
//# sourceMappingURL=role.component.js.map