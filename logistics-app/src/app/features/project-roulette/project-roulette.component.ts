import { CommonModule } from '@angular/common';
import { Component, Input, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { AddNameDialogComponent } from '../add-name-dialog/add-name-dialog.component';
import { ProjectStoreService } from '../../services/stores/project-store.service';
import { IProjectData } from '../../models/IProjectData';
import { Guid } from 'guid-typescript';
import { LoadingSpinnerService } from '../../services/loading-spinner.service';
import { FrameworkService } from '../../services/framework.service';
import { ButtonStoreService } from '../../services/stores/button-store.service';
import { AuthService } from '../../services/authentication/auth.service';
import { eRole } from '../../models/enum/eRole';
import { SharedDialogComponent } from '../../framework/shared-dialog/shared-dialog.component';
import { IDialogData } from '../../models/IDialogData';

@Component({
  selector: 'project-roulette',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule],
  templateUrl: './project-roulette.component.html',
  styleUrl: './project-roulette.component.scss'
})
export class ProjectRouletteComponent {
  @Input() projects: IProjectData[] = [];
  @Input() sortedBy: 'date' | 'alphabet' = 'date';

  private _dialog: MatDialog = inject(MatDialog);
  private _prjStore: ProjectStoreService = inject(ProjectStoreService);
  private _spinner: LoadingSpinnerService = inject(LoadingSpinnerService);
  private _framework: FrameworkService = inject(FrameworkService);
  private _btnStore: ButtonStoreService = inject(ButtonStoreService);
  private _auth: AuthService = inject(AuthService);


  constructor(){
    
  }

  public getSortedProjects()
  {
    if(this.sortedBy === 'alphabet')
    {
      return this.projects.sort((a,b) => a.projectName.localeCompare(b.projectName))
    }
    
    return this.projects.sort((a, b) => new Date(b.creationDate).getTime() - new Date(a.creationDate).getTime());
  }

  public addProject()
  {
    if(this.isAuthorized())
    {
      const dialogRef = this._dialog.open(AddNameDialogComponent);
  
      dialogRef.afterClosed().subscribe(async(name: string) => {
        if(!!name && name != '')
        {
          this._spinner.show("Project is created...", new Promise<void>(async(resolve, reject) => {
            let item: IProjectData = {
              projectName: name,
              creationDate: new Date(),
              projectGuid: Guid.create().toString()
            }
            await this._prjStore.create(item);
            resolve();
          }));
        }
      })
    }
    else
    {
      var dialogData: IDialogData = {
        icon: 'warning',
        title:"Not authorized!",
        text: "The role " + this._auth.getUserRole() + " isn´t authorized to create a new project!",
        okButtonText: "Close",
        hasCancelButton: false
      }
      this._dialog.open(SharedDialogComponent, {
        data: dialogData
      })
    }
  }

  public loadProject(prj: IProjectData)
  {
    this._spinner.show("Project is loading...", new Promise<void>(async(resolve, reject) => {
      let project: IProjectData | undefined = await this._prjStore.loadProject(prj.projectGuid);
      if(!!project)
      {
        if(!!this._framework.toolbar){
          this._framework.toolbar.subtitle = prj.projectName
        } 
        this._btnStore.boxButton.click();
      }
      resolve();
    }))
  }

  public formatDate(pDate: Date)
  {
    pDate = new Date(pDate);
    return pDate.getFullYear() + "/" + pDate.getMonth() + "/" + pDate.getDate();
  }

  private isAuthorized()
  {
    let role: string = this._auth.getUserRole();

    if(role == eRole.admin || role == eRole.keeper || role == eRole.leader)
    {
      return true;
    }
    return false;
  }
}
