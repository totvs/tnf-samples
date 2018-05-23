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
        core_1.Injectable()
        /**
         * @description
         * Essa classe permite fazer a configuração do serviço REST
         * @extends HubCRUDService
         */
        ,
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], UserService);
    return UserService;
}(hub_crud_service_1.HubCRUDService));
exports.UserService = UserService;
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
}(hub_entity_1.HubEntity));
exports.UserData = UserData;
//# sourceMappingURL=user.service.js.map