import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Course } from '../models/course-model';
import { environment } from 'src/environments/environment';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private url = "Courses"

  constructor(private _httpClient: HttpClient, private _snackBar: MatSnackBar) { }

  // public getCourses() : Observable<Course[]> {
  //   return this._httpClient.get<Course[]>(`${environment.apiUrl}/${this.url}`);
  // }

  public getCourses(): Observable<Course[]> {
    return this._httpClient.get<Course[]>(`${environment.apiUrl}/${this.url}`).pipe(
      catchError(this.handleError.bind(this))
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      this.openSnackBar(error.error)
    }
    // Return an observable with a user-facing error message.
    // The backend returned an unsuccessful response code.
    // The response body may contain clues as to what went wrong.
    return throwError(() => new Error(`Backend returned code ${error.status}, body was: ${error.error}`));
  }

  private openSnackBar(message: string) {
    this._snackBar.open(message, 'Cancel', {
      duration: 3000
    });
  }
}
