import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.less']
})
export class ForgotPasswordComponent {
  email: string;

  emailFormControl = new FormControl('', [
    Validators.email,
  ]);

  sendMail(): void {
    
  }
}
