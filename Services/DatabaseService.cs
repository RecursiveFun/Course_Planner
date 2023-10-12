using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Course_Planner_App_Felix__Berinde.Models;
using SQLite;
using Xamarin.Essentials;

namespace Course_Planner_App_Felix__Berinde.Services
{
    public static class DatabaseService
    {

        private static SQLiteAsyncConnection _db;
        private static SQLiteConnection _dbConnection;

        static async Task Init()
        {
            if (_db != null) //don't create db if it already exists
            {
                return;
            }

            //Get an absolute path to the database file.
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Terms.db");

            _db = new SQLiteAsyncConnection(databasePath);
            _dbConnection = new SQLiteConnection(databasePath);

            await _db.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Course>();
            await _db.CreateTableAsync<Assessment>();
        }

        #region Term methods

        public static async Task AddTerm(string title, DateTime startDate, DateTime endDate)
        {
            await Init();
            var term = new Term()
            {
                Title = title,
                StartDate = startDate,
                EndDate = endDate
            };

            await _db.InsertAsync(term);
        }

        public static async Task RemoveTerm(int id)
        {
            await Init();
            await _db.DeleteAsync<Term>(id);
        }

        public static async Task<IEnumerable<Term>> GetTerms()
        {
            await Init();

            var terms = await _db.Table<Term>().ToListAsync();

            return terms;
        }

        public static async Task UpdateTerm(int id, string title, DateTime startDate, DateTime endDate)
        {
            await Init();

            var termQuery = await _db.Table<Term>().Where(i => i.Id == id).FirstOrDefaultAsync();

            if (termQuery != null)
            {
                termQuery.Title = title;
                termQuery.StartDate = startDate;
                termQuery.EndDate = endDate;

                await _db.UpdateAsync(termQuery);
            }
        }

        #endregion

        #region Course Methods


        public static async Task<IEnumerable<Course>> GetCourses()
        {
            await Init();

            var courses = await _db.Table<Course>().ToListAsync();

            return courses;
        }

        public static async Task<IEnumerable<Course>> GetCourses(int termId)
        {
            await Init();

            var courses = await _db.Table<Course>().Where(i => i.TermId == termId).ToListAsync();

            return courses;
        }


        public static async Task AddCourse(int id, string title, DateTime startDate, DateTime endDate, string status, string instructorName, string instructorPhone, string instructorEmail, string notes, bool startNotification, bool endNotification)
        {
            await Init();
            var course = new Course()
            {
                TermId = id,
                Title = title,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                InstructorName = instructorName,
                InstructorPhone = instructorPhone,
                InstructorEmail = instructorEmail,
                Notes = notes,
                StartNotification = startNotification,
                EndNotification = endNotification
            };

            await _db.InsertAsync(course);
        }

        public static async Task UpdateCourse(int id, string title, DateTime startDate, DateTime endDate, string status, string instructorName, string instructorPhone, string instructorEmail, string notes, bool startNotification, bool endNotification)
        {
            await Init();

            var courseQuery = await _db.Table<Course>().Where(i => i.Id == id).FirstOrDefaultAsync();

            if (courseQuery != null)
            {
                courseQuery.Title = title;
                courseQuery.StartDate = startDate;
                courseQuery.EndDate = endDate;
                courseQuery.Status = status;
                courseQuery.InstructorName = instructorName;
                courseQuery.InstructorPhone = instructorPhone;
                courseQuery.InstructorEmail = instructorEmail;
                courseQuery.Notes = notes;
                courseQuery.StartNotification = startNotification;
                courseQuery.EndNotification = endNotification;

                await _db.UpdateAsync(courseQuery);
            }
        }

        public static async Task RemoveCourse(int id)
        {
            await Init();
            await _db.DeleteAsync<Course>(id);
        }

        #endregion

        #region Asssessments Methods


        public static async Task<IEnumerable<Assessment>> GetAssessments()
        {
            await Init();

            var assessments = await _db.Table<Assessment>().ToListAsync();

            return assessments;
        }

        public static async Task<IEnumerable<Assessment>> GetAssessments(int courseId)
        {
            await Init();

            var assessments = await _db.Table<Assessment>().Where(i => i.CourseId == courseId).ToListAsync();

            return assessments;
        }


        public static async Task AddAssessment(int id, string title, DateTime startDate, DateTime endDate, string type,  bool startNotification, bool endNotification)
        {
            await Init();
            var assessment = new Assessment()
            {
                CourseId = id,
                Title = title,
                StartDate = startDate,
                EndDate = endDate,
                Type = type,
                StartNotification = startNotification,
                EndNotification = endNotification
            };

            await _db.InsertAsync(assessment);
        }

        public static async Task UpdateAssessment(int id, string title, DateTime startDate, DateTime endDate, string type, bool startNotification, bool endNotification)
        {
            await Init();

            var assessmentQuery = await _db.Table<Assessment>().Where(i => i.Id == id).FirstOrDefaultAsync();

            if (assessmentQuery != null)
            {
                assessmentQuery.Title = title;
                assessmentQuery.StartDate = startDate;
                assessmentQuery.EndDate = endDate;
                assessmentQuery.Type = type;
                assessmentQuery.StartNotification = startNotification;
                assessmentQuery.EndNotification = endNotification;

                await _db.UpdateAsync(assessmentQuery);
            }
        }

        public static async Task RemoveAssessment(int id)
        {
            await Init();
            await _db.DeleteAsync<Assessment>(id);
        }


        #endregion


        #region DemoData

        public static async void LoadSampleData()
        {
            await Init();

            Term term = new Term
            {
                Title = "Test Term 1",
                StartDate = DateTime.Today.Date,
                EndDate = DateTime.Today.AddMonths(6).Date

            };
            await _db.InsertAsync(term);

            Course course = new Course
            {
                TermId = 1,
                Title = "Mobile App Development",
                StartDate = DateTime.Today.Date,
                EndDate = DateTime.Today.AddMonths(3).Date,
                Status = "Active",
                InstructorName = "Felix Berinde",
                InstructorEmail = "fberind@wgu.edu",
                InstructorPhone = "(313) 570-7466",
                Notes = "This is a note.",
                StartNotification = true,
                EndNotification = true
            };
            await _db.InsertAsync(course);


            Assessment objectiveAssessment = new Assessment
            {
                CourseId = 1,
                Title = "Test Objective Assessment",
                StartDate = DateTime.Today.AddMonths(4).Date,
                EndDate = DateTime.Today.AddMonths(6).Date,
                Type = "Objective",
                StartNotification = true,
                EndNotification = true
            };
            await _db.InsertAsync(objectiveAssessment);

            Assessment performanceAssessment = new Assessment
            {
                CourseId = 1,
                Title = "Test Performance Assessment",
                StartDate = DateTime.Today.AddMonths(4).Date,
                EndDate = DateTime.Today.AddMonths(6).Date,
                Type = "Performance",
                StartNotification = true,
                EndNotification = true
            };
            await _db.InsertAsync(performanceAssessment);
        }


        public static async Task ClearSampleData()
        {
            await Init();

            await _db.DropTableAsync<Term>();
            await _db.DropTableAsync<Course>();
            await _db.DropTableAsync<Assessment>();
            _db = null;
            _dbConnection = null;
        }

        #endregion


    }
}
