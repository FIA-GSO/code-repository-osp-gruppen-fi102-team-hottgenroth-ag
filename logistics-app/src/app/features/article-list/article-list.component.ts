import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { IArticleData } from '../../models/IArticleData';

@Component({
  selector: 'article-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './article-list.component.html',
  styleUrl: './article-list.component.scss'
})
export class ArticleListComponent {
  @Input() articles!: IArticleData;
}
