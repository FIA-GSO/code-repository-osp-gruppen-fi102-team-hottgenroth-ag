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
import { AuthService } from '../../services/authentication/auth.service';
import { eRole } from '../../models/enum/eRole';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { ArticleDialogComponent } from '../../features/article-dialog/article-dialog.component';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-transport-box-page',
  standalone: true,
  imports: [CommonModule, TransportBoxListComponent, MatCardModule, 
    MatTabsModule, TransportBoxDetailsComponent, ArticleListComponent,
  MatButtonModule, MatIconModule],
  templateUrl: './transport-box-page.component.html',
  styleUrl: './transport-box-page.component.scss'
})
export class TransportBoxPageComponent {
  private _logisticStore: LogisticsStoreService = inject(LogisticsStoreService);

  public selectedBox: ITransportBoxData | undefined;

  public get transportBoxes(): ITransportBoxData[]
  {
    return this._logisticStore.transportboxStore.getItems();
  }

  public get articles(): IArticleData[]
  {
    if(!!this.selectedBox)
    {
      return this._logisticStore.articleStore.getArticlesForBox(this.selectedBox.boxGuid);
    }
    return [];
  }

}
