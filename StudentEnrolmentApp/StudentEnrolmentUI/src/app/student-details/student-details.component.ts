import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { catchError, of } from 'rxjs';
import { Course } from '../models/course-model';
import { StudentDetails } from '../models/student-details.model';
import { CourseService } from '../services/course.service';
import { StudentDetailsService } from '../services/student-details.service';
import { StudentRegistrationFormComponent } from './student-registration-form/student-registration-form.component';

@Component({
  selector: 'app-student-details',
  templateUrl: './student-details.component.html',
  styleUrls: ['./student-details.component.scss']
})
export class StudentDetailsComponent implements OnInit {

  studentDetails: StudentDetails[] = [];
  courses: Course[] = [];
  studentDetailsForRegistrationForm: StudentDetails = new StudentDetails();

  constructor(private _studentDetailsService: StudentDetailsService, private _courseService: CourseService) { }

  ngOnInit(): void {
    this.getStudentDetails();
    this.getCourses();
  }

  passToRegistrationForm(student: any) {
    this.studentDetailsForRegistrationForm = student;
  }

  getStudentDetails() {
    this._studentDetailsService
      .getStudentDetails().subscribe((result) => (this.studentDetails = result,
        console.log("hereee", result)));
  }

  getCourses() {
    this._courseService
      .getCourses()
      .subscribe((result: Course[]) => (this.courses = result, console.log("here", result)));
  }

}
