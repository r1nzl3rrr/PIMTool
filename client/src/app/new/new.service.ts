import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee } from '../shared/models/employee';
import { Group } from '../shared/models/group';

@Injectable({
  providedIn: 'root'
})
export class NewService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getEmployees(){
    return this.http.get<Employee[]>(this.baseUrl + 'employees/all');
  }

  getGroups(){
    return this.http.get<Group[]>(this.baseUrl + 'groups/all');
  }

}
