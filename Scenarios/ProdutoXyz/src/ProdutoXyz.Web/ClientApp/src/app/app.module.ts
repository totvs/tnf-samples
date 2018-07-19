import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, ActivatedRoute } from '@angular/router';

import { ThfModule } from '@totvs/thf-ui';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { TokenInterceptor } from './auth/token.interceptor';
import { AuthService } from './auth/auth.service';
import { AuthCallbackComponent } from './auth/callback/auth-callback.component';
import { AuthGuard } from './auth/auth.guard';
import { UserPermissionService } from './auth/userPermission.service';

import { HubMessageComponent } from './utils/hub-message/hub-message.component';
import { HomeComponent } from './home/home.component';
import { FeatureComponent } from './feature/feature.component';
import { ApplicationComponent } from './application/application.component';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        FeatureComponent,
        ApplicationComponent,
        AuthCallbackComponent,
        HubMessageComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        BrowserModule,
        AppRoutingModule,
        ThfModule,
        HttpClientModule,
        RouterModule
    ],
    exports: [
        HubMessageComponent
    ],
    providers: [
        AuthGuard,
        AuthService,
        UserPermissionService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptor,
            multi: true
        }
    ],
    bootstrap: [AppComponent]
})

export class AppModule {

    constructor(private activatedRoute: ActivatedRoute) {
        let dictionary = {};

        window.location.search.substr(1).split("&").forEach(p => {
            let split = p.split("=");
            dictionary[split[0]] = decodeURIComponent(split[1]);
        });

        if (dictionary["redirect_url"])
            localStorage.setItem("redirect_url", dictionary["redirect_url"]);
    }

}
