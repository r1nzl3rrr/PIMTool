import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormControl, FormGroup, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { Group } from 'src/app/shared/models/group';
import { Employee } from 'src/app/shared/models/employee';
import { NewService } from './../new.service';
import { Router } from '@angular/router';
import { debounceTime, finalize, map, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit{

  groups: Group[] = [];
  employees: Employee[] = [];
  showAlert = false;
  errors: string[] = [];

  constructor(private newService: NewService, private datePipe: DatePipe, private router: Router){}

  statusOptions = [
    {name: 'New', value: 'NEW'},
    {name: 'Planned', value: 'PLA'},
    {name: 'In progress', value: 'INP'},
    {name: 'Finished', value: 'FIN'},
  ];

  createForm = new FormGroup({
    project_Number: new FormControl('', Validators.required, [this.validateNumberNotExisted()]),
    name: new FormControl('', Validators.required),
    customer: new FormControl('', Validators.required),
    members: new FormControl(''),
    group_Id: new FormControl(0, Validators.min(1)),
    status: new FormControl('NEW', Validators.required),
    start_Date: new FormControl('', Validators.required),
    end_Date: new FormControl('', Validators.required),
  });

  ngOnInit(): void {
    this.getGroups();
    this.getEmployees();
  }

  onSubmit(){
    if (this.createForm.invalid) {
      this.createForm.markAllAsTouched();
      this.showAlert = true;
      return;
    }

    

    let createFormObj = this.createForm.value;
    createFormObj.start_Date = this.datePipe.transform(createFormObj.start_Date, 'yyyy-MM-dd');
    createFormObj.end_Date = this.datePipe.transform(createFormObj.end_Date, 'yyyy-MM-dd');
    if (createFormObj.start_Date != null && createFormObj.end_Date != null && new Date(createFormObj.end_Date) < new Date(createFormObj.start_Date)) {
      this.createForm.controls['end_Date'].setErrors({ 'isInvalid': true });
      return;
    }
    createFormObj.group_Id = Number(createFormObj.group_Id)
    delete createFormObj.members;
    
    this.newService.createProject(createFormObj).subscribe({
      next: () => this.router.navigateByUrl('/manage'),
      error: error => {console.log(error);
      this.errors = error.errors;}
    })
  }

  getGroups(){
    this.newService.getGroups().subscribe({
      next: response => {
        this.groups = response;
      },
      error: error => console.log(error)
    })
  }

  dismissAlert(){
    this.showAlert = false;
  }

  getEmployees(){
    this.newService.getEmployees().subscribe({
      next: response => {
        this.employees = response;
      },
      error: error => console.log(error)
    })
  }

  validateNumberNotExisted(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      return control.valueChanges.pipe(
        debounceTime(1000),
        take(1),
        switchMap(() => {
          return this.newService.checkProjectNumberExists(control.value).pipe(
            map(result => result ? {numberExists: true} : null),
            finalize(() => control.markAsTouched())
          )
        })
      )
    }
  }
}


