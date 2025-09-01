import { HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpOptionsService {

  constructor() {
   
   }


  generateHttpOptions(options: {
    params?: { [key: string]: any };
    headers?: { [key: string]: any };
    observe?: 'body' | 'response';
    responseType?: 'json' | 'text' | 'blob';
  }): any {
    let headers = new HttpHeaders();

    // Process headers
    if (options.headers) {
      for (const key in options.headers) {
        if (
          options.headers.hasOwnProperty(key) &&
          typeof options.headers[key] === 'string'
        ) {
          headers = headers.set(key, options.headers[key]);
        } else {
          console.error(`Invalid header value for key: ${key}`, options.headers[key]);
        }
      }
    }

    // Process params
    let params = new HttpParams();
    if (options.params) {
      for (const key in options.params) {
        if (
          options.params.hasOwnProperty(key) &&
          typeof options.params[key] === 'string'
        ) {
          params = params.set(key, options.params[key]);
        } else {
          console.error(`Invalid param value for key: ${key}`, options.params[key]);
        }
      }
    }

    // Build the final httpOptions
    const httpOptions: any = {
      headers,
      params,
    };

    if (options.observe) {
      httpOptions.observe = options.observe;
    }
    if (options.responseType) {
      httpOptions.responseType = options.responseType;
    }

    //console.log('Generated HTTP Options:', httpOptions);
    return httpOptions;
  }



  getDefaultOptions(): any {
    const options = {
      headers: this.getHeaders(),
    }
     return  this.generateHttpOptions(options);
    //return options;
  }

  public getHeaders(): { [key: string]: string } {
    let token = localStorage.getItem("token");
    // return new HttpHeaders({
    //   'Content-Type': 'application/json',
    //   'accept': 'application/json',
    //   'authorization': 'Bearer ' + (token || ''),
    // })

    return {
      'Content-Type': 'application/json',
      accept: 'application/json',
      authorization: 'Bearer ' + (token || ''),
    }
  }
  
  
}

