
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Break.Module.Core.Entity
{
    public class DateTimeWorkSchedule
    {
        public int Id { get; set; }
        // [Column(TypeName = "datetime")]
        public DateTime dateTime { get; set; }

    }
}