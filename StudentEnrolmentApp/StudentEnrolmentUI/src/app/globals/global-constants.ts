export class GlobalConstants {
    public static ItemPerPage: number = 50;
    public static PageSize: number[] = [10, 50, 100, 200, 500];
    public static AllowFiltering: boolean = true;

    public static AddSuccessMessage: string = "Student details added successfully";
    public static UpdateSuccessMessage: string = "Student details updated successfully";
    public static EditingEnrolmentsConfirmMessage: string = "You are editing enrolments, are you sure you want to submit";
    public static StudentNotEnroledAlertMessage: string = "Please enrol the student in a course";
    public static EditInprogressMessage: string = "Please cancel the editing in progress to edit new item";
    public static EnrolmentsCantBeNullMessage: string = "Student should be enroled on atleast 1 course.";
    public static CourseAlreadyAddedMessage: string = "You have already added this course";
    public static CancelText: string = "Cancel";
    public static SubmitText: string = "Submit";
    public static UpdateText: string = "Update";
    public static AddText: string = "Add";
    public static DateTimeFormat: string = "yyyy-MM-ddTHH:mm:ss";
    public static Locale: string = "en_US";
    public static LocalStorageVariableName: string = "isEditing";
    

}