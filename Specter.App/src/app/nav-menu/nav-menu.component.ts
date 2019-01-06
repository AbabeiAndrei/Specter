import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.less']
})
export class NavMenuComponent {
  isExpanded = false;
  currPage = '';
  appName = environment.appName;

  constructor(public authService: AuthenticationService, private router: Router) {
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
