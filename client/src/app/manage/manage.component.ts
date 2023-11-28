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
  @ViewChild('search') searchTerm?: ElementRef;

  projects: Project[] = [];
  manageParams = new ManageParams;
  totalCount = 0;
  statusOptions = [
    {name: 'New', value: 'NEW'},
    {name: 'Planned', value: 'PLA'},
    {name: 'In progress', value: 'INP'},
    {name: 'Finished', value: 'FIN'},
  ]

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

  onPageChanged(event: any){
    if(this.manageParams.pageNumber !== event){
      this.manageParams.pageNumber = event;
      this.getProjects();
    }
  }

  onStatusSelected(event: any) {
    this.manageParams.statusCode = event.target.value;
    this.getProjects();
  }

  onSearch() {
    this.manageParams.name = this.searchTerm?.nativeElement.value;
    this.manageParams.customerName = this.searchTerm?.nativeElement.value;
    this.manageParams.groupLeaderVisa = this.searchTerm?.nativeElement.value;
    this.manageParams.number = +this.searchTerm?.nativeElement.value;
    this.manageParams.pageNumber = 1;
    this.getProjects();
  }

  onReset(){
    if(this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.manageParams = new ManageParams();
    this.getProjects();
  }
}
