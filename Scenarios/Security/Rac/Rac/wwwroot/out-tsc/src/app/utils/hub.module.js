"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var common_1 = require("@angular/common");
var thf_module_1 = require("@totvs/thf-ui/thf.module");
var forms_1 = require("@angular/forms");
var core_1 = require("@angular/core");
var HubModule = /** @class */ (function () {
    /**
     * @description
     * Classe principal do módulo de Hub
     */
    function HubModule() {
    }
    HubModule = __decorate([
        core_1.NgModule({
            declarations: [],
            imports: [
                forms_1.FormsModule,
                thf_module_1.ThfModule,
                common_1.CommonModule
            ],
            exports: []
        })
        /**
         * @description
         * Classe principal do módulo de Hub
         */
    ], HubModule);
    return HubModule;
}());
exports.HubModule = HubModule;
//# sourceMappingURL=hub.module.js.map