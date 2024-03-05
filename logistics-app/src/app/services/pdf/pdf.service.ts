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

  public async createPdf(jsonData: any): Promise<any> {
    const url = this._serviceURL;

    var transferObject: IPdfData = { 
      transportbox: this._logisticStore.transportboxStore.getItems(),
      project: this._logisticStore.projectStore.getLoadedProject()
    }

    var allBoxes = this._logisticStore.transportboxStore.getItems()
    var projectInfo = this._logisticStore.projectStore.getLoadedProject()

    console.log(allBoxes)
    console.log(projectInfo)

    try {
      const pdfData = await this._request.post(url, transferObject);
      return pdfData;
    } catch (error) {
      console.error('Error creating PDF:', error);
      throw error;
    }
  }

  public openPdf(byteArray: Uint8Array, fileName: string): void {
    const blob = new Blob([byteArray], { type: 'application/pdf' });
    const url = window.URL.createObjectURL(blob);

    // Öffnen Sie das PDF in einem neuen Browser-Tab
    const pdfWindow = window.open();
    pdfWindow!.location.href = url;

    // Optional: Benennen Sie das PDF um
    pdfWindow!.document.title = fileName;
  }

  // open a csv-file in base64 Format in a new tab (URL will be like "blob:domain/GUID")
  public openBase64(inputString: string, format: string, name: string): void {
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
