import { IHoliday } from './holiday';

export interface IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IHoliday[];
}