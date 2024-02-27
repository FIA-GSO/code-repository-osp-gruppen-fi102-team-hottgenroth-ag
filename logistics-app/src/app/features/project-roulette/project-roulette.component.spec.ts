import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectRouletteComponent } from './project-roulette.component';

describe('ProjectRouletteComponent', () => {
  let component: ProjectRouletteComponent;
  let fixture: ComponentFixture<ProjectRouletteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProjectRouletteComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProjectRouletteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
