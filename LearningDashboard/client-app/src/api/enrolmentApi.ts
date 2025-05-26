import axios from 'axios';
import { Enrolment, EnrolmentReportItem } from '../types';

const BASE_URL = '/api/enrolments';

export const getEnrolments = () => axios.get<Enrolment[]>(BASE_URL);
export const getByStudent = (studentId: number) => axios.get<Enrolment[]>(`${BASE_URL}/student/${studentId}`);
export const getByCourse = (courseId: number) => axios.get<Enrolment[]>(`${BASE_URL}/course/${courseId}`);
export const enrolStudent = (enrolment: Enrolment) => axios.post(BASE_URL, enrolment);
export const unenrolStudent = (enrolment: Enrolment) => axios.delete(BASE_URL, { data: enrolment });
export const getReport = () => axios.get<EnrolmentReportItem[]>(`${BASE_URL}/report`);
