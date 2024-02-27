import { Injectable, inject } from "@angular/core";
import { IToolbarButton } from "../../models/IToolbarButton";
import { Guid } from "guid-typescript";
import { AuthService } from "../authentication/auth.service";
import { FrameworkService } from "../framework.service";
import { INavRailItem } from "../../models/INavRailItem";
import { Router } from "@angular/router";
import { PdfService } from "../pdf/pdf.service";

@Injectable({ providedIn: 'root' })
export class ButtonStoreService
{
  private _authService: AuthService = inject(AuthService);
  private _framework: FrameworkService = inject(FrameworkService);
  private _router: Router = inject(Router);
  private _pdfService: PdfService = inject(PdfService);

  public projectButton!: INavRailItem;
  public boxButton!: INavRailItem;


  public logoutButton!: IToolbarButton;
  public pdfButton!: IToolbarButton;


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

    this.pdfButton = this._framework.createToolbarButton(
      "picture_as_pdf",
      Guid.create().toString(),
      "Print PDF",
      () => this.printPDF(this)
    )
  }


  private logout(context: ButtonStoreService)
  {
    context._authService.logout();
  }

  private prjClicked(context: ButtonStoreService)
  {
    context._router.navigate(["./projects"]);
  }

  private boxClicked(context: ButtonStoreService)
  {
    context._router.navigate(["./transportbox"]);
  }

  private async printPDF(context: ButtonStoreService)
  {
    var o = {};
    var s = await this._pdfService.createPdf(o);

    //this._pdfService.openPdf(s,'test')

    this.openBase64(s, "application/pdf;base64", "test");
  }

    // open a csv-file in base64 Format in a new tab (URL will be like "blob:domain/GUID")
    private openBase64(inputString: string, format: string, name: string): void {
      let byteCharacters: string = atob(inputString);
      let byteNumbers: number[] = new Array(byteCharacters.length);
      console.log(byteCharacters)
      console.log(byteNumbers)
      for (var i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
      }
      let byteArray: Uint8Array = new Uint8Array(byteNumbers);
  
      let file: Blob = new Blob([byteArray], { type: format }); // application/pdf;base64'   ; text/csv;charset=utf-8
  
        this.downloadBlob(file, name);
    }
  
    /**
   * Downloads the file from a blob
   * @param pBlob blob to download
   * @param pFileName name of the downloaded file
   */
    private downloadBlob(pBlob: Blob, pFileName: string) {
      let url = window.URL.createObjectURL(pBlob);
      let a = document.createElement('a');
      document.body.appendChild(a);
      a.setAttribute('style', 'display: none');
      a.href = url;
      a.download = pFileName;
      a.click();
      window.URL.revokeObjectURL(url);
      a.remove();
    }
}