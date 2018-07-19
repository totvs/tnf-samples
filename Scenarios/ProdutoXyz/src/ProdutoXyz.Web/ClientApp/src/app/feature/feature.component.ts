import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { userAccessor } from '../auth/userAccessor';
import { UserPermissionService } from '../auth/userPermission.service';
import { Permission } from '../auth/user.extensions';

@Component({
    selector: 'app-feature',
    templateUrl: './feature.component.html',
    styleUrls: ['./feature.component.css'],
    encapsulation: ViewEncapsulation.None
})
export class FeatureComponent implements OnInit {
    public permissions: Permission[];

    constructor(public userPermission: UserPermissionService) {
    }

    ngOnInit() {

        this.userPermission.getUserPermissions().then(data => {
            this.permissions = data;
        });
    }
}
