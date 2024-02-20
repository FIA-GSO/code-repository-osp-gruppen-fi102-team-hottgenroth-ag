import { Injectable, inject } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { RequestService } from '../request/request.service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PdfService {

  private _request: RequestService = inject(RequestService);
  private readonly _serviceURL = environment.serviceURL + environment.pdfServicePath;

  constructor() { }

  public async createPdf(jsonData: any): Promise<any> {
    const url = this._serviceURL;

    try {
      const pdfData = await this._request.post(url, jsonData);
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
}
