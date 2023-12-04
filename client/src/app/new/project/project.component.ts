import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormControl, FormGroup, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { Group } from 'src/app/shared/models/group';
import { Employee } from 'src/app/shared/models/employee';
import { NewService } from './../new.service';
import { Router } from '@angular/router';
import { Observable, debounceTime, finalize, map, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit{

  groups: Group[] = [];
  showAlert = false;
  memberIds: number[] = [];

  constructor(private newService: NewService, private datePipe: DatePipe, private router: Router){}

  statusOptions = [
    {name: 'New', value: 'NEW'},
    {name: 'Planned', value: 'PLA'},
    {name: 'In progress', value: 'INP'},
    {name: 'Finished', value: 'FIN'},
  ];

  createForm = new FormGroup({
    project_Number: new FormControl('', [Validators.required, Validators.max(9999)], [this.validateNumberNotExisted()]),
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
    // Saving draft to localStorage
    const draft = localStorage.getItem("DRAFT_1");
    if(draft){
      this.createForm.setValue(JSON.parse(draft));
    }
    this.createForm.valueChanges
      .subscribe(value => localStorage.setItem("DRAFT_1", JSON.stringify(value)));
    // Clear the localStorage when exiting application
    window.addEventListener("beforeunload", () => {
      localStorage.clear();
    });
  }

  onCancel(){
    this.router.navigateByUrl('/manage');
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
    this.getMemberIds();
    this.createProject(createFormObj);
  }

  getMemberIds(){
    const membersValue = this.createForm.get('members')?.value;
    if (Array.isArray(membersValue)) {
      this.memberIds = membersValue.map((member: any) => member.value);
    }
  }

  createProject(formObj: any){
    this.newService.createProject(formObj).subscribe({
      next: () => this.addMembers(this.memberIds),
      error: error => {
        console.log(error);
      }
    })
  }

  addMembers(idsArray: number[]){
    this.newService.addMembers(idsArray).subscribe({
      next: () => {
        localStorage.clear();
        this.router.navigateByUrl('/manage')
      },
      error: error => {
        console.log(error); 
      }
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

  requestAutocompleteItems = (text: string): Observable<any[]> => {
    return this.newService.getEmployees().pipe(
      map(employees => employees.map(employee => (
        {
          display: employee.visa + ': ' + employee.first_Name.toUpperCase() + ' ' + employee.last_Name.toUpperCase(),
          value: employee.id
        }
      )))
    );
  };

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

  onEnterKey(event: any): void {
    event.preventDefault();
  }
}


