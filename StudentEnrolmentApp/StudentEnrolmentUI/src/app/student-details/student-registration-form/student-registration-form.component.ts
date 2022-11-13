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
import { MatSnackBar } from '@angular/material/snack-bar';
import { GlobalConstants } from 'src/app/globals/global-constants';

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
    this.studentDetails = value;
    this.dataSource = new MatTableDataSource<CourseEnrolment>(value.courseEnrolment);
    this.dataSource.paginator = this.paginatorCourseEnrollment;
    this.dataSource.sort = this.sortCourseEnrolment;
    this.enrolmentDetails = new CourseEnrolment();
    this.isUpdate = true;
  };

  @Input() set courseModel(value: Course[]) {
    this.courses = value;
    this.filteredOptions = value;
  };

  @Output() addUpdateCancelNotification = new EventEmitter<string>();

  dataSource!: MatTableDataSource<CourseEnrolment>;
  displayedColumns: string[] = ['courseCode', 'courseName', 'enrolmentStatus', 'occurrence', 'action'];
  @ViewChild(MatPaginator) paginatorCourseEnrollment!: MatPaginator;
  @ViewChild(MatSort) sortCourseEnrolment!: MatSort;

  constructor(private _studentDetailsService: StudentDetailsService,
    private ref: ChangeDetectorRef,
    private _snackBar: MatSnackBar) {

  }

  ngOnInit(): void {
  }

  onSubmit() {
  }

  editEnrolment(data: any) {
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
    var indexOfEnrolmentToUpdate = this.dataSource.data.findIndex(x => x.enrolmentId == this.enrolmentDetails.enrolmentId);

    this.dataSource.data[indexOfEnrolmentToUpdate].enrolmentId = this.enrolmentDetails.enrolmentId;
    this.dataSource.data[indexOfEnrolmentToUpdate].enrolmentStatus = courseEnrolmentForm.value.enrolmentStatus;
    this.dataSource.data[indexOfEnrolmentToUpdate].courseEntryDate = formatDate(courseEnrolmentForm.value.courseEntryDate, GlobalConstants.DateTimeFormat, GlobalConstants.Locale);
    this.dataSource.data[indexOfEnrolmentToUpdate].expectedEndDate = formatDate(courseEnrolmentForm.value.expectedEndDate, GlobalConstants.DateTimeFormat, GlobalConstants.Locale);
    this.dataSource.data[indexOfEnrolmentToUpdate].modeOfAttendance = courseEnrolmentForm.value.modeOfAttendance;
    this.dataSource.data[indexOfEnrolmentToUpdate].occurrence = courseEnrolmentForm.value.occurrence;
    this.dataSource.data[indexOfEnrolmentToUpdate].yearOfStudy = courseEnrolmentForm.value.yearOfStudy;
    this.dataSource.data[indexOfEnrolmentToUpdate].academicYear = courseEnrolmentForm.value.academicYear;

    var courseCode = this.courses.find(x => x.courseName === courseEnrolmentForm.value.course);
    this.dataSource.data[indexOfEnrolmentToUpdate].course.courseCode = courseCode ? courseCode.courseCode : "";
    this.dataSource.data[indexOfEnrolmentToUpdate].course.courseName = courseEnrolmentForm.value.course;

    this.resetCourseEnrolmentForm(courseEnrolmentForm);
  }

  createEnrolmentId() {
    var enrolmentId: any;
    // if it is an update and we already have the enrolment ids
    // in the course enrolment table.
    // studentDetails.studentId will be 0 in case it is a fresh record/"add new student"
    if (this.studentDetails.studentId != 0) {
      enrolmentId = this.dataSource.data.reduce((max, obj) => (Number(max.enrolmentId.split("/")[1]) > Number(obj.enrolmentId.split("/")[1])) ? max : obj).enrolmentId;
      enrolmentId = this.studentDetails.studentId + "/" + String(Number(enrolmentId.split("/")[1]) + 1);
      // return enrolmentId;
    }
    else {
      // if it is an add and the course enrolment table is empty
      // in this case the ids will be populated by the backend apis
      enrolmentId = this.studentDetails.courseEnrolment.length == 0 ? 1 : this.dataSource.data.length + 1
    }
    return enrolmentId;
  }

  addCourseEnrolment(courseEnrolmentForm: NgForm) {
    var courseCode = this.courses.find(x => x.courseName === courseEnrolmentForm.value.course);
    
    // if course already added throw error
    const found = this.studentDetails.courseEnrolment.some(e => e.course.courseCode === newCourse.courseCode);
    if(found){
      alert(GlobalConstants.CourseAlreadyAddedMessage);
      return;
    }

    var newCourse: Course = {
      courseCode: courseCode ? courseCode.courseCode : "",
      courseName: courseEnrolmentForm.value.course
    };

    var courseEntryDate = formatDate(courseEnrolmentForm.value.courseEntryDate, GlobalConstants.DateTimeFormat, GlobalConstants.Locale);
    var expectedEndDate = formatDate(courseEnrolmentForm.value.expectedEndDate, GlobalConstants.DateTimeFormat, GlobalConstants.Locale);

    var newCourseEnrolment: CourseEnrolment = {
      enrolmentId: this.createEnrolmentId().toString(),
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
    this.addUpdateCancelNotification.emit("form cancelled");
  }

  resetCourseEnrolmentForm(courseEnrollmentForm: NgForm) {
    courseEnrollmentForm.reset();
    this.enrolmentDetails = new CourseEnrolment();
    this.isEnrolmentFormUpdate = false;
  }

  submitStudentDetailsConfirmation(studentRegistrationForm: NgForm, courseEnrolmentForm: NgForm) {
    // student should be enrolled before submitting the form
    if (this.studentDetails.courseEnrolment.length != 0) {
      // checking on academiYear whether it is dirty or invalid as it is a required field.
      // we are manually checking this because the form states were not behaving as expected. 
      if (courseEnrolmentForm.form.value.academicYear != "" || courseEnrolmentForm.form.invalid) {
        if (confirm(GlobalConstants.EditingEnrolmentsConfirmMessage)) {
          this.submitStudentDetails(studentRegistrationForm, courseEnrolmentForm);
        }
      } else {
        this.submitStudentDetails(studentRegistrationForm, courseEnrolmentForm);
      }
    }
    else {
      alert(GlobalConstants.StudentNotEnroledAlertMessage);
    }
  }

  submitStudentDetails(studentRegistrationForm: NgForm, courseEnrolmentForm: NgForm) {
    this.studentDetails.firstName = studentRegistrationForm.value.firstName;
    this.studentDetails.lastName = studentRegistrationForm.value.lastName;
    this.studentDetails.knownAs = studentRegistrationForm.value.knownAs;
    this.studentDetails.displayName = studentRegistrationForm.value.displayName;
    this.studentDetails.dateOfBirth = formatDate(studentRegistrationForm.value.dob, GlobalConstants.DateTimeFormat, GlobalConstants.Locale);
    this.studentDetails.gender = studentRegistrationForm.value.gender;
    this.studentDetails.universityEmail = studentRegistrationForm.value.universityEmail;
    // this.studentDetails.networkId = studentRegistrationForm.value.networkId;
    this.studentDetails.homeOrOverseas = studentRegistrationForm.value.homeOrOverseas;
    // this.studentDetails.courseEnrolment = this.dataSource.data;


    if (this.studentDetails.studentId) {
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
      const filterValue = searchString.toLowerCase();

      this.filteredOptions = this.filteredOptions.filter(option => option.courseCode.toLowerCase().includes(filterValue) ||
        option.courseName.toLowerCase().includes(filterValue));
    }
  }

  addStudentDetails(studentRegistrationForm: NgForm, courseEnrolmentForm: NgForm) {
    this._studentDetailsService
      .addStudentDetails(this.studentDetails)
      .subscribe((result) => (
        this.resetAllForms(studentRegistrationForm, courseEnrolmentForm),
        this.openSnackBar(GlobalConstants.AddSuccessMessage),
        this.addUpdateCancelNotification.emit("add successful")));
  }

  updateStudentDetails(studentRegistrationForm: NgForm, courseEnrolmentForm: NgForm) {
    this._studentDetailsService
      .updateStudentDetails(this.studentDetails)
      .subscribe((result) => (
        this.resetAllForms(studentRegistrationForm, courseEnrolmentForm),
        this.openSnackBar(GlobalConstants.UpdateSuccessMessage),
        this.addUpdateCancelNotification.emit("update successful")));
  }

  deleteEnrolment(row: any) {
    if (this.dataSource.data.length > 1) {
      const indx = this.studentDetails.courseEnrolment.findIndex(v => v.enrolmentId === row.enrolmentId);
      this.studentDetails.courseEnrolment.splice(indx, indx >= 0 ? 1 : 0);
      this.dataSource = new MatTableDataSource<CourseEnrolment>(this.studentDetails.courseEnrolment);
      // this.dataSource = new MatTableDataSource<CourseEnrolment>(
      //   this.studentDetails.courseEnrolment.filter((item, index) => item.enrolmentId !== row.enrolmentId));
      // var valueToDelete = this.studentDetails.courseEnrolment.find(x => x.enrolmentId == row.enrolmentId)
      // if (valueToDelete != null) {
      //   this.tempArrayForDelete.push(valueToDelete);
      // }
    }
    else {
      alert(GlobalConstants.EnrolmentsCantBeNullMessage);
    }

  }

  openSnackBar(message: string) {
    this._snackBar.open(message, 'Cancel', {
      duration: 5000
    });
  }


}
