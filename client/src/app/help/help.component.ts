import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-help',
  templateUrl: './help.component.html',
  styleUrls: ['./help.component.scss']
})
export class HelpComponent {
  baseUrl = environment.apiUrl;
  validationErrors: string[] = [];

  constructor(private http: HttpClient) {}

  get404Error(){
    this.http.get(this.baseUrl + 'projects/50').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
      
    })
  }

  get500Error(){
    this.http.get(this.baseUrl + 'errortests/servererror').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
      
    })
  }

  get400Error(){
    this.http.get(this.baseUrl + 'errortests/badrequest').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
      
    })
  }

  get400ValidationError(){
    this.http.get(this.baseUrl + 'projects/fifty').subscribe({
      next: response => console.log(response),
      error: error => {
        console.log(error);
        this.validationErrors = error.errors;
      }
      
    })
  }
}
