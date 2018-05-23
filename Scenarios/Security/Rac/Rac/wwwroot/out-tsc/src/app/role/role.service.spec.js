"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var role_service_1 = require("./role.service");
describe('RoleService', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            providers: [role_service_1.RoleService]
        });
    });
    it('should be created', testing_1.inject([role_service_1.RoleService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=role.service.spec.js.map