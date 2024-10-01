import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [courses, setCourses] = useState([]);
    const [newCourse, setNewCourse] = useState({ subject: '', courseNumber: '', description: '' });
    const [searchTerm, setSearchTerm] = useState('');

    useEffect(() => {
        fetchCourses();
    }, []);

    const fetchCourses = async () => {
        const response = await fetch('/api/Course/GetCourses')
            .catch(error => console.error('Error fetching data:', error));

        if (response.ok) {
            const data = await response.json();
            setCourses(data);
        }
    };

    const addCourse = async () => {
        const response = await fetch('/api/Course/AddCourse', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newCourse)
        });
        if (response.ok) {
            fetchCourses();
            setNewCourse({ subject: '', courseNumber: '', description: '' });
        } else {
            const errorData = await response.text();
            alert(`Error adding course: ${errorData || 'Unknown error'}`);
        }
    };

    const deleteCourse = async (id) => {
        const response = await fetch(`/api/Course/DeleteCourse/${id}`, {
            method: 'DELETE'
        });
        if (response.ok) {
            fetchCourses();
        } else {
            alert('Error deleting course');
        }
    };

    const filteredCourses = courses.filter(course =>
        course.description.toLowerCase().includes(searchTerm.toLowerCase())
    );

    return (
        <div>
            <h1>Course Management</h1>

            <div>
                <h2>Add New Course</h2>
                <form onSubmit={(e) => { e.preventDefault(); addCourse(); }}>
                    <label>Subject: </label>
                    <input
                        type="text"
                        value={newCourse.subject}
                        onChange={(e) => setNewCourse({ ...newCourse, subject: e.target.value })}
                        required
                    />
                    <br />
                    <label>Course Number: </label>
                    <input
                        type="text"
                        value={newCourse.courseNumber}
                        onChange={(e) => setNewCourse({ ...newCourse, courseNumber: e.target.value })}
                        required
                    />
                    <br />
                    <label>Description: </label>
                    <input
                        type="text"
                        value={newCourse.description}
                        onChange={(e) => setNewCourse({ ...newCourse, description: e.target.value })}
                        required
                    />
                    <br />
                    <button type="submit">Add Course</button>
                </form>
            </div>

            <div>
                <h2>Search Courses</h2>
                <input
                    type="text"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    placeholder="Search by description..."
                />
            </div>

            <div>
                <h2>Course List</h2>
                {courses.length === 0
                    ? <p>No courses available.</p>
                    : <table className="table table-striped">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Subject</th>
                                <th>Course Number</th>
                                <th>Description</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {filteredCourses.map(course => (
                                <tr key={course.id}>
                                    <td>{course.id}</td>
                                    <td>{course.subject}</td>
                                    <td>{course.courseNumber}</td>
                                    <td>{course.description}</td>
                                    <td>
                                        <button onClick={() => deleteCourse(course.id)}>Delete</button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                }
            </div>
        </div>
    );
}

export default App;