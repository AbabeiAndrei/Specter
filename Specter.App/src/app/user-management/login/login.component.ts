import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less']
})
export class LoginComponent {
  remeberMe: boolean;
  password: string;
  email: string;

  emailFormControl = new FormControl('', [
    Validators.email,
  ]);

  performLogin(): void {
    
  }
}
