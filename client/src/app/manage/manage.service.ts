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
}
