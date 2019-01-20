// Import the right packages
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

// Enter the namespace for the models
namespace WorkshopDev6B.Models {
    /// <summary>
    /// The study context class
    /// Creates a DbContext which maps classes to the database
    /// This class represents the database
    /// </summary>
    public class StudyContext : DbContext {
        // Create several class variables of the DbSet type
        // These will become the tables in our database
        public DbSet<StudyYear> StudyYears { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Grade> Grades { get; set; }

        // We need a constructor in order to use specific functionality of our parent class (DbContext)
        public StudyContext( DbContextOptions<StudyContext> contextOptions ) : base( contextOptions ) { }

        /// <summary>
        /// We override the OnModelCreating method
        /// This methods allows us to set primary keys and foreign keys
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder is a class that contains our relations and keys</param>
        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            // We set the primary keys of the Grade table
            modelBuilder.Entity<Grade>()
                .HasKey( grade => new { grade.StudentId, grade.AssignmentId } );

            // We create the foreign key reference from Grade to Student
            modelBuilder.Entity<Grade>()
                .HasOne( grade => grade.Student )
                .WithMany( student => student.Grades )
                .HasForeignKey( grade => grade.StudentId );

            // We create the foreign key reference from Student to grade
            modelBuilder.Entity<Grade>()
                .HasOne( grade => grade.Assignment )
                .WithMany( assignment => assignment.Deliveries )
                .HasForeignKey( grade => grade.AssignmentId );
        }
    }

    // The study year class
    public class StudyYear {
        // Primary Key
        public int StudyYearId { get; set; }
        public string Name { get; set; }
    }

    // The student class
    public class Student {
        // Primary Key
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Foreign Key (O-M)
        public int StudyYearId { get; set; }
        public StudyYear StudyYear { get; set; }

        // Reference for relation (M-M)
        public virtual List<Grade> Grades { get; set; }
    }

    // The assignment class
    public class Assignment {
        // Primary Key
        public int AssignmentId { get; set; }
        public string Name { get; set; }

        // Foreign Key (O-M)
        public int StudyYearId { get; set; }
        public StudyYear StudyYear { get; set; }

        // Reference for relation (M-M)
        public virtual List<Grade> Deliveries { get; set; }
    }

    // The grade class
    public class Grade {
        // Foreign Key (O-M)
        public int StudentId { get; set; }
        public Student Student { get; set; }

        // Foreign Key (O-M)
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public int Points { get; set; }
    }
}
