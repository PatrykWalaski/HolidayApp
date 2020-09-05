import { Component, OnInit } from '@angular/core';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/user';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
  providers: [{ provide: BsDropdownConfig, useValue: { isAnimated: true, autoClose: true } }]
})
export class NavBarComponent implements OnInit {
  currentUser$: Observable<IUser>;

  constructor(private accountService: AccountService) { 
  }

  ngOnInit() {
    console.log('init user');
    this.currentUser$ = this.accountService.currentUser$;
    console.log(this.currentUser$);
  }

  logout() {
    this.accountService.logout();
  }
}
