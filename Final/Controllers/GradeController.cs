using System.Collections.Generic;
using System.Linq;
using WorkshopDev6B.Models;
using Microsoft.AspNetCore.Mvc;

namespace WorkshopDev6B.Controllers {
    [Route( "api/[controller]" )]
    public class GradeController : Controller {
        StudyContext context;

        public GradeController( StudyContext context ) {
            this.context = context;
        }

        private Grade fillGradeKeys( Grade grade ) {
            grade.Student = this.context.Students
                .Where( student => student.StudentId == grade.StudentId )
                .FirstOrDefault();

            grade.Assignment = this.context.Assignments
                .Where( assignment => assignment.AssignmentId == grade.AssignmentId )
                .FirstOrDefault();

            return grade;
        }

        [HttpGet]
        // GET api/grade
        public IActionResult Get() {
            List<Grade> grades = this.context.Grades.ToList();

            for ( int index = 0; index < grades.Count(); index = index + 1 ) {
                grades[index] = this.fillGradeKeys( grades[index] );
            }

            return Ok( grades );
        }

        [HttpGet( "{studentId}/{assignmentId}" )]
        // GET api/grade/3
        public IActionResult Get( int studentId, int assignmentId ) {
            Grade grade = this.context.Grades
                .Where( currentGrade => 
                    currentGrade.AssignmentId == assignmentId && 
                    currentGrade.StudentId == studentId )
                .FirstOrDefault();

            if ( grade != null ) {
                grade = this.fillGradeKeys( grade );
                return Ok( grade );
            }

            return NotFound();
        }


        [HttpPost]
        // POST api/assignment
        public IActionResult Post( [FromBody] Grade newGrade ) {
            if ( newGrade == null || !ModelState.IsValid ) {
                return BadRequest();
            }

            this.context.Grades.Add( newGrade );
            this.context.SaveChanges();

            return Ok();
        }

        [HttpPut( "{studentId}/{assignmentId}" )]
        // PUT api/assignment/3
        public IActionResult Put( int studentId, int assignmentId, [FromBody] Grade updatedGrade ) {
            if ( updatedGrade != null && ModelState.IsValid ) {
                Grade grade = this.context.Grades
                    .Where( currentGrade =>
                        currentGrade.AssignmentId == assignmentId &&
                        currentGrade.StudentId == studentId )
                    .FirstOrDefault();

                if ( grade != null ) {
                    grade.Points = updatedGrade.Points;

                    this.context.Grades.Update( grade );
                    this.context.SaveChanges();

                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }

        [HttpDelete( "{studentId}/{assignmentId}" )]
        // DELETE api/assignment/3
        public IActionResult Delete( int studentId, int assignmentId ) {
            Grade grade = this.context.Grades
                .Where( currentGrade =>
                    currentGrade.AssignmentId == assignmentId &&
                    currentGrade.StudentId == studentId )
                .FirstOrDefault();

            if ( grade != null ) {
                this.context.Grades.Remove( grade );
                this.context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }
    }
}
