import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { AuthGuard } from './auth/auth.guard';
import { AuthCallbackComponent } from './auth/callback/auth-callback.component';
import { HomeComponent } from './home/home.component';
import { ApplicationComponent } from './application/application.component';
import { FeatureComponent } from './feature/feature.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
        canActivate: [AuthGuard]
    },
    {
        path: 'home',
        component: HomeComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'application',
        component: ApplicationComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'feature',
        component: FeatureComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'auth-callback',
        component: AuthCallbackComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
