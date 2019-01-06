export class Timesheet {
    id: string;
    internalId: string;
    name: string;
    description: string;
    date: Date | string;
    time: number;
    category: string;
    categoryId: string;
    project: string;
    projectId: string;
    delivery: string;
    deliveryId: string;
    locked: boolean;
}
