import { Component, inject } from '@angular/core';
import { PdfService } from '../../services/pdf/pdf.service';

@Component({
  selector: 'app-project-page',
  standalone: true,
  imports: [],
  templateUrl: './project-page.component.html',
  styleUrl: './project-page.component.scss'
})
export class ProjectPageComponent {

  private _pdfService: PdfService = inject(PdfService);

  public async pdf() {
    var o = {};
    var s = await this._pdfService.createPdf(o);

    console.log(s)

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
