import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { StudentDetails } from 'src/app/models/student-details.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-student-details-table',
  templateUrl: './student-details-table.component.html',
  styleUrls: ['./student-details-table.component.scss']
})
export class StudentDetailsTableComponent implements OnInit {

  // @Input() studentDetails: StudentDetails[] = [];
  @Output() updateStudent = new EventEmitter<StudentDetails>();

  @Input() set studentDetails(value:StudentDetails[]) {
    console.log(value);
    this.dataSource = new MatTableDataSource<StudentDetails>(value);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  };
  dataSource!: MatTableDataSource<StudentDetails>;

  displayedColumns: string[] = ['studentId', 'firstName', 'lastName', 'displayName', 'dateOfBirth', 'gender', 'universityEmail', 'networkId', 'homeOrOverseas', 'edit'];

  @ViewChild(MatPaginator, {static: true}) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor() { }

  ngOnInit(): void {
    // this.dataSource = new MatTableDataSource<StudentDetails>(this.studentDetails);
    // console.log("here",this.dataSource)
    // console.log("here",this.studentDetails)

  }

  editStudent(student: StudentDetails) {
    console.log("table", student)
    this.updateStudent.emit(student);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

}
