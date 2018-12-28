import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { CategoryService } from 'src/services/category.service';
import { Category } from 'src/models/category';
import { Project } from 'src/models/project';
import { ProjectService } from 'src/services/project.service';
import { Delivery } from 'src/models/delivery';

@Component({
  selector: 'app-timesheet-component',
  templateUrl: './timesheet.component.html',
  styleUrls: ['./timesheet.component.less']
})
export class TimesheetComponent {

  date = new FormControl(new Date());
  
  categoryControl = new FormControl('', [Validators.required]);
  projectControl = new FormControl('', [Validators.required]);
  deliveryControl = new FormControl('', [Validators.required]);
  
  categories: Category[] = [];
  projects: Project[] = [];
  deliveries: Delivery[] = [];
  
  constructor(private categoryService: CategoryService, private projectService: ProjectService) {  }

  ngOnInit() {
    this.categoryService.getAll().subscribe(c => {
      this.categories = c;
    });

    this.projectService.getAll().subscribe(p => {
      this.projects = p;
    })
  }

  setDelivery(project: Project){
    this.deliveries = project.deliveries;
    console.log(project.name);
  }
}