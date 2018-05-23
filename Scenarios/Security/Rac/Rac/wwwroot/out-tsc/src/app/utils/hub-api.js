"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
require("rxjs/add/operator/map");
var http_1 = require("@angular/common/http");
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
        var pars = new http_1.HttpParams();
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
exports.HubApiQueryString = HubApiQueryString;
var HubApiFilter = /** @class */ (function () {
    function HubApiFilter() {
    }
    return HubApiFilter;
}());
exports.HubApiFilter = HubApiFilter;
//# sourceMappingURL=hub-api.js.map