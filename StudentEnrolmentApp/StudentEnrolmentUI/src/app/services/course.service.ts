import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Course } from '../models/course-model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private url = "Courses"

  constructor(private httpClient: HttpClient) { }

  public getCourses() : Observable<Course[]> {
    return this.httpClient.get<Course[]>(`${environment.apiUrl}/${this.url}`);
  }


}
