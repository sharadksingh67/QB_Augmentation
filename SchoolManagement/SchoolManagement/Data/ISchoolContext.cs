using SchoolManagement.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SchoolManagement.Data
{
    public interface ISchoolContext : IDisposable
    {
        DbSet<Course> Courses { get; set; }
        DbSet<Enrollment> Enrollments { get; set; }
        DbSet<Student> Students { get; set; }
        int SaveChanges();
        DbEntityEntry Entry(object entity);        
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}