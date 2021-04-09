using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fond.DbFolder
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options)
            : base(options)
        {

        }



        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Convert> Converts { get; set; }
        public DbSet<ConvertProject> ConvertProjects { get; set; }

        public DbSet<QuestionType> QuestionTypes { get; set; }

    }
}
