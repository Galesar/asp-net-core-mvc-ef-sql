using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_lib.Models;

namespace MVC_lib.Models
{
    public static class SampleData
    {
        public static void Initialize(ApplicationContext context)
        {
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book
                    {
                        m_Name = "Give me this job, please",
                        m_Description = "This book is about how I tried to get this job."
                    },
                    new Book
                    {
                        m_Name = "Give me this job. part 2,",
                        m_Description = "But i hope, that i can do it."
                    });
            };
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        m_Name = "Admin"
                    });
                context.SaveChanges();
            }
        }
    }
}
