import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';

import { UserManager, UserManagerSettings, User, WebStorageStateStore } from 'oidc-client';

import { IEvent, EventDispatcher } from '../events/event';
import { HubMessageComponent } from '../utils/hub-message/hub-message.component';
import { environment } from '../../environments/environment';
import { retry } from 'rxjs/operators';

@Injectable()
export class AuthService {
    private manager: UserManager = new UserManager(getClientSettings());
    private user: User = null;
    private internalOnCompleteAuthentication: EventDispatcher<User> = new EventDispatcher<User>();
    private internalonAccessTokenExpired: EventDispatcher<boolean> = new EventDispatcher<boolean>();

    public onCompleteAuthentication: () => IEvent<User> =
        () => this.internalOnCompleteAuthentication;

    public onAccessTokenExpired: () => IEvent<boolean> =
        () => this.internalonAccessTokenExpired;

    constructor() {

        this.manager.events.addAccessTokenExpired(e => {
            this.internalonAccessTokenExpired.dispatch(true);
        });

        // Get user after callback
        this.getUser().then(user => {
            this.user = user;

            if (this.user) {
                this.internalOnCompleteAuthentication.dispatch(user);
            }
        });
    }

    isLoggedIn = (): boolean => !!this.user && !this.user.expired;

    getAuthorizationHeaderValue(): string {
        return this.user ? `${this.user.token_type} ${this.user.access_token}` : '';
    }

    signoutRedirect() {
        this.manager.signoutRedirect();
    }

    isHostUser(): boolean {
        if (this.user == null || this.user == undefined)
            return false;

        return (this.user.profile.email == 'admin@seudominio.com.br')
    }

    getUserTokenType(): string {
        return this.user.token_type;
    }

    getUserAccessToken(): string {
        return this.user.access_token;
    }

    getUser(): Promise<User> {
        return this.manager.getUser();
    }

    async startAuthentication(): Promise<void> {
        await this.manager.signinRedirect();
    }

    async logout(): Promise<void> {
        await this.manager.signoutRedirect();
    }

    async completeAuthentication(): Promise<void> {
        this.user = await this.manager.signinRedirectCallback();

        if (this.user)
            this.internalOnCompleteAuthentication.dispatch(this.user);
    }

    public authorize(urlCurrent): Promise<boolean> {
        return this.getUser().then(user => {

            if (user) {

                if (user.expired) {
                    this.manager.signoutRedirect();
                    return false;
                }

                return true;
            }

            this.startAuthentication();
            return false;
        });
    }
}

export function getClientSettings(): UserManagerSettings {

    var settings: UserManagerSettings = {
        authority: environment.authorityEndPoint,
        client_id: 'produto_xyz',
        redirect_uri: `${environment.applicationEndPoint}/auth-callback`,
        post_logout_redirect_uri: environment.applicationEndPoint,

        // these two will be done dynamically from the buttons clicked, but are
        // needed if you want to use the silent_renew
        response_type: "code id_token token",
        scope: "openid profile email authorization_api offline_access",

        // this will toggle if profile endpoint is used
        loadUserInfo: true,

        // silent renew will get a new access_token via an iframe 
        // just prior to the old access_token expiring (60 seconds prior)
        silent_redirect_uri: `${environment.applicationEndPoint}/assets/silent-renew.html`,
        automaticSilentRenew: true,

        // will revoke (reference) access tokens at logout time
        revokeAccessTokenOnSignout: true,

        // will raise events for when user has performed a logout at IdentityServer
        monitorSession: true,
        // this will allow all the OIDC protocol claims to be visible in the window. normally a client app 
        // wouldn't care about them or want them taking up space
        filterProtocolClaims: false
    };

    return settings;
}
