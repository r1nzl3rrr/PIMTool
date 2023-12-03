import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee } from '../shared/models/employee';
import { Group } from '../shared/models/group';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class NewService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getEmployees(){
    return this.http.get<Employee[]>(this.baseUrl + 'employees/all');
  }

  getGroups(){
    return this.http.get<Group[]>(this.baseUrl + 'groups/all');
  }

  checkProjectNumberExists(number: number){
    return this.http.get<boolean>(this.baseUrl + 'projects/numberexists?number=' + number);
  }

  createProject(values: any){
    return this.http.post(this.baseUrl + 'projects', values);
  }

}
