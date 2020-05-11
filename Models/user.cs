using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MVC_lib.Models
{
    public class User
    {
        [MaxLength(30)] [NotNull]
        public string m_Name { get; set; }
        [Key]
        public int m_ID { get; set; }

        public List<Book> Book { get; set; }
    }
}
