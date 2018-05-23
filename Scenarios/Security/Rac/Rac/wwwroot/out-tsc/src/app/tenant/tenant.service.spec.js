"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var tenant_service_1 = require("./tenant.service");
describe('TenantService', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            providers: [tenant_service_1.TenantService]
        });
    });
    it('should be created', testing_1.inject([tenant_service_1.TenantService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=tenant.service.spec.js.map