import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent {
  createForm = new FormGroup({
    project_Number: new FormControl('', Validators.required),
    name: new FormControl('', Validators.required),
    customer: new FormControl('', Validators.required),
    group: new FormControl('', Validators.required),
    status: new FormControl('', Validators.required),
    startDate: new FormControl('', Validators.required),
    endDate: new FormControl('', Validators.required),
  })
}
