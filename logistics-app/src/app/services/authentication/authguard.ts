import { Inject, Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';

import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard
{
  private _loginService: AuthService = inject(AuthService);

  constructor() { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    return this.getLoggedIn();
  }

  async getLoggedIn(): Promise<boolean | UrlTree> {
    return this._loginService.hasToken();
  }
}

