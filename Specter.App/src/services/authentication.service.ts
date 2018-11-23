import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from '../models/user';
import { LoginModel } from 'src/models/loginModel';
import { environment } from '../environments/environment'

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;
    private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(model: LoginModel) {
        return this.http.post<any>(`${environment.apiUrl}/users/authenticate`, JSON.stringify(model),  this.options)
                   .pipe(map(user => {
                            // login successful if there's a jwt token in the response
                            if (user && user.token) {
                                // store user details and jwt token in local storage to keep user logged in between page refreshes
                                localStorage.setItem('currentUser', JSON.stringify(user));
                                this.currentUserSubject.next(user);
                            }

                            return user;
                        }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}