// We import all the packages necessary
import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import * as Models from '../Models'
import { Link } from 'react-router-dom';;

// We create a function that loads the students from the database
async function loadStudents(): Promise<Models.Student[]> {
    // Get the students from the database using an HTTP request
    let jsonData = await fetch(
        './api/student/', 
        { 
            method: 'get', 
            credentials: 'include', 
            headers: { 
                'content-type': 'application/json' 
            } 
        }
    );

    // Get the result and convert it from JSON into something useable
    let result = await jsonData.json();

    // Return the result
    return result;
}

// We create a function that allows deletion of a student from the database
async function deleteStudent(studentId:number): Promise<string> {
    // Remove a student from the database using an HTTP request
    let jsonData = await fetch(
        './api/student/' + studentId, 
        { 
            method: 'delete', 
            credentials: 'include', 
            headers: { 
                'content-type': 'application/json' 
            }
        }
    );

    // Get the statustext from the deletion request
    let result = await jsonData.statusText;

    // Return the result in a little package
    return `${result}`;
}

// Create the studentManager, this component will show a list of all student with the ability to delete them
export class StudentManager 
    extends React.Component<RouteComponentProps<{}>, {Students: Models.Student[] | "Loading..."}> {

    // Create a constructor in order to initialize the props and state
    constructor( props: RouteComponentProps<{}> ) {
        // Start by initializing the props
        super( props );

        // Continue with the state
        this.state = { Students: "Loading..." };
    }

    // A function that gets the student from the datase
    // This function is seperated because of multiple calls from different methods
    getStudents() {
        // Load the students and place the result in the then function
        loadStudents().then(
            // Change the state of the component (this forces the component to reload)
            students => this.setState({ ...this.state, Students: students })
        );
    }

    // The component will mount method is called before the component is going to be loaded onto the page
    componentWillMount() {
        // Get the students from the database
        this.getStudents();
    }

    // The delete function deletes a student from the database
    delete(studentId:number) {
        // Delete the student and place the response code into the then function
        deleteStudent(studentId).then(
            // Reload the students
            result => this.getStudents()
        );
    }

    // The render method: this is what displays the component
    public render() {
        // Check whether the students are being loaden
        if ( this.state.Students == "Loading...") {
            // If so, tell the visitor
            return <div>Loading...</div>
        }

        // Return the information of all students
        return <div>
            <h1>Students</h1>

            <br/>

            { /* Start to create a table */ }
            <table className="Table">
                <thead>
                    <tr>
                        { /* Print the headers of the table with some spaces to make it look acceptable */ }
                        <th>Student Id&nbsp;&nbsp;&nbsp;</th>
                        <th>Student Name&nbsp;&nbsp;&nbsp;</th>
                        <th>Student Year&nbsp;&nbsp;&nbsp;</th>
                        <th>Actions&nbsp;&nbsp;&nbsp;</th>
                    </tr>
                </thead>

                <tbody>
                    { /* Loop over the students and print their information */ }
                    { this.state.Students.map((student) => <tr>
                        <td>{student.studentId}&nbsp;&nbsp;&nbsp;</td>
                        <td>{student.firstName} {student.lastName}&nbsp;&nbsp;&nbsp;</td>
                        <td>{student.studyYear.name}&nbsp;&nbsp;&nbsp;</td>
                        <td>
                            { /* Create a button that deletes the student by calling the delete function */ }
                            <button onClick={() => this.delete(student.studentId)}>Delete</button>&nbsp;&nbsp;

                            { /* Create a link to the student component */ }
                            <Link to={ '/student/' + student.studentId } >Bekijken</Link>
                        </td>
                    </tr> )}
                </tbody>
            </table>
        </div>;
    }
}
