import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentEnrolmentFormComponent } from './student-enrolment-form.component';

describe('StudentEnrolmentFormComponent', () => {
  let component: StudentEnrolmentFormComponent;
  let fixture: ComponentFixture<StudentEnrolmentFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentEnrolmentFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentEnrolmentFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
