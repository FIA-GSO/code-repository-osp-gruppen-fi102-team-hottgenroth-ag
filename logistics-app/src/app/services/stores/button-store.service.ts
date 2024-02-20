import { Injectable, inject } from "@angular/core";
import { IToolbarButton } from "../../models/IToolbarButton";
import { Guid } from "guid-typescript";
import { AuthService } from "../authentication/auth.service";
import { FrameworkService } from "../framework.service";

@Injectable({ providedIn: 'root' })
export class ButtonStoreService
{
  private _authService: AuthService = inject(AuthService);
  private _framework: FrameworkService = inject(FrameworkService);


  public loginButton!: IToolbarButton;


  constructor()
  {
    this.initToolbarButtons();
  }

  private initToolbarButtons()
  {
    this.loginButton = this._framework.createToolbarButton(
      "logout",
      Guid.create().toString(),
      "Logout",
      () => this.logout(this)
    )
  }


  private logout(context: ButtonStoreService)
  {
    context._authService.logout();
  }
}