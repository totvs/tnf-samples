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
var hub_entity_1 = require("./../utils/hub-entity");
var http_1 = require("@angular/common/http");
var core_1 = require("@angular/core");
var hub_crud_service_1 = require("../utils/hub-crud-service");
var TenantService = /** @class */ (function (_super) {
    __extends(TenantService, _super);
    /**
     * Construtor para inicializar a classe.
     * @param http Serviço HTTP que permite realizar as requisições para o servidor.
     */
    function TenantService(http) {
        var _this = _super.call(this, http, TenantData) || this;
        /** serviceName: define o nome do serviço para comunicação */
        _this.serviceName = 'tenant';
        /**
         * Atributo com as colunas apresentadas na tabela da lista.
         */
        _this.columns = [
            { column: 'tenantName', label: 'Nome do Tenant' },
            { column: 'name', label: 'Nome de Exibição' },
            // { column: 'connectionString', label: 'String de Conexão' },
            { column: 'isActive', label: 'Ativo', type: 'check' }
        ];
        return _this;
    }
    TenantService = __decorate([
        core_1.Injectable()
        /**
         * @description
         * Essa classe permite fazer a configuração do serviço REST
         * @extends HubCRUDService
         */
        ,
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], TenantService);
    return TenantService;
}(hub_crud_service_1.HubCRUDService));
exports.TenantService = TenantService;
/**
 * @description
 * A classe representa a entidade da tabela tenant e os atributos
 * são equivalentes aos campos da tabela
 * @extends HubEntity
 */
var TenantData = /** @class */ (function (_super) {
    __extends(TenantData, _super);
    /**
     * Método construtor para inicializar a classe tenants.
     * @param Objeto representando a tenants.
     */
    function TenantData(obj) {
        if (obj === void 0) { obj = { id: undefined, tenantName: '', name: '', isActive: true }; }
        var _this = _super.call(this) || this;
        /**
         * Nome do tenant
         */
        _this.tenantName = '';
        /**
         * Descrição do tenant
         */
        _this.name = '';
        /**
         * Conexão do tenant
         */
        // private connectionString: string = '';
        /**
         * Tenant está ativo
         */
        _this.isActive = false;
        _this.id = obj.id;
        _this.tenantName = obj.tenantName;
        _this.name = obj.name;
        // this.connectionString = obj.connectionString;
        _this.isActive = obj.isActive;
        return _this;
    }
    return TenantData;
}(hub_entity_1.HubEntity));
exports.TenantData = TenantData;
//# sourceMappingURL=tenant.service.js.map