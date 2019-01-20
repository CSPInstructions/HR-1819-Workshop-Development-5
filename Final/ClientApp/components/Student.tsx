// We import the stuff that we need
import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import * as Models from '../Models';

// We start by creating a loadStudent function that gets a student from the database
async function loadStudent(StudentId: number): Promise<Models.Student> {
    // Make a request to the database and store the response
    let jsonData = await fetch( 
        './api/student/' + StudentId, 

        { 
            method: 'get', 
            credentials: 'include', 
            headers: { 
                'content-type': 'application/json' 
            }
        }
    );

    // Convert the data from JSON to something usable
    let result = jsonData.json()

    // Return the result
    return result;
}

// Create a class that will represent the student component
// We need a single prop (this comes from the URL), which is the id of the student that we are displaying
// We also need a student in our state, in order to display it's data
export class Student 
    extends React.Component<RouteComponentProps<{StudentId: string}>, {Student: Models.Student | "Loading..."}> {

    // The constructor of the class, we need this in order to initialize the props and state
    constructor( props: RouteComponentProps<{StudentId: string}>) {
        // Start by initializing the props
        super(props);

        // Continue with the state
        this.state = { Student: "Loading..."};
    }

    // The component will mount method is called before the component is going to be loaded onto the page
    componentWillMount() {
        // We call the load student function and use it's result in a follow-up method
        loadStudent( +this.props.match.params.StudentId ).then(
            // Change the state of the component (this forces the component to reload)
            student => this.setState({ ...this.state, Student: student })
        )
    }

    // The render method: this is what displays the component
    public render() {
        // Check whether data is still being loaded
        if ( this.state.Student == "Loading..." ) {
            // If so, return loading
            return <div>Loading...</div>
        }

        // If not, return the data of the student
        return <div>
            <h1>{ this.state.Student.firstName } { this.state.Student.lastName}</h1>
            <h2>{ this.state.Student.studyYear.name }</h2>
        </div>
    }
}