import { Component, OnInit } from '@angular/core';
import { IHoliday } from '../shared/models/holiday';
import { ManageService } from './manage.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.css']
})
export class ManageComponent implements OnInit {
  holidays: IHoliday[];
  holiday: IHoliday;
  headElements = ['Id', 'Hotel Name', 'Country', 'City', 'Price'];

  constructor(private manageService: ManageService, private router: Router, private toastr: ToastrService) { }

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

  deleteHoliday(id: number){  
    event.stopPropagation();
    this.manageService.deleteHoliday(id).subscribe( response => {
      console.log('deleted');
      this.toastr.success('Offer deleted.');
      this.router.navigateByUrl('/manage');
      this.loadHolidays();
    }, error => {
      this.toastr.error('Failed while deleting an offer.');
      console.log(error);
    });
  }

}
