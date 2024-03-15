import { Injectable, inject } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { RequestService } from '../request/request.service';
import { environment } from '../../../environments/environment';
import { LogisticsStoreService } from '../stores/logistics-store.service';
import { IPdfData } from '../../models/IPdfData';

@Injectable({
  providedIn: 'root'
})
export class PdfService {

  private _request: RequestService = inject(RequestService);
  private readonly _serviceURL = environment.serviceURL + environment.pdfServicePath;
  private _logisticStore: LogisticsStoreService = inject(LogisticsStoreService);

  constructor() {   }

  public async createPdf(): Promise<string> {
    const url = this._serviceURL;

    var transferObject: IPdfData = { 
      transportbox: this._logisticStore.transportboxStore.getItems(),
      project: this._logisticStore.projectStore.getLoadedProject()
    }

    try 
    {
      //WIr erstellen mit den Boxdaten die PDF
      const base64Pdf: string = await this._request.post(url, transferObject);
      return base64Pdf;
    }
    catch (error) 
    {
      console.error('Error creating PDF:', error);
      throw error;
    }
  }

  // Wir öffnen den Base64 string in einem übergebenen Format
  //Zuerst wird der Base64 zu einem Blob umgewandelt und dann gedownloadet
  public openBase64(inputString: string, format: string, name: string): void {
    let byteCharacters: string = atob(inputString);
    let byteNumbers: number[] = new Array(byteCharacters.length);

    for (var i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    let byteArray: Uint8Array = new Uint8Array(byteNumbers);

    let file: Blob = new Blob([byteArray], { type: format });

      this.downloadBlob(file, name);
  }

  //Wir downloaden die PDf über den Browser
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
