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
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';

@Component({
  selector: 'project-roulette',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule],
  templateUrl: './project-roulette.component.html',
  styleUrl: './project-roulette.component.scss'
})
export class ProjectRouletteComponent {
  //Diese Werte, werden durch die Parent (einbindende Komponente) übergeben
  @Input() projects: IProjectData[] = [];
  @Input() sortedBy: 'date' | 'alphabet' = 'date';

  private _dialog: MatDialog = inject(MatDialog);
  private _logisticsStore: LogisticsStoreService = inject(LogisticsStoreService);
  private _spinner: LoadingSpinnerService = inject(LoadingSpinnerService);
  private _framework: FrameworkService = inject(FrameworkService);
  private _btnStore: ButtonStoreService = inject(ButtonStoreService);
  private _auth: AuthService = inject(AuthService);


  constructor(){
    
  }

  //Wir Sotieren die Projekte nach alphabet oder Datum
  public getSortedProjects(): IProjectData[]
  {
    if(this.sortedBy === 'alphabet')
    {
      return this.projects.sort((a,b) => a.projectName.localeCompare(b.projectName))
    }
    
    return this.projects.sort((a, b) => new Date(b.creationDate).getTime() - new Date(a.creationDate).getTime());
  }

  public addProject(): void
  {
    //Darf der User ein Projekt erstellen?
    if(this.isAuthorized())
    {
      const dialogRef = this._dialog.open(AddNameDialogComponent);
      //Wir erlauben die Eingabe eines Namen und erstellen daraufhin das Projekt
      dialogRef.afterClosed().subscribe(async(name: string) => {
        if(!!name && name != '')
        {
          this._spinner.show("Project is created...", new Promise<void>(async(resolve, reject) => {
            let item: IProjectData = {
              projectName: name,
              creationDate: new Date(),
              projectGuid: Guid.create().toString()
            }
            await this._logisticsStore.projectStore.create(item);
            resolve();
          }));
        }
      })
    }
    else
    {
      //Der User ist nicht berechtigt und wird darauf hingewiesen
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

  public loadProject(prj: IProjectData): void
  {
    //Wir Laden ein Project und setzen dessen Name in die Toolbar
    this._spinner.show("Project is loading...", new Promise<void>(async(resolve, reject) => {
      //Die Laderoutine wird getriggert
      let project: IProjectData | undefined = await this._logisticsStore.loadProject(prj.projectGuid);
      if(!!project)
      {
        if(!!this._framework.toolbar){
          this._framework.toolbar.subtitle = prj.projectName
        } 
        //Wir leiten über den Button auf die Transportboxseite
        this._btnStore.boxButton.click();
      }
      resolve();
    }))
  }

  //Wir formatieren das Datum auf ein Lesbares Format
  public formatDate(pDate: Date): string
  {
    pDate = new Date(pDate);
    return pDate.getFullYear() + "/" + pDate.getMonth() + "/" + pDate.getDate();
  }

  //WIr überprüfen ob der User die benötigten Rechte hat
  private isAuthorized(): boolean
  {
    let role: string = this._auth.getUserRole();

    if(role == eRole.admin || role == eRole.keeper || role == eRole.leader)
    {
      return true;
    }
    return false;
  }
}
