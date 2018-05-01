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
var oidc_client_1 = require("oidc-client");
var environment_1 = require("../../environments/environment");
var event_1 = require("../events/event");
var AuthService = /** @class */ (function () {
    function AuthService() {
        var _this = this;
        this.manager = new oidc_client_1.UserManager(getClientSettings());
        this.user = null;
        this.internalOnCompleteAuthentication = new event_1.EventDispatcher();
        this.internalonAccessTokenExpired = new event_1.EventDispatcher();
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
        core_1.Injectable(),
        __metadata("design:paramtypes", [])
    ], AuthService);
    return AuthService;
}());
exports.AuthService = AuthService;
function getClientSettings() {
    var CurrentUrl = window.location.protocol + "//" + window.location.host + "/";
    return {
        authority: environment_1.environment.authorityEndPoint,
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
        userStore: new oidc_client_1.WebStorageStateStore({ store: localStorage })
    };
}
exports.getClientSettings = getClientSettings;
//# sourceMappingURL=auth.service.js.map