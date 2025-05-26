import axios from 'axios';
import { Course } from '../types';

const BASE_URL = '/api/courses';

export const getCourses = () => axios.get<Course[]>(BASE_URL);
export const getCourse = (id: number) => axios.get<Course>(`${BASE_URL}/${id}`);
export const addCourse = (course: Course) => axios.post(BASE_URL, course);
export const updateCourse = (id: number, course: Course) => axios.put(`${BASE_URL}/${id}`, course);
export const deleteCourse = (id: number) => axios.delete(`${BASE_URL}/${id}`);
