import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/services/user.service';
import { User, UserCreate } from 'src/models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less']
})
export class RegisterComponent {
  name: string;
  email: string;
  password: string;
  password2: string;
  emailFormControl = new FormControl('', [
    Validators.email,
  ]);

  errors: string[];

  constructor(private userManager: UserService,
    private router: Router) { }

  doRegister(): void {

    this.errors = [];

    if (this.name === '') {
      this.errors.push('Name is empty');
    }

    if (this.email === '' || this.emailFormControl.invalid) {
      this.errors.push('Email is empty or invalid');
    }

    if (this.password === '') {
      this.errors.push('Password is empty');
    }

    if (this.password2 !== this.password) {
      this.errors.push('Password confirmation failed');
    }

    if (this.errors.length > 0) {
      return;
    }

    const name = this.getName(this.name);
    const model = new UserCreate();

    model.email = this.email;
    model.firstName = name[0];
    model.lastName = name[1];
    model.password = this.password;

    this.userManager.register(model).subscribe(r => {
      this.router.navigateByUrl('/');
    }, e => {
      this.errors.push(e);
    });
  }

  getName(name: string) {
    name = name.trim();
    return [
      name.split(' ')[0],
      name.substring(name.indexOf(' ') + 1)
    ];
  }
}
