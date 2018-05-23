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
var environment_1 = require("../../environments/environment");
var OrganizationService = /** @class */ (function (_super) {
    __extends(OrganizationService, _super);
    /**
     * Construtor para inicializar a classe.
     * @param http Serviço HTTP que permite realizar as requisições para o servidor.
     */
    function OrganizationService(http) {
        var _this = _super.call(this, http, OrganizationData) || this;
        /** serviceName: define o nome do serviço para comunicação */
        _this.serviceName = 'organizationUnit';
        /**
         * Atributo com as colunas apresentadas na tabela da lista.
         */
        _this.columns = [
            { column: 'tenantName', label: 'Nome do Tenant' },
            { column: 'name', label: 'Nome da Organização' },
            { column: 'displayName', label: 'Nome de Exibição' }
        ];
        return _this;
    }
    /**
       * Método que executa o get no servidor, passando parâmetros via query string, além dos path param.
       * @param query Parâmetros query string.
       * @param pathParam Parâmetros do pathParam.
       */
    OrganizationService.prototype.getQueryByTenant = function (query, tenantId) {
        var pathParam = [];
        for (var _i = 2; _i < arguments.length; _i++) {
            pathParam[_i - 2] = arguments[_i];
        }
        var url = this.urlParser.apply(this, pathParam);
        return this.http.get(environment_1.environment.managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url + "/bytenant/" + tenantId, {
            params: query.getQueryString()
        });
    };
    OrganizationService = __decorate([
        core_1.Injectable()
        /**
         * @description
         * Essa classe permite fazer a configuração do serviço REST
         * @extends HubCRUDService
         */
        ,
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], OrganizationService);
    return OrganizationService;
}(hub_crud_service_1.HubCRUDService));
exports.OrganizationService = OrganizationService;
/**
 * @description
 * A classe representa a entidade da tabela organization e os atributos
 * são equivalentes aos campos da tabela
 * @extends HubEntity
 */
var OrganizationData = /** @class */ (function (_super) {
    __extends(OrganizationData, _super);
    /**
     * Método construtor para inicializar a classe organizations.
     * @param Objeto representando a organizations.
     */
    function OrganizationData(obj) {
        if (obj === void 0) { obj = { id: undefined, name: '', displayName: '', parentId: 0, tenantId: 0 }; }
        var _this = _super.call(this) || this;
        _this.tenantId = 0;
        _this.name = '';
        _this.displayName = '';
        _this.parentId = 0;
        _this.id = obj.id;
        _this.tenantId = obj.tenantId;
        _this.name = obj.name;
        _this.displayName = obj.displayName;
        _this.parentId = obj.parentId;
        return _this;
    }
    return OrganizationData;
}(hub_entity_1.HubEntity));
exports.OrganizationData = OrganizationData;
//# sourceMappingURL=organizationUnit.service.js.map