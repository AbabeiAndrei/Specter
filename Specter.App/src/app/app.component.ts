import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from '../services/authentication.service';
import { User } from '../models/user';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  title = environment.appName;
  currentUser: User;

  get darkMode():boolean {
    return this.currentUser != null && this.currentUser.darkMode;
  }
  set darkMode(value:boolean) {
    this.authenticationService.toggleDarkMode(value).subscribe(() => {
      if(this.currentUser != null)
        this.currentUser.darkMode = value;
    });
  } 

  constructor(private router: Router, private authenticationService: AuthenticationService ) {
      this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  logout() {
      this.authenticationService.logout();
      this.router.navigate(['/login']);
  }
}
