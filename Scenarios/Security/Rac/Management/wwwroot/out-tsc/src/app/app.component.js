"use strict";
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
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var auth_service_1 = require("./auth/auth.service");
var auth_user_service_1 = require("./auth/auth.user.service");
var hub_message_component_1 = require("./utils/hub-message/hub-message.component");
var router_1 = require("@angular/router");
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
            if (event instanceof router_1.NavigationStart) {
                _this.showComponent = event.url === "" || event.url === "/" || event.url.startsWith("/auth-callback");
            }
        });
    }
    AppComponent.prototype.ngOnInit = function () {
        var _this = this;
        exports.hubMessage = this.messages;
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
            exports.hubMessage.openMessageQuestion('Sua sessão expirou. Deseja realizar o login novamente?', "Sessão expirada")
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
        core_1.ViewChild(hub_message_component_1.HubMessageComponent),
        __metadata("design:type", hub_message_component_1.HubMessageComponent)
    ], AppComponent.prototype, "messages", void 0);
    AppComponent = __decorate([
        core_1.Component({
            selector: 'app-root',
            templateUrl: './app.component.html',
            styleUrls: ['./app.component.css']
        }),
        __metadata("design:paramtypes", [auth_service_1.AuthService, auth_user_service_1.AuthUserService, router_1.Router])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map