import { CommonModule } from '@angular/common';
import { Component, Input, inject } from '@angular/core';
import { IArticleData } from '../../models/IArticleData';
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';

@Component({
  selector: 'article-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './article-list.component.html',
  styleUrl: './article-list.component.scss'
})
export class ArticleListComponent {
  @Input() articles!: IArticleData;

  private _logisticStore: LogisticsStoreService = inject(LogisticsStoreService);

  constructor(){
    console.log(this._logisticStore.articleStore.getItems());
  }
}
