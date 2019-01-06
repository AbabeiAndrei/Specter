import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less']
})
export class RegisterComponent {
  email: string;
  password: string;
  password2: string;
  emailFormControl = new FormControl('', [
    Validators.email,
  ]);

  doRegister(): void {

  }
}
