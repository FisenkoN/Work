using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School.WEB.Controllers;
using School.WEB.Models;

namespace School.WEB.Data
{
    public sealed class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Admin> Admins { get; set; }

        public SchoolDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            InitializeData();
        }

        private void InitializeData()
        {
            var adminRole = new Role { Name = "admin" };
            var studentRole = new Role { Name = "student" };
            var teacherRole = new Role { Name = "teacher" };

            Roles.AddRange(adminRole, studentRole, teacherRole);

            SaveChanges();
            
            var t1 = new Teacher
            {
                FirstName = "Billie",
                LastName = "Palmer",
                Gender = Gender.Male,
                Age = 44,
                Image = "https://i.pinimg.com/564x/24/62/9a/24629a3766888d4339ace8eabdf2e30a.jpg"
            };
            var t2 = new Teacher
            {
                FirstName = "Rosa",
                LastName = "Powell",
                Gender = Gender.Female,
                Age = 23,
                Image = "https://i.pinimg.com/564x/4a/1d/63/4a1d63fb5c4541e83e06e206b65b8587.jpg"
            };
            var t3 = new Teacher
            {
                FirstName = "Sheila",
                LastName = "White",
                Gender = Gender.Female,
                Age = 55,
                Image = "https://i.pinimg.com/564x/f8/a9/dd/f8a9dd5024d6ebed33ae33db724d84e3.jpg"
            };
            var t4 = new Teacher
            {
                FirstName = "Jeremy",
                LastName = "Allison",
                Gender = Gender.Male,
                Age = 66,
                Image = "https://i.pinimg.com/564x/85/f7/3c/85f73ca021b9049778b5ef027f6d74ae.jpg"
            };
            var t5 = new Teacher
            {
                FirstName = "Ira",
                LastName = "Fitzgerald",
                Gender = Gender.Female,
                Age = 68,
                Image = "https://i.pinimg.com/564x/d6/44/11/d64411f084e68f1099dff5756fcf3011.jpg"
            };
            var t6 = new Teacher
            {
                FirstName = "Melody",
                LastName = "Morrison",
                Gender = Gender.Female,
                Age = 20,
                Image = "https://i.pinimg.com/564x/08/74/9b/08749b41d74203ded24925381ae03bc2.jpg"
            };
            var t7 = new Teacher
            {
                FirstName = "Dianna",
                LastName = "Ballard",
                Gender = Gender.Female,
                Age = 50,
                Image = "https://i.pinimg.com/564x/cb/c2/9c/cbc29c209eebfe595cf52e9411f5c717.jpg"
            };
            var t8 = new Teacher
            {
                FirstName = "Bertha",
                LastName = "Torres",
                Gender = Gender.Female,
                Age = 41,
                Image = "https://i.pinimg.com/564x/20/c5/e5/20c5e56c764ca60e5ce5d79a87fe2cb3.jpg"
            };
            var t9 = new Teacher
            {
                FirstName = "Irene",
                LastName = "Bennett",
                Gender = Gender.Female,
                Age = 21,
                Image = "https://i.pinimg.com/564x/6c/2a/de/6c2ade6217fd53f6ecc6a3cfa4446c5b.jpg"
            };
            var t10 = new Teacher
            {
                FirstName = "Santiago",
                LastName = "Lucas",
                Gender = Gender.Male,
                Age = 51,
                Image = "https://i.pinimg.com/564x/e2/38/f4/e238f4f89d6666a7e5d9b79da38d5593.jpg"
            };

            Teachers.AddRange(t1,
                t2,
                t3,
                t4,
                t5,
                t6,
                t7,
                t8,
                t9,
                t10);

            SaveChanges();

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

            Subjects.AddRange(
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

            SaveChanges();

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

            Classes.AddRange(c1,
                c2,
                c3,
                c4,
                c5,
                c6);

            var subjPacket1 = new List<Subject>
            {
                subj1,
                subj2,
                subj4,
                subj9,
                subj5,
                subj12,
                subj11
            };

            var subjPacket2 = new List<Subject>
            {
                subj3,
                subj2,
                subj4,
                subj9,
                subj6,
                subj12,
                subj8
            };

            var subjPacket3 = new List<Subject>
            {
                subj1,
                subj2,
                subj6,
                subj3,
                subj7,
                subj10,
                subj11
            };

            var subjPacket4 = new List<Subject>
            {
                subj1,
                subj6,
                subj7,
                subj8
            };

            var subjPacket5 = new List<Subject>
            {
                subj10,
                subj12,
                subj4,
                subj7,
                subj5,
                subj2,
                subj11
            };

            var subjPacket6 = new List<Subject>
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

            SaveChanges();

            var s1 = new Student
            {
                FirstName = "Rebecca",
                LastName = "King",
                Age = 16,
                Class = c1,
                ClassId = c1.Id,
                Gender = Gender.Female,
                Image =
                    "https://lh3.googleusercontent.com/fife/AAWUweV5qQx8oFqW_5QSK3xjoE7-4Oe0e-i-dIfLdmlsbd2mXvVCUijsoSQl-O2bvAdNZUeQge2KIruU4txsgsM-Ay85CpeRL2Yq1AllZeMkCT1NvMAA74bSfHhUFe8xzzbLFkjqQoKoO0lMtnGTTXILOLhPJ1fMXS-FvfbUENqMdhAgLkrntnU2bPR4pYLzKa5aEiOjxMZ2EHcmEOlLpPa1JnlNFlr9WH2cDruxbCaXcNHZ0FjpB2fjI2dWXY_3_3Jr3dGw4Pggh_WYL2JU5fy3qwuDv1MdtRbNgibTFojQPEVJ_-eGfFaPZBB6wUAvFC81sw7vVF_XyCKTPm0rn8y9UOve_rLGtxhjD7IY79cK8vqE4u4pHWDlSQnVknbzKdDS-liBfvMPDeNrtXROMlV7g1IJyxlkqrVt_6hMfRCTCUZ2bXxwb3ZqqeCO1x6wTGpY3xVXfjX_HR0jlBjX21Zy8iyXjBP4DUERqZHkSqBLsKpEurdxHiNgMy3BgpSpW8gre6jpMIJO0nOATOUXM0okM0g6-bf_N2vt91UbEHLyl0qgDhrjgpIzj_2EDoLbRkvnu51ONU4DFAQjM5z4iZ9KdB1heagb7lBkRoDH_8E6jmgbpU-z0ZHwmeas5psCqwb-zcvQ4DFDPwKHl1eGup3zx9XK1mwW6uJW-7xgu-jZlYjRrBRmDLqF0q6iGtRZixjMcsMf4sNa4dI5jHuPC4E9VC6QtsPR0c8Ddw=w250-h238-p-k-nu-ft"
            };

            var s2 = new Student
            {
                FirstName = "Melissa",
                LastName = "Thompson",
                Age = 17,
                Class = c1,
                ClassId = c1.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/fe/1b/c8/fe1bc805ff8ad5f0fe3de50d76c98a10.jpg"
            };

            var s3 = new Student
            {
                FirstName = "George",
                LastName = "Long",
                Age = 16,
                Class = c1,
                ClassId = c1.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/50/4a/40/504a40337fa40398b3a456095529bcaa.jpg"
            };

            var s4 = new Student
            {
                FirstName = "Karen",
                LastName = "Bell",
                Age = 16,
                Class = c2,
                ClassId = c2.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/09/7d/3e/097d3ec42955647f62754bf83d5d67c4.jpg"
            };

            var s5 = new Student
            {
                FirstName = "Louise",
                LastName = "Bryant",
                Age = 17,
                Class = c2,
                ClassId = c2.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/8a/df/f2/8adff2075693ad9460ef9dc9899b0d5a.jpg"
            };


            var s6 = new Student
            {
                FirstName = "Carolyn",
                LastName = "Mitchell",
                Age = 16,
                Class = c2,
                ClassId = c2.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/d2/58/bb/d258bbdc60685818768420b9be8efbfb.jpg"
            };

            var s7 = new Student
            {
                FirstName = "Matthew",
                LastName = "Hill",
                Age = 16,
                Class = c2,
                ClassId = c2.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/6b/03/d8/6b03d8981533f204442fb7992d818be9.jpg"
            };

            var s8 = new Student
            {
                FirstName = "Jessica",
                LastName = "Patterson",
                Age = 16,
                Class = c3,
                ClassId = c3.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/ea/c9/1f/eac91f81bdd65d82824ef298c064b908.jpg"
            };

            var s9 = new Student
            {
                FirstName = "Sandra",
                LastName = "Lopez",
                Age = 17,
                Class = c3,
                ClassId = c3.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/c1/42/f6/c142f62b103a03f82766044e68a62065.jpg"
            };

            var s10 = new Student
            {
                FirstName = "Beverly",
                LastName = "Howard",
                Age = 16,
                Class = c3,
                ClassId = c3.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/03/76/1f/03761fc7a9ac6947f5542547d4cfaa31.jpg"
            };

            var s11 = new Student
            {
                FirstName = "Carol",
                LastName = "Campbell",
                Age = 17,
                Class = c3,
                ClassId = c3.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/c9/8b/28/c98b289dcb856c7e1482dcde8ba4987e.jpg"
            };

            var s12 = new Student
            {
                FirstName = "Richard",
                LastName = "Parker",
                Age = 16,
                Class = c3,
                ClassId = c3.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/d0/44/37/d04437c6f17436318ca9388afad4f305.jpg"
            };

            var s13 = new Student
            {
                FirstName = "Todd",
                LastName = "Alexander",
                Age = 17,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/e5/ae/83/e5ae835f8e705a203ff4c6dfd9d01faf.jpg"
            };

            var s14 = new Student
            {
                FirstName = "Donna",
                LastName = "Butler",
                Age = 16,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/44/c6/a0/44c6a0ae9e98e83ea40152b216400457.jpg"
            };

            var s15 = new Student
            {
                FirstName = "Howard",
                LastName = "Roberts",
                Age = 16,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/04/67/28/046728a58d56553e08f6faf837681171.jpg"
            };

            var s16 = new Student
            {
                FirstName = "Frank",
                LastName = "Wilson",
                Age = 16,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/f8/94/81/f89481ccce1d0f1b4d44373985afcb16.jpg"
            };

            var s17 = new Student
            {
                FirstName = "George",
                LastName = "Diaz",
                Age = 16,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/2e/44/f3/2e44f3552b32a499b75411bfab6c2d3b.jpg"
            };

            var s18 = new Student
            {
                FirstName = "Chris",
                LastName = "Griffin",
                Age = 16,
                Class = c4,
                ClassId = c4.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/79/a6/d1/79a6d14de47ed1f017ad7adb932c9574.jpg"
            };

            var s19 = new Student
            {
                FirstName = "Amanda",
                LastName = "James",
                Age = 17,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/45/75/56/457556342f7242fd51e1e92a6726ad2b.jpg"
            };

            var s20 = new Student
            {
                FirstName = "Sandra",
                LastName = "Miller",
                Age = 18,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/16/6b/da/166bda5198c3b4e8e12a33c845237bd5.jpg"
            };

            var s21 = new Student
            {
                FirstName = "Carol",
                LastName = "Washington",
                Age = 17,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/98/f6/87/98f6874787c7796f9e90603396d2568c.jpg"
            };

            var s22 = new Student
            {
                FirstName = "Doris",
                LastName = "Johnson",
                Age = 17,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/d1/3a/4c/d13a4c933675dc8addffe1cefbe230fd.jpg"
            };

            var s23 = new Student
            {
                FirstName = "Eugene",
                LastName = "Wright",
                Age = 16,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/ab/9e/53/ab9e53b748f4a45b1aaad922d0788d54.jpg"
            };

            var s24 = new Student
            {
                FirstName = "Margaret",
                LastName = "Collins",
                Age = 16,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/2f/f8/aa/2ff8aadab8f8c579cef07605dac09cfa.jpg"
            };

            var s25 = new Student
            {
                FirstName = "Henry",
                LastName = "Hall",
                Age = 17,
                Class = c5,
                ClassId = c5.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/6c/ff/06/6cff0629994f5cf5afbe485e684f90dd.jpg"
            };

            var s26 = new Student
            {
                FirstName = "Frances ",
                LastName = "Thomas",
                Age = 14,
                Class = c6,
                ClassId = c6.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/2c/53/86/2c53864a5526760365939f604407992e.jpg"
            };

            var s27 = new Student
            {
                FirstName = "Pamela",
                LastName = "Bennett",
                Age = 15,
                Class = c6,
                ClassId = c6.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/39/52/73/395273a6b2261c3b4d1f7a9473feeebd.jpg"
            };

            var s28 = new Student
            {
                FirstName = "Kimberly",
                LastName = "Morgan",
                Age = 14,
                Class = c6,
                ClassId = c6.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/fc/a9/4e/fca94e39e3057545a69754298d1431bc.jpg"
            };

            var s29 = new Student
            {
                FirstName = "Terry",
                LastName = "Campbell",
                Age = 14,
                Class = c6,
                ClassId = c6.Id,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/236x/55/5f/ad/555fad34030f6cd00c63dbc54074133d.jpg"
            };

            var s30 = new Student
            {
                FirstName = "Rachel",
                LastName = "Patterson",
                Age = 15,
                Class = c6,
                ClassId = c6.Id,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/fb/93/42/fb93429f0f7a1e968c0f2c2e91526159.jpg"
            };

            var s31 = new Student
            {
                FirstName = "Olha",
                LastName = "Cina",
                Age = 11,
                Gender = Gender.Female,
                Image = "https://i.pinimg.com/564x/a5/d9/2a/a5d92a760dadaa43a59704402abb2ae7.jpg"
            };

            var s32 = new Student
            {
                FirstName = "Terry",
                LastName = "Opraa",
                Age = 12,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/94/a1/8b/94a18be3790c298f1649930b64528994.jpg"
            };

            var s33 = new Student
            {
                FirstName = "Sin",
                LastName = "Bad",
                Age = 15,
                Gender = Gender.Male,
                Image = "https://i.pinimg.com/564x/7c/57/47/7c5747448271c2e48c00c5257803a7fd.jpg"
            };

            Students.AddRange(
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

            SaveChanges();

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
            
            SaveChanges();
            
            var admin = new User()
            {
                Email = "nazarfesenko6@gmail.com",
                Password = "Nazar1@3Nazar",
                Role = Roles.FirstOrDefault(r => r.Name == "admin"),
                RoleId = Roles.FirstOrDefault(r => r.Name == "admin")?.Id
            };
            
            var student = new User()
            {
                Email = "nazarfesenk@gmail.com",
                Password = "Nazard1@3Nazar",
                Role = Roles.FirstOrDefault(r => r.Name == "student"),
                RoleId = Roles.FirstOrDefault(r => r.Name == "student")?.Id
            };
            
            Users.AddRange(admin,student);
            
            SaveChanges();

            var stud = Students.FirstOrDefault(s => s.Id == 1);

            stud.User = Users.FirstOrDefault(u => u.Email == "nazarfesenk@gmail.com");

            stud.UserId = Users.FirstOrDefault(u => u.Email == "nazarfesenk@gmail.com").Id;

            var adminUser = new Admin
            {
                Age = 20,
                FirstName = "Nazarii",
                LastName = "Fisenko",
                User = Users.FirstOrDefault(u => u.Email == "nazarfesenko6@gmail.com"),
                UserId = Users.FirstOrDefault(u => u.Email == "nazarfesenko6@gmail.com").Id,
            };
            
            Admins.Add(adminUser);

            SaveChanges();

            var a = Users.FirstOrDefault(u => u.Email == "nazarfesenko6@gmail.com");

            a.Admin = Admins.FirstOrDefault();

            a.AdminId = Admins.FirstOrDefault().Id;
            
            var s = Users.FirstOrDefault(u => u.Email == "nazarfesenk@gmail.com");

            s.Student = Students.Find(1);
            
            s.StudentId = Students.Find(1).Id;

            SaveChanges();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>(p => p.UserId);
            
            modelBuilder
                .Entity<User>()
                .HasOne(u => u.Student)
                .WithOne(a => a.User)
                .HasForeignKey<Student>(p => p.UserId);
            
            modelBuilder
                .Entity<User>()
                .HasOne(u => u.Teacher)
                .WithOne(a => a.User)
                .HasForeignKey<Teacher>(p => p.UserId);
                
        }

        public SchoolDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            InitializeData();
        }
    }
}