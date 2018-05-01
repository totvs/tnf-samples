"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var organizationUnit_service_1 = require("./organizationUnit.service");
describe('OrganizationService', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            providers: [organizationUnit_service_1.OrganizationService]
        });
    });
    it('should be created', testing_1.inject([organizationUnit_service_1.OrganizationService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=organizationUnit.service.spec.js.map