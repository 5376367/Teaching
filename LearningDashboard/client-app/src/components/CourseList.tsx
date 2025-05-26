import React, { useEffect, useState } from 'react';
import { Course } from '../types';
import { getCourses, deleteCourse } from '../api/courseApi';

export default function CourseList() {
    const [courses, setCourses] = useState<Course[]>([]);

    useEffect(() => {
        getCourses().then(res => setCourses(res.data));
    }, []);

    const handleDelete = (id: number) => {
        deleteCourse(id).then(() => setCourses(courses.filter(c => c.id !== id)));
    };

    return (
        <div>
            <h2>Courses</h2>
            <ul>
                {courses.map(course => (
                    <li key={course.id}>
                        {course.title} - {course.description}
                        <button onClick={() => handleDelete(course.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}
