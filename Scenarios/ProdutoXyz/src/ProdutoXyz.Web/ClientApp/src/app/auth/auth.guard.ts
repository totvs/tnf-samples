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

        var isAuthorized = await this.authService.authorize(this.router.url);

        if (isAuthorized) {
            var user = await this.authService.getUser();
            var userWithPermission = UserPermission.AddPremissionFeatures(user);
            userWithPermission.permissions = await this.userPermissionService.getUserPermissions();
            userAccessor.setCurrentUser(userWithPermission);
        }

        return isAuthorized;
    }
}
