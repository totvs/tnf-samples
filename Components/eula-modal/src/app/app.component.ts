import { Component, DoCheck, inject, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

import { PoMenuItem, PoMenuModule, PoPageModule } from '@po-ui/ng-components';

import {
  AppHeaderComponent,
  HeaderOptionsInterface,
  UserInfoService,
} from '@totvs-apps-components/header';

import { AuthService } from '@totvs-apps-components/auth';

import { I18nPipe } from '@shared/pipes/i18n.pipe';
import {
  EnvironmentType,
  EulaModalComponent,
  OptInOptions,
  ClientInfoDto,
} from '@totvs-smartlink-components/eula-modal';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    PoMenuModule,
    PoPageModule,
    AppHeaderComponent,
    EulaModalComponent,
  ],
  template: `
    <app-header-component [options]="headerConfig" />
    <po-menu
      [p-menus]="menus"
      [hidden]="hiddenMenu"
      [p-collapsed]="collapsed"
    />
    <div class="router menu-page-forbidden">
      <!-- <div class="router-root"> -->
      <router-outlet />
    </div>
    <eula-modal [options]="eulaOptions" />
  `,
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit, DoCheck {
  collapsed = false;
  hiddenMenu = false;

  readonly menus: Array<PoMenuItem> = [
    //TODO: importar o array de rotas da feature
  ];

  headerConfig: HeaderOptionsInterface = {
    appName: 'Portal',
    logoAppLink: '/',
    enableAppSettings: true,
    enableNotifications: true,
  };

  router = inject(Router);
  i18nPipe = inject(I18nPipe);
  authService = inject(AuthService);

  eulaOptions: OptInOptions = {
    appCode: 'produtoabc',
    totvsCode: 'TTY43432',
    environment: EnvironmentType.Staging,
  };

  ngDoCheck(): void {
    const router = document.getElementsByClassName('router')[0].classList;
    const poMenuCollapsed = document
      .getElementsByTagName('po-menu')[0]!
      .parentElement!.classList.contains('po-collapsed-menu');

    if (poMenuCollapsed) {
      router.add('menu-collapsed');
      return;
    }

    router.remove('menu-collapsed');
  }
  ngOnInit(): void {
    this.onLoadData();
  }
  onLoadData() {
    this.router.initialNavigation();
    const token = this.authService.getTokenPayload();

    if (token !== null) {
      console.info('Token de autenticação retornado com sucesso');

      document
        .getElementsByClassName('router')[0]
        .classList.remove('menu-page-forbidden');
    } else {
      console.warn('Token de autenticação nulo');
    }
  }
}
