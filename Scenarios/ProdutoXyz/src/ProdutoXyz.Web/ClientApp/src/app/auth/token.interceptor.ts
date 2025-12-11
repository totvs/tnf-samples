import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';

import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { hubMessage } from '../app.component';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(public auth: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return Observable.fromPromise(this.handleAccess(request, next));
    }

    private async handleAccess(request: HttpRequest<any>, next: HttpHandler): Promise<HttpEvent<any>> {

        var user = await this.auth.getUser();

        if (user && !user.expired) {

            request = request.clone({
                setHeaders: {
                    'Authorization': `${user.token_type} ${user.access_token}`
                }
            });
        }

        var promisse = next.handle(request).toPromise();

        promisse.catch((error) => {

            if (error.status == 404) {
                hubMessage.openMessageError(error.message);
                return;
            }

            var messages = '';

            if (error.status == 401) {
                messages += "Acesso não authorizado. Sem permissões para acessar esse recurso" + '<br/>';
                messages += 'Verifique os logs da aplicação para maiores detalhes.';

                hubMessage.openMessageError(messages);
                return;
            }

            // Error response TNF
            for (var i = 0; i < error.error.details.length; i++) {
                messages += error.error.details[i].message + '<br/>';
            }

            if (error.status == 500) {
                messages += error.error.message + '<br/>';
                messages += 'Verifique os logs da aplicação para maiores detalhes.';
            }

            hubMessage.openMessageError(messages);

            return Observable.empty<any>();
        });

        return promisse;
    }

}
