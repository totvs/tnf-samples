"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var hub_api_1 = require("./hub-api");
var Subject_1 = require("rxjs/Subject");
var environment_1 = require("../../environments/environment");
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
        this.gotData = new Subject_1.Subject();
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
        return this.http.get(environment_1.environment.managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url, {
            params: new hub_api_1.HubApiQueryString({
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
        return this.http.get(environment_1.environment.managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url, {
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
        return this.http.post(environment_1.environment.managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url, body);
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
        return this.http.put(environment_1.environment.managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url, body);
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
        return this.http.delete(environment_1.environment.managementEndPoint + "/" + this.baseUrl + "/" + this.serviceName + url);
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
        this.getQuery(new hub_api_1.HubApiQueryString({
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
                    _this.gotData = new Subject_1.Subject();
                }
            }
            else {
                self.hasNext = false;
                self.items = [];
                _this.gotData.complete();
                _this.gotData = new Subject_1.Subject();
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
exports.HubCRUDService = HubCRUDService;
//# sourceMappingURL=hub-crud-service.js.map