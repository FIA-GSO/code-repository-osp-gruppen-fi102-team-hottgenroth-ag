import { Injectable, inject } from "@angular/core";
import { IToolbarButton } from "../../models/IToolbarButton";
import { Guid } from "guid-typescript";
import { AuthService } from "../authentication/auth.service";
import { FrameworkService } from "../framework.service";
import { INavRailItem } from "../../models/INavRailItem";
import { Router } from "@angular/router";
import { PdfService } from "../pdf/pdf.service";
import { SharedDialogComponent } from "../../framework/shared-dialog/shared-dialog.component";
import { LoadingSpinnerService } from "../loading-spinner.service";
import { MatDialog } from "@angular/material/dialog";
import { Subscription } from "rxjs";
import { ExportService } from "../export/export.service";

@Injectable({ providedIn: 'root' })
export class ButtonStoreService
{
  private _authService: AuthService = inject(AuthService);
  private _framework: FrameworkService = inject(FrameworkService);
  private _router: Router = inject(Router);
  private _spinner: LoadingSpinnerService = inject(LoadingSpinnerService);
  private _dialog: MatDialog = inject(MatDialog);
  private _exportService: ExportService = inject(ExportService);


  public projectButton!: INavRailItem;
  public boxButton!: INavRailItem;
  
  
  public exportButton!: IToolbarButton;
  public logoutButton!: IToolbarButton;
  public userButton!: IToolbarButton;

  private _exportSubscription!: Subscription;

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
    this.logoutButton = this._framework.createToolbarButton(
      "logout",
      Guid.create().toString(),
      "Logout",
      () => this.logout(this)
    )

    this.userButton = this._framework.createToolbarButton(
      "account_circle",
      Guid.create().toString(),
      "Account",
      () => this.accountClicked(this)
    )

    this.exportButton = this._framework.createToolbarButton(
      "share",
      Guid.create().toString(),
      "Export",
      () => this.btnExportClicked(this)
    );

  }


  private logout(context: ButtonStoreService)
  {
    context._authService.logout();
  }
  
  private btnExportClicked(context: ButtonStoreService)
  {
    if(context._exportSubscription)
    {
      context._exportSubscription.unsubscribe();
    }

    const dialogref = context._dialog.open(SharedDialogComponent, {
      data:
      {
        icon: "warning",
        title: "Export",
        text: "Do you want to export the database?",
        okButtonText: "Confirm",
        cancelButtonText: "Cancel",
        hasCancelButton: true
      }
    })

    context._exportSubscription = dialogref.componentInstance.onPositiveButtonClicked.subscribe(async() => {
      context._spinner.show("Database is exporting...", new Promise<void>(async(resolve,reject) => {
        await context._exportService.downloadExportedDatabase();
        resolve();
      }));
    })
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