import React, { useEffect, useState } from 'react';
import { getReport } from '../api/enrolmentApi';
import { EnrolmentReportItem } from '../types';

export default function EnrolmentReport() {
    const [report, setReport] = useState<EnrolmentReportItem[]>([]);

    useEffect(() => {
        getReport().then(res => setReport(res.data));
    }, []);

    return (
        <div>
            <h2>Enrolment Report</h2>
            <table>
                <thead>
                    <tr>
                        <th>Course</th>
                        <th>Enrolled Students</th>
                    </tr>
                </thead>
                <tbody>
                    {report.map(item => (
                        <tr key={item.courseId}>
                            <td>{item.courseTitle}</td>
                            <td>{item.studentCount}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
