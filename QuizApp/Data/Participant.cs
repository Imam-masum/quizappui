﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Data
{
    public class Participant
    {
        [Key]
        public int ParticipantId { get; set; }
        

        [Column(TypeName ="nvarchar(50)")]
        public string Email { get; set; }
        [Column(TypeName ="nvarchar(100)")]

        public string Name { get; set; }

        public int Score { get; set; }
        public int TimeTaken { get; set; }
    }
    
}
