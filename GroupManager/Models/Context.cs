using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GroupManager.Models
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupCategory> GroupCategories { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<LessonComment> LessonComments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostCategory> PostCategories { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionCategory> QuestionCategories { get; set; }
        public virtual DbSet<QuestionComment> QuestionComments { get; set; }
        public virtual DbSet<Quiz> Quizs { get; set; }
        public virtual DbSet<QuizCategory> QuizCategories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<User_Group> User_Group { get; set; }
        public virtual DbSet<User_Role> User_Role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(e => e.Posts)
                .WithOptional(e => e.Group)
                .HasForeignKey(e => e.Group_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Quizs)
                .WithOptional(e => e.Group)
                .HasForeignKey(e => e.Group_ID);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.User_Group)
                .WithOptional(e => e.Group)
                .HasForeignKey(e => e.Group_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<GroupCategory>()
                .HasMany(e => e.Groups)
                .WithOptional(e => e.GroupCategory)
                .HasForeignKey(e => e.Category_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PostCategory>()
                .HasMany(e => e.Posts)
                .WithOptional(e => e.PostCategory)
                .HasForeignKey(e => e.Category_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<QuestionCategory>()
                .HasMany(e => e.Questions)
                .WithOptional(e => e.QuestionCategory)
                .HasForeignKey(e => e.Category_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<QuizCategory>()
                .HasMany(e => e.Quizs)
                .WithOptional(e => e.QuizCategory)
                .HasForeignKey(e => e.Category_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.User_Role)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.Role_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Groups)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Created_By);

            modelBuilder.Entity<User>()
                .HasMany(e => e.GroupCategories)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Created_By);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Lessons)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Created_By);

            modelBuilder.Entity<User>()
                .HasMany(e => e.LessonComments)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Created_By)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Posts)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Category_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.PostCategories)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Created_By);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PostComments)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Created_By)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Questions)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Created_By);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Questions1)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.Created_By);

            modelBuilder.Entity<User>()
                .HasMany(e => e.QuestionCategories)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Created_By);

            modelBuilder.Entity<User>()
                .HasMany(e => e.QuestionComments)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Created_By);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Quizs)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Created_By);

            modelBuilder.Entity<User>()
                .HasMany(e => e.QuizCategories)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Created_By);

            modelBuilder.Entity<User>()
                .HasMany(e => e.User_Group)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.User_ID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.User_Role)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.User_ID);
        }
    }
}
