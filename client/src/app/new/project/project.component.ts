import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';

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
    members: new FormControl(''),
    group: new FormControl('', Validators.required),
    status: new FormControl('new', Validators.required),
    start_Date: new FormControl('', Validators.required),
    end_Date: new FormControl('', Validators.required),
  })

  constructor(private datePipe: DatePipe){}

  onSubmit(){
    let createFormObj = this.createForm.value;
    if(createFormObj.start_Date) createFormObj.start_Date = this.datePipe.transform(createFormObj.start_Date, 'yyyy-MM-dd');
    if(createFormObj.end_Date) createFormObj.end_Date = this.datePipe.transform(createFormObj.end_Date, 'yyyy-MM-dd');
    delete createFormObj.members;
    console.log(createFormObj);
    this.createForm.reset();
    this.createForm.patchValue({ status: 'new', group: '' });
  }
}


