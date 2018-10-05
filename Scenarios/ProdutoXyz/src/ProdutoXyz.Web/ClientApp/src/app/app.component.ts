import { Component, OnInit, ViewChild } from '@angular/core';

import { AuthService } from './auth/auth.service';
import { MenuItem, UserPermissionService } from './auth/userPermission.service';
import { HubMessageComponent } from './utils/hub-message/hub-message.component';
import { Router, NavigationStart } from '@angular/router';
import { ThfToolbarAction, ThfToolbarProfile } from '@totvs/thf-ui/components/thf-toolbar';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

    public isLoggedIn: boolean = false;
    public title: string = '';

    public menus: MenuItem[] = [
        { label: 'Aplicação', link: 'application', permission: '', action: '' },
        { label: 'Features', link: 'feature', permission: '', action: '' }
    ];

    public thfMenus: MenuItem[] = [
        { label: 'Inicio', link: 'home', permission: '', action: '' }
    ];

    public profile: ThfToolbarProfile;
    public profileActions: Array<ThfToolbarAction> = [
        { label: 'Sair', action: () => { this.logout(); } }
    ];

    @ViewChild(HubMessageComponent) messages: HubMessageComponent;

    constructor(protected http: HttpClient, private authService: AuthService, private userPermissionService: UserPermissionService, private router: Router) {
        router.events.forEach((event) => {
            if (event instanceof NavigationStart) {

                var invalidRoute = event.url === "" || event.url === "/" || event.url.startsWith("/auth-callback");

                if (!invalidRoute)
                    sessionStorage.setItem("current.route", event.url);
            }
        });
    }

    ngOnInit() {
        hubMessage = this.messages;

        this.isLoggedIn = false;

        this.authService.onCompleteAuthentication().subscribe(user => {

            var profileTitle = null;

            if (user && user.profile) {

                profileTitle = user.profile.name;

                if (Array.isArray(user.profile.name)) {
                    profileTitle = user.profile.name[1];
                }
            }

            this.profile = {
                title: profileTitle
            };

            if (user) {
                this.userPermissionService.getUserAuthorizedMenus(this.menus).subscribe(authorizedMenus => {
                    this.isLoggedIn = true;
                    this.thfMenus = this.thfMenus.concat(authorizedMenus);

                    var currentRoute = sessionStorage.getItem("current.route");
                    if (currentRoute && currentRoute.length > 0) {

                        currentRoute = currentRoute.replace(/^\/+/g, '');

                        var menu = authorizedMenus.find(w => w.link == currentRoute);

                        if (menu)
                            this.router.navigate([menu.link]);
                        else
                            this.router.navigate(['/home']);
                    }
                    else {
                        this.router.navigate(['/home']);
                    }
                });
            }
        });

        this.authService.onAccessTokenExpired().subscribe((expired) => {

            hubMessage.openMessageQuestion('Sua sessão expirou. Deseja realizar o login novamente?', "Sessão expirada")
                .subscribe((confirm) => {
                    if (confirm)
                        this.authService.logout();
                });
        });
    }

    async logout() {
        await this.authService.logout();
    }
}

export let hubMessage: HubMessageComponent;
