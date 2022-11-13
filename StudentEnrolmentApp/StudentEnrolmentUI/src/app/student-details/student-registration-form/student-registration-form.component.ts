import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { CourseEnrolment, StudentDetails } from 'src/app/models/student-details.model';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Course } from 'src/app/models/course-model';
import { Observable } from 'rxjs';
import { formatDate } from '@angular/common';
import { StudentDetailsService } from 'src/app/services/student-details.service';

@Component({
  selector: 'app-student-registration-form',
  templateUrl: './student-registration-form.component.html',
  styleUrls: ['./student-registration-form.component.scss']
})
export class StudentRegistrationFormComponent implements OnInit {

  isUpdate: boolean = false;
  isEnrolmentFormUpdate: boolean = false;
  enrolmentDetails: CourseEnrolment = new CourseEnrolment();
  studentDetails: StudentDetails = new StudentDetails();
  filteredOptions: Course[] = [];
  courses: Course[] = [];

  @Input() set studentModel(value: StudentDetails) {
    console.log("here", value);
    this.studentDetails = value;
    this.dataSource = new MatTableDataSource<CourseEnrolment>(value.courseEnrolment);
    this.dataSource.paginator = this.paginatorCourseEnrollment;
    this.dataSource.sort = this.sortCourseEnrolment;
    this.enrolmentDetails = new CourseEnrolment();
    this.isUpdate = true;
    console.log(this.isUpdate);
  };

  @Input() set courseModel(value: Course[]) {
    this.courses = value;
    this.filteredOptions = value;
  };

  @Output() addUpdateNotification = new EventEmitter<string>();

  dataSource!: MatTableDataSource<CourseEnrolment>;
  displayedColumns: string[] = ['courseCode', 'courseName', 'enrolmentStatus', 'occurrence', 'action'];
  @ViewChild(MatPaginator) paginatorCourseEnrollment!: MatPaginator;
  @ViewChild(MatSort) sortCourseEnrolment!: MatSort;

  // studentModel: StudentDetails = new StudentDetails();
  // coursesEnrolled: CourseEnrolment[] = this.studentModel.courseEnrolment;

  constructor(private _studentDetailsService: StudentDetailsService, private ref: ChangeDetectorRef) {

  }

  ngOnInit(): void {
    // this.coursesEnrolled = this.studentModel.courseEnrolment;
  }

  onSubmit() {
    console.log(this.studentModel);
  }

  editEnrolment(data: any) {
    console.log(data);
    this.enrolmentDetails = data;
    this.isEnrolmentFormUpdate = true;
    this.ref.detectChanges();
  }

  resetRegistrationTable() {
    this.dataSource = new MatTableDataSource<CourseEnrolment>();
  }

  addUpdateCourseEnrolment(courseEnrolmentForm: NgForm) {
    if (this.isEnrolmentFormUpdate) {
      this.updateCourseEnrolment(courseEnrolmentForm);
    }
    else {
      this.addCourseEnrolment(courseEnrolmentForm);
    }
  }

  updateCourseEnrolment(courseEnrolmentForm: NgForm) {
    var indexOfEnrolmentToUpdate = this.studentDetails.courseEnrolment.findIndex(x => x.enrolmentId == courseEnrolmentForm.value.enrolmentId);
    console.log("courseEnrolmentForm.value", courseEnrolmentForm.value);

    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].enrolmentId = courseEnrolmentForm.value.enrolmentId;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].enrolmentStatus = courseEnrolmentForm.value.enrolmentStatus;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].courseEntryDate = formatDate(courseEnrolmentForm.value.courseEntryDate, 'yyyy-MM-ddTHH:mm:ss', "en_US");
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].expectedEndDate = formatDate(courseEnrolmentForm.value.expectedEndDate, 'yyyy-MM-ddTHH:mm:ss', "en_US");
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].modeOfAttendance = courseEnrolmentForm.value.modeOfAttendance;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].occurrence = courseEnrolmentForm.value.occurrence;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].yearOfStudy = courseEnrolmentForm.value.yearOfStudy;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].academicYear = courseEnrolmentForm.value.academicYear;

    var courseCode = this.courses.find(x => x.courseName === courseEnrolmentForm.value.course);
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].course.courseCode = courseCode ? courseCode.courseCode : "";
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].course.courseName = courseEnrolmentForm.value.course;

    console.log("this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate]", this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate]);
    this.dataSource = new MatTableDataSource<CourseEnrolment>(this.studentDetails.courseEnrolment);
    console.log(courseEnrolmentForm);
    this.resetCourseEnrolmentForm(courseEnrolmentForm);
  }

  createEnrolmentId() {
    var enrolmentId = this.studentDetails.courseEnrolment.reduce((max, obj) => (Number(max.enrolmentId.split("/")[1]) > Number(obj.enrolmentId.split("/")[1])) ? max : obj).enrolmentId;
    enrolmentId = this.studentDetails.studentId + "/" + String(Number(enrolmentId.split("/")[1]) + 1);
    return enrolmentId;
  }

  addCourseEnrolment(courseEnrolmentForm: NgForm) {
    console.log("Add clicked");
    var courseCode = this.courses.find(x => x.courseName === courseEnrolmentForm.value.course);
    var newCourse: Course = {
      courseCode: courseCode ? courseCode.courseCode : "",
      courseName: courseEnrolmentForm.value.course
    };

    var courseEntryDate = formatDate(courseEnrolmentForm.value.courseEntryDate, 'yyyy-MM-ddTHH:mm:ss', "en_US");
    var expectedEndDate = formatDate(courseEnrolmentForm.value.expectedEndDate, 'yyyy-MM-ddTHH:mm:ss', "en_US");
    var enrolmentId = this.studentDetails.courseEnrolment.length == 0 ? 1 : this.studentDetails.courseEnrolment.length + 1
    var newCourseEnrolment: CourseEnrolment = {
      enrolmentId: enrolmentId.toString(),
      enrolmentStatus: courseEnrolmentForm.value.enrolmentStatus,
      courseEntryDate: courseEntryDate,
      expectedEndDate: expectedEndDate,
      modeOfAttendance: courseEnrolmentForm.value.modeOfAttendance,
      occurrence: courseEnrolmentForm.value.occurrence,
      yearOfStudy: courseEnrolmentForm.value.yearOfStudy,
      academicYear: courseEnrolmentForm.value.academicYear,
      course: newCourse
    };
    this.studentDetails.courseEnrolment.push(newCourseEnrolment);
    this.dataSource = new MatTableDataSource<CourseEnrolment>(this.studentDetails.courseEnrolment);
    console.log(this.studentDetails);
    this.resetCourseEnrolmentForm(courseEnrolmentForm);

  }

  resetAllForms(studentRegistrationForm: NgForm, courseEnrolmentForm: NgForm) {
    studentRegistrationForm.reset();
    courseEnrolmentForm.reset();
    this.enrolmentDetails = new CourseEnrolment();
    this.studentDetails = new StudentDetails();
    this.dataSource = new MatTableDataSource<CourseEnrolment>();
    this.isEnrolmentFormUpdate = false;
    this.isUpdate = false;
  }

  resetCourseEnrolmentForm(courseEnrollmentForm: NgForm) {
    courseEnrollmentForm.reset();
    this.enrolmentDetails = new CourseEnrolment();
    this.isEnrolmentFormUpdate = false;
  }

  submitStudentDetailsConfirmation(studentRegistrationForm: NgForm, courseEnrolmentForm: NgForm) {
    console.log(courseEnrolmentForm);
    if (courseEnrolmentForm.form.value.academicYear != "" || courseEnrolmentForm.form.invalid) {
      if (confirm("You are editing enrolments, are you sure you want to submit")) {
        this.submitStudentDetails(studentRegistrationForm, courseEnrolmentForm);
      }
    } else {
      this.submitStudentDetails(studentRegistrationForm, courseEnrolmentForm);
    }

  }

  submitStudentDetails(studentRegistrationForm: NgForm, courseEnrolmentForm: NgForm) {
    this.studentDetails.firstName = studentRegistrationForm.value.firstName;
    this.studentDetails.lastName = studentRegistrationForm.value.lastName;
    this.studentDetails.knownAs = studentRegistrationForm.value.knownAs;
    this.studentDetails.displayName = studentRegistrationForm.value.displayName;
    this.studentDetails.dateOfBirth = formatDate(studentRegistrationForm.value.dob, 'yyyy-MM-ddTHH:mm:ss', "en_US");
    this.studentDetails.gender = studentRegistrationForm.value.gender;
    this.studentDetails.universityEmail = studentRegistrationForm.value.universityEmail;
    // this.studentDetails.networkId = studentRegistrationForm.value.networkId;
    this.studentDetails.homeOrOverseas = studentRegistrationForm.value.homeOrOverseas;
    this.studentDetails.courseEnrolment = this.dataSource.data;

    if (this.isUpdate) {
      this.updateStudentDetails(studentRegistrationForm, courseEnrolmentForm);
    }
    else {
      this.addStudentDetails(studentRegistrationForm, courseEnrolmentForm);
    }
  }

  doFilter(searchString: any) {
    // var filteredModel = this.courseModel.filter(c => c.courseCode === searchString || c.courseName === searchString);
    if (!searchString) this.filteredOptions = this.courses;
    else {
      console.log(searchString);
      const filterValue = searchString.toLowerCase();

      this.filteredOptions = this.filteredOptions.filter(option => option.courseCode.toLowerCase().includes(filterValue) ||
        option.courseName.toLowerCase().includes(filterValue));
    }
  }

  addStudentDetails(studentRegistrationForm: NgForm, courseEnrolmentForm: NgForm) {
    this._studentDetailsService
      .addStudentDetails(this.studentDetails)
      .subscribe((result) => (this.resetAllForms(studentRegistrationForm, courseEnrolmentForm),
        this.addUpdateNotification.emit("add successful")));
  }

  updateStudentDetails(studentRegistrationForm: NgForm, courseEnrolmentForm: NgForm) {
    this._studentDetailsService
      .updateStudentDetails(this.studentDetails)
      .subscribe((result) => (this.resetAllForms(studentRegistrationForm, courseEnrolmentForm),
        this.addUpdateNotification.emit("update successful")));
  }

  deleteEnrolment(row: any) {
    console.log(row);
    console.log(this.studentDetails.courseEnrolment.filter((item, index) => index !== row));
    this.dataSource = new MatTableDataSource<CourseEnrolment>(this.studentDetails.courseEnrolment.filter((item, index) => item.enrolmentId !== row.enrolmentId))

    // this.dataSource.data.splice(index, 1);
    // this.dataSource._updateChangeSubscription();
    // this.dataSource.paginator = this.paginatorCourseEnrollment; 
    // this.dataSource.sort = this.sortCourseEnrolment;
  }
}
