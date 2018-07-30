import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AuthService } from './auth.service';
import { environment } from '../../environments/environment';
import { IEvent, EventDispatcher } from '../events/event';
import { Permission } from './user.extensions';

@Injectable()
export class UserPermissionService {

    constructor(private http: HttpClient) { }

    getUserAuthorizedMenus(menus: MenuItem[]): IEvent<MenuItem[]> {
        let event: EventDispatcher<MenuItem[]> = new EventDispatcher<MenuItem[]>();
        let permissions = menus.map(m => m.permission);

        this.getUserPermissionsGranted(permissions).then(data => {
            let authorizedMenus = [];

            for (let index = 0; index < menus.length; index++) {
                if (data[index] || !menus[index].permission)
                    authorizedMenus.push(menus[index]);
            }

            event.dispatch(authorizedMenus);
        });

        return event;
    }

    async getUserPermissions(): Promise<Permission[]> {

        var nameOfPermissions = [
            "Management.Tenant",
            "Management.Organization",
            "Management.Role",
            "Management.User",
            "ProdutoXyz.Application",
            "ProdutoXyz.Application.Name",
            "ProdutoXyz.Application.UserInfo"
        ];

        var response = await this.getUserPermissionsGranted(nameOfPermissions);

        var permissions: Permission[] = [];

        for (var i = 0; i < nameOfPermissions.length; i++) {
            permissions.push({ key: nameOfPermissions[i], value: response[i] });
        }

        return permissions;
    }

    async getUserPermissionsGranted(permissions: string[]): Promise<boolean[]> {
        return await this.http.post<boolean[]>(`${environment.authorityEndPoint}/api/Permissions`, permissions).toPromise();
    }

    async getFeatures(): Promise<string[]> {
        return await this.http.get<string[]>(`${environment.authorityEndPoint}/api/Features`).toPromise();
    }
}

export class MenuItem {
    public label: string;
    public link: string;
    public permission: string;
    public action: string | Function;
}
