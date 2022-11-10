import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { CourseEnrolment, StudentDetails } from 'src/app/models/student-details.model';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Course } from 'src/app/models/course-model';

@Component({
  selector: 'app-student-registration-form',
  templateUrl: './student-registration-form.component.html',
  styleUrls: ['./student-registration-form.component.scss']
})
export class StudentRegistrationFormComponent implements OnInit {

  isUpdate: boolean = false;
  enrolmentDetails: CourseEnrolment = new CourseEnrolment();
  studentDetails: StudentDetails = new StudentDetails();

  @Input() courseModel: Course[] = [];
  @Output() submitForm = new EventEmitter<StudentDetails>();

  @Input() set studentModel(value: StudentDetails) {
    console.log("here", value);
    this.studentDetails = value;
    this.dataSource = new MatTableDataSource<CourseEnrolment>(value.courseEnrolment);
    this.dataSource.paginator = this.paginatorCourseEnrollment;
    this.dataSource.sort = this.sortCourseEnrolment;
    this.enrolmentDetails = new CourseEnrolment();
  };

  dataSource!: MatTableDataSource<CourseEnrolment>;
  displayedColumns: string[] = ['courseCode', 'courseName', 'enrolmentStatus', 'occurrence', 'edit'];

  @ViewChild(MatPaginator, { static: true }) paginatorCourseEnrollment!: MatPaginator;
  @ViewChild(MatSort) sortCourseEnrolment!: MatSort;

  // studentModel: StudentDetails = new StudentDetails();
  // coursesEnrolled: CourseEnrolment[] = this.studentModel.courseEnrolment;


  defaultGender = "female"

  constructor(private ref: ChangeDetectorRef) {

  }

  ngOnInit(): void {
    // this.coursesEnrolled = this.studentModel.courseEnrolment;
  }

  onSubmit() {

    // console.log(this.studentModel);
    console.log(this.studentModel);

  }

  editEnrolment(data: any) {
    console.log(data);
    this.enrolmentDetails = data;
    this.isUpdate = true;
    this.ref.detectChanges();
  }

  resetRegistrationTable() {
    this.dataSource = new MatTableDataSource<CourseEnrolment>();
  }

  addUpdateCourseEnrolment(courseEnrolmentForm: NgForm) {
    if (this.isUpdate) {
      this.updateCourseEnrolment(courseEnrolmentForm);
    }
    else {
      this.addCourseEnrolment(courseEnrolmentForm);
    }
  }

  updateCourseEnrolment(courseEnrolmentForm: NgForm) {
    var indexOfEnrolmentToUpdate = this.studentDetails.courseEnrolment.findIndex(x => x.enrolmentId == courseEnrolmentForm.value.enrolmentId);
    console.log(courseEnrolmentForm.value);

    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].enrolmentId = courseEnrolmentForm.value.enrolmentId;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].enrolmentStatus = courseEnrolmentForm.value.enrolmentStatus;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].courseEntryDate = courseEnrolmentForm.value.courseEntryDate;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].expectedEndDate = courseEnrolmentForm.value.expectedEndDate;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].modeOfAttendance = courseEnrolmentForm.value.modeOfAttendance;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].occurrence = courseEnrolmentForm.value.occurrence;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].yearOfStudy = courseEnrolmentForm.value.yearOfStudy;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].academicYear = courseEnrolmentForm.value.academicYear;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].course.courseCode = courseEnrolmentForm.value.courseCode;
    this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate].course.courseName = courseEnrolmentForm.value.courseName;

    console.log(this.studentDetails.courseEnrolment[indexOfEnrolmentToUpdate]);
    this.dataSource = new MatTableDataSource<CourseEnrolment>(this.studentDetails.courseEnrolment);
    this.isUpdate = false;
    console.log(courseEnrolmentForm);
  }

  createEnrolmentId() {
    var enrolmentId = this.studentDetails.courseEnrolment.reduce((max, obj) => (Number(max.enrolmentId.split("/")[1]) > Number(obj.enrolmentId.split("/")[1])) ? max : obj).enrolmentId;
    enrolmentId = this.studentDetails.studentId + "/" + String(Number(enrolmentId.split("/")[1]) + 1);
    return enrolmentId;
  }

  addCourseEnrolment(courseEnrolmentForm: NgForm) {
    var newCourse: Course = {
      courseCode: courseEnrolmentForm.value.courseCode,
      courseName: courseEnrolmentForm.value.courseName
    };

    var newCourseEnrolment: CourseEnrolment = {
      enrolmentId: this.createEnrolmentId(),
      enrolmentStatus: courseEnrolmentForm.value.enrolmentStatus,
      courseEntryDate: courseEnrolmentForm.value.courseEntryDate,
      expectedEndDate: courseEnrolmentForm.value.expectedEndDate,
      modeOfAttendance: courseEnrolmentForm.value.modeOfAttendance,
      occurrence: courseEnrolmentForm.value.occurrence,
      yearOfStudy: courseEnrolmentForm.value.yearOfStudy,
      academicYear: courseEnrolmentForm.value.academicYear,
      course: newCourse
    };
    this.studentDetails.courseEnrolment.push(newCourseEnrolment);
    this.dataSource = new MatTableDataSource<CourseEnrolment>(this.studentDetails.courseEnrolment);
    console.log(this.studentDetails);
  }

  resetAllForms(studentRegistrationForm: NgForm, courseEnrollmentForm: NgForm) {
    studentRegistrationForm.reset();
    // courseEnrollmentForm.resetForm();
    this.enrolmentDetails = new CourseEnrolment();
    this.studentDetails = new StudentDetails();
    this.isUpdate = false;
  }

  resetCourseEnrolmentForm(courseEnrollmentForm: NgForm) {
    courseEnrollmentForm.reset();
    this.enrolmentDetails = new CourseEnrolment();
    this.isUpdate = false;
  }

}
