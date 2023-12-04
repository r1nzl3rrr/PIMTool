import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ManageParams } from './../shared/models/manageParams';
import { Pagination } from '../shared/models/pagination';
import { Project } from '../shared/models/project';
import { environment } from 'src/environments/environment.development';
import { Employee } from '../shared/models/employee';
import { Group } from '../shared/models/group';

@Injectable({
  providedIn: 'root'
})
export class ManageService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getProjects(manageParams: ManageParams){
    let params = new HttpParams();
    params = params.append('sort', manageParams.sort);
    params = params.append('pageIndex', manageParams.pageNumber);
    params = params.append('pageSize', manageParams.pageSize);
    params = params.append('statusCode', manageParams.statusCode);
    if(manageParams.search) params = params.append('search', manageParams.search);

    return this.http.get<Pagination<Project[]>>(this.baseUrl + 'projects', {params});
  }

  getProject(id: number){
    return this.http.get<Project>(this.baseUrl + 'projects/' + id);
  }

  deleteProject(id: number){
    return this.http.delete(this.baseUrl + 'projects?projectIds=' + id);
  }

  deleteProjects(idsArray: string){
    return this.http.delete(this.baseUrl + 'projects?projectIds=' + idsArray);
  }

  getEmployees(){
    return this.http.get<Employee[]>(this.baseUrl + 'employees/all');
  }

  getEmployeesByProjectId(id: number){
    return this.http.get<Employee[]>(this.baseUrl + 'projects/getemployees/' + id);
  }

  getGroups(){
    return this.http.get<Group[]>(this.baseUrl + 'groups/all');
  }

  updateMembers(id: number, memIds: number[]){
    return this.http.post(this.baseUrl + 'projects/updatemembers/' + id, memIds);
  }

  updateProject(id: number, values: any){
    return this.http.put(this.baseUrl + 'projects/' + id, values);
  }
}
