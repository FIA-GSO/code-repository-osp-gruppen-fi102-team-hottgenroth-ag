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
    // Überprüfe ob der Parameter ein String ist oder ein Base64 String
    if (typeof (pJson) === 'string') 
    {
      try 
      {
        window.atob(pJson);
        return pJson;
      }
      catch
      {
        // parameter war ein string aber kein base64 string, deswegen 
        //überprüf ob es ein JSON ist
        pJson = JSON.stringify(pJson);
      }
    }
    else 
    {
      //Alles andere wird zum JSON gemacht
      pJson = JSON.stringify(pJson);
    }

    var refMap: any = {};
    
    //Wir filtern die $id aus dem Ergebnisobjekt raus
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

  //Hier werden alle HTTP-Aufrufe ausprogrammiert damit wir dies nur einmal machen müssen

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

  public getBlob(url: string, headers: HttpHeaders | undefined = undefined,
    params: HttpParams | undefined = undefined): Promise<any | HttpErrorResponse> {
    return new Promise((resolve, reject) => {
      this.http.get(url, { headers: headers, params: params, responseType: 'blob' }).pipe(
        catchError(error => {
          return throwError(() => {
            console.error(error.message || 'server Error');
            this.errorReceived.emit(error);
            reject(error);
          });
        })
      ).subscribe((data: any) => {
        resolve(data);
      });
    });
  }

  public post(url: string, data: any, headers: HttpHeaders | undefined = undefined,
    params: HttpParams | undefined = undefined): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.post<any>(url, data, { headers: headers, params: params }).pipe(
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
      this.http.put<any>(url, data, { headers: headers, params: params }).pipe(
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
