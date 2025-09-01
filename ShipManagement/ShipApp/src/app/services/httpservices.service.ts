import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { APP_CONSTANTS, MethodType } from '../app.constants';
import { HttpOptionsService } from './http-options.service';

@Injectable({
  providedIn: 'root'
})
export class HttpservicesService {
  http = inject(HttpClient);
  httpoptionservice =inject(HttpOptionsService);
  years: number[] = [];

  constructor() { }

  public handleHtppRequest(methodType: MethodType, url: string, body?: any, options?: any): Observable<any> {
    url = APP_CONSTANTS.API_URI + url;
    switch (methodType) {
      case MethodType.GET:
        return this.http.get(url, options ?? this.httpoptionservice.getDefaultOptions());
      case MethodType.POST:
        return this.http.post(url, body, options ?? this.httpoptionservice.getDefaultOptions());

      case MethodType.PUT:
        return this.http.put(url, body, options ?? this.httpoptionservice.getDefaultOptions());

      case MethodType.PATCH:
        return this.http.patch(url, body, options ?? this.httpoptionservice.getDefaultOptions());

      case MethodType.DELETE:
        return this.http.delete(url, options ?? this.httpoptionservice.getDefaultOptions());

      default:
        throw new Error(`Unsupported HTTP method: ${methodType}`);
    }

  }






}
