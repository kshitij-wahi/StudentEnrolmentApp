import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

import { StudentDetails } from '../models/student-details.model';

@Injectable({
  providedIn: 'root'
})
export class StudentDetailsService {

  private url = "StudentDetails"

  constructor(private httpClient: HttpClient) { }

  public getStudentDetails() : Observable<StudentDetails[]> {
    return this.httpClient.get<StudentDetails[]>(`${environment.apiUrl}/${this.url}`);
  }

  public getSingleStudentDetail(id: number) : Observable<StudentDetails[]> {
    return this.httpClient.get<StudentDetails[]>(`${environment.apiUrl}/${this.url}/`+id);
  }

  public addStudentDetails(studentDetail: StudentDetails) : Observable<StudentDetails[]> {
    return this.httpClient.post<StudentDetails[]>(`${environment.apiUrl}/${this.url}`, studentDetail);
  }

  public updateStudentDetails(studentDetail: StudentDetails) : Observable<StudentDetails[]> {
    return this.httpClient.put<StudentDetails[]>(`${environment.apiUrl}/${this.url}`, studentDetail);
  }
}
