import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'client';

  constructor(private router: Router) {}

  ngOnInit(): void {
  }
  
  isServerError(): boolean{
    return this.router.url === '/server-error'
  }
}
