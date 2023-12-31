﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Course_Planner_App_Felix__Berinde.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TermId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }
        public bool StartNotification { get; set; }
        public bool EndNotification { get; set; }
    }
}
