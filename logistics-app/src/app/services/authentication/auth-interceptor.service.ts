//////////////////////////////////////////////////////////////////////////
// Basically it is not recommended to send the token on every request.  //
// Only send the token to endpoints you really need to send them to.    //
// So if we do an interceptor we are comparing the route the request    //
// goes to to a set of routes which should be secured.                  //
//////////////////////////////////////////////////////////////////////////

import { HttpEvent, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { from, lastValueFrom } from 'rxjs';
import { AuthService } from './auth.service';
import { environment } from '../../../environments/environment';

export const authInterceptor: HttpInterceptorFn = (request, next) =>
{
  var authService: AuthService = inject(AuthService);

  var requestHandler = new RequestHandler(authService);

  var url = request.url.trim()[request.url.length - 1] === "/" ? request.url.slice(0, -1) : request.url;

  if (environment.securedRoutes.find((z: string) => new RegExp("\\b(" + z + ")(\/+[^\s]*)?\\b","gi").test(url)))
  {
    return from(requestHandler.handleRequest(request, next));
  }

  return next(request);
}

export class RequestHandler
{
  constructor(public loginService: AuthService) { }

  public async handleRequest(request: HttpRequest<any>, next: HttpHandlerFn): Promise<HttpEvent<unknown>>
  {
    try
    {
      // get auth factory manually to avoid circular dependencies
      let token: string | undefined;
      var hasToken: boolean = this.loginService.hasToken();
      var expired: boolean = false;

      if (hasToken) 
      {
        expired = !this.loginService.isTokenValid();
      }

      if (hasToken && !expired) 
      {
        token = this.loginService.getToken();
      }
      else if(hasToken && expired)
      {
        this.loginService.logout();
      }

      if (!!token) 
      {
        // set bearer token in request header
        var headers = request.headers.set('Authorization', 'Bearer ' + token);
        request = request.clone({ headers: headers });
      }
    }
    catch (reason: any)
    {
      console.log("Error in HttpInterceptor: " + reason);
    }

    // return observable as promise because async functions have to return a promise
    return lastValueFrom(next(request));
  }
}
