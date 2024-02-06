import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { TransportBoxListComponent } from '../../features/transport-box-list/transport-box-list.component';
import { LoginComponent } from '../../features/login/login.component';
import { ProjectStoreService } from '../../services/stores/project-store.service';

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [CommonModule, TransportBoxListComponent, LoginComponent],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.scss'
})
export class HomepageComponent {
  private _projectStore: ProjectStoreService = inject(ProjectStoreService);
  constructor(){
    this._projectStore.loadIntitalData()
  }

}
