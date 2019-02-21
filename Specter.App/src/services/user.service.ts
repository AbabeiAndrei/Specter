import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User, UserCreate } from '../models/user';
import { environment } from '../environments/environment';
import { ChangePassword } from 'src/models/changePassword';

@Injectable({ providedIn: 'root' })
export class UserService {

  constructor(private http: HttpClient) { }

  getAll() {
      return this.http.get<User[]>(`${environment.apiUrl}/users`);
  }

  register(model: UserCreate) {
    return this.http.post<User[]>(`${environment.apiUrl}/users/register`, model);
  }

  changePassword(model: ChangePassword) {
    return this.http.patch<any>(`${environment.apiUrl}/users/changePassword`, model);
  }
}
