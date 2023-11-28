import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ManageParams } from './../shared/models/manageParams';
import { Pagination } from '../shared/models/pagination';
import { Project } from '../shared/models/project';

@Injectable({
  providedIn: 'root'
})
export class ManageService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProjects(manageParams: ManageParams){
    let params = new HttpParams();

    if(manageParams.number > 0) params = params.append('number', manageParams.number);
    params = params.append('sort', manageParams.sort);
    params = params.append('pageIndex', manageParams.pageNumber);
    params = params.append('pageSize', manageParams.pageSize);
    if(manageParams.name) params = params.append('name', manageParams.name);
    if(manageParams.customerName) params = params.append('customerName', manageParams.customerName);
    if(manageParams.statusCode) params = params.append('statusCode', manageParams.statusCode);
    if(manageParams.groupLeaderVisa) params = params.append('groupLeaderVisa', manageParams.groupLeaderVisa);

    return this.http.get<Pagination<Project[]>>(this.baseUrl + 'projects', {params});
  }

  getProject(id: number){
    return this.http.get<Project>(this.baseUrl + 'projects/' + id);
  }
}
