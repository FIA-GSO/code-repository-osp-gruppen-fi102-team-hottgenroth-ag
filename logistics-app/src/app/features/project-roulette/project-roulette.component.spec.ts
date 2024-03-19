import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectRouletteComponent } from './project-roulette.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { IProjectData } from '../../models/IProjectData';
import { Guid } from 'guid-typescript';

const project1: IProjectData = {
  projectGuid: Guid.create().toString(),
  projectName: "Madagaskar",
  creationDate: new Date()
}
const project2: IProjectData = {
  projectGuid: Guid.create().toString(),
  projectName: "Afghanistan",
  creationDate: new Date("2023-03-12")
}
describe('ProjectRouletteComponent', () => {
  let component: ProjectRouletteComponent;
  let fixture: ComponentFixture<ProjectRouletteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ProjectRouletteComponent,
        HttpClientTestingModule,
        BrowserAnimationsModule
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProjectRouletteComponent);
    component = fixture.componentInstance;
    component.projects = [project1, project2]
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('sort right', () => {
    component.sortedBy = 'alphabet';
    const sortedName = component.getSortedProjects();
    expect(sortedName[0]).toEqual(project2);

    component.sortedBy = 'date';
    const sortedDate = component.getSortedProjects();
    expect(sortedDate[0]).toEqual(project1);
  });

  it('format date correctly', () => {
    var date = new Date();
    let correctFormat = date.getFullYear() + "/" + date.getMonth() + "/" + date.getDate();
    expect(component.formatDate(date)).toEqual(correctFormat);
  });

});
