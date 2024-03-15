import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { MatDialog } from '@angular/material/dialog';
import { RequestService } from '../request/request.service';
import { SharedDialogComponent } from '../../framework/shared-dialog/shared-dialog.component';
import { IDialogData } from '../../models/IDialogData';



@Injectable({
  providedIn: 'root'
})
export class ExportService 
{
  private _dialog: MatDialog = inject(MatDialog);
  private _request: RequestService = inject(RequestService);
  
  private _exportDatabaseUrl: string = environment.serviceURL + environment.exportServicePath;

  //Wir exportieren die DB und bekommen das Ergebnis als Blob
  private async exportCatalogDatabaseAsBlob(): Promise<any>
  {
    return await this._request.getBlob(this._exportDatabaseUrl);
  }

  public async downloadExportedDatabase(): Promise<void>
  {
    try
    {
      //Wir exportieren die DB und bekommen das Ergebnis als Blob
      var result: Blob | undefined = await this.exportCatalogDatabase();

      if (!!result)
      {
        //Wir downloaden den Blob unter dem Namen logisticsDB.sqlite
        this.downloadBlob(result, "logisticsDB.sqlite");
      }
    }
    catch (error)
    {
      console.log(error);
    }
  }

  private async exportCatalogDatabase(): Promise<Blob | undefined>
  {
    try
    {
      var result: Blob | undefined = undefined;

      //Wir kriegen die exportierte DB als Blob
      var blob: Blob | undefined = await this.exportCatalogDatabaseAsBlob();
      //Ist das Ergebnis wirklich ein Blob?
      if (!!blob && blob instanceof Blob)
      {
        result = blob;
      }
      else
      {
        throw Error("blob not instance of Blob or undefined");
      }
      
      return result;
    }
    catch(error)
    {
      //Export fehlgeschlagen, informiere user
      if (error instanceof Error)
      {
        console.log(error.message)
        let dialogData: IDialogData = 
        {
          icon: "warning",
          title: "Export failed!",
          text: "The export of the database failed!",
          okButtonText: "Close",
          hasCancelButton: false
        }
        this._dialog.open(SharedDialogComponent, {
          data: dialogData
        })
      }
      return undefined;
    }
  }

  //Wir downloaden die Datenbank im Browser
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
