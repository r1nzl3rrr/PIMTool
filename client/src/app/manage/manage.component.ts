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
  @ViewChild('name') name?: ElementRef;
  @ViewChild('number') number?: ElementRef;
  @ViewChild('customerName') customerName?: ElementRef;
  @ViewChild('statusCode') statusCode?: ElementRef;
  @ViewChild('groupLeaderVisa') groupLeaderVisa?: ElementRef;

  projects: Project[] = [];
  manageParams = new ManageParams;
  totalCount = 0;

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

  onSearch() {
    this.manageParams.name = this.name?.nativeElement.value;
    this.manageParams.customerName = this.customerName?.nativeElement.value;
    this.manageParams.statusCode = this.statusCode?.nativeElement.value;
    this.manageParams.groupLeaderVisa = this.groupLeaderVisa?.nativeElement.value;
    this.manageParams.number = +this.number?.nativeElement.value;
    this.manageParams.pageNumber = 1;
    this.getProjects();
  }

  onReset(){
    if(this.name) this.name.nativeElement.value = '';
    if(this.customerName) this.customerName.nativeElement.value = '';
    if(this.statusCode) this.statusCode.nativeElement.value = '';
    if(this.groupLeaderVisa) this.groupLeaderVisa.nativeElement.value = '';
    if(this.number) this.number.nativeElement.value = 0;
    this.manageParams = new ManageParams();
    this.getProjects();
  }
}
