using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using School.DAL.Entities;

namespace School.DAL.DataInitialization
{
    public static class MyDataInitializer
    {
        public static void InitializeData(SchoolDbContext dbContext)
        {
            var t1 = new Teacher
            {
                FirstName = "Billie",
                LastName = "Palmer",
                Gender = Gender.Male,
                Age = 44
            };
            var t2 = new Teacher
            {
                FirstName = "Rosa",
                LastName = "Powell",
                Gender = Gender.Female,
                Age = 23
            };
            var t3 = new Teacher
            {
                FirstName = "Sheila",
                LastName = "White",
                Gender = Gender.Female,
                Age = 55
            };
            var t4 = new Teacher
            {
                FirstName = "Jeremy",
                LastName = "Allison",
                Gender = Gender.Male,
                Age = 66
            };
            var t5 = new Teacher
            {
                FirstName = "Ira",
                LastName = "Fitzgerald",
                Gender = Gender.Female,
                Age = 68
            };
            var t6 = new Teacher
            {
                FirstName = "Melody",
                LastName = "Morrison",
                Gender = Gender.Female,
                Age = 20
            };
            var t7 = new Teacher
            {
                FirstName = "Dianna",
                LastName = "Ballard",
                Gender = Gender.Female,
                Age = 50
            };
            var t8 = new Teacher
            {
                FirstName = "Bertha",
                LastName = "Torres",
                Gender = Gender.Female,
                Age = 41
            };
            var t9 = new Teacher
            {
                FirstName = "Irene",
                LastName = "Bennett",
                Gender = Gender.Female,
                Age = 21
            };
            var t10 = new Teacher
            {
                FirstName = "Santiago",
                LastName = "Lucas",
                Gender = Gender.Male,
                Age = 51
            };

            dbContext.Teachers.AddRange(t1,
                t2,
                t3,
                t4,
                t5,
                t6,
                t7,
                t8,
                t9,
                t10);

            dbContext.SaveChanges();

            var subj1 = new Subject
            {
                Name = "Math",
                Teachers = new List<Teacher>
                {
                    t1,
                    t5
                }
            };
            
            var subj2 = new Subject
            {
                Name = "Art",
                Teachers = new List<Teacher>
                {
                    t2
                }
            };
            
            var subj3 = new Subject
            {
                Name = "English",
                Teachers = new List<Teacher>
                {
                    t4,
                    t6
                }
            };
            
            var subj4 = new Subject
            {
                Name = "Music",
                Teachers = new List<Teacher>
                {
                    t2
                }
            };
            
            var subj5 = new Subject
            {
                Name = "Science",
                Teachers = new List<Teacher>
                {
                    t1,
                    t5
                }
            };
            
            var subj6 = new Subject
            {
                Name = "History",
                Teachers = new List<Teacher>
                {
                    t10
                }
            };
            
            var subj7 = new Subject
            {
                Name = "Geography",
                Teachers = new List<Teacher>
                {
                    t10
                }
            };
            
            var subj8 = new Subject
            {
                Name = "IT",
                Teachers = new List<Teacher>
                {
                    t3
                }
            };
            
            var subj9 = new Subject
            {
                Name = "Biology",
                Teachers = new List<Teacher>
                {
                    t7
                }
            };
            
            var subj10 = new Subject
            {
                Name = "Drama",
                Teachers = new List<Teacher>
                {
                    t8
                }
            };
            
            var subj11 = new Subject
            {
                Name = "Swimming",
                Teachers = new List<Teacher>
                {
                    t9
                }
            };
            
            var subj12 = new Subject
            {
                Name = "Physical education",
                Teachers = new List<Teacher>
                {
                    t9
                }
            };

            dbContext.Subjects.AddRange(
                subj1,
                subj2,
                subj3,
                subj4,
                subj5,
                subj6,
                subj7,
                subj8,
                subj9,
                subj10,
                subj11,
                subj12);

            dbContext.SaveChanges();

            var c1 = new Class
            {
                Name = "10A",
                Teacher = t1,
                TeacherId = t1.Id
            };
            
            var c2 = new Class
            {
                Name = "10B",
                Teacher = t6,
                TeacherId = t6.Id
            };
            
            var c3 = new Class
            {
                Name = "11A",
                Teacher = t10,
                TeacherId = t10.Id
            };
            
            var c4 = new Class
            {
                Name = "10C",
                Teacher = t3,
                TeacherId = t3.Id
            };
            
            var c5 = new Class
            {
                Name = "11B",
                Teacher = t2,
                TeacherId = t2.Id
            };
            
            var c6 = new Class
            {
                Name = "9A",
                Teacher = t9,
                TeacherId = t9.Id
            };

            dbContext.Classes.AddRange(c1,
                c2,
                c3,
                c4,
                c5,
                c6);

            var subjPacket1 = new List<Subject>()
            {
                subj1,
                subj2,
                subj4,
                subj9,
                subj5,
                subj12,
                subj11
            };
            
            var subjPacket2 = new List<Subject>()
            {
                subj3,
                subj2,
                subj4,
                subj9,
                subj6,
                subj12,
                subj8
            };
            
            var subjPacket3 = new List<Subject>()
            {
                subj1,
                subj2,
                subj6,
                subj3,
                subj7,
                subj10,
                subj11
            };
            
            var subjPacket4 = new List<Subject>()
            {
                subj1,
                subj6,
                subj7,
                subj8
            };
            
            var subjPacket5 = new List<Subject>()
            {
                subj10,
                subj12,
                subj4,
                subj7,
                subj5,
                subj2,
                subj11
            };
            
            var subjPacket6 = new List<Subject>()
            {
                subj10,
                subj12,
                subj4,
                subj7,
                subj5,
                subj2,
                subj11,
                subj1,
                subj3,
                subj6,
                subj8,
                subj9
            };

            dbContext.SaveChanges();

            var s1 = new Student
            {
                FirstName = "Rebecca",
                LastName = "King",
                Age = 16,
                Class = c1,
                ClassId = c1.Id,
                Gender = Gender.Female
            };

            var s2 = new Student
            {
                FirstName = "Melissa",
                LastName = "Thompson",
                Age = 17,
                Class = c1,
                ClassId = c1.Id,
                Gender = Gender.Female
            };

            var s3 = new Student
            {
                FirstName = "George",
                LastName = "Long",
                Age = 16,
                Class = c1,
                ClassId = c1.Id,
                Gender = Gender.Male
            };

            var s4 = new Student
            {
                FirstName = "Karen",
                LastName = "Bell",
                Age = 16,
                Class = c2,
                ClassId = c2.Id,
                Gender = Gender.Male
            };

            var s5 = new Student
            {
                FirstName = "Louise",
                LastName = "Bryant",
                Age = 17,
                Class = c2,
                ClassId = c2.Id,
                Gender = Gender.Male
            };


            var s6 = new Student
            {
                FirstName = "Carolyn",
                LastName = "Mitchell",
                Age = 16,
                Class = c2,
                ClassId = c2.Id,
                Gender = Gender.Female
            };

            var s7 = new Student
            {
                FirstName = "Matthew",
                LastName = "Hill",
                Age = 16,
                Class = c2,
                ClassId = c2.Id,
                Gender = Gender.Male
            };

            var s8 = new Student
            {
                FirstName = "Jessica",
                LastName = "Patterson",
                Age = 16,
                Class = c3,
                ClassId = c3.Id,
                Gender = Gender.Female
            };

            var s9 = new Student
            {
                FirstName = "Sandra",
                LastName = "Lopez",
                Age = 17,
                Class = c3,
                ClassId = c3.Id,
                Gender = Gender.Female
            };

            var s10 = new Student
            {
                FirstName = "Beverly",
                LastName = "Howard",
                Age = 16,
                Class = c3,
                ClassId = c3.Id,
                Gender = Gender.Female
            };

            var s11 = new Student
            {
                FirstName = "Carol",
                LastName = "Campbell",
                Age = 17,
                Class = c3,
                ClassId = c3.Id,
                Gender = Gender.Female
            };

            var s12 = new Student
            {
                FirstName = "Richard",
                LastName = "Parker",
                Age = 16,
                Class = c3,
                ClassId = c3.Id,
                Gender = Gender.Male
            };

            var s13 = new Student
            {
                FirstName = "Todd",
                LastName = "Alexander",
                Age = 17,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Male
            };

            var s14 = new Student
            {
                FirstName = "Donna",
                LastName = "Butler",
                Age = 16,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Female
            };

            var s15 = new Student
            {
                FirstName = "Howard",
                LastName = "Roberts",
                Age = 16,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Male
            };

            var s16 = new Student
            {
                FirstName = "Frank",
                LastName = "Wilson",
                Age = 16,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Male
            };

            var s17 = new Student
            {
                FirstName = "George",
                LastName = "Diaz",
                Age = 16,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Male
            };

            var s18 = new Student
            {
                FirstName = "Chris",
                LastName = "Griffin",
                Age = 16,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Male
            };

            var s19 = new Student
            {
                FirstName = "Amanda",
                LastName = "James",
                Age = 17,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Female
            };

            var s20 = new Student
            {
                FirstName = "Sandra",
                LastName = "Miller",
                Age = 18,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Female
            };

            var s21 = new Student
            {
                FirstName = "Carol",
                LastName = "Washington",
                Age = 17,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Female
            };

            var s22 = new Student
            {
                FirstName = "Doris",
                LastName = "Johnson",
                Age = 17,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Female
            };

            var s23 = new Student
            {
                FirstName = "Eugene",
                LastName = "Wright",
                Age = 16,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Male
            };

            var s24 = new Student
            {
                FirstName = "Margaret",
                LastName = "Collins",
                Age = 16,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Female
            };

            var s25 = new Student
            {
                FirstName = "Henry",
                LastName = "Hall",
                Age = 17,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Male
            };

            var s26 = new Student
            {
                FirstName = "Frances ",
                LastName = "Thomas",
                Age = 14,
                Class = c6,
                ClassId = c6.Id,
                Gender = Gender.Female
            };

            var s27 = new Student
            {
                FirstName = "Pamela",
                LastName = "Bennett",
                Age = 15,
                Class = c6,
                ClassId = c6.Id,
                Gender = Gender.Female
            };

            var s28 = new Student
            {
                FirstName = "Kimberly",
                LastName = "Morgan",
                Age = 14,
                Class = c6,
                ClassId = c6.Id,
                Gender = Gender.Female
            };

            var s29 = new Student
            {
                FirstName = "Terry",
                LastName = "Campbell",
                Age = 14,
                Class = c6,
                ClassId = c6.Id,
                Gender = Gender.Male
            };

            var s30 = new Student
            {
                FirstName = "Rachel",
                LastName = "Patterson",
                Age = 15,
                Class = c6,
                ClassId = c6.Id,
                Gender = Gender.Female
            };

            var s31 = new Student
            {
                FirstName = "Olha",
                LastName = "Cina",
                Age = 11,
                Gender = Gender.Female
            };

            var s32 = new Student
            {
                FirstName = "Terry",
                LastName = "Opraa",
                Age = 12,
                Gender = Gender.Male
            };

            var s33 = new Student
            {
                FirstName = "Sin",
                LastName = "Bad",
                Age = 15,
                Gender = Gender.Male
            };
            
            dbContext.Students.AddRange(
                s1,
                s2,
                s3,
                s4,
                s5,
                s6,
                s7,
                s8,
                s9,
                s10,
                s11,
                s12,
                s13,
                s14,
                s15,
                s16,
                s17,
                s18,
                s19,
                s20,
                s21,
                s22,
                s23,
                s24,
                s25,
                s26,
                s27,
                s28,
                s29,
                s30,
                s31,
                s32,
                s33);

            dbContext.SaveChanges();

            foreach (var subject in subjPacket3)
                s28.Subjects.Add(subject);

            foreach (var subject in subjPacket6)
                s29.Subjects.Add(subject);

            foreach (var subject in subjPacket1)
                s30.Subjects.Add(subject);

            foreach (var subject in subjPacket1)
                s18.Subjects.Add(subject);

            foreach (var subject in subjPacket4)
                s13.Subjects.Add(subject);

            foreach (var subject in subjPacket2)
                s12.Subjects.Add(subject);

            foreach (var subject in subjPacket4)
                s11.Subjects.Add(subject);

            foreach (var subject in subjPacket4)
                s14.Subjects.Add(subject);

            foreach (var subject in subjPacket3)
                s15.Subjects.Add(subject);

            foreach (var subject in subjPacket6)
                s16.Subjects.Add(subject);

            foreach (var subject in subjPacket1)
                s17.Subjects.Add(subject);

            foreach (var subject in subjPacket5)
                s25.Subjects.Add(subject);

            foreach (var subject in subjPacket3)
                s20.Subjects.Add(subject);

            foreach (var subject in subjPacket3)
                s21.Subjects.Add(subject);

            foreach (var subject in subjPacket6)
                s19.Subjects.Add(subject);

            foreach (var subject in subjPacket6)
                s22.Subjects.Add(subject);

            foreach (var subject in subjPacket2)
                s23.Subjects.Add(subject);

            foreach (var subject in subjPacket5)
                s24.Subjects.Add(subject);

            foreach (var subject in subjPacket6)
                s10.Subjects.Add(subject);

            foreach (var subject in subjPacket2)
                s9.Subjects.Add(subject);

            foreach (var subject in subjPacket6)
                s8.Subjects.Add(subject);

            foreach (var subject in subjPacket5)
                s7.Subjects.Add(subject);

            foreach (var subject in subjPacket1)
                s6.Subjects.Add(subject);

            foreach (var subject in subjPacket4)
                s5.Subjects.Add(subject);

            foreach (var subject in subjPacket6)
                s26.Subjects.Add(subject);

            foreach (var subject in subjPacket2)
                s27.Subjects.Add(subject);

            foreach (var subject in subjPacket6)
                s31.Subjects.Add(subject);

            foreach (var subject in subjPacket1)
                s32.Subjects.Add(subject);

            foreach (var subject in subjPacket3)
                s33.Subjects.Add(subject);

            

            dbContext.SaveChanges();
        }

        public static void RecreateDatabase(SchoolDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();
        }
    }
}