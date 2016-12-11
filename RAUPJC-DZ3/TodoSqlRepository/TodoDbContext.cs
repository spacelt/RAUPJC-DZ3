using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TodoSqlRepository
{
    public class TodoDbContext : DbContext
    {

        public IDbSet<TodoItem> TodoItems { get; set; }

        public TodoDbContext(String connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*modelBuilder.Entity<Student>().HasKey(s => s.Jmbag);
            modelBuilder.Entity<Student>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Student>().HasOptional(s => s.Mentor).WithMany(p => p.StudentsMentoring);
            modelBuilder.Entity<Student>().HasMany(s => s.ClassesAttending).WithMany(c => c.StudentsAttending);
            modelBuilder.Entity<Student>().HasMany(s => s.ClassesAttending).WithMany();

            modelBuilder.Entity<Professor>().HasKey(p => p.Id);
            modelBuilder.Entity<Professor>().HasMany(p => p.ClassesTeaching).WithMany(c => c.ProfessorsTeaching);


            modelBuilder.Entity<Class>().HasKey(c => c.Id);
            */

            modelBuilder.Entity<TodoItem>().HasKey(i => i.Id);
            modelBuilder.Entity<TodoItem>().Property(i => i.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(i => i.IsCompleted).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(i => i.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(i => i.UserId).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(i => i.DateCompleted).IsOptional();

        }
    }
}
