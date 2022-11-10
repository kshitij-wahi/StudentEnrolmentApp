import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentDetailsTableComponent } from './student-details-table.component';

describe('StudentDetailsTableComponent', () => {
  let component: StudentDetailsTableComponent;
  let fixture: ComponentFixture<StudentDetailsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentDetailsTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentDetailsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
