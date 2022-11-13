import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, of, switchMap, throwError } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

import { StudentDetails } from '../models/student-details.model';

@Injectable({
  providedIn: 'root'
})
export class StudentDetailsService {

  private url = "StudentDetails"

  constructor(private _httpClient: HttpClient, private _snackBar: MatSnackBar) { }

  public getStudentDetails(): Observable<StudentDetails[]> {
    return this._httpClient.get<StudentDetails[]>(`${environment.apiUrl}/${this.url}`).pipe(
      catchError(this.handleError.bind(this))
    );
  }

  // public getSingleStudentDetail(id: number) : Observable<StudentDetails[]> {
  //   return this.httpClient.get<StudentDetails[]>(`${environment.apiUrl}/${this.url}/`+id);
  // }

  public addStudentDetails(studentDetail: StudentDetails) {
    return this._httpClient.post(`${environment.apiUrl}/${this.url}`, studentDetail).pipe(
      catchError(this.handleError.bind(this))
    );
  }

  public updateStudentDetails(studentDetail: StudentDetails) {
    return this._httpClient.put(`${environment.apiUrl}/${this.url}`, studentDetail).pipe(
      catchError(this.handleError.bind(this))
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      console.error('An error occurred:', error.error.detail);

      this.openSnackBar(error.error.detail)
    }
    // Return an observable with a user-facing error message.
    // The backend returned an unsuccessful response code.
    // The response body may contain clues as to what went wrong.
    return throwError(() => new Error(`Backend returned code ${error.status}`));
  }

  private openSnackBar(message: string) {
    this._snackBar.open(message, 'Cancel', {
      duration: 5000
    });
  }

}
