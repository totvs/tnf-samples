import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
    selector: 'app-application',
    templateUrl: './application.component.html',
    styleUrls: ['./application.component.css']
})
export class ApplicationComponent {
    public ajaxResult;

    constructor(private http: HttpClient) {
    }

    getApplicationName() {

        this.http.get(`${environment.applicationEndPoint}/api/application/name`).subscribe(
            this.display,
            this.display);
    }

    getUserInfo() {

        this.http.get(`${environment.applicationEndPoint}/api/application/userInfo`).subscribe(
            this.display,
            this.display);
    }

    display: (response) => void =
        (response) => { this.ajaxResult = JSON.stringify(response, null, 2); };
}
