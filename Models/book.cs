using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MVC_lib.Models
{
    public class Book
    {
        [Key]

        public int m_ID { get; set; }
        [MaxLength(40)] [NotNull]
        public string m_Name { get; set; }
        [MaxLength(100)] [NotNull]
        public string m_Description { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }
    }
}
