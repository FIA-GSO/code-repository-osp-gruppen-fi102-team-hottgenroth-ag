import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  @Output() errorReceived: EventEmitter<any> = new EventEmitter();

  constructor(private http: HttpClient) { }

  private parseAndResolve(pJson: string): any {
    // Check if parameter is of type string and also of type base64
    if (typeof (pJson) === 'string') 
    {
      try 
      {
        window.atob(pJson);
        return pJson;
      }
      catch
      {
        // parameter was a string but not of type base64, so check if it is json
        pJson = JSON.stringify(pJson);
      }
    }
    else 
    {
      pJson = JSON.stringify(pJson);
    }

    var refMap: any = {};

    return JSON.parse(pJson, function (key, value) {
      if (key === '$id') 
      {
        refMap[value] = this;

        // return undefined so that the property is deleted
        return void (0);
      }

      if (value && value.$ref) 
      {
        return refMap[value.$ref];
      }

      return value;
    });
  }

  public get(url: string, headers: HttpHeaders | undefined = undefined,
    params: HttpParams | undefined = undefined): Promise<any | HttpErrorResponse> {
    return new Promise((resolve, reject) => {
      this.http.get<any>(url, { headers: headers, params: params }).pipe(
        catchError(error => {
          return throwError(() => {
            console.error(error.message || 'server Error');
            this.errorReceived.emit(error);
            reject(error);
          });
        })
      ).subscribe((data: any) => {
        resolve(this.parseAndResolve(data));
      });
    });
  }

  public post(url: string, data: any, headers: HttpHeaders | undefined = undefined,
    params: HttpParams | undefined = undefined): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.post<any>(url, JSON.stringify(data), { headers: headers, params: params }).pipe(
        catchError(error => {
          return throwError(() => {
            console.error(error.message || 'server Error');
            this.errorReceived.emit(error);
            reject(error);
          });
        })
      ).subscribe((data: any) => {
        resolve(this.parseAndResolve(data));
      });
    });
  }

  public put(url: string, data: any, headers: HttpHeaders | undefined = undefined,
    params: HttpParams | undefined = undefined): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.put<any>(url, JSON.stringify(data), { headers: headers, params: params }).pipe(
        catchError(error => {
          return throwError(() => {
            console.error(error.message || 'server Error');
            this.errorReceived.emit(error);
            reject(error);
          });
        })
      ).subscribe((data: any) => {
        resolve(this.parseAndResolve(data));
      });
    });
  }

  public delete(url: string, headers: HttpHeaders | undefined = undefined,
    params: HttpParams | undefined = undefined): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.delete<any>(url, { headers: headers, params: params }).pipe(
        catchError(error => {
          return throwError(() => {
            console.error(error.message || 'server Error');
            this.errorReceived.emit(error);
            reject(error);
          });
        })
      ).subscribe((data: any) => {
        resolve(this.parseAndResolve(data));
      });
    });
  }
}
