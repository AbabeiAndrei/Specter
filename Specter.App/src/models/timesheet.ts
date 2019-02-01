export class Timesheet {
    id: string;
    internalId: string;
    name: string;
    description: string;
    created: Date | string;
    date: Date;
    time: number;
    category: string;
    categoryId: string;
    project: string;
    projectId: string;
    delivery: string;
    deliveryId: string;
    locked: boolean;
}
