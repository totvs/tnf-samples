webpackJsonp(["common"],{

/***/ "../../../../../src/app/organizationUnit/organizationUnit.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return OrganizationService; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OrganizationData; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__utils_hub_entity__ = __webpack_require__("../../../../../src/app/utils/hub-entity.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common_http__ = __webpack_require__("../../../common/esm5/http.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__utils_hub_crud_service__ = __webpack_require__("../../../../../src/app/utils/hub-crud-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");
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
        return this.http.get(__WEBPACK_IMPORTED_MODULE_4__environments_environment__["a" /* environment */].managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url + "/bytenant/" + tenantId, {
            params: query.getQueryString()
        });
    };
    OrganizationService = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_2__angular_core__["B" /* Injectable */])()
        /**
         * @description
         * Essa classe permite fazer a configuração do serviço REST
         * @extends HubCRUDService
         */
        ,
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__angular_common_http__["b" /* HttpClient */]])
    ], OrganizationService);
    return OrganizationService;
}(__WEBPACK_IMPORTED_MODULE_3__utils_hub_crud_service__["a" /* HubCRUDService */]));

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
}(__WEBPACK_IMPORTED_MODULE_0__utils_hub_entity__["a" /* HubEntity */]));



/***/ }),

/***/ "../../../../../src/app/role/role.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return RoleService; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return RoleData; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__utils_hub_entity__ = __webpack_require__("../../../../../src/app/utils/hub-entity.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common_http__ = __webpack_require__("../../../common/esm5/http.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__utils_hub_crud_service__ = __webpack_require__("../../../../../src/app/utils/hub-crud-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");
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





var RoleService = /** @class */ (function (_super) {
    __extends(RoleService, _super);
    /**
     * Construtor para inicializar a classe.
     * @param http Serviço HTTP que permite realizar as requisições para o servidor.
     */
    function RoleService(http) {
        var _this = _super.call(this, http, RoleData) || this;
        /** serviceName: define o nome do serviço para comunicação */
        _this.serviceName = 'role';
        /**
         * Atributo com as colunas apresentadas na tabela da lista.
         */
        _this.columns = [
            { column: 'tenantName', label: 'Nome do Tenant' },
            { column: 'name', label: 'Nome da Role' },
            { column: 'displayName', label: 'Nome de Exibição' },
            { column: 'isDefault', label: 'Padrão', type: 'check' }
        ];
        return _this;
    }
    /**
     * Método que executa o get no servidor, passando parâmetros via query string, além dos path param.
     * @param query Parâmetros query string.
     * @param pathParam Parâmetros do pathParam.
     */
    RoleService.prototype.getFeaturesByTenant = function (tenantId) {
        return this.http.get("" + __WEBPACK_IMPORTED_MODULE_4__environments_environment__["a" /* environment */].authorizationEndPoint + this.baseUrl + "/features/bytenant/" + tenantId);
    };
    /**
       * Método que executa o get no servidor, passando parâmetros via query string, além dos path param.
       * @param query Parâmetros query string.
       * @param pathParam Parâmetros do pathParam.
       */
    RoleService.prototype.getQueryByTenant = function (query, tenantId) {
        var pathParam = [];
        for (var _i = 2; _i < arguments.length; _i++) {
            pathParam[_i - 2] = arguments[_i];
        }
        var url = this.urlParser.apply(this, pathParam);
        return this.http.get(__WEBPACK_IMPORTED_MODULE_4__environments_environment__["a" /* environment */].managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url + "/bytenant/" + tenantId, {
            params: query.getQueryString()
        });
    };
    RoleService = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_2__angular_core__["B" /* Injectable */])()
        /**
         * @description
         * Essa classe permite fazer a configuração do serviço REST
         * @extends HubCRUDService
         */
        ,
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__angular_common_http__["b" /* HttpClient */]])
    ], RoleService);
    return RoleService;
}(__WEBPACK_IMPORTED_MODULE_3__utils_hub_crud_service__["a" /* HubCRUDService */]));

/**
 * @description
 * A classe representa a entidade da tabela role e os atributos
 * são equivalentes aos campos da tabela
 * @extends HubEntity
 */
var RoleData = /** @class */ (function (_super) {
    __extends(RoleData, _super);
    /**
     * Método construtor para inicializar a classe roles.
     * @param Objeto representando a roles.
     */
    function RoleData(obj) {
        if (obj === void 0) { obj = { id: undefined, name: '', displayName: '', isDefault: true, permissions: [], tenantId: 0 }; }
        var _this = _super.call(this) || this;
        _this.tenantId = 0;
        _this.name = '';
        _this.displayName = '';
        _this.isDefault = false;
        _this.permissions = [];
        _this.id = obj.id;
        _this.tenantId = obj.tenantId;
        _this.name = obj.name;
        _this.displayName = obj.displayName;
        _this.isDefault = obj.isDefault;
        _this.permissions = obj.permissions ? obj.permissions.map(function (p) { return p.name || p; }) : [];
        return _this;
    }
    return RoleData;
}(__WEBPACK_IMPORTED_MODULE_0__utils_hub_entity__["a" /* HubEntity */]));



/***/ }),

/***/ "../../../../../src/app/tenant/tenant.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return TenantService; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return TenantData; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__utils_hub_entity__ = __webpack_require__("../../../../../src/app/utils/hub-entity.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common_http__ = __webpack_require__("../../../common/esm5/http.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__utils_hub_crud_service__ = __webpack_require__("../../../../../src/app/utils/hub-crud-service.ts");
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
        Object(__WEBPACK_IMPORTED_MODULE_2__angular_core__["B" /* Injectable */])()
        /**
         * @description
         * Essa classe permite fazer a configuração do serviço REST
         * @extends HubCRUDService
         */
        ,
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__angular_common_http__["b" /* HttpClient */]])
    ], TenantService);
    return TenantService;
}(__WEBPACK_IMPORTED_MODULE_3__utils_hub_crud_service__["a" /* HubCRUDService */]));

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
}(__WEBPACK_IMPORTED_MODULE_0__utils_hub_entity__["a" /* HubEntity */]));



/***/ }),

/***/ "../../../../../src/app/tenant/tenant.thflookup.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return TenantThfLookupService; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__tenant_service__ = __webpack_require__("../../../../../src/app/tenant/tenant.service.ts");
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



var TenantThfLookupService = /** @class */ (function () {
    function TenantThfLookupService(tenantService) {
        this.tenantService = tenantService;
    }
    TenantThfLookupService.prototype.getFilteredData = function (filter, page, pageSize) {
        return this.tenantService.getQuery(new __WEBPACK_IMPORTED_MODULE_2__utils_hub_api__["a" /* HubApiQueryString */]({
            page: page,
            pageSize: pageSize,
            filter: [{ key: "tenantName", value: filter }]
        }));
    };
    TenantThfLookupService.prototype.getObjectByValue = function (value) {
        return this.tenantService.get(value);
    };
    TenantThfLookupService = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["B" /* Injectable */])(),
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__tenant_service__["b" /* TenantService */]])
    ], TenantThfLookupService);
    return TenantThfLookupService;
}());



/***/ }),

/***/ "../../../../../src/app/utils/hub-api.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HubApiQueryString; });
/* unused harmony export HubApiFilter */
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_rxjs_add_operator_map__ = __webpack_require__("../../../../rxjs/_esm5/add/operator/map.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common_http__ = __webpack_require__("../../../common/esm5/http.js");


/**
 * @description HubApiQueryString
 * Esta classe define os métodos e atributos para consulta de dados
 */
var HubApiQueryString = /** @class */ (function () {
    /**
     * Método construtor que inicializa o objeto
     * @param entidade
     */
    function HubApiQueryString(_a) {
        var _b = _a === void 0 ? { page: 1, pageSize: 20, filter: undefined, expand: undefined } : _a, page = _b.page, pageSize = _b.pageSize, filter = _b.filter, expand = _b.expand;
        this.page = page;
        this.pageSize = pageSize;
        this.filter = filter;
        this.expand = expand;
    }
    /**
     * Retorna os parâmetros de consulta(page, pageSize, filter e expand) em formato string
     */
    HubApiQueryString.prototype.getQueryString = function () {
        var pars = new __WEBPACK_IMPORTED_MODULE_1__angular_common_http__["e" /* HttpParams */]();
        var cont;
        if (this.page) {
            cont = this.page.toString();
            if (cont) {
                pars = pars.set('page', cont);
            }
        }
        if (this.pageSize) {
            cont = this.pageSize.toString();
            if (cont) {
                pars = pars.set('pageSize', cont);
            }
        }
        if (this.expand) {
            cont = this.expand.join(',');
            if (cont) {
                pars = pars.set('expand', cont);
            }
        }
        if (this.filter) {
            for (var _i = 0, _a = this.filter; _i < _a.length; _i++) {
                var f = _a[_i];
                pars = pars.set(f.key, f.value.toString());
            }
        }
        return pars;
    };
    return HubApiQueryString;
}());

var HubApiFilter = /** @class */ (function () {
    function HubApiFilter() {
    }
    return HubApiFilter;
}());



/***/ }),

/***/ "../../../../../src/app/utils/hub-crud-component-edit.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HubCrudComponentForm; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_forms__ = __webpack_require__("../../../forms/esm5/forms.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__app_component__ = __webpack_require__("../../../../../src/app/app.component.ts");
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
 * Classe abstrata que implementa a interface IHubCrudComponentForm, implementando os métodos padrão.
 */
var HubCrudComponentForm = /** @class */ (function () {
    /**
     * Construtor para inicialização da classe.
     * @param activatedRoute Objeto para obter os parâmetros da URL.
     * @param router Parâmetro para navegação entre as rotas.
     * @param dataService Serviço de dados da entidade a ser manipulada.
     * @param Entity Entidade a ser manipulada.
     */
    function HubCrudComponentForm(activatedRoute, router, dataService, Entity) {
        this.activatedRoute = activatedRoute;
        this.router = router;
        this.dataService = dataService;
        this.Entity = Entity;
        /**
         * Atributo indica se o registro filho foi modificado.
         */
        this.childModified = false;
        /**
         * Atributo indica se irá fechar a tela ao salvar.
         */
        this.closeOnSave = true;
        this.isUpdate = false;
        /**
         * Evento emitido antes de editar;
         */
        this.beforeEditing = new __WEBPACK_IMPORTED_MODULE_1__angular_core__["w" /* EventEmitter */]();
        /**
         * Evento emitido antes de editar;
         */
        this.beforePersisting = new __WEBPACK_IMPORTED_MODULE_1__angular_core__["w" /* EventEmitter */]();
        /**
         * Comportamentos para formulário filho de outro
         */
        this.asGrandChild = false;
        this.data = new Entity(undefined);
    }
    /**
     * Método executado ao iniciar o componente, realizando os tratamentos iniciais, como leitura de parâmetros
     * da URL.
     */
    HubCrudComponentForm.prototype.ngOnInit = function () {
        var _this = this;
        this.activatedRoute.queryParams.subscribe(function (params) {
            if (params['_back']) {
                _this.lastUrl = params['_back'];
                _this.asGrandChild = true;
            }
        });
        this.activatedRoute.params.subscribe(function (params) {
            _this.paramId = undefined;
            if (params['id'] !== undefined) {
                _this.paramId = params['id'];
                _this.isUpdate = true;
            }
            _this.params = params;
            if (_this.paramId !== undefined) {
                _this.saveNew = undefined;
                _this.dataService.get(_this.paramId).subscribe(function (res) {
                    _this.data = new _this.Entity(res);
                    _this.beforeEditing.emit(_this.data);
                });
            }
            else
                _this.beforeEditing.emit(_this.data);
        });
    };
    /**
     * Método executado ao voltar.
     * @param saveNew Indica que a ação está em sendo executada ao utilizar o botão "Salvar e Novo".
     * @param cancel Indica que está sendo executado a partir do botão "Cancelar".
     */
    HubCrudComponentForm.prototype.back = function (saveNew, cancel) {
        if (saveNew === void 0) { saveNew = false; }
        if (cancel === void 0) { cancel = false; }
        this.childModified = false;
        if (saveNew === true) {
            this.data = new this.Entity(undefined);
            if (this.form) {
                this.form.resetForm(this.data);
            }
        }
        else {
            if (cancel || this.closeOnSave) {
                if (this.asGrandChild) {
                    this.router.navigate([this.lastUrl]);
                }
                else {
                    this.router.navigate([this.dataService.serviceName]);
                }
            }
            else {
                if (!this.asGrandChild) {
                    var rota = [this.dataService.serviceName + "/edit"].concat(this.paramId.toString());
                    this.router.navigate(rota);
                }
            }
        }
    };
    /**
     * Método retorna se o formulário teve algum dado alterado.
     */
    HubCrudComponentForm.prototype.isFormChanged = function () {
        var control;
        if (this.childModified) {
            return true;
        }
        if (this.form) {
            // tslint:disable-next-line:forin
            for (var x in this.form.form.controls) {
                control = this.form.form.controls[x];
                if (control.dirty) {
                    return true;
                }
            }
        }
        return false;
    };
    /**
     * Marca os campos como Pristine
     */
    HubCrudComponentForm.prototype.markFormPristine = function () {
        for (var x in this.form.form.controls) {
            if (this.form.form.controls) {
                this.form.form.controls[x].markAsPristine();
            }
        }
    };
    /**
     * Método retorna se o formulário é válido.
     */
    HubCrudComponentForm.prototype.isFormValid = function () {
        var control;
        var ret = true;
        if (this.form) {
            // tslint:disable-next-line:forin
            for (var x in this.form.form.controls) {
                control = this.form.form.controls[x];
                if (control.errors) {
                    control.markAsDirty({ onlySelf: true });
                    ret = false;
                }
            }
        }
        return ret;
    };
    /**
     * Método executado ao pressionar o botão "Cancelar".
     */
    HubCrudComponentForm.prototype.cancel = function () {
        var _this = this;
        if (this.isFormChanged() === false) {
            this.back(false, true);
        }
        else {
            __WEBPACK_IMPORTED_MODULE_2__app_component__["b" /* hubMessage */].openMessageQuestion('Deseja cancelar a edição do registro?', 'Edição').subscribe(function (confirm) {
                if (confirm) {
                    _this.back(false, true);
                }
            });
        }
    };
    /**
     * Método que retorna um post ou put de acordo com o método necessário para a ação atual do
     * formulário (inclusão ou edição).
     */
    HubCrudComponentForm.prototype.getMethod = function () {
        if (this.saveNew === undefined && this.data.id !== undefined) {
            return this.dataService.put(this.data, this.data.id);
        }
        else {
            return this.dataService.post(this.data);
        }
    };
    /**
     * Método que execução inclusão/edição do registro.
     * @param saveNew Parâmetro indica se a opção utilizada foi "Salvar" ou "Salvar e Novo".
     */
    HubCrudComponentForm.prototype.save = function (saveNew) {
        var _this = this;
        if (saveNew === void 0) { saveNew = false; }
        var self = this;
        if (this.isFormChanged() === false
            && this.saveNew === undefined
            && this.cancel !== undefined) {
            __WEBPACK_IMPORTED_MODULE_2__app_component__["b" /* hubMessage */].openMessageQuestion('Os dados não foram alterados. Continuar editando?', 'Edição').subscribe(function (confirm) {
                if (confirm === false) {
                    self.back(false, true);
                }
            });
        }
        else {
            if (this.isFormValid()) {
                this.beforePersisting.emit(this.data);
                this.getMethod()
                    .subscribe(function (success) {
                    if (success) {
                        if (!!success.messages && success.messages)
                            return;
                        __WEBPACK_IMPORTED_MODULE_2__app_component__["b" /* hubMessage */].openMessageInformation("Registro " + (_this.saveNew === undefined ? 'alterado' : 'incluído') + " com sucesso!");
                        if (_this.saveNew && success && success.id) {
                            _this.paramId = success.id;
                        }
                        _this.markFormPristine();
                        _this.back(saveNew);
                    }
                });
            }
        }
    };
    /**
     * Método executado ao pressionar o botão "Salvar e Novo".
     */
    HubCrudComponentForm.prototype.saveNew = function () {
        this.save(true);
    };
    /**
     * Método para verificar se o registro filho pode ser editado ou não.
     * Em caso positivo realiza a navegação para a rota apropriada para tal.
     * @param child Serviço de dados do filho.
     * @param childIdValue Array contendo a chave primária do registro filho.
     */
    HubCrudComponentForm.prototype.defaultGrandChildEditHandle = function (child) {
        var childIdValue = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            childIdValue[_i - 1] = arguments[_i];
        }
        if (this.childModified) {
            return __WEBPACK_IMPORTED_MODULE_2__app_component__["b" /* hubMessage */].openMessageInformation('Salve o registro antes de efetuar essa operação.');
        }
        var rota = [this.dataService.serviceName + "/edit/" + child.serviceName];
        childIdValue.forEach(function (el) {
            rota = rota.concat(el);
        });
        this.router.navigate(rota, { queryParams: { _back: this.router.url } });
    };
    __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_1__angular_core__["_10" /* ViewChild */])('form'),
        __metadata("design:type", __WEBPACK_IMPORTED_MODULE_0__angular_forms__["h" /* NgForm */])
    ], HubCrudComponentForm.prototype, "form", void 0);
    return HubCrudComponentForm;
}());



/***/ }),

/***/ "../../../../../src/app/utils/hub-crud-component-list.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HubCrudComponentList; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__("../../../forms/esm5/forms.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__app_component__ = __webpack_require__("../../../../../src/app/app.component.ts");
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
 * @description HubCrudComponentList
 * Classe abstrata para o componente de listagem de dados
 * @extends HubEntity
 */
var HubCrudComponentList = /** @class */ (function () {
    /**
     * Método construtor que inicializa o objeto
     * @param router
     * @param dataService
     * @param Entity
     */
    function HubCrudComponentList(router, dataService, Entity) {
        var _this = this;
        this.router = router;
        this.dataService = dataService;
        this.Entity = Entity;
        /**
         * thfListActions: atributo com as ações dos registros lista de dados
         */
        this.thfListActions = [
            { action: 'update', label: 'Editar' },
            { action: 'delete', label: 'Excluir' }
        ];
        /** thfFilter: atributo que define as ações de filtro */
        this.thfFilter = {
            placeholder: 'Busca',
            action: this.filterAction,
            ngModel: 'quickSearch',
        };
        this.thfAdvancedSearchName = "Filtrar Busca";
        /**
         * Ação primária do modal de busca avançada
         */
        this.advancedSearchPrimaryAction = {
            label: 'Confirmar',
            action: function () {
                _this.confirmAdvancedSearch();
            }
        };
        /**
         * Ação secundária do modal de busca avançada
         */
        this.advancedSearchSecondaryAction = {
            label: 'Cancelar',
            action: function () {
                _this.cancelAdvancedSearch();
            }
        };
    }
    /**
     * Método padrão do Angular executado ao inicializar o componente
     */
    HubCrudComponentList.prototype.ngOnInit = function () {
        if (this.filterInit) {
            this.filterAction(true);
        }
        else {
            this.dataService.page = 1;
            this.dataService.getItems();
        }
    };
    /**
     * Método que possui as ações de filtro de dados
     * @param isAdvancedSearch
     */
    HubCrudComponentList.prototype.filterAction = function (isAdvancedSearch) {
        if (isAdvancedSearch === void 0) { isAdvancedSearch = false; }
        this.dataService.page = 1;
        this.setFilters(isAdvancedSearch);
        this.dataService.getItems();
    };
    /**
     * método que abre o modal de pesquisa avançada
     */
    HubCrudComponentList.prototype.openAdvancedSearch = function () {
        this.thfAdvancedSearch.open();
    };
    /**
     * método que cancela e fecha o modal de pesquisa avançada
     */
    HubCrudComponentList.prototype.cancelAdvancedSearch = function () {
        this.thfAdvancedSearch.close();
    };
    /** método que confirma e fecha o modal de pesquisa avançada */
    HubCrudComponentList.prototype.confirmAdvancedSearch = function () {
        if (this.isFormValid()) {
            this.thfAdvancedSearch.close();
            this.filterAction(true);
        }
    };
    /**
     * método que trata o registro que será enviado para edição
     * @param item item/registro da lista
     */
    HubCrudComponentList.prototype.update = function (item) {
        if (item) {
            item = new this.Entity(item);
            this.updateRecord(item.id);
        }
    };
    /**
     * método que trata o registro que será enviado para exclusão
     * @param item item/registro da lista
     */
    HubCrudComponentList.prototype.delete = function (item) {
        if (item) {
            item = new this.Entity(item);
            this.deleteRecord(item.id);
        }
    };
    /**
     * método que valida o registro passado por parâmetro e faz a abertura da página de edição
     * @param id parâmetro que recebe o id do registro que será editado
     */
    HubCrudComponentList.prototype.updateRecord = function (id) {
        var rota = [this.dataService.serviceName + "/edit"].concat(id.toString());
        this.router.navigate(rota);
    };
    /**
     * método que valida o registro passado por parâmetro antes da exclusão
     * @param id parâmetro que recebe o id do registro que será excluído
     * @param confirmation indica se deve ser exibida a mensagem de confirmação
     */
    HubCrudComponentList.prototype.deleteRecord = function (id, confirmation) {
        if (confirmation === void 0) { confirmation = true; }
        var self = this;
        __WEBPACK_IMPORTED_MODULE_2__app_component__["b" /* hubMessage */].openMessageQuestion('Deseja realmente excluir o registro selecionado?', 'Exclusão')
            .subscribe(function (confirm) {
            if (confirm) {
                self.dataService.delete(id)
                    .subscribe(function (data) {
                    if (data && data.messages !== null) {
                        if (data.messages[0].code >= 400) {
                            __WEBPACK_IMPORTED_MODULE_2__app_component__["b" /* hubMessage */].openMessageError(data.messages[0].detail);
                            return;
                        }
                        __WEBPACK_IMPORTED_MODULE_2__app_component__["b" /* hubMessage */].openMessageInformation('Registro excluído com sucesso!');
                    }
                    self.dataService.page = 1;
                    self.dataService.getItems();
                });
            }
        });
    };
    /**
     * método que valida se o formulário está válido
     */
    HubCrudComponentList.prototype.isFormValid = function () {
        var control;
        var ret = true;
        // tslint:disable-next-line:forin
        for (var x in this.form.form.controls) {
            control = this.form.form.controls[x];
            if (control.errors) {
                control.markAsDirty({ onlySelf: true });
                ret = false;
            }
        }
        return ret;
    };
    __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* ViewChild */])('form'),
        __metadata("design:type", __WEBPACK_IMPORTED_MODULE_1__angular_forms__["h" /* NgForm */])
    ], HubCrudComponentList.prototype, "form", void 0);
    return HubCrudComponentList;
}());



/***/ }),

/***/ "../../../../../src/app/utils/hub-crud-service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HubCRUDService; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__hub_api__ = __webpack_require__("../../../../../src/app/utils/hub-api.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__ = __webpack_require__("../../../../rxjs/_esm5/Subject.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");



/**
 * @description
 * Classe abstrata que implementa os métodos padrão da interface IHubCRUDService, sendos os métodos específicos
 * implementados diretamente no serviço que extende da classe abstrata.
 */
var HubCRUDService = /** @class */ (function () {
    /**
     * Construtor da classe.
     * @param http Serviço HTTP para consumo de serviços REST.
     * @param Entity Objeto que aponta para o modelo de dados do serviço.
     */
    function HubCRUDService(http, Entity) {
        this.http = http;
        this.Entity = Entity;
        /**
         * Atributo da URL que será definido no serviço que extende a classe.
         */
        this.baseUrl = "api";
        /**
         * Atributo para controle de paginação, indicando qual a página a ser recuperada do servidor.
         */
        this.page = 1;
        /**
         * Atributo para controle de paginação, indicando o número de registros a ser recuperados do servidor.
         */
        this.pageSize = 20;
        /**
         * Atributo para controle da paginação, indicando se existe a próxima página.
         */
        this.hasNext = false;
        /**
         * Array dos elementos que serão mostrados na lista.
         */
        this.items = [];
        /**
         * Atributo que relaciona os filhos presentes em um array retornado do serviço REST.
         */
        this.expand = [];
        /**
         * Array utilizado para conversão do padrão que a interface utiliza e que o REST consome, representando os
         * valores qu serão removidos da "raiz" do objeto.
         */
        this.removeFromData = [];
        /**
         * Emite os dados recebidos pelo getItems
         */
        this.gotData = new __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__["a" /* Subject */]();
    }
    /**
     * Método que retorna as colunas da tabela, definido no serviço que extende a classe.
     */
    HubCRUDService.prototype.getColumns = function () {
        return this.columns;
    };
    /**
     * Método que adiciona valores ao atributo expand.
     * @param expand String a ser adicionada ao array.
     */
    HubCRUDService.prototype.addExpand = function (expand) {
        if (!this.expand.includes(expand))
            this.expand.push(expand);
    };
    /**
     * Método que adiciona valores ao atributo removeFromData.
     * @param remove Array de valores a serem concatenados ao atributo removeFromData.
     */
    HubCRUDService.prototype.setRemoveFromData = function () {
        var remove = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            remove[_i] = arguments[_i];
        }
        this.removeFromData = this.removeFromData.concat(remove);
    };
    /**
     * Método que adiciona os pathParam a URL, a partir dos valores globais e do array recebido.
     * @param p Array de valores a serem adicionados a URL.
     */
    HubCRUDService.prototype.urlParser = function () {
        var p = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            p[_i] = arguments[_i];
        }
        var url = '/';
        p.forEach(function (param) {
            if (param) {
                url += param + "/";
            }
        });
        return url.substr(0, url.length - 1);
    };
    /**
     * Método que converte os objetos do padrão retornado pelo REST para o padrão da interface.
     * @param dataIn Objeto de entrada.
     * @param outData Dados de retorno.
     * @param ignore Dados a serem ignorados.
     */
    HubCRUDService.prototype.fromRest = function (dataIn, outData, ignore) {
        if (ignore === void 0) { ignore = ''; }
        outData = outData || {};
        for (var _i = 0, _a = Object.keys(dataIn); _i < _a.length; _i++) {
            var prop = _a[_i];
            if (!ignore || prop.search(ignore) === -1) {
                outData[prop] = dataIn[prop];
            }
        }
        return outData;
    };
    /**
     * Método recebe um array representando os path param, para realizar um get para o servidor.
     */
    HubCRUDService.prototype.get = function () {
        var _this = this;
        var pathParam = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            pathParam[_i] = arguments[_i];
        }
        if (pathParam.length === 0) {
            throw new Error('HubCRUDService: pathParam obrigatório. Para retornar mais de um registro, utilize getQuery/getItems');
        }
        var url = this.urlParser.apply(this, pathParam);
        return this.http.get(__WEBPACK_IMPORTED_MODULE_2__environments_environment__["a" /* environment */].managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url, {
            params: new __WEBPACK_IMPORTED_MODULE_0__hub_api__["a" /* HubApiQueryString */]({
                page: undefined,
                pageSize: undefined,
                expand: this.expand
            }).getQueryString()
        })
            .map(function (res) {
            var data = _this.fromRest(res); // length e messages
            return data;
        });
    };
    /**
     * Método que executa o get no servidor, passando parâmetros via query string, além dos path param.
     * @param query Parâmetros query string.
     * @param pathParam Parâmetros do pathParam.
     */
    HubCRUDService.prototype.getQuery = function (query) {
        var pathParam = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            pathParam[_i - 1] = arguments[_i];
        }
        var url = this.urlParser.apply(this, pathParam);
        return this.http.get(__WEBPACK_IMPORTED_MODULE_2__environments_environment__["a" /* environment */].managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url, {
            params: query.getQueryString()
        });
    };
    /**
     * Método que recebe o corpo da requisição, além dos path param que irão na URL para reailizar um post no
     * servidor.
     * @param body Objeto com o corpo da requisição.
     * @param pathParam Parâmetros de path, passados para o serviço.
     */
    HubCRUDService.prototype.post = function (body) {
        var pathParam = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            pathParam[_i - 1] = arguments[_i];
        }
        var url = this.urlParser.apply(this, pathParam);
        return this.http.post(__WEBPACK_IMPORTED_MODULE_2__environments_environment__["a" /* environment */].managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url, body);
    };
    /**
     * Método que recebe o corpo da requisição, além dos path param que irão na URL para reailizar um put no
     * servidor.
     * @param body Objeto com o corpo da requisição.
     * @param pathParam Parâmetros de path, passados para o serviço.
     */
    HubCRUDService.prototype.put = function (body) {
        var pathParam = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            pathParam[_i - 1] = arguments[_i];
        }
        var url = this.urlParser.apply(this, pathParam);
        return this.http.put(__WEBPACK_IMPORTED_MODULE_2__environments_environment__["a" /* environment */].managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url, body);
    };
    /**
     * Método recebe um array representando os path param, para realizar um delete para o servidor.
     */
    HubCRUDService.prototype.delete = function () {
        var pathParam = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            pathParam[_i] = arguments[_i];
        }
        var url = this.urlParser.apply(this, pathParam);
        return this.http.delete(__WEBPACK_IMPORTED_MODULE_2__environments_environment__["a" /* environment */].managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url);
    };
    /**
     * Método para receber os dados do servidor, verificar as mensagens e setar o atributo items.
     * @param more Parâmetro que indica se o get é para buscar a próxima página.
     * @param cast Parâmetro que indica se será executada a conversão do objeto para o modelo de dados.
     */
    HubCRUDService.prototype.getItems = function (more, cast) {
        var _this = this;
        if (more === void 0) { more = false; }
        if (cast === void 0) { cast = false; }
        var self = this;
        this.getQuery(new __WEBPACK_IMPORTED_MODULE_0__hub_api__["a" /* HubApiQueryString */]({
            page: this.page,
            pageSize: this.pageSize,
            filter: this.filter
        })).subscribe(function (success) {
            if (success !== undefined && success !== null) {
                self.hasNext = success.hasNext || success.hasNext;
                if (cast && self.Entity) {
                    for (var i_1 = 0; i_1 < success.items.length; i_1++) {
                        success.items[i_1] = new _this.Entity(success.items[i_1]);
                    }
                    ;
                }
                if (more === true) {
                    self.items = self.items.concat(success.items);
                }
                else {
                    self.items = success.items;
                }
                var checkColumns = self.getColumns().reduce(function (arr, c) {
                    if (c.type === "check")
                        arr.push(c.column);
                    return arr;
                }, []);
                for (var itemIndex in self.items) {
                    var item = self.items[itemIndex];
                    for (var i in checkColumns) {
                        var col = checkColumns[i];
                        item[col] = item[col] ? 'Sim' : 'Não';
                    }
                }
                _this.gotData.next(success.items);
                if (!self.hasNext) {
                    _this.gotData.complete();
                    _this.gotData = new __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__["a" /* Subject */]();
                }
            }
            else {
                self.hasNext = false;
                self.items = [];
                _this.gotData.complete();
                _this.gotData = new __WEBPACK_IMPORTED_MODULE_1_rxjs_Subject__["a" /* Subject */]();
            }
        });
    };
    /**
     * Método para buscar a próxima página.
     * @param page Página que será retornada
     * @param cast Indicação de conversão do objeto para o modelo de dados.
     */
    HubCRUDService.prototype.more = function (page, cast) {
        if (page === void 0) { page = this.page + 1; }
        if (cast === void 0) { cast = false; }
        if (page > this.page) {
            this.page = page;
        }
        this.getItems(true, cast);
    };
    return HubCRUDService;
}());



/***/ }),

/***/ "../../../../../src/app/utils/hub-entity.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HubEntity; });
/**
 * @description
 * Classe abstrata que implementa os métodos da interface IHubEntity
 */
var HubEntity = /** @class */ (function () {
    function HubEntity() {
    }
    return HubEntity;
}());



/***/ })

});
//# sourceMappingURL=common.chunk.js.map