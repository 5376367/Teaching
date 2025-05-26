import React from 'react';
import CourseList from './components/CourseList';
import EnrolmentList from './components/EnrolmentList';
import EnrolmentReport from './components/EnrolmentReport';

function App() {
    return (
        <div style={{ padding: '20px' }}>
            <h1>Learning Dashboard</h1>
            <CourseList />
            <EnrolmentList />
            <EnrolmentReport />
        </div>
    );
}

export default App;
