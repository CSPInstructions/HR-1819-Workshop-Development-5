using System.Collections.Generic;
using System.Linq;
using WorkshopDev6B.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WorkshopDev6B.Controllers {
    [Route( "api/[controller]" )]
    public class DatabaseController : Controller {
        StudyContext context;

        public DatabaseController( StudyContext context ) {
            this.context = context;
        }

        [HttpGet][Route("LoadDemoData")]
        // GET api/student/loaddemodata
        public IActionResult LoadDemoData() {
            #region Delete Everything
            foreach ( Grade grade in this.context.Grades ) {
                this.context.Grades.Remove( grade );
            }

            this.context.SaveChanges();

            foreach ( Assignment assignment in this.context.Assignments ) {
                this.context.Assignments.Remove( assignment );
            }

            this.context.SaveChanges();

            foreach ( Student student in this.context.Students ) {
                this.context.Students.Remove( student );
            }

            this.context.SaveChanges();

            foreach ( StudyYear studyYear in this.context.StudyYears ) {
                this.context.StudyYears.Remove( studyYear );
            }

            this.context.SaveChanges();
            #endregion

            #region Add New Items
            List<StudyYear> studyYears = new List<StudyYear> {
                new StudyYear {
                    StudyYearId = 1,
                    Name = "Jaar 1"
                },

                new StudyYear {
                    StudyYearId = 2,
                    Name = "Jaar 2"
                },

                new StudyYear {
                    StudyYearId = 3,
                    Name = "Jaar 3"
                },

                new StudyYear {
                    StudyYearId = 4,
                    Name = "Jaar 4"
                }
            };

            List<Student> students = new List<Student> {
                new Student {
                    StudentId = 1,
                    FirstName = "Jimmy",
                    LastName = "Kijas",
                    StudyYearId = 1,
                },

                new Student {
                    StudentId = 2,
                    FirstName = "Charline",
                    LastName = "Lui",
                    StudyYearId = 1,
                },

                new Student {
                    StudentId = 3,
                    FirstName = "Kimyiu",
                    LastName = "Walstra",
                    StudyYearId = 2,
                },

                new Student {
                    StudentId = 4,
                    FirstName = "Jay",
                    LastName = "Verbeek",
                    StudyYearId = 2,
                },

                new Student {
                    StudentId = 5,
                    FirstName = "Damian",
                    LastName = "Hogendoorn",
                    StudyYearId = 3,
                },

                new Student {
                    StudentId = 6,
                    FirstName = "Jeroen",
                    LastName = "Eijsink",
                    StudyYearId = 3,
                },

                new Student {
                    StudentId = 7,
                    FirstName = "Fabian",
                    LastName = "de Penning",
                    StudyYearId = 4,
                },

                new Student {
                    StudentId = 8,
                    FirstName = "Ruud",
                    LastName = "Tönissen",
                    StudyYearId = 4,
                }
            };

            List<Assignment> assignments = new List<Assignment> {
                new Assignment {
                    AssignmentId = 1,
                    Name = "Development",
                    StudyYearId = 1
                },

                new Assignment {
                    AssignmentId = 2,
                    Name = "Skills",
                    StudyYearId = 2

                },

                new Assignment {
                    AssignmentId = 3,
                    Name = "Analyse",
                    StudyYearId = 3

                },

                new Assignment {
                    AssignmentId = 4,
                    Name = "Project",
                    StudyYearId = 4

                }
            };

            List<Grade> grades = new List<Grade> {
                new Grade {
                    StudentId = 1,
                    AssignmentId = 1,
                    Points = 100
                },

                new Grade {
                    StudentId = 2,
                    AssignmentId = 1,
                    Points = 71
                },

                new Grade {
                    StudentId = 3,
                    AssignmentId = 2,
                    Points = 90
                },

                new Grade {
                    StudentId = 4,
                    AssignmentId = 2,
                    Points = 12
                },

                new Grade {
                    StudentId = 5,
                    AssignmentId = 3,
                    Points = 56
                },

                new Grade {
                    StudentId = 6,
                    AssignmentId = 3,
                    Points = 39
                }
            };


            this.context.StudyYears.AddRange( studyYears );
            this.context.Students.AddRange( students );
            this.context.Assignments.AddRange( assignments );
            this.context.Grades.AddRange( grades );
            #endregion

            this.context.SaveChanges();
            return Ok( "Demo Data Loaded" );
        }
    }
}
