webpackJsonp(["main"],{

/***/ "../../../../../src/$$_lazy_route_resource lazy recursive":
/***/ (function(module, exports, __webpack_require__) {

var map = {
	"app/organizationUnit/organizationUnit.module": [
		"../../../../../src/app/organizationUnit/organizationUnit.module.ts",
		"common",
		"organizationUnit.module"
	],
	"app/role/role.module": [
		"../../../../../src/app/role/role.module.ts",
		"role.module",
		"common"
	],
	"app/tenant/tenant.module": [
		"../../../../../src/app/tenant/tenant.module.ts",
		"common",
		"tenant.module"
	],
	"app/user/user.module": [
		"../../../../../src/app/user/user.module.ts",
		"user.module",
		"common"
	]
};
function webpackAsyncContext(req) {
	var ids = map[req];
	if(!ids)
		return Promise.reject(new Error("Cannot find module '" + req + "'."));
	return Promise.all(ids.slice(1).map(__webpack_require__.e)).then(function() {
		return __webpack_require__(ids[0]);
	});
};
webpackAsyncContext.keys = function webpackAsyncContextKeys() {
	return Object.keys(map);
};
webpackAsyncContext.id = "../../../../../src/$$_lazy_route_resource lazy recursive";
module.exports = webpackAsyncContext;

/***/ }),

/***/ "../../../../../src/app/app-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppRoutingModule; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/esm5/router.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__auth_auth_guard__ = __webpack_require__("../../../../../src/app/auth/auth.guard.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__auth_callback_auth_callback_component__ = __webpack_require__("../../../../../src/app/auth/callback/auth-callback.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__home_home_component__ = __webpack_require__("../../../../../src/app/home/home.component.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};





var routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
        canActivate: [__WEBPACK_IMPORTED_MODULE_2__auth_auth_guard__["a" /* AuthGuard */]]
    },
    {
        path: 'home',
        component: __WEBPACK_IMPORTED_MODULE_4__home_home_component__["a" /* HomeComponent */],
        canActivate: [__WEBPACK_IMPORTED_MODULE_2__auth_auth_guard__["a" /* AuthGuard */]]
    },
    {
        path: 'tenant',
        loadChildren: 'app/tenant/tenant.module#TenantModule',
        canActivate: [__WEBPACK_IMPORTED_MODULE_2__auth_auth_guard__["a" /* AuthGuard */]]
    },
    {
        path: 'organizationUnit',
        loadChildren: 'app/organizationUnit/organizationUnit.module#OrganizationModule',
        canActivate: [__WEBPACK_IMPORTED_MODULE_2__auth_auth_guard__["a" /* AuthGuard */]]
    },
    {
        path: 'user',
        loadChildren: 'app/user/user.module#UserModule',
        canActivate: [__WEBPACK_IMPORTED_MODULE_2__auth_auth_guard__["a" /* AuthGuard */]]
    },
    {
        path: 'role',
        loadChildren: 'app/role/role.module#RoleModule',
        canActivate: [__WEBPACK_IMPORTED_MODULE_2__auth_auth_guard__["a" /* AuthGuard */]]
    },
    {
        path: 'auth-callback',
        component: __WEBPACK_IMPORTED_MODULE_3__auth_callback_auth_callback_component__["a" /* AuthCallbackComponent */]
    }
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["J" /* NgModule */])({
            imports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["e" /* RouterModule */].forRoot(routes)],
            exports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["e" /* RouterModule */]]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());



/***/ }),

/***/ "../../../../../src/app/app.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".spin-loader {\r\n  position: absolute;\r\n  left: 50%;\r\n  top: 50%;\r\n  z-index: 1;\r\n  width: 150px;\r\n  height: 150px;\r\n  margin: -75px 0 0 -75px;\r\n  border: 16px solid #f3f3f3;\r\n  border-radius: 50%;\r\n  border-top: 16px solid #3498db;\r\n  width: 120px;\r\n  height: 120px;\r\n  -webkit-animation: spin 2s linear infinite;\r\n  animation: spin 2s linear infinite;\r\n}\r\n  \r\n  @-webkit-keyframes spin {\r\n    0% { -webkit-transform: rotate(0deg); }\r\n    100% { -webkit-transform: rotate(360deg); }\r\n  }\r\n  \r\n  @keyframes spin {\r\n    0% { -webkit-transform: rotate(0deg); transform: rotate(0deg); }\r\n    100% { -webkit-transform: rotate(360deg); transform: rotate(360deg); }\r\n  }\r\n  \r\n  .button-div-voltar {\r\n  position: absolute;\r\n  right: 5px;\r\n  bottom: 5px;\r\n}", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/app.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"thf-wrapper\" [hidden]=\"!isLoggedIn\">\r\n  <thf-toolbar \r\n    t-title=\"Gerenciamento do RAC\" \r\n    t-user-src=\"../assets/images/totvs-avatar-default.svg\"\r\n    [t-user-name]=\"user && user.profile ? user.profile.name : null\"\r\n    [t-user-actions]=\"userActions\"\r\n    [t-show-notification]=\"false\">\r\n  </thf-toolbar>\r\n  \r\n  <thf-menu [t-menus]=\"thfMenus\"></thf-menu>\r\n  \r\n  <router-outlet></router-outlet>\r\n  <app-hub-message></app-hub-message>\r\n\r\n  <div class=\"button-div-voltar\" *ngIf=\"showComponent\">\r\n    <thf-button\r\n      *ngIf=\"!!redictUrl\"\r\n      t-label=\"Voltar\"\r\n      t-primary=\"true\"\r\n      (t-click)=\"redirectUrl()\">\r\n    </thf-button>\r\n  </div>\r\n</div>\r\n<div class=\"spin-loader\" [hidden]=\"isLoggedIn\"></div>"

/***/ }),

/***/ "../../../../../src/app/app.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppComponent; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return hubMessage; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__auth_auth_service__ = __webpack_require__("../../../../../src/app/auth/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__auth_auth_user_service__ = __webpack_require__("../../../../../src/app/auth/auth.user.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__utils_hub_message_hub_message_component__ = __webpack_require__("../../../../../src/app/utils/hub-message/hub-message.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_router__ = __webpack_require__("../../../router/esm5/router.js");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};





var AppComponent = /** @class */ (function () {
    function AppComponent(authService, authUserService, router) {
        var _this = this;
        this.authService = authService;
        this.authUserService = authUserService;
        this.router = router;
        this.showComponent = true;
        this.redictUrl = localStorage['redirect_url'];
        this.isLoggedIn = false;
        this.title = '';
        this.menus = [
            { label: 'Tenant', link: 'tenant', permission: 'Management.Tenant', action: '' },
            { label: 'Organização', link: 'organizationUnit', permission: 'Management.Organization', action: '' },
            { label: 'Roles', link: 'role', permission: 'Management.Role', action: '' },
            { label: 'Usuários', link: 'user', permission: 'Management.User', action: '' }
        ];
        this.thfMenus = [
            { label: 'Inicio', link: 'home', permission: '', action: '' }
        ];
        this.userActions = [
            { label: 'Sair', action: 'logout' }
        ];
        router.events.forEach(function (event) {
            if (event instanceof __WEBPACK_IMPORTED_MODULE_4__angular_router__["c" /* NavigationStart */]) {
                _this.showComponent = event.url === "" || event.url === "/" || event.url.startsWith("/auth-callback");
            }
        });
    }
    AppComponent.prototype.ngOnInit = function () {
        var _this = this;
        hubMessage = this.messages;
        this.isLoggedIn = false;
        this.authService.onCompleteAuthentication().subscribe(function (user) {
            _this.user = user;
            if (_this.user) {
                _this.authUserService.getUserAuthorizedMenus(_this.menus).subscribe(function (authorizedMenus) {
                    _this.isLoggedIn = true;
                    _this.thfMenus = _this.thfMenus.concat(authorizedMenus);
                });
            }
        });
        this.authService.onAccessTokenExpired().subscribe(function (expired) {
            hubMessage.openMessageQuestion('Sua sessão expirou. Deseja realizar o login novamente?', "Sessão expirada")
                .subscribe(function (confirm) {
                if (confirm)
                    _this.authService.signoutRedirect();
            });
        });
    };
    AppComponent.prototype.redirectUrl = function () {
        window.location.href = decodeURIComponent(this.redictUrl);
    };
    AppComponent.prototype.logout = function () {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.authService.logout()];
                    case 1:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* ViewChild */])(__WEBPACK_IMPORTED_MODULE_3__utils_hub_message_hub_message_component__["a" /* HubMessageComponent */]),
        __metadata("design:type", __WEBPACK_IMPORTED_MODULE_3__utils_hub_message_hub_message_component__["a" /* HubMessageComponent */])
    ], AppComponent.prototype, "messages", void 0);
    AppComponent = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["n" /* Component */])({
            selector: 'app-root',
            template: __webpack_require__("../../../../../src/app/app.component.html"),
            styles: [__webpack_require__("../../../../../src/app/app.component.css")]
        }),
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__auth_auth_service__["a" /* AuthService */], __WEBPACK_IMPORTED_MODULE_2__auth_auth_user_service__["a" /* AuthUserService */], __WEBPACK_IMPORTED_MODULE_4__angular_router__["d" /* Router */]])
    ], AppComponent);
    return AppComponent;
}());

/** hubMessage: Referência ao componente de mensagem
 *
 */
var hubMessage;


/***/ }),

/***/ "../../../../../src/app/app.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppModule; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_common_http__ = __webpack_require__("../../../common/esm5/http.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_common__ = __webpack_require__("../../../common/esm5/common.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_forms__ = __webpack_require__("../../../forms/esm5/forms.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_platform_browser__ = __webpack_require__("../../../platform-browser/esm5/platform-browser.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_router__ = __webpack_require__("../../../router/esm5/router.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__totvs_thf_ui__ = __webpack_require__("../../../../@totvs/thf-ui/index.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__app_component__ = __webpack_require__("../../../../../src/app/app.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__app_routing_module__ = __webpack_require__("../../../../../src/app/app-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__auth_token_interceptor__ = __webpack_require__("../../../../../src/app/auth/token.interceptor.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__auth_auth_service__ = __webpack_require__("../../../../../src/app/auth/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__auth_callback_auth_callback_component__ = __webpack_require__("../../../../../src/app/auth/callback/auth-callback.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__auth_auth_guard__ = __webpack_require__("../../../../../src/app/auth/auth.guard.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__auth_auth_user_service__ = __webpack_require__("../../../../../src/app/auth/auth.user.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_14__utils_hub_message_hub_message_component__ = __webpack_require__("../../../../../src/app/utils/hub-message/hub-message.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_15__home_home_component__ = __webpack_require__("../../../../../src/app/home/home.component.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
















var AppModule = /** @class */ (function () {
    function AppModule(activatedRoute) {
        this.activatedRoute = activatedRoute;
        var dictionary = {};
        window.location.search.substr(1).split("&").forEach(function (p) {
            var split = p.split("=");
            dictionary[split[0]] = decodeURIComponent(split[1]);
        });
        if (dictionary["redirect_url"])
            localStorage.setItem("redirect_url", dictionary["redirect_url"]);
    }
    AppModule = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_1__angular_core__["J" /* NgModule */])({
            declarations: [
                __WEBPACK_IMPORTED_MODULE_7__app_component__["a" /* AppComponent */],
                __WEBPACK_IMPORTED_MODULE_15__home_home_component__["a" /* HomeComponent */],
                __WEBPACK_IMPORTED_MODULE_11__auth_callback_auth_callback_component__["a" /* AuthCallbackComponent */],
                __WEBPACK_IMPORTED_MODULE_14__utils_hub_message_hub_message_component__["a" /* HubMessageComponent */]
            ],
            imports: [
                __WEBPACK_IMPORTED_MODULE_2__angular_common__["b" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_forms__["d" /* FormsModule */],
                __WEBPACK_IMPORTED_MODULE_4__angular_platform_browser__["a" /* BrowserModule */],
                __WEBPACK_IMPORTED_MODULE_8__app_routing_module__["a" /* AppRoutingModule */],
                __WEBPACK_IMPORTED_MODULE_6__totvs_thf_ui__["a" /* ThfModule */],
                __WEBPACK_IMPORTED_MODULE_0__angular_common_http__["c" /* HttpClientModule */],
                __WEBPACK_IMPORTED_MODULE_5__angular_router__["e" /* RouterModule */]
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_14__utils_hub_message_hub_message_component__["a" /* HubMessageComponent */]
            ],
            providers: [
                __WEBPACK_IMPORTED_MODULE_12__auth_auth_guard__["a" /* AuthGuard */],
                __WEBPACK_IMPORTED_MODULE_10__auth_auth_service__["a" /* AuthService */],
                __WEBPACK_IMPORTED_MODULE_13__auth_auth_user_service__["a" /* AuthUserService */],
                {
                    provide: __WEBPACK_IMPORTED_MODULE_0__angular_common_http__["a" /* HTTP_INTERCEPTORS */],
                    useClass: __WEBPACK_IMPORTED_MODULE_9__auth_token_interceptor__["a" /* TokenInterceptor */],
                    multi: true
                }
            ],
            bootstrap: [__WEBPACK_IMPORTED_MODULE_7__app_component__["a" /* AppComponent */]]
        }),
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_5__angular_router__["a" /* ActivatedRoute */]])
    ], AppModule);
    return AppModule;
}());



/***/ }),

/***/ "../../../../../src/app/auth/auth.guard.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AuthGuard; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__auth_auth_service__ = __webpack_require__("../../../../../src/app/auth/auth.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var AuthGuard = /** @class */ (function () {
    function AuthGuard(authService) {
        this.authService = authService;
    }
    AuthGuard.prototype.canActivate = function () {
        if (this.authService.isLoggedIn())
            return true;
        this.authService.startAuthentication();
        return false;
    };
    AuthGuard = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["B" /* Injectable */])(),
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__auth_auth_service__["a" /* AuthService */]])
    ], AuthGuard);
    return AuthGuard;
}());



/***/ }),

/***/ "../../../../../src/app/auth/auth.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AuthService; });
/* unused harmony export getClientSettings */
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_oidc_client__ = __webpack_require__("../../../../oidc-client/lib/oidc-client.min.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_oidc_client___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_oidc_client__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__events_event__ = __webpack_require__("../../../../../src/app/events/event.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};




var AuthService = /** @class */ (function () {
    function AuthService() {
        var _this = this;
        this.manager = new __WEBPACK_IMPORTED_MODULE_1_oidc_client__["UserManager"](getClientSettings());
        this.user = null;
        this.internalOnCompleteAuthentication = new __WEBPACK_IMPORTED_MODULE_3__events_event__["a" /* EventDispatcher */]();
        this.internalonAccessTokenExpired = new __WEBPACK_IMPORTED_MODULE_3__events_event__["a" /* EventDispatcher */]();
        this.onCompleteAuthentication = function () { return _this.internalOnCompleteAuthentication; };
        this.onAccessTokenExpired = function () { return _this.internalonAccessTokenExpired; };
        this.isLoggedIn = function () { return !!_this.user && !_this.user.expired; };
        this.manager.events.addAccessTokenExpired(function (e) {
            _this.internalonAccessTokenExpired.dispatch(true);
        });
        // Get user after callback
        this.getUser().then(function (user) {
            _this.user = user;
            if (_this.user) {
                if (_this.user.expired)
                    _this.manager.signinRedirectCallback();
                else
                    _this.internalOnCompleteAuthentication.dispatch(user);
            }
        });
    }
    AuthService.prototype.getAuthorizationHeaderValue = function () {
        return this.user ? this.user.token_type + " " + this.user.access_token : '';
    };
    AuthService.prototype.signinSilentCallback = function () {
        this.manager.signinSilentCallback();
    };
    AuthService.prototype.signoutRedirect = function () {
        this.manager.signoutRedirect();
    };
    AuthService.prototype.isHostUser = function () {
        if (this.user == null || this.user == undefined)
            return false;
        return (this.user.profile.email == 'admin@tnf-rac.com.br');
    };
    AuthService.prototype.getUserTokenType = function () {
        return this.user.token_type;
    };
    AuthService.prototype.getUserAccessToken = function () {
        return this.user.access_token;
    };
    AuthService.prototype.getUser = function () {
        return this.manager.getUser();
    };
    AuthService.prototype.startAuthentication = function () {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.manager.signinRedirect()];
                    case 1:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    AuthService.prototype.logout = function () {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.manager.signoutRedirect()];
                    case 1:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    AuthService.prototype.completeAuthentication = function () {
        return __awaiter(this, void 0, void 0, function () {
            var _a;
            return __generator(this, function (_b) {
                switch (_b.label) {
                    case 0:
                        if (!!this.user) return [3 /*break*/, 2];
                        _a = this;
                        return [4 /*yield*/, this.manager.signinRedirectCallback()];
                    case 1:
                        _a.user = _b.sent();
                        _b.label = 2;
                    case 2:
                        if (this.user)
                            this.internalOnCompleteAuthentication.dispatch(this.user);
                        return [2 /*return*/];
                }
            });
        });
    };
    AuthService = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["B" /* Injectable */])(),
        __metadata("design:paramtypes", [])
    ], AuthService);
    return AuthService;
}());

function getClientSettings() {
    var CurrentUrl = window.location.protocol + "//" + window.location.host + "/";
    return {
        authority: __WEBPACK_IMPORTED_MODULE_2__environments_environment__["a" /* environment */].authorityEndPoint,
        client_id: 'rac_oidc',
        redirect_uri: CurrentUrl + "auth-callback",
        post_logout_redirect_uri: CurrentUrl,
        // these two will be done dynamically from the buttons clicked, but are
        // needed if you want to use the silent_renew
        response_type: "code id_token token",
        scope: "openid profile email authorization_api management_api offline_access",
        // this will toggle if profile endpoint is used
        loadUserInfo: true,
        // silent renew will get a new access_token via an iframe 
        // just prior to the old access_token expiring (60 seconds prior)
        silent_redirect_uri: CurrentUrl + "assets/silent-renew.html",
        automaticSilentRenew: true,
        // will revoke (reference) access tokens at logout time
        revokeAccessTokenOnSignout: true,
        // will raise events for when user has performed a logout at IdentityServer
        monitorSession: true,
        // this will allow all the OIDC protocol claims to be visible in the window. normally a client app 
        // wouldn't care about them or want them taking up space
        filterProtocolClaims: false,
        userStore: new __WEBPACK_IMPORTED_MODULE_1_oidc_client__["WebStorageStateStore"]({ store: localStorage })
    };
}


/***/ }),

/***/ "../../../../../src/app/auth/auth.user.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AuthUserService; });
/* unused harmony export MenuItem */
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common_http__ = __webpack_require__("../../../common/esm5/http.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__auth_service__ = __webpack_require__("../../../../../src/app/auth/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__events_event__ = __webpack_require__("../../../../../src/app/events/event.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};





var AuthUserService = /** @class */ (function () {
    function AuthUserService(http, authService) {
        this.http = http;
        this.authService = authService;
    }
    AuthUserService.prototype.getUserAuthorizedMenus = function (menus) {
        var event = new __WEBPACK_IMPORTED_MODULE_4__events_event__["a" /* EventDispatcher */]();
        var permissions = menus.map(function (m) { return m.permission; });
        this.getUserPermissionsGranted(permissions).then(function (data) {
            var authorizedMenus = [];
            for (var index = 0; index < menus.length; index++) {
                if (data[index] || !menus[index].permission)
                    authorizedMenus.push(menus[index]);
            }
            event.dispatch(authorizedMenus);
        });
        return event;
    };
    AuthUserService.prototype.getUserPermissions = function () {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.http.get(__WEBPACK_IMPORTED_MODULE_3__environments_environment__["a" /* environment */].authorizationEndPoint + "api/Permissions").toPromise()];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    };
    AuthUserService.prototype.getUserPermissionsGranted = function (permissions) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.http.post(__WEBPACK_IMPORTED_MODULE_3__environments_environment__["a" /* environment */].authorizationEndPoint + "api/Permissions", permissions).toPromise()];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    };
    AuthUserService.prototype.getFeatures = function () {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.http.get(__WEBPACK_IMPORTED_MODULE_3__environments_environment__["a" /* environment */].authorizationEndPoint + "api/Features").toPromise()];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    };
    AuthUserService = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["B" /* Injectable */])(),
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__angular_common_http__["b" /* HttpClient */], __WEBPACK_IMPORTED_MODULE_2__auth_service__["a" /* AuthService */]])
    ], AuthUserService);
    return AuthUserService;
}());

var MenuItem = /** @class */ (function () {
    function MenuItem() {
    }
    return MenuItem;
}());



/***/ }),

/***/ "../../../../../src/app/auth/callback/auth-callback.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AuthCallbackComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__auth_service__ = __webpack_require__("../../../../../src/app/auth/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_router__ = __webpack_require__("../../../router/esm5/router.js");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var AuthCallbackComponent = /** @class */ (function () {
    function AuthCallbackComponent(authService, router) {
        this.authService = authService;
        this.router = router;
    }
    AuthCallbackComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.authService.onCompleteAuthentication()
            .subscribe(function () {
            _this.router.navigate(['/home']);
        });
        this.authService.completeAuthentication();
    };
    AuthCallbackComponent = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["n" /* Component */])({
            selector: 'app-auth-callback',
            template: ''
        }),
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__auth_service__["a" /* AuthService */], __WEBPACK_IMPORTED_MODULE_2__angular_router__["d" /* Router */]])
    ], AuthCallbackComponent);
    return AuthCallbackComponent;
}());



/***/ }),

/***/ "../../../../../src/app/auth/token.interceptor.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return TokenInterceptor; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__auth_service__ = __webpack_require__("../../../../../src/app/auth/auth.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var TokenInterceptor = /** @class */ (function () {
    function TokenInterceptor(auth) {
        this.auth = auth;
    }
    TokenInterceptor.prototype.intercept = function (request, next) {
        request = request.clone({
            setHeaders: {
                'Authorization': this.auth.getAuthorizationHeaderValue()
            }
        });
        return next.handle(request);
    };
    TokenInterceptor = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["B" /* Injectable */])(),
        __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1__auth_service__["a" /* AuthService */]])
    ], TokenInterceptor);
    return TokenInterceptor;
}());



/***/ }),

/***/ "../../../../../src/app/events/event.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return EventDispatcher; });
var EventDispatcher = /** @class */ (function () {
    function EventDispatcher() {
        this._subscriptions = new Array();
        this.dispatched = false;
        this.dispatchArgs = undefined;
    }
    EventDispatcher.prototype.subscribe = function (fn) {
        if (fn) {
            if (this.dispatched)
                fn(this.dispatchArgs);
            else
                this._subscriptions.push(fn);
        }
    };
    EventDispatcher.prototype.unsubscribe = function (fn) {
        var i = this._subscriptions.indexOf(fn);
        if (i > -1) {
            this._subscriptions.splice(i, 1);
        }
    };
    EventDispatcher.prototype.dispatch = function (args) {
        if (!this.dispatched) {
            for (var _i = 0, _a = this._subscriptions; _i < _a.length; _i++) {
                var handler = _a[_i];
                handler(args);
            }
            this.dispatched = true;
            this.dispatchArgs = args;
        }
    };
    return EventDispatcher;
}());



/***/ }),

/***/ "../../../../../src/app/home/home.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/home/home.component.html":
/***/ (function(module, exports) {

module.exports = "\r\n"

/***/ }),

/***/ "../../../../../src/app/home/home.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HomeComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var HomeComponent = /** @class */ (function () {
    function HomeComponent() {
    }
    HomeComponent.prototype.ngOnInit = function () {
    };
    HomeComponent = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["n" /* Component */])({
            selector: 'app-home',
            template: __webpack_require__("../../../../../src/app/home/home.component.html"),
            styles: [__webpack_require__("../../../../../src/app/home/home.component.css")]
        })
    ], HomeComponent);
    return HomeComponent;
}());



/***/ }),

/***/ "../../../../../src/app/utils/hub-message/hub-message.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".hub-message-icon-error {\r\n    font-size: 45px;\r\n    color: #C71111;\r\n}\r\n\r\n.hub-message-icon-notification {\r\n    font-size: 45px;\r\n    color: #E2980F;\r\n}\r\n\r\n.hub-message-icon-message {\r\n    font-size: 45px;\r\n    color: #075c72;\r\n}\r\n\r\n.hub-message-icon-more {\r\n    font-size: 45px;\r\n    color: #2c373a;\r\n}", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/utils/hub-message/hub-message.component.html":
/***/ (function(module, exports) {

module.exports = "<thf-modal\r\n[t-primary-action]=\"data.primaryAction\"\r\n[t-secondary-action]=\"data.secondaryAction\"\r\n[t-title]=\"data.title\" >\r\n    <div style=\"position: absolute;\">\r\n        <span [ngSwitch]=\"data.typeMessage\" style=\"height: 100%\">\r\n            <span *ngSwitchCase=\"'error'\"       class=\"hub-message-icon-error thf-icon thf-icon-close\"></span>\r\n            <span *ngSwitchCase=\"'warning'\"     class=\"hub-message-icon-notification thf-icon thf-icon-document\"></span>\r\n            <span *ngSwitchCase=\"'information'\" class=\"hub-message-icon-more thf-icon thf-icon-message\"></span>\r\n            <span *ngSwitchCase=\"'question'\"    class=\"hub-message-icon-more thf-icon thf-icon-message\"></span>\r\n        </span>\r\n    </div>\r\n    <div style=\"margin-bottom: 4px;margin-left: 55px; vertical-align: top; min-height: 45px\" [innerHTML]=\"data.body\"></div>\r\n    <form #checkForm=\"ngForm\">\r\n        <thf-checkbox-group *ngIf=\"checkOpts.length > 0\"\r\n            t-label=\"Opções\"\r\n            [t-options]=\"checkOpts\"\r\n            name=\"checkValues\"\r\n            [(ngModel)]=\"checkValues\"\r\n            t-required=\"true\">\r\n        </thf-checkbox-group>\r\n    </form>\r\n</thf-modal>"

/***/ }),

/***/ "../../../../../src/app/utils/hub-message/hub-message.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HubMessageComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__totvs_thf_ui_components_thf_modal_thf_modal_component__ = __webpack_require__("../../../../@totvs/thf-ui/components/thf-modal/thf-modal.component.js");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var HubMessageComponent = /** @class */ (function () {
    /**
     * Componente de mensagens.
     * Pode ser utilizado como instância única, desde que declarado no app.component.
     */
    function HubMessageComponent() {
        /**
        * Dados utilizados pelo formulário
        */
        this.data = {
            primaryAction: this.getPrimaryAction('Primário'),
            secondaryAction: this.getSecondaryAction('Secundário'),
            title: '',
            body: '',
            typeMessage: ''
        };
        /**
         * Opções disponíveis para o checkbox
         */
        this.checkOpts = [];
        /**
         * Valores selecionados do checkbox
         */
        this.checkValues = [];
        /**
         * Referência do array enviado para criação do checkbox
         */
        this.inputCheck = [];
    }
    /**
     * Monta e retorna a ação primária
     * @param label Label da ação primária
     */
    HubMessageComponent.prototype.getPrimaryAction = function (label) {
        var _this = this;
        return {
            action: function () { _this.emitPrimary(); },
            label: label
        };
    };
    /**
     * Monta e retorna a ação secundária
     * @param label Label da ação secundária
     */
    HubMessageComponent.prototype.getSecondaryAction = function (label) {
        var _this = this;
        return {
            action: function () { _this.emitSecondary(); },
            label: label
        };
    };
    /**
     * Apresenta a mensagem. Utilizado para mensagens de erro.
     * @param message mensagem
     * @param title título da mensagem, padrão 'Erro'
     */
    HubMessageComponent.prototype.openMessageError = function (message, title) {
        if (title === void 0) { title = 'Erro'; }
        this.data.typeMessage = 'error';
        this.data.primaryAction = this.getPrimaryAction('Ok');
        this.data.secondaryAction = undefined;
        this.data.title = title;
        this.data.body = message;
        this.modal.open();
    };
    /**
     * Apresenta a mensagem. Utilizado para mensagens informativas.
     * @param message mensagem
     * @param title título da mensagem, padrão 'Informação'
     */
    HubMessageComponent.prototype.openMessageInformation = function (message, title) {
        if (title === void 0) { title = 'Informação'; }
        this.data.typeMessage = 'information';
        this.data.primaryAction = this.getPrimaryAction('Ok');
        this.data.secondaryAction = undefined;
        this.data.title = title;
        this.data.body = message;
        this.modal.open();
    };
    /**
     * Apresenta a mensagem. Utilizado para mensagens de alerta.
     * @param message mensagem
     * @param title título da mensagem, padrão 'Atenção'
     */
    HubMessageComponent.prototype.openMessageWarning = function (message, title) {
        if (title === void 0) { title = 'Atenção'; }
        this.data.typeMessage = 'warning';
        this.data.primaryAction = this.getPrimaryAction('Ok');
        this.data.secondaryAction = undefined;
        this.data.title = title;
        this.data.body = message;
        this.modal.open();
    };
    /**
     * Realiza uma pergunta ao usuário.
     * @param message mensagem
     * @param title título da mensagem
     * @returns EventEmitter<boolean>
     * @example
     * .openMessageQuestion(message, title).subscribe((confirm) => {console.log(confirm)})
     */
    HubMessageComponent.prototype.openMessageQuestion = function (message, title, options) {
        if (options === void 0) { options = []; }
        this.data.typeMessage = 'question';
        this.data.primaryAction = this.getPrimaryAction('Sim');
        this.data.secondaryAction = this.getSecondaryAction('Não');
        this.data.title = title;
        this.data.body = message;
        this.inputCheck = options;
        for (var _i = 0, options_1 = options; _i < options_1.length; _i++) {
            var opt = options_1[_i];
            this.checkOpts.push({ label: opt.label, value: opt.label });
            if (opt.checked) {
                this.checkValues.push(opt.label);
            }
        }
        this.modal.open();
        this.modalEvent = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["w" /* EventEmitter */]();
        return this.modalEvent;
    };
    /**
    * Função disparada quando a ação principal for clicada pelo usuário
    */
    HubMessageComponent.prototype.emitPrimary = function () {
        var _this = this;
        this.inputCheck.forEach(function (opt) {
            if (_this.checkValues.findIndex(function (value) { return opt.label === value; }) > -1) {
                opt.checked = true;
            }
            else {
                opt.checked = false;
            }
        });
        if (this.modalEvent && this.data.typeMessage === 'question') {
            this.modalEvent.emit(true);
            this.modalEvent.unsubscribe();
        }
        this.modal.close();
        this.checkOpts = [];
        this.inputCheck = [];
        this.checkValues = [];
    };
    /**
    * Função disparada quando a ação secundária for clicada pelo usuário
    */
    HubMessageComponent.prototype.emitSecondary = function () {
        var _this = this;
        this.inputCheck.forEach(function (opt) {
            if (_this.checkValues.findIndex(function (value) { return opt.label === value; }) > -1) {
                opt.checked = true;
            }
            else {
                opt.checked = false;
            }
        });
        if (this.modalEvent && this.data.typeMessage === 'question') {
            this.modalEvent.emit(false);
            this.modalEvent.unsubscribe();
        }
        this.modal.close();
        this.checkOpts = [];
        this.inputCheck = [];
        this.checkValues = [];
    };
    __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_10" /* ViewChild */])(__WEBPACK_IMPORTED_MODULE_1__totvs_thf_ui_components_thf_modal_thf_modal_component__["a" /* ThfModalComponent */]),
        __metadata("design:type", __WEBPACK_IMPORTED_MODULE_1__totvs_thf_ui_components_thf_modal_thf_modal_component__["a" /* ThfModalComponent */])
    ], HubMessageComponent.prototype, "modal", void 0);
    HubMessageComponent = __decorate([
        Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["n" /* Component */])({
            selector: 'app-hub-message',
            template: __webpack_require__("../../../../../src/app/utils/hub-message/hub-message.component.html"),
            styles: [__webpack_require__("../../../../../src/app/utils/hub-message/hub-message.component.css")]
        })
        /**
         * @description Componente de mensagens.
         * Pode ser utilizado como instância única, desde que declarado no app.component.
         */
        ,
        __metadata("design:paramtypes", [])
    ], HubMessageComponent);
    return HubMessageComponent;
}());



/***/ }),

/***/ "../../../../../src/environments/environment.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return environment; });
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
var environment = {
    production: false,
    authorityEndPoint: 'http://localhost:5000/',
    authorizationEndPoint: 'http://localhost:5001/',
    managementEndPoint: 'http://localhost:5002'
};


/***/ }),

/***/ "../../../../../src/main.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm5/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__ = __webpack_require__("../../../platform-browser-dynamic/esm5/platform-browser-dynamic.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__app_app_module__ = __webpack_require__("../../../../../src/app/app.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");




if (__WEBPACK_IMPORTED_MODULE_3__environments_environment__["a" /* environment */].production) {
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_17" /* enableProdMode */])();
}
Object(__WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__["a" /* platformBrowserDynamic */])().bootstrapModule(__WEBPACK_IMPORTED_MODULE_2__app_app_module__["a" /* AppModule */])
    .catch(function (err) { return console.log(err); });


/***/ }),

/***/ 0:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__("../../../../../src/main.ts");


/***/ })

},[0]);
//# sourceMappingURL=main.bundle.js.map