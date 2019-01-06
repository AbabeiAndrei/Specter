import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '../../../services/authentication.service';
import { LoginModel } from 'src/models/loginModel';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less']
})
export class LoginComponent {
  remeberMe: boolean;
  password: string;
  email: string;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';

  emailFormControl = new FormControl('', [
    Validators.email,
  ]);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
  ) { }

  ngOnInit() {
    // reset login status
    this.authenticationService.logout();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  performLogin() {
    this.submitted = true;

    this.loading = true;
    const model = new LoginModel;
    model.email = this.email;
    model.password = this.password;
    model.remeber = this.remeberMe;

    this.authenticationService.login(model)
        .pipe(first())
        .subscribe(data => this.router.navigate([this.returnUrl]),
                   error => {
                    this.error = error;
                    this.loading = false;
                  });
  }
}
