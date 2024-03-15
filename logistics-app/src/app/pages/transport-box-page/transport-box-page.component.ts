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
import { MatDialog } from '@angular/material/dialog';
import { AddBoxSelectionListComponent } from '../../features/add-box-selection-list/add-box-selection-list.component';
import { SharedDialogComponent } from '../../framework/shared-dialog/shared-dialog.component';
import { IProjectData } from '../../models/IProjectData';
import { AuthService } from '../../services/authentication/auth.service';
import { eRole } from '../../models/enum/eRole';
import { AddArticleSelectionListComponent } from '../../features/add-article-selection-list/add-article-selection-list.component';
import { Guid } from 'guid-typescript';
import { eArticleState } from '../../models/enum/eArticleState';

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
  private _dialog: MatDialog = inject(MatDialog);
  private _auth: AuthService = inject(AuthService);

  private _selectedBox: ITransportBoxData | undefined;
  public get selectedBox(): ITransportBoxData | undefined
  {
    return this._selectedBox;
  }

  public set selectedBox(box: ITransportBoxData | undefined)
  {
    this._selectedBox = box;
  }

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
    if(!!this._selectedBox)
    {
      return this._logisticStore.articleStore.getArticlesForBox(this._selectedBox.boxGuid);
    }
    return [];
  }

  public isSearching: boolean = false;
  public searchingValue: string = "";

  private _searchTextChanged = new Subject<string>();

  private _subscription!: Subscription;
  private _dialogSubscription!: Subscription;
  private _dialogArtSubscription!: Subscription;

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
        this._pdfService.openBase64(pdfByteArray, "application/pdf;base64", "Inventarliste");
        resolve();
      }
      catch(err: any)
      {
        reject();
      }
    }))
  }


  public async addTransportBoxToProject()
  {
    this._spinner.show("Loading boxes...", new Promise<void>(async(res, rej) => {
      var boxes = await this._logisticStore.transportboxStore.getAllBoxesWithoutPrj();
      res();
  
      if(boxes.length > 0)
      {
        const dialogRef = this._dialog.open(AddBoxSelectionListComponent,
          {
            data: boxes
          });

        if(!!this._dialogSubscription)
        {
          this._dialogSubscription.unsubscribe();
        }

        this._dialogSubscription = dialogRef.afterClosed().subscribe((boxList: ITransportBoxData[]) => {
          var prj: IProjectData | undefined = this._logisticStore.projectStore.getLoadedProject();
          if(!!boxList && boxList.length > 0 && !!prj)
          {
            this._spinner.show("Adding boxes...", new Promise<void>(async(res, rej) => {
              for(let i = 0;boxList.length > i;i++)
              {
                let item = boxList[i];
                item.projectGuid = prj!.projectGuid;
                await this._logisticStore.transportboxStore.updateToProject(item);
              }
        
              //load prj because added new boxes
              await this._logisticStore.loadProject(prj!.projectGuid);
              this._transportBoxes = this._logisticStore.transportboxStore.getItems();
              res();
            }))
          }
        })
      }
      else
      {
        this._dialog.open(SharedDialogComponent, {
          data:
          {
            icon: "warning",
            title: "Not Found",
            text: "No box without project exists!",
            okButtonText: "Close",
          }
        })
      }
    }));
  }

  public isAuthorized(): boolean
  {
    let role: string = this._auth.getUserRole();

    if(role == eRole.admin || role == eRole.keeper || role == eRole.leader)
    {
      return true;
    }
    return false;
  }

  public addArticlesToBox()
  {
    this._spinner.show("Loading article...", new Promise<void>(async(res, rej) => {
      var arts = await this._logisticStore.articleStore.getAllBaseArticles();
      res();
  
      if(arts.length > 0)
      {
        const dialogRef = this._dialog.open(AddArticleSelectionListComponent,
          {
            data: arts
          });

        if(!!this._dialogArtSubscription)
        {
          this._dialogArtSubscription.unsubscribe();
        }

        this._dialogArtSubscription = dialogRef.afterClosed().subscribe((artList: IArticleData[]) => {
          if(!!artList && artList.length > 0 && !!this.selectedBox)
          {
            this._spinner.show("Adding articles...", new Promise<void>(async(res, rej) => {
              for(let i = 0;artList.length > i;i++)
              {
                let item: IArticleData = artList[i];
                item.boxGuid = this.selectedBox!.boxGuid;
                item.articleBoxAssignment = Guid.create().toString();
                item.position = 0;
                item.expiryDate = undefined;
                item.status = eArticleState.none;
                item.quantity = 0;
                await this._logisticStore.articleStore.createArtToBox(item);
              }

              this._logisticStore.loadArticleForBox(this._selectedBox!.boxGuid)
              res();
            }))
          }
        })
      }
      else
      {
        this._dialog.open(SharedDialogComponent, {
          data:
          {
            icon: "warning",
            title: "Not Found",
            text: "No article exists!",
            okButtonText: "Close",
          }
        })
      }
    }));
  }
}
