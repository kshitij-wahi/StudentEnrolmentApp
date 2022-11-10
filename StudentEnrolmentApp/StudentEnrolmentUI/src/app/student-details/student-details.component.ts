import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
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
  isUpdate: boolean = false;
  studentDetailsForRegistrationForm: StudentDetails = new StudentDetails();

  constructor(private studentDetailsService: StudentDetailsService, private courseService: CourseService) { }

  ngOnInit(): void {

  }

  passToRegistrationForm(student: any) {
    this.studentDetailsForRegistrationForm = student;
    this.isUpdate = true;
  }

  submitRegistrationForm(student: any) {
    if (this.isUpdate) {
      this.addStudentDetails(student);
    }
    else {
      this.updateStudentDetails(student);
    }
  }

  getStudentDetails(){
    this.studentDetailsService
      .getStudentDetails()
      .subscribe((result: StudentDetails[]) => (this.studentDetails = result, console.log("here",result)));
  }

  getCourses(){
    this.courseService
      .getCourses()
      .subscribe((result: Course[]) => (this.courses = result, console.log("here",result)));
  }

  addStudentDetails(student: any) {
    this.studentDetailsService
      .addStudentDetails(student)
      .subscribe((result: StudentDetails[]) => (this.studentDetails = result));
  }

  updateStudentDetails(student: any) {
    this.studentDetailsService
      .updateStudentDetails(student)
      .subscribe((result: StudentDetails[]) => (this.studentDetails = result));
  }
}
