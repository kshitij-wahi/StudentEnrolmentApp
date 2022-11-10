import { Component, Input, OnInit } from '@angular/core';
import { CourseEnrolment } from 'src/app/models/student-details.model';

@Component({
  selector: 'app-student-enrolment-form',
  templateUrl: './student-enrolment-form.component.html',
  styleUrls: ['./student-enrolment-form.component.scss']
})
export class StudentEnrolmentFormComponent implements OnInit {

  @Input() coursesEnrolled: CourseEnrolment[] = [];
  enrolmentModel: CourseEnrolment = new CourseEnrolment();
  
  constructor() { }

  ngOnInit(): void {
  }
  onSubmit(){

  }
}
