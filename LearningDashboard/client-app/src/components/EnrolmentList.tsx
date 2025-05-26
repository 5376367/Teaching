import React, { useEffect, useState } from 'react';
import { Enrolment } from '../types';
import { getEnrolments, unenrolStudent } from '../api/enrolmentApi';

export default function EnrolmentList() {
    const [enrolments, setEnrolments] = useState<Enrolment[]>([]);

    useEffect(() => {
        getEnrolments().then(res => setEnrolments(res.data));
    }, []);

    const handleUnenrol = (studentId: number, courseId: number) => {
        unenrolStudent({ studentId, courseId }).then(() =>
            setEnrolments(enrolments.filter(e => !(e.studentId === studentId && e.courseId === courseId)))
        );
    };

    return (
        <div>
            <h2>Enrolments</h2>
            <ul>
                {enrolments.map((e, index) => (
                    <li key={index}>
                        Student {e.studentId} in Course {e.courseId}
                        <button onClick={() => handleUnenrol(e.studentId, e.courseId)}>Unenrol</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}
