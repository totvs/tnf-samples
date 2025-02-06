import { Injectable, inject } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { AuthService } from '@totvs-apps-components/auth';
import { TokenHelperService } from '@totvs-apps-components/core';

/**
 * Guard de autenticação
 * Deve ser importado em todas as rotas
 */
@Injectable({ providedIn: 'root' })
export class AuthGuard {
    router = inject(Router);
    authService = inject(AuthService);
    tokenHelperService = inject(TokenHelperService);
    canActivate(): boolean {
        const token = this.tokenHelperService.getTokenPayload();

        if (this.authService.getToken() && token) {
            if (
                token.authorities?.includes('ADMIN') === false &&
                token.authorities?.includes('PRODUCT_ADMIN') === false
            ) {
                this.router.navigate(['/page-forbidden']);
                return false;
            }
            return true;
        }

        this.authService.oauthRedirect().subscribe();
        return false;
    }
}

export const canActivateUser: CanActivateFn = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
) => {
    return inject(AuthGuard).canActivate();
}