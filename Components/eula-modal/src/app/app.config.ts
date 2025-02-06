import { provideRouter, withComponentInputBinding } from '@angular/router';
import { routes } from './app.routes';
import { APP_INITIALIZER, ApplicationConfig, importProvidersFrom, LOCALE_ID } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PoHttpRequestModule, PoI18nConfig, PoI18nModule, PoI18nService } from '@po-ui/ng-components';
import { AuthModuleOptions, provideTotvsAppAuth } from '@totvs-apps-components/auth';
import { HeaderModuleOptions, provideTotvsAppHeader } from '@totvs-apps-components/header';
import { environment } from '../environments/environment';
import { generalEn, generalEs, generalPt } from './shared/i18n';
import { registerLocaleData } from '@angular/common';
import ptBr from '@angular/common/locales/pt';
import { I18nPipe } from './shared/pipes/i18n.pipe';

const authOptions: AuthModuleOptions = {
  config: {
    shouldPromptTokenRequest: environment.shouldPromptTokenRequest,
    appCodeDevMode: environment.appCodeDevMode,
    DEV: environment.DEV,
    STAGING: environment.STAGING,
    PROD: environment.PROD,
    pkceEnabled: false,
  }
};

const headerOptions: HeaderModuleOptions = {
  communicationApplicationID: environment.communicationApplicationID,
  DEV: environment.DEV,
  STAGING: environment.STAGING,
  PROD: environment.PROD,
};

registerLocaleData(ptBr);

const i18nConfig: PoI18nConfig = {
  default: {
    language: 'pt-BR',
    context: 'general',
    cache: true,
  },
  contexts: {
    general: {
      'en-US': generalEn,
      'es-ES': generalEs,
      'pt-BR': generalPt,
    },
  },
};

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes, withComponentInputBinding()),
    provideTotvsAppAuth(authOptions),
    provideTotvsAppHeader(headerOptions),
    importProvidersFrom([BrowserAnimationsModule, PoHttpRequestModule]),
    importProvidersFrom(PoI18nModule.config(i18nConfig)),
    {
      provide: APP_INITIALIZER,
      useFactory: (poI18nService: PoI18nService) => () =>
        poI18nService.getLiterals().subscribe(),
      deps: [PoI18nService],
      multi: true,
    },
    {
      provide: LOCALE_ID,
      useFactory: (poI18nService: PoI18nService) => poI18nService.getLanguage(),
      deps: [PoI18nService],
    },
    {
      provide: I18nPipe,
    },
  ],

};