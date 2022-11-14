# StudentEnrolmentApp
A student enrolment application made using Angular 14 and .Net 6 

The UI has been kept minimal and intuitive. Material components have been used for forms and tables, bootstrap has been used for placement of objects in the real estate.
Because the UI has student registration and enrolment, we have kept both of them together below the table as opposed to the two being on two different screens/tabs. This is because enrolment requires information from the student details section and if someone is editing a student's enrolment, they need to have all the information in front of them. This can be done in other ways by displaying that information on the enrolment editing page as well but that would have made the things more complicated and would have increased the number of workflow scenarios between different screens. Because this is an MVP and focus is more on the usability along with the time period deadline, the current implementation is more beneficial than a complex UI and backend which will require a lot of refactoring later on.

Comments - 
1) We have not given a delete button on the student table as deleting a main record is not expected. It is either a soft delete or older records are archived.
2) The enrolments are being updated along with the student details in the backend API. As we decided to go ahead with an MVP and the same db structure as given in json, we kept the rest of the things conforming to it. Too much deviance of the API structure from the db workflows can result in a lot of refactoring of the code. This is also in accordance with code smell speculative generality. 

Workflows - 
1) From the front page we can either edit or add a student.
2) While editing a student, if we want to leave it in between and edit another student, we need to cancel the current editing by pressing the cancel button at the bottom.
3) While adding/editing enrolments of a student, we cannot delete all the enrolments. There has to be a minimum of 1 enrolment associated with a student. Since this application looks like a page that will be used by the staff(as we are displaying the student list as well), if they are manually entering details of a student and forget to enter the course enrolment details then that can be a problem. 
Note for point 3: The architecture of the application is kept in a way that even if this is not required we can easily switch to letting them enter just the student details and enrol them on a course at a later stage. 

A few known bugs -
1) Table width on smaller screen size might exceed the screen instead of containing the table inside a container with its own scroll bar.
2) The application has only been tested on chrome
3) When we click on edit in the student details table, the @Input of the child component triggers but it is not triggering form validation somehow. This is a possible issue with angular and we might need to dig a little deeper into it.

