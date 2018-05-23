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
var core_1 = require("@angular/core");
var tenant_service_1 = require("./tenant.service");
var hub_api_1 = require("../utils/hub-api");
var TenantThfLookupService = /** @class */ (function () {
    function TenantThfLookupService(tenantService) {
        this.tenantService = tenantService;
    }
    TenantThfLookupService.prototype.getFilteredData = function (filter, page, pageSize) {
        return this.tenantService.getQuery(new hub_api_1.HubApiQueryString({
            page: page,
            pageSize: pageSize,
            filter: [{ key: "tenantName", value: filter }]
        }));
    };
    TenantThfLookupService.prototype.getObjectByValue = function (value) {
        return this.tenantService.get(value);
    };
    TenantThfLookupService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [tenant_service_1.TenantService])
    ], TenantThfLookupService);
    return TenantThfLookupService;
}());
exports.TenantThfLookupService = TenantThfLookupService;
//# sourceMappingURL=tenant.thflookup.service.js.map