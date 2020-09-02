import { Component, OnInit } from '@angular/core';
import { IHoliday } from '../shared/models/holiday';
import { ManageService } from './manage.service';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.css']
})
export class ManageComponent implements OnInit {
  holidays: IHoliday[];
  holiday: IHoliday;
  headElements = ['Id', 'Hotel Name', 'Country', 'City', 'Price'];

  constructor(private manageService: ManageService) { }

  ngOnInit() {
    this.loadHolidays();
  }

  loadHolidays(){
    this.manageService.getHolidays().subscribe( response => {
      this.holidays = response;
    }, error => {
      console.log(error);
    });
  }

}
