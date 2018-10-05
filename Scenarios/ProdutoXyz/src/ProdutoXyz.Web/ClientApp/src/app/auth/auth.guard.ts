import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { AuthService } from '../auth/auth.service'
import { Observable } from "rxjs/Observable";
import { UserPermissionService } from './userPermission.service';
import { UserPermission } from './user.extensions';
import { userAccessor } from './userAccessor';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(
        private authService: AuthService,
        private router: Router,
        private userPermissionService: UserPermissionService) { }

    async canActivate(): Promise<boolean> {

        var user = await this.authService.getUser();

        if (user) {

            if (user.expired) {
                this.authService.logout();
                return false;
            }

            var userWithPermission = UserPermission.AddPremissionFeatures(user);
            userWithPermission.permissions = await this.userPermissionService.getUserPermissions();
            userAccessor.setCurrentUser(userWithPermission);

            return true;
        }

        this.authService.login();
        return false;
    }
}
