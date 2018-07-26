import { Component, OnInit } from '@angular/core';

import { AuthService } from '../auth.service'
import { Router } from '@angular/router';

@Component({
    selector: 'app-auth-callback',
    template: ''
})
export class AuthCallbackComponent implements OnInit {

    constructor(private authService: AuthService, private router: Router) { }

    ngOnInit() {
        this.authService.completeAuthentication();
    }
}
