export interface Course {
    id: number;
    title: string;
    description: string;
}

export interface Enrolment {
    studentId: number;
    courseId: number;
}

export interface EnrolmentReportItem {
    courseId: number;
    courseTitle: string;
    studentCount: number;
}
