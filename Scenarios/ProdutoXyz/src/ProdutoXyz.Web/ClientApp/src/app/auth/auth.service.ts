import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';

import { UserManager, UserManagerSettings, User, WebStorageStateStore, Log } from 'oidc-client';

import { IEvent, EventDispatcher } from '../events/event';
import { HubMessageComponent } from '../utils/hub-message/hub-message.component';
import { environment } from '../../environments/environment';
import { retry } from 'rxjs/operators';
import { Host } from '../utils/host';

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

    isHostUser(): boolean {
        if (this.user == null || this.user == undefined)
            return false;

        return (this.user.profile.email == 'admin@seudominio.com.br')
    }

    getUser(): Promise<User> {
        return this.manager.getUser();
    }

    logout() {
        this.manager.signoutRedirect().then(() => {
            this.manager.clearStaleState();
            this.user = null;
        });
    }

    login() {
        this.manager.signinRedirect();
    }

    async completeAuthentication(): Promise<void> {
        this.user = await this.manager.signinRedirectCallback();

        if (this.user && !this.user.expired)
            this.internalOnCompleteAuthentication.dispatch(this.user);
    }
}

Log.logger = window.console;
Log.level = Log.INFO;

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

        //acr_values = tenant: abc,

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
