import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/shared/models/project';
import { ManageService } from '../manage.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Group } from 'src/app/shared/models/group';
import { Observable, map } from 'rxjs';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.scss']
})
export class ProjectEditComponent implements OnInit {
  groups: Group[] = [];
  showAlert = false;
  memberIds: number[] = [];
  project?: Project;
  members: any[] = [];
  id: number = Number(this.activatedRoute.snapshot.paramMap.get('id'));
  
  statusOptions = [
    {name: 'New', value: 'NEW'},
    {name: 'Planned', value: 'PLA'},
    {name: 'In progress', value: 'INP'},
    {name: 'Finished', value: 'FIN'},
  ];

  updateForm = new FormGroup({
    project_Number: new FormControl(0, [Validators.required, Validators.max(9999)]),
    name: new FormControl('', Validators.required),
    customer: new FormControl('', Validators.required),
    members: new FormControl(''),
    group_Id: new FormControl(0, Validators.min(1)),
    status: new FormControl('NEW', Validators.required),
    start_Date: new FormControl('', Validators.required),
    end_Date: new FormControl('', Validators.required),
  });

  constructor(private manageService: ManageService, private activatedRoute: ActivatedRoute, private router: Router, private datePipe: DatePipe) {}

  ngOnInit(): void {
    this.loadProject();
    this.getGroups();
    this.loadMembers();
    
  }

  loadProject(){
    if(this.id) this.manageService.getProject(this.id).subscribe({
      next: project => {
        this.project = project;
        this.patchFormValues(project);
      },
      error: error => console.log(error)
    });
  }

  loadMembers(){
    if(this.id) this.manageService.getEmployeesByProjectId(this.id).pipe(
      map(employees => employees.map(employee => ({
        display: employee.visa + ': ' + employee.first_Name.toUpperCase() + ' ' + employee.last_Name.toUpperCase(),
        value: employee.id
      })))
    ).subscribe({
      next: members => this.members = members,
      error: error => console.log(error)
    })
  }

  updateProject(formObj: any){
    if(this.id) this.manageService.updateProject(this.id, formObj).subscribe({
      next: () => this.updateMembers(this.memberIds),
      error: error => {
        console.log(error);
      }
    })
  }

  updateMembers(idsArray: number[]){
   if(this.id) this.manageService.updateMembers(this.id, idsArray).subscribe({
      next: () => {
        this.router.navigateByUrl('/manage')
      },
      error: error => {
        console.log(error); 
      }
    })
  }

  patchFormValues(project: Project) {
    this.updateForm.patchValue({
      project_Number: project.project_Number,
      name: project.name,
      customer: project.customer,
      group_Id: project.group,
      status: project.status,
      start_Date: project.start_Date,
      end_Date: project.end_Date
    });

    this.updateForm.get('project_Number')?.disable();
  }

  onCancel(){
    this.router.navigateByUrl('/manage');
  }

  onSubmit(){
    if (this.updateForm.invalid) {
      this.updateForm.markAllAsTouched();
      this.showAlert = true;
      return;
    }

    let updateFormObj = this.updateForm.value;
    updateFormObj.start_Date = this.datePipe.transform(updateFormObj.start_Date, 'yyyy-MM-dd');
    updateFormObj.end_Date = this.datePipe.transform(updateFormObj.end_Date, 'yyyy-MM-dd');

    if (updateFormObj.start_Date != null && updateFormObj.end_Date != null && new Date(updateFormObj.end_Date) < new Date(updateFormObj.start_Date)) {
      this.updateForm.controls['end_Date'].setErrors({ 'isInvalid': true });
      return;
    }

    updateFormObj.group_Id = Number(updateFormObj.group_Id)
    delete updateFormObj.members;
    this.getMemberIds();
    this.updateProject(updateFormObj);
  }

  getGroups(){
    this.manageService.getGroups().subscribe({
      next: response => {
        this.groups = response;
      },
      error: error => console.log(error)
    })
  }

  getMemberIds(){
    const membersValue = this.updateForm.get('members')?.value;
    if (Array.isArray(membersValue)) {
      this.memberIds = membersValue.map((member: any) => member.value);
    }
  }

  dismissAlert(){
    this.showAlert = false;
  }

  onEnterKey(event: any): void {
    event.preventDefault();
  }

  requestAutocompleteItems = (text: string): Observable<any[]> => {
    return this.manageService.getEmployees().pipe(
      map(employees => employees.map(employee => (
        {
          display: employee.visa + ': ' + employee.first_Name.toUpperCase() + ' ' + employee.last_Name.toUpperCase(),
          value: employee.id
        }
      )))
    );
  };
}
