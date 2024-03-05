import { Injectable, inject } from "@angular/core";
import { IToolbarButton } from "../../models/IToolbarButton";
import { Guid } from "guid-typescript";
import { AuthService } from "../authentication/auth.service";
import { FrameworkService } from "../framework.service";
import { INavRailItem } from "../../models/INavRailItem";
import { Router } from "@angular/router";

@Injectable({ providedIn: 'root' })
export class ButtonStoreService
{
  private _authService: AuthService = inject(AuthService);
  private _framework: FrameworkService = inject(FrameworkService);
  private _router: Router = inject(Router);

  public projectButton!: INavRailItem;
  public boxButton!: INavRailItem;


  public logoutButton!: IToolbarButton;
  public pdfButton!: IToolbarButton;
  public userButton!: IToolbarButton;


  constructor()
  {
    this.initNavRailButtons();
    this.initToolbarButtons();
  }

  private initNavRailButtons()
  {
    this.projectButton = this._framework.createNavRailItem(
      "Projects",
      Guid.create().toString(),
      () => this.prjClicked(this)
    );
    this.boxButton = this._framework.createNavRailItem(
      "Transportboxes",
      Guid.create().toString(),
      () => this.boxClicked(this)
    );
  }
  private initToolbarButtons()
  {
    this.loginButton = this._framework.createToolbarButton(
      "logout",
      Guid.create().toString(),
      "Logout",
      () => this.logout(this)
    )

    this.pdfButton = this._framework.createToolbarButton(
      "picture_as_pdf",
      Guid.create().toString(),
      "Print PDF",
      () => this.printPDF(this)
    )

    this.userButton = this._framework.createToolbarButton(
      "account_circle",
      Guid.create().toString(),
      "Account",
      () => this.accountClicked(this)
    )
  }


  private logout(context: ButtonStoreService)
  {
    context._authService.logout();
  }

  private accountClicked(context: ButtonStoreService)
  {
    context._router.navigate(["./user"]);
  }

  private prjClicked(context: ButtonStoreService)
  {
    context._router.navigate(["./projects"]);
  }

  private boxClicked(context: ButtonStoreService)
  {
    context._router.navigate(["./transportbox"]);
  }
}