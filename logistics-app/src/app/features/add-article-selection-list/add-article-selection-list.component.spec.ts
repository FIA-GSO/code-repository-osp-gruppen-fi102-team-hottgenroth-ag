import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddArticleSelectionListComponent } from './add-article-selection-list.component';

describe('AddArticleSelectionListComponent', () => {
  let component: AddArticleSelectionListComponent;
  let fixture: ComponentFixture<AddArticleSelectionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddArticleSelectionListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddArticleSelectionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
