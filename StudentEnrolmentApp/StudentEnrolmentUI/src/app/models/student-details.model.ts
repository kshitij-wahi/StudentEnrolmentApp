import { Course } from "./course-model";

export class StudentDetails {
  studentId: number = 0;
  firstName = "";
  lastName = "";
  knownAs = "";
  displayName = "";
  dateOfBirth = "";
  gender = "";
  universityEmail = "";
  networkId = "";
  homeOrOverseas = "";
  courseEnrolment: CourseEnrolment[] = [];
}

export class CourseEnrolment {
  enrolmentId="";
  academicYear = "";
  yearOfStudy = "";
  occurrence = "";
  modeOfAttendance = "";
  enrolmentStatus = "";
  courseEntryDate = "";
  expectedEndDate = "";
  course: Course = new Course;
}
