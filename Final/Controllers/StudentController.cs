// Import the required packages
using System.Collections.Generic;
using System.Linq;
using WorkshopDev6B.Models;
using Microsoft.AspNetCore.Mvc;
using WorkshopDev6B.ExtensionsMethods;

// Enter the namespace of the controllers
namespace WorkshopDev6B.Controllers {
    // Give the controller a route
    // [controller] Will be replaced with the name of the controller class (minus the 'Controller' part)
    // This means that [controller] will become Student
    // Accessing the controller will be done through: http://localhost:54554/api/student
    [Route( "api/[controller]" )]
    // The StudentController
    public class StudentController : Controller {
        // We need the context in order to access the data in the database
        private readonly StudyContext context;

        // A constructor is necessary in order to get the context ready
        public StudentController( StudyContext context ) {
            this.context = context;
        }

        /// <summary>
        /// We create a function called fillStudentKeys in order to get information from the student
        /// References to foreign key values are always null, this function fills the StudyYear reference
        /// </summary>
        /// <returns>A student with the StudyYear reference filled</returns>
        /// <param name="student">A student</param>
        private Student FillStudentKeys( Student student ) {
            student.StudyYear = this.context.StudyYears
                .Where( year => year.StudyYearId == student.StudyYearId )
                .FirstOrDefault();

            // Return the instance of student where the StudyYear has been filled
            return student;
        }

        /// <summary>
        /// Get Request: /api/student/
        /// Get all the students in the database
        /// </summary>
        /// <returns>A list of students in the database, return an empty list of none are present</returns>
        [HttpGet]
        public IActionResult Get() {
            // Save all the students in a list
            List<Student> students = this.context.Students.ToList();

            // Loop over the students using a for loop
            for ( int index = 0; index < students.Count(); index = index + 1 ) {
                // Foreach students, get the studyyear information
                students[index] = this.FillStudentKeys( students[index] );
            }

            // Return the list of students (StatusCode: 200)
            return Ok( students );
        }

        /// <summary>
        /// Get Request: /api/student/paged/0
        /// Get all the students using the pagination function
        /// </summary>
        /// <returns>A page with the requested students</returns>
        /// <param name="pageIndex">The index of the page that is supposed to be returned</param>
        [HttpGet( "Paged/{pageIndex}" )]
        public IActionResult Paged( int pageIndex ) {
            // Get the requested page
            Page<Student> page = this.context.Students.GetPage( pageIndex, 5, student => student.StudentId );

            // Check whether the page contains data
            if (page == null) {
                // If not, return NotFound (StatusCode: 404)
                return NotFound();
            }

            // Return the page (StatusCode: 200)
            return Ok( page );
        }

        /// <summary>
        /// Get Request: /api/student/1
        /// Get the details of a specific student
        /// </summary>
        /// <returns>Return the requested student, returns NotFound otherwise</returns>
        /// <param name="id">The id of the student requested</param>
        [HttpGet( "{id}" )]
        public IActionResult Get( int id ) {
            // Search for the student in the database
            Student student = this.context.Students
                .Where( currentStudent => currentStudent.StudentId == id )
                .FirstOrDefault();

            // Check whether we found the student
            if ( student != null ) {
                // If we did, fill the information for the student
                student = this.FillStudentKeys( student );

                // Return the student (StatusCode: 200)
                return Ok( student );
            }

            // Return NotFound (StatusCode: 404)
            return NotFound();
        }

        /// <summary>
        /// Post Request: /api/student/
        /// Will create a new instance of student with the data provided in the request
        /// The data has to be send using JSON
        /// </summary>
        /// <returns>Return the status of the request</returns>
        /// <param name="newStudent">The data that has been provided in the body, converted to a student</param>
        [HttpPost]
        public IActionResult Post( [FromBody] Student newStudent ) {
            // Check whether a student is present
            if ( newStudent == null || !ModelState.IsValid ) {
                // If not, return a BadRequest (StatusCode: 400)
                return BadRequest();
            }

            // Add the student to the database
            this.context.Students.Add( newStudent );

            // Save the changes to the databse
            this.context.SaveChanges();

            // Return an all clear (StatusCode: 200)
            return Ok();
        }

        /// <summary>
        /// Put Request: /api/student/1
        /// </summary>
        /// <returns>Return the status of the request</returns>
        /// <param name="id">The id of the student that has to be updated</param>
        /// <param name="updatedStudent">The information of the updated student</param>
        [HttpPut( "{id}" )]
        public IActionResult Put( int id, [FromBody] Student updatedStudent ) {
            // Check whether there is a student present
            if ( updatedStudent != null && ModelState.IsValid ) {
                // If so, try to get the student from the database
                Student student = this.context.Students
                    .Where( currentStudent => currentStudent.StudentId == id )
                    .FirstOrDefault();

                // Check whether the student has been found
                if ( student != null ) {
                    // Change the information of the student
                    student.FirstName = updatedStudent.FirstName;
                    student.LastName = updatedStudent.LastName;
                    student.StudyYearId = updatedStudent.StudyYearId;

                    // Update the student in the database
                    this.context.Students.Update( student );

                    // Save the changes to the database
                    this.context.SaveChanges();

                    // Return an all clear (StatusCode: 200)
                    return Ok();
                }

                // The student hasn't been found (StatusCode: 404)
                return NotFound();
            }

            // The student is invalid / not present (StatusCode: 400)
            return BadRequest();
        }

        /// <summary>
        /// Delete Request: /api/student/3
        /// Delete a student from the database
        /// </summary>
        /// <returns>Return the status code of the request</returns>
        /// <param name="id">The id of the student that has to be deleted</param>
        [HttpDelete( "{id}" )]
        public IActionResult Delete( int id ) {
            // Try to find the requested student
            Student student = this.context.Students
                .Where( currentStudent => currentStudent.StudentId == id )
                .FirstOrDefault();

            // Check whether the student has been found
            if ( student != null ) {
                // If so, remove the student from the database
                this.context.Students.Remove( student );

                // Save the changes to the database
                this.context.SaveChanges();

                // Return an all clear (StatusCode: 200)
                return Ok();
            }

            // Return a NotFound(StatusCode: 400)
            return NotFound();
        }
    }
}
