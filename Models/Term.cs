using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Course_Planner_App_Felix__Berinde.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
