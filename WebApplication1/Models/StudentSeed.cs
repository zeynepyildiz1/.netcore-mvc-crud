using ExcelDataReader;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace WebApplication1.Models
{
    public class StudentSeed : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(
               
              GetStudentList()
                );
        }
        private static List<Student> GetStudentList()
        {
            List<Student> students = new List<Student>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + "StudentList.xlsx";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    int Count = 0;
                    while (reader.Read())
                    {
                        Count++;
                        students.Add(new Student()
                        { Id = Count,
                            Name = reader.GetValue(0).ToString(),
                            Roll = reader.GetValue(1).ToString(),
                            Email = reader.GetValue(2).ToString(),
                        }); ; ;
                    }
                }
            }
            return students;
        }
    }
}
