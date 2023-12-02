import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Project } from '../shared/models/project';
import { ManageParams } from '../shared/models/manageParams';
import { ManageService } from './manage.service';


@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.scss']
})
export class ManageComponent implements OnInit{
  @ViewChild('select') selectElement?: ElementRef;
  @ViewChild('search') searchTerm?: ElementRef;

  projects: Project[] = [];
  manageParams = new ManageParams;
  totalCount = 0;
  selectedProjectIds: number[] = [];
  statusOptions = [
    {name: 'New', value: 'NEW'},
    {name: 'Planned', value: 'PLA'},
    {name: 'In progress', value: 'INP'},
    {name: 'Finished', value: 'FIN'},
  ];

  constructor(private manageService: ManageService) {}

  ngOnInit(): void {
    this.getProjects();
  }

  getProjects(){
    this.manageService.getProjects(this.manageParams).subscribe({
      next: response => {
        this.projects = response.data;
        this.manageParams.pageNumber = response.pageIndex;
        this.manageParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      error: error => console.log(error)
    })
  }

  getStatusName(status: string): string {
    const foundStatus = this.statusOptions.find(option => option.value === status);
    return foundStatus ? foundStatus.name : '';
  }

  isNew(status: string): boolean{
    return status === 'NEW';
  }

  onPageChanged(event: any){
    if(this.manageParams.pageNumber !== event){
      this.manageParams.pageNumber = event;
      this.getProjects();
    }
  }

  onStatusSelected(event: any) {
    this.manageParams.statusCode = event.target.value;
    this.manageParams.pageNumber = 1;
    if(this.selectElement) this.selectElement.nativeElement.style.color ='#333333';
    this.getProjects();
  }

  onStatusFocus(){
    if(this.selectElement) this.selectElement.nativeElement.style.color ='#333333';
  }

  onSearch() {
    this.manageParams.search = this.searchTerm?.nativeElement.value;
    this.manageParams.pageNumber = 1;
    this.getProjects();
  }

  onReset(){
    if(this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.manageParams = new ManageParams();
    this.selectedProjectIds = [];
    if(this.selectElement){
      this.selectElement.nativeElement.selectedIndex = 0;
      this.selectElement.nativeElement.style.color ='rgba(0,0,0,0.5)';
    } 
    this.getProjects();
  }

  onProjectSelected(event: any, projectId: number) {
    if (event.target.checked) {
      this.selectedProjectIds.push(projectId);
    } else {
      const index = this.selectedProjectIds.indexOf(projectId);
      if (index > -1) {
        this.selectedProjectIds.splice(index, 1);
      }
    }
  }

  onProjectDelete(id: number){
    this.manageService.deleteProject(id).subscribe({
      next: () => {
        this.getProjects();
      },
      error: error => {
        console.log(error);
      }
    });
  }

  onProjectsDelete(){
    this.manageService.deleteProjects(this.selectedProjectIds.join(',')).subscribe({
      next: () => {
        this.selectedProjectIds = [];
        this.getProjects();
      },
      error: error => {
        console.log(error);
      }
    });
  }
}
