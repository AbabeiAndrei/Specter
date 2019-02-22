import { Timesheet } from './timesheet';

export class Report {
    filter: string;
    filterText: string;
    date: Date | string;
    timesheets: Timesheet[];
}
