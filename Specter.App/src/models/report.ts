import { Timesheet } from './timesheet';

export class Report {

    filter: string;
    date: Date | string;
    timesheets: Timesheet[];
}
