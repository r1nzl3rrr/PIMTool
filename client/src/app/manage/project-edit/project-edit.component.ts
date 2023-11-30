import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/shared/models/project';
import { ManageService } from '../manage.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.scss']
})
export class ProjectEditComponent implements OnInit {
  project?: Project;
  constructor(private manageService: ManageService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadProject();
  }

  loadProject(){
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if(id) this.manageService.getProject(+id).subscribe({
      next: project => this.project = project,
      error: error => console.log(error)
    });
  }
}
