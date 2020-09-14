import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.css']
})
export class PagerComponent implements OnInit {
  @Input() totalCount: number;
  @Input() pageSize: number;
  @Output() pageChanged = new EventEmitter<number>();
  currentPage = 1;
  constructor() { }

  ngOnInit(): void {
  }

  onPagerChanged(event: any) {
    this.pageChanged.emit(event.page);
  }

  setPage(pageNo: number): void {
      this.currentPage = pageNo;
  }
}
