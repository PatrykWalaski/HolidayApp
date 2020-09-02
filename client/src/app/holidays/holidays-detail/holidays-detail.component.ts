import { Component, OnInit, Input } from '@angular/core';
import { IHoliday } from '../../shared/models/holiday';
import { HolidaysService } from '../holidays.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-holidays-detail',
  templateUrl: './holidays-detail.component.html',
  styleUrls: ['./holidays-detail.component.css']
})
export class HolidaysDetailComponent implements OnInit {
  holiday: IHoliday;

  constructor(private holidaysService: HolidaysService, private activateRoute: ActivatedRoute) { }

  ngOnInit() {
    this.loadHoliday();
  }

  loadHoliday() {
    this.holidaysService.getHolidayById(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.holiday = response;
    }, error => {
      console.log(error);
    });
  }

  handleDisplayName(displayName: string)
  {
      switch (displayName) {
        case 'BB':
          return 'Breakfast';
          break;
        case 'HB':
          return 'Breakfast and Dinner';
          break;
        case 'FB':
          return 'Three Meals';
          break;
        default:
          return displayName;
          break;
      }
  }

}
