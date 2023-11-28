import { Component, Input } from '@angular/core';
import { Project } from 'src/app/shared/models/project';

@Component({
  selector: 'app-project-item',
  templateUrl: './project-item.component.html',
  styleUrls: ['./project-item.component.scss']
})
export class ProjectItemComponent {
  @Input() project?: Project;
}
