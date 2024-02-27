import { CommonModule } from '@angular/common';
import { Component, Input, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { AddNameDialogComponent } from '../add-name-dialog/add-name-dialog.component';
import { ProjectStoreService } from '../../services/stores/project-store.service';

@Component({
  selector: 'project-roulette',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule],
  templateUrl: './project-roulette.component.html',
  styleUrl: './project-roulette.component.scss'
})
export class ProjectRouletteComponent {
  @Input() projects: any[] = [];
  @Input() sortedBy: 'date' | 'alphabet' = 'date';

  private _dialog: MatDialog = inject(MatDialog);
  private _prjStore: ProjectStoreService = inject(ProjectStoreService);

  constructor(){
    
  }

  public getSortedProjects()
  {
    if(this.sortedBy === 'alphabet')
    {
      return this.projects.sort((a,b) => a.name.localeCompare(b.name))
    }
    
    return this.projects.sort((a, b) => new Date(b.created).getTime() - new Date(a.created).getTime());
  }

  public addProject()
  {
    const dialogRef = this._dialog.open(AddNameDialogComponent);

    dialogRef.afterClosed().subscribe((name: string) => {
      if(!!name && name != '')
      {
        let d = new Date();
        let item = {
          name: name,
          created: d.getFullYear() + "/" + d.getDate() + "/" + d.getMonth()
        }
  
        this.projects.push(item)
      }
    })

  }
}
