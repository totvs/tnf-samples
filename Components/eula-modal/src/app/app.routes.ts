import { Routes } from '@angular/router';
import { HomeComponent } from '@shared/home/home.component';
import { OAuthLoginComponent, PageForbiddenComponent } from '@totvs-apps-components/auth';

export const routes: Routes = [
    //TODO: adicionar rotas aqui
    // * utilizar lady-loading (loadComponent)
    // * proteger a rota se necessário com algum Guard
    // não alterar abaixo
    {
        path: 'home',
        component: HomeComponent,
    },
    {
        path: 'oauth/login',
        component: OAuthLoginComponent,
    },
    {
        path: 'page-forbidden',
        component: PageForbiddenComponent,
    },
    {
        path: '**',
        redirectTo: 'home',
        pathMatch: 'full',
    },
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
    },
];
