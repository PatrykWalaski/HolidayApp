import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Client';
  token: string;
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.token = localStorage.getItem('token');
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    this.accountService.loadCurrentUser(this.token).subscribe(() => {
      console.log('loaded user');
    }, error => {
      console.log(error);
    });
  }
}
