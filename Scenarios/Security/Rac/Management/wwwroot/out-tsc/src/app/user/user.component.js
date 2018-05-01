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
var user_service_1 = require("./user.service");
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
        var _this = _super.call(this, router, dataService, user_service_1.UserData) || this;
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
        core_1.ViewChild(thf_modal_component_1.ThfModalComponent),
        __metadata("design:type", thf_modal_component_1.ThfModalComponent)
    ], UserComponent.prototype, "thfAdvancedSearch", void 0);
    UserComponent = __decorate([
        core_1.Component({
            selector: 'app-user',
            templateUrl: './user.component.html',
            styleUrls: ['./user.component.css']
        }),
        __metadata("design:paramtypes", [router_1.Router, user_service_1.UserService])
    ], UserComponent);
    return UserComponent;
}(hub_crud_component_list_1.HubCrudComponentList));
exports.UserComponent = UserComponent;
//# sourceMappingURL=user.component.js.map