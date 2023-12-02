import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { Group } from 'src/app/shared/models/group';
import { Employee } from 'src/app/shared/models/employee';
import { NewService } from './../new.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit{

  groups: Group[] = [];
  employees: Employee[] = [];

  statusOptions = [
    {name: 'New', value: 'NEW'},
    {name: 'Planned', value: 'PLA'},
    {name: 'In progress', value: 'INP'},
    {name: 'Finished', value: 'FIN'},
  ];

  createForm = new FormGroup({
    project_Number: new FormControl('', Validators.required),
    name: new FormControl('', Validators.required),
    customer: new FormControl('', Validators.required),
    members: new FormControl(''),
    group: new FormControl(0, Validators.required),
    status: new FormControl('NEW', Validators.required),
    start_Date: new FormControl('', Validators.required),
    end_Date: new FormControl('', Validators.required),
  });

  constructor(private newService: NewService , private datePipe: DatePipe){}

  ngOnInit(): void {
    this.getGroups();
    this.getEmployees();
  }

  onSubmit(){
    let createFormObj = this.createForm.value;
    if(createFormObj.start_Date) createFormObj.start_Date = this.datePipe.transform(createFormObj.start_Date, 'yyyy-MM-dd');
    if(createFormObj.end_Date) createFormObj.end_Date = this.datePipe.transform(createFormObj.end_Date, 'yyyy-MM-dd');
    createFormObj.group = Number(createFormObj.group)
    delete createFormObj.members;
    console.log(createFormObj);
    this.createForm.reset();
    this.createForm.patchValue({ status: 'NEW', group: 0 });
  }

  getGroups(){
    this.newService.getGroups().subscribe({
      next: response => {
        this.groups = response;
      },
      error: error => console.log(error)
    })
  }

  getEmployees(){
    this.newService.getEmployees().subscribe({
      next: response => {
        this.employees = response;
      },
      error: error => console.log(error)
    })
  }
}


