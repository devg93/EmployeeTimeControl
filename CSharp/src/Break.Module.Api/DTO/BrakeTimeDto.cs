 using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Break.Module.Api.DTO

{
  

    public class BrakeTimeDto 
    {
        public int Id { get; set; }

        [Required]
        public ICollection<DateTimeWorkSchedule> StartTime { get; set; } = new List<DateTimeWorkSchedule>();

        [Required]
        public ICollection<DateTimeWorkSchedule> EndTime { get; set; } = new List<DateTimeWorkSchedule>();

        public int? busyId { get; set; }

        public busyChecker? busyChecker { get; set; }
    }
}

    public class DateTimeWorkSchedule
    {
        public int Id { get; set; }
        // [Column(TypeName = "datetime")]
        public DateTime dateTime { get; set; }

    }

    public class busyChecker
    {
        public int Id { get; set; }
        public bool busy { get; set; }

    }





