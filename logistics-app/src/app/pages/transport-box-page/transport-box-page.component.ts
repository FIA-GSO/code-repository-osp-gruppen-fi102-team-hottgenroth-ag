import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { TransportBoxListComponent } from '../../features/transport-box-list/transport-box-list.component';
import { MatCardModule } from '@angular/material/card';
import {MatTabsModule} from '@angular/material/tabs';
import { TransportBoxDetailsComponent } from '../../features/transport-box-details/transport-box-details.component';
import { ITransportBoxData } from '../../models/ITransportBoxData';
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';
import { ArticleListComponent } from '../../features/article-list/article-list.component';
import { IArticleData } from '../../models/IArticleData';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Subject, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { PdfService } from '../../services/pdf/pdf.service';
import { LoadingSpinnerService } from '../../services/loading-spinner.service';

@Component({
  selector: 'app-transport-box-page',
  standalone: true,
  imports: [CommonModule, TransportBoxListComponent, MatCardModule, 
    MatTabsModule, TransportBoxDetailsComponent, ArticleListComponent,
  MatButtonModule, MatIconModule, MatToolbarModule, MatFormFieldModule, MatInputModule],
  templateUrl: './transport-box-page.component.html',
  styleUrl: './transport-box-page.component.scss'
})
export class TransportBoxPageComponent {
  private _logisticStore: LogisticsStoreService = inject(LogisticsStoreService);
  private _pdfService: PdfService = inject(PdfService);
  private _spinner: LoadingSpinnerService = inject(LoadingSpinnerService);

  public selectedBox: ITransportBoxData | undefined;

  private _transportBoxes!: ITransportBoxData[];
  public get transportBoxes(): ITransportBoxData[]
  {
    if(this.searchingValue != "")
    {
      return this._transportBoxes.filter(box => box.boxCategory.toUpperCase().includes(this.searchingValue.toUpperCase()));
    }
    return this._transportBoxes;
  }

  public get articles(): IArticleData[]
  {
    if(!!this.selectedBox)
    {
      return this._logisticStore.articleStore.getArticlesForBox(this.selectedBox.boxGuid);
    }
    return [];
  }

  public isSearching: boolean = false;
  public searchingValue: string = "";

  private _searchTextChanged = new Subject<string>();

  private _subscription!: Subscription;
  constructor()
  {
    this._transportBoxes = this._logisticStore.transportboxStore.getItems();
  }

  public closeSearch(): void
  {
    this.isSearching = false;
    this.searchingValue = "";

    if(!!this._subscription)
    {
      this._subscription.unsubscribe();
    }
  }

  public searchWithString(pValue: string)
  {
    this._searchTextChanged.next(pValue);
  }

  public openSearch()
  {
    this.isSearching = true;

    this._subscription = this._searchTextChanged
    .pipe(debounceTime(300))
    .pipe(distinctUntilChanged())
    .subscribe((searchValue: string) => {
      console.log(searchValue)
      this.searchingValue = searchValue;
    });
  }

  public printPDF(): void
  {
    this._spinner.show("Pdf is creating...", new Promise<void>(async(resolve, reject) => {
      try
      {
        var pdfByteArray: string = await  this._pdfService.createPdf();
        this._pdfService.openBase64(pdfByteArray, "application/pdf;base64", "Tabelle");
        resolve();
      }
      catch(err: any)
      {
        reject();
      }
    }))
  }
}
