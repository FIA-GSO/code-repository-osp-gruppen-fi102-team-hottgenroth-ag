import { ComponentFixture, TestBed } from '@angular/core/testing';
import {HttpClientTestingModule } from '@angular/common/http/testing';
import { ArticleListComponent } from './article-list.component';
import { Guid } from 'guid-typescript';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

const article1 = {
  articleName: "Article1", 
  articleGuid: Guid.create().toString(),
  articleBoxAssignment: Guid.create().toString(),
  boxGuid: Guid.create().toString(),
  description: "Article1 20,10",
  unit: "pcs",
  quantity: 12,
  position: 2.2,
  status: "None",
  expiryDate: new Date()
}
const article3 = 
{
  articleName: "Article3", 
  articleGuid: Guid.create().toString(),
  articleBoxAssignment: Guid.create().toString(),
  boxGuid: Guid.create().toString(),
  description: "Article1 20,10",
  unit: "pcs",
  quantity: 12,
  position: 2.3,
  status: "None",
  expiryDate: new Date()
}
const article2 = 
{
  articleName: "Article21", 
  articleGuid: Guid.create().toString(),
  articleBoxAssignment: Guid.create().toString(),
  boxGuid: Guid.create().toString(),
  description: "Article1 20,10",
  unit: "pcs",
  quantity: 12,
  position: 12,
  status: "None",
  expiryDate: new Date()
}
const article4 = 
{
  articleName: "Article4", 
  articleGuid: Guid.create().toString(),
  articleBoxAssignment: Guid.create().toString(),
  boxGuid: Guid.create().toString(),
  description: "Article1 20,10",
  unit: "pcs",
  quantity: 12,
  position: 2,
  status: "None",
  expiryDate: new Date()
}

describe('ArticleListComponent', () => {
  let component: ArticleListComponent;
  let fixture: ComponentFixture<ArticleListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        ArticleListComponent,
        BrowserAnimationsModule
    ],
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ArticleListComponent);
    component = fixture.componentInstance;
    component.articles = [article2, article1, article3, article4]
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('get first positions', () => {
    expect(component.getAllFirstPositions()).toContain(article4);
  });

  it('get all same positions', () => {
    let listTwoPosition = component.getAllArticlesFromSamePositions(article4)
    expect(listTwoPosition).toBeDefined();
    expect(listTwoPosition.length).toBeGreaterThan(0);
    expect(listTwoPosition).toContain(article1);
    expect(listTwoPosition).toContain(article3);
  });

  it('correctly sorted', () => {
    expect(component.getSortedArticles()[0].position).toEqual(2);
  });

  it('is first position', () => {
    expect(component.isArticleFirstPosition(article4)).toBeTrue();
  });

});
