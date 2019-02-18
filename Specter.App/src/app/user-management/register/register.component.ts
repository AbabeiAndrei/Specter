import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/services/user.service';
import { User, UserCreate } from 'src/models/user';

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

  constructor(private userManager: UserService) { }

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

    const name = getName(this.name);
    const model = new UserCreate();

    model.email = this.email;
    model.firstName = this.name[0];
    model.lastName = this.name[1];
    model.password = this.password;

    this.userManager.register(model);
  }
  
  getName(name: string): 
}
