using System.Collections.Generic;
using System.Linq;
using WorkshopDev6B.Models;
using Microsoft.AspNetCore.Mvc;

namespace WorkshopDev6B.Controllers {
    [Route( "api/[controller]" )]
    public class StudyYearController : Controller {
        StudyContext context;

        public StudyYearController( StudyContext context ) {
            this.context = context;
        }

        [HttpGet]
        // GET api/studyyear
        public IActionResult Get() {
            return Ok( this.context.StudyYears.ToList() );
        }

        [HttpGet( "{id}" )]
        // GET api/studyyear/3
        public IActionResult Get( int id ) {
            StudyYear studyYear = this.context.StudyYears
                .Where( currentYear => currentYear.StudyYearId == id )
                .FirstOrDefault();

            if ( studyYear != null ) {
                return Ok( studyYear );
            }

            return NotFound();
        }


        [HttpPost]
        // POST api/studyyear
        public IActionResult Post( [FromBody] StudyYear newYear ) {
            if ( newYear == null || !ModelState.IsValid) {
                return BadRequest();
            }

            this.context.StudyYears.Add( newYear );
            this.context.SaveChanges();

            return Ok();
        }

        [HttpPut( "{id}" )]
        // PUT api/studyyear/3
        public IActionResult Put( int id, [FromBody] StudyYear updatedYear ) {
            if ( updatedYear != null && ModelState.IsValid ) {
                StudyYear studyYear = this.context.StudyYears
                    .Where( currentYear => currentYear.StudyYearId == id )
                    .FirstOrDefault();

                if ( studyYear != null ) {
                    studyYear.Name = updatedYear.Name;
                    this.context.StudyYears.Update( studyYear );
                    this.context.SaveChanges();

                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }

        // DELETE api/studyyear/3
        [HttpDelete( "{id}" )]
        public IActionResult Delete( int id ) {
            StudyYear studyYear = this.context.StudyYears
                .Where( currentYear => currentYear.StudyYearId == id )
                .FirstOrDefault();

            if ( studyYear != null ) {
                this.context.StudyYears.Remove( studyYear );
                this.context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }
    }
}
