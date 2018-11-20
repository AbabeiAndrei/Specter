import { Injectable } from '@angular/core';
import {
    Http,
    RequestOptions,
    RequestOptionsArgs,
    Response,
    Request,
    Headers,
    XHRBackend
} from '@angular/http';

import { environment } from '../environments/environment';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { SpecterRequestOptions } from './request-options';

@Injectable ()
export class HttpService extends Http {
    public token: string;

    constructor(backend: XHRBackend,defaultOptions: SpecterRequestOptions) 
    {
        super(backend, defaultOptions);
    }

    // HttpService
    get(url: string, options?: RequestOptionsArgs): Observable<any> {
        return super.get(this.getFullUrl(url), this.requestOptions(options))
                    .catchError() // Catch exception here
                    .do((res: Response) => {
                        // Handle success, maybe display notification
                    }, (error: any) => {
                        // Handle errors
                    })
                    .finally(() => {
                        // Request completed
                    });
    }
    private getFullUrl(url: string): string {
            return this.apiUrl + url;
        }
    private requestOptions(options?: RequestOptionsArgs): RequestOptionsArgs {
    if (options == null) {
                options = new MyCustomRequestOptions();
            }
    if (options.headers == null) {
                options.headers = new Headers();
            }
    return options;
        }
}