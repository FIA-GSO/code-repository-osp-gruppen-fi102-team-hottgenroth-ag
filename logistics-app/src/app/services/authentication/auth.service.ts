import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { RequestService } from '../request/request.service';
import { ILoginData } from '../../models/ILoginData';
import { JwtHelperService } from "@auth0/angular-jwt";
import { Router } from '@angular/router';
import { IUserData } from '../../models/IUserData';

const tokenLocalStorageKey = "token";

@Injectable({
  providedIn: 'root'
})
export class AuthService 
{
  private _request: RequestService = inject(RequestService);
  private _router: Router = inject(Router);
  
  private readonly _serviceURL = environment.serviceURL + environment.loginServicePath;
  
  protected jwtHelper: JwtHelperService;


  constructor(){
    this.jwtHelper = new JwtHelperService();
  }

  public async login(user: ILoginData): Promise<void>
  {
    let result: any = await this._request.post(environment.serviceURL + environment.loginServicePath, user);

    if(!!result && !!result.token)
    {
      localStorage.setItem(tokenLocalStorageKey, JSON.stringify(result.token));    
      this._router.navigate([""]);
    }
  }

  public async register(user: ILoginData): Promise<boolean>
  {
    let result: any = await this._request.post(environment.serviceURL + environment.loginServicePath + "/register", user);

    if(!!result)
    {
      return true;
    }
    
    return false;
  }

  public logout(): void
  {
    if(this.hasToken())
    {
      localStorage.removeItem(tokenLocalStorageKey);
      this._router.navigate(["/login"]);
    }
  }

  public async updateUserRole(user: IUserData): Promise<boolean>
  {
    let success: boolean = await this._request.put(environment.serviceURL + environment.loginServicePath, user);
    return success;
  }

  public getToken(): string 
  {
    let token = localStorage.getItem(tokenLocalStorageKey);
    if(token != null)
    {
      return JSON.parse(token);
    } 
  
    return "";
  }

  public hasToken(): boolean 
  {
    return this.getToken() != '';
  }

  public isTokenValid(): boolean
  {
    let token: string = this.getToken();
    if(token != '')
    {
      // Decode the raw token
      var isExpired = this.jwtHelper.isTokenExpired(token);
      return !isExpired;
    }
    return false;
  }

  public getUserRole(): string
  {
    let token: string = this.getToken();
    if(token != '')
    {
      // Decode the raw token
      var decodedToken: any = this.jwtHelper.decodeToken(token);
      return !!decodedToken && !!decodedToken.Role ? decodedToken.Role : "";
    }
    return "";
  }

  public getUserId(): string
  {
    let token: string = this.getToken();
    if(token != '')
    {
      // Decode the raw token
      var decodedToken: any = this.jwtHelper.decodeToken(token);
      return !!decodedToken && !!decodedToken.UserId ? decodedToken.UserId : "";
    }
    return "";
  }

  public getUserEmail(): string
  {
    let token: string = this.getToken();
    if(token != '')
    {
      // Decode the raw token
      var decodedToken: any = this.jwtHelper.decodeToken(token);
      return !!decodedToken && !!decodedToken.UserEmail ? decodedToken.UserEmail : "";
    }
    return "";
  }

  public async getAllUser(): Promise<IUserData[]>
  {
    try
    {
      let result: any = await this._request.get(environment.serviceURL + environment.loginServicePath);
      return result;
    }
    catch(err: any)
    {
      console.log(err);
      return [];
    }
  }
}
