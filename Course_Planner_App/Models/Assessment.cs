using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Course_Planner_App_Felix__Berinde.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public bool StartNotification { get; set; }
        public bool EndNotification { get; set; }
    }
}
