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
    //Login aufruf
    let result: any = await this._request.post(this._serviceURL, user);

    //Kommt ein ergebnis zurück speichern wir das Token in den Localstorage
    if(!!result && !!result.token)
    {
      localStorage.setItem(tokenLocalStorageKey, JSON.stringify(result.token));    
      //Wir navigieren weiter, das Routing geht dann auf die Projektseite
      this._router.navigate([""]);
    }
  }

  //Wir registrieren den User in der DB
  public async register(user: ILoginData): Promise<boolean>
  {
    let result: any = await this._request.post(this._serviceURL + "/register", user);

    if(!!result)
    {
      return true;
    }
    
    return false;
  }

  //Wir löschen das Token und navigieren zum Login
  public logout(): void
  {
    if(this.hasToken())
    {
      localStorage.removeItem(tokenLocalStorageKey);
      this._router.navigate(["/login"]);
    }
  }

  //Wir updaten die Userrolle
  public async updateUserRole(user: IUserData): Promise<boolean>
  {
    let success: boolean = await this._request.put(this._serviceURL, user);
    return success;
  }

  //Wir hiolen das Token aus dem Localstorage und parsen es zum String zurück
  public getToken(): string 
  {
    let token = localStorage.getItem(tokenLocalStorageKey);
    if(token != null)
    {
      return JSON.parse(token);
    } 
  
    return "";
  }

  //Wir prüfen ob ein Token existiert
  public hasToken(): boolean 
  {
    return this.getToken() != '';
  }

  //Wir prüfen ob das TOken noch gültig ist
  public isTokenValid(): boolean
  {
    let token: string = this.getToken();
    if(token != '')
    {
      // Zum decodieren nehmen wir eine externe Library
      var isExpired = this.jwtHelper.isTokenExpired(token);
      return !isExpired;
    }
    return false;
  }

  //Wir lesen die User Rolle aus dem token aus
  public getUserRole(): string
  {
    let token: string = this.getToken();
    if(token != '')
    {
      // Zum decodieren nehmen wir eine externe Library
      var decodedToken: any = this.jwtHelper.decodeToken(token);
      return !!decodedToken && !!decodedToken.Role ? decodedToken.Role : "";
    }
    return "";
  }

  //Wir lesen die User Id aus dem token aus
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

  //Wir lesen die User Mail aus dem token aus
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

  //Wir holen alle registrierten User
  public async getAllUser(): Promise<IUserData[]>
  {
    try
    {
      let result: any = await this._request.get(this._serviceURL);
      return result;
    }
    catch(err: any)
    {
      console.log(err);
      return [];
    }
  }
}
