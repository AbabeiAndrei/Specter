import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.less']
})
export class NavMenuComponent {
  isExpanded = false;
  currPage = '';
  appName = environment.appName;

  get darkMode():boolean {
      return this.appComponent.darkMode;
  }
  set darkMode(value:boolean) {
      this.appComponent.darkMode = value;
  }

  constructor(public authService: AuthenticationService, private router: Router, private appComponent: AppComponent) {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  getClass(item: string) {
    if (this.router.url.endsWith(item)) {
      return 'active';
    }

    return '';
  }
}
