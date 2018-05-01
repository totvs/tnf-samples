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
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/common/http");
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var forms_1 = require("@angular/forms");
var platform_browser_1 = require("@angular/platform-browser");
var router_1 = require("@angular/router");
var thf_ui_1 = require("@totvs/thf-ui");
var app_component_1 = require("./app.component");
var app_routing_module_1 = require("./app-routing.module");
var token_interceptor_1 = require("./auth/token.interceptor");
var auth_service_1 = require("./auth/auth.service");
var auth_callback_component_1 = require("./auth/callback/auth-callback.component");
var auth_guard_1 = require("./auth/auth.guard");
var auth_user_service_1 = require("./auth/auth.user.service");
var hub_message_component_1 = require("./utils/hub-message/hub-message.component");
var home_component_1 = require("./home/home.component");
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
        core_1.NgModule({
            declarations: [
                app_component_1.AppComponent,
                home_component_1.HomeComponent,
                auth_callback_component_1.AuthCallbackComponent,
                hub_message_component_1.HubMessageComponent
            ],
            imports: [
                common_1.CommonModule,
                forms_1.FormsModule,
                platform_browser_1.BrowserModule,
                app_routing_module_1.AppRoutingModule,
                thf_ui_1.ThfModule,
                http_1.HttpClientModule,
                router_1.RouterModule
            ],
            exports: [
                hub_message_component_1.HubMessageComponent
            ],
            providers: [
                auth_guard_1.AuthGuard,
                auth_service_1.AuthService,
                auth_user_service_1.AuthUserService,
                {
                    provide: http_1.HTTP_INTERCEPTORS,
                    useClass: token_interceptor_1.TokenInterceptor,
                    multi: true
                }
            ],
            bootstrap: [app_component_1.AppComponent]
        }),
        __metadata("design:paramtypes", [router_1.ActivatedRoute])
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map