import { Component, OnInit } from '@angular/core';
import { HolidaysService } from './holidays.service';
import { IHoliday } from '../shared/models/holiday';

@Component({
  selector: 'app-holidays',
  templateUrl: './holidays.component.html',
  styleUrls: ['./holidays.component.scss']
})
export class HolidaysComponent implements OnInit {
  holidays: IHoliday[];

  constructor(private holidaysService: HolidaysService) { }

  ngOnInit() {
    this.getHolidays();
  }

  getHolidays()
  {
    this.holidaysService.getHolidays().subscribe(response => {
      this.holidays = response;
      console.log(this.holidays);
    }, error => {
      console.log(error);
    });
  }
}
