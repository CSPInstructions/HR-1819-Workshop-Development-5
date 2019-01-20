using System.Collections.Generic;
using System.Linq;
using WorkshopDev6B.Models;
using Microsoft.AspNetCore.Mvc;

namespace WorkshopDev6B.Controllers {
    [Route( "api/[controller]" )]
    public class AssignmentController : Controller {
        StudyContext context;

        public AssignmentController( StudyContext context ) {
            this.context = context;
        }

        private Assignment fillAssignmentKeys( Assignment assignment ) {
            assignment.StudyYear = this.context.StudyYears
                .Where( year => year.StudyYearId == assignment.StudyYearId )
                .FirstOrDefault();

            assignment.Deliveries = this.context.Grades
                .Where( grade => grade.AssignmentId == assignment.AssignmentId )
                .ToList();

            return assignment;
        }

        [HttpGet]
        // GET api/assignment
        public IActionResult Get() {
            List<Assignment> assignments = this.context.Assignments.ToList();

            for ( int index = 0; index < assignments.Count(); index = index + 1 ) {
                assignments[index] = this.fillAssignmentKeys( assignments[index] );
            }

            return Ok( assignments );
        }

        [HttpGet( "{id}" )]
        // GET api/assignment/3
        public IActionResult Get( int id ) {
            Assignment assignment = this.context.Assignments
                .Where( currentAssignment => currentAssignment.AssignmentId == id )
                .FirstOrDefault();

            if ( assignment != null ) {
                assignment = this.fillAssignmentKeys( assignment );
                return Ok( assignment );
            }

            return NotFound();
        }


        [HttpPost]
        // POST api/assignment
        public IActionResult Post( [FromBody] Assignment newAssignment ) {
            if ( newAssignment == null || !ModelState.IsValid ) {
                return BadRequest();
            }

            this.context.Assignments.Add( newAssignment );
            this.context.SaveChanges();

            return Ok();
        }

        [HttpPut( "{id}" )]
        // PUT api/assignment/3
        public IActionResult Put( int id, [FromBody] Assignment updatedAssignment ) {
            if ( updatedAssignment != null && ModelState.IsValid ) {
                Assignment assignment = this.context.Assignments
                    .Where( currentAssignment => currentAssignment.AssignmentId == id )
                    .FirstOrDefault();

                if ( assignment != null ) {
                    assignment.Name = updatedAssignment.Name;
                    assignment.StudyYearId = updatedAssignment.StudyYearId;

                    this.context.Assignments.Update( assignment );
                    this.context.SaveChanges();

                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }

        [HttpDelete( "{id}" )]
        // DELETE api/assignment/3
        public IActionResult Delete( int id ) {
            Assignment assignment = this.context.Assignments
                .Where( currentAssignment => currentAssignment.AssignmentId == id )
                .FirstOrDefault();

            if ( assignment != null ) {
                this.context.Assignments.Remove( assignment );
                this.context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }
    }
}
