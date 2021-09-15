using System;
using System.Collections.Generic;
using System.Linq;
using School.BLL.Dto;
using School.BLL.Services;

namespace School.UI
{
    internal static class Program
    {
        private static void Main()
        {
            var main = new MainService();
            MainInterface(main);
        }

        private static void MainInterface(MainService main)
        {
            Console.WriteLine("********** Welcome to My App **********\n");

            string mainChoice;

            do
            {
                Console.WriteLine("Choose your role:\n");

                Console.WriteLine("\nAdmin: 1\nStudent: 2\nVisitor: 3\nExit: 0\nElse you attempt will be repeat");

                Console.WriteLine();

                mainChoice = Console.ReadLine();

                switch (mainChoice)
                {
                    case "1":
                        AdminInterface(main);
                        break;

                    case "2":
                        StudentInterface(main);
                        break;

                    case "3":
                        VisitorInterface(main);
                        break;
                }
            } while (mainChoice != "0");

            Console.WriteLine("Thanks for use my app, you can test my app like other user)");
        }

        private static void StudentInterface(MainService main)
        {
            var studentService = new StudentService(main);

            string choiceMainStudent;

            Console.WriteLine("So, You can find your page for id and EDIT, DELETE page");

            Console.WriteLine();

            do
            {
                Console.WriteLine("Show my page            1\n" +
                                  "Show my classmates      2\n" +
                                  "Show my class teacher   3\n" +
                                  "Exit                    0\n" +
                                  "Repeat                  any number");

                choiceMainStudent = Console.ReadLine();

                Console.WriteLine();

                int id;

                switch (choiceMainStudent)
                {
                    case "1":
                        Console.WriteLine("\nStudents:\n");
                        foreach (var student in studentService.GetStudents())
                            Console.WriteLine($"{student.Id}\t\t{student.FullName}");

                        Console.WriteLine("\n");

                        Console.WriteLine("Enter your Id ;)\n");

                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("\nYou entered wrong data\n");

                            break;
                        }

                        Console.WriteLine();

                        try
                        {
                            var s = studentService.GetStudentForId(id);

                            if (s != null)
                            {
                                var @class = s.ClassId != null
                                    ? studentService.GetClassForId(s.ClassId)?.Name
                                    : "no class";

                                Console.WriteLine($"FullName: {s.FullName}\n" +
                                                  $"Age:      {s.Age}\n" +
                                                  $"Class:    {@class}\n" +
                                                  $"Gender:   {s.Gender}\n" +
                                                  "Subjects:           \n" +
                                                  studentService.GetSubjects(s.Id).Aggregate("\t\t",
                                                      (current, subject) => current + subject.Name + "\n\t\t")
                                );
                            }
                            else
                            {
                                Console.WriteLine("\n\nYou entered bad value.\n Pls enter correct value :)\n\n");
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("\nStudent not found!\n");

                            break;
                        }

                        Console.WriteLine("\n");

                        string choiceMainStudent1;

                        do
                        {
                            Console.WriteLine("You can edit data");

                            Console.WriteLine("\nEdit       -   1\n" +
                                              "Back         -   0\n" +
                                              "Exit         -   9\n" +
                                              "Repeat       -   any key\n");


                            choiceMainStudent1 = Console.ReadLine();

                            switch (choiceMainStudent1)
                            {
                                case "1":
                                    Console.WriteLine("What do you want edit?\n");

                                    string choiceEdit;

                                    do
                                    {
                                        Console.WriteLine("\n");

                                        try
                                        {
                                            var s = studentService.GetStudentForId(id);

                                            if (s != null)
                                            {
                                                var @class = s.ClassId != null
                                                    ? studentService.GetClassForId(s.ClassId)?.Name
                                                    : "no class";

                                                Console.WriteLine($"FullName: {s.FullName}\n" +
                                                                  $"Age:      {s.Age}\n" +
                                                                  $"Class:    {@class}\n" +
                                                                  $"Gender:   {s.Gender}\n" +
                                                                  "Subjects:           \n" +
                                                                  studentService.GetSubjects(s.Id).Aggregate("\t\t",
                                                                      (current, subject) =>
                                                                          current + subject.Name + "\n\t\t")
                                                );
                                            }
                                            else
                                            {
                                                Console.WriteLine(
                                                    "\n\nYou entered bas value.\n Pls enter correct value :)\n\n");

                                                break;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            Console.WriteLine("Student not found!");

                                            break;
                                        }

                                        Console.WriteLine("\n");

                                        Console.WriteLine("First name       1\n" +
                                                          "Last name        2\n" +
                                                          "Age              3\n" +
                                                          "Gender           4\n" +
                                                          "Class            5\n" +
                                                          "Subjects         6\n" +
                                                          "Back             0\n" +
                                                          "Exit             9\n" +
                                                          "Repeat           any key\n\n");

                                        choiceEdit = Console.ReadLine();

                                        switch (choiceEdit)
                                        {
                                            case "1":
                                                Console.WriteLine("Enter new First name");

                                                var firstName = Console.ReadLine();

                                                if (Validation.FirstOrLastName(firstName))
                                                    studentService.Edit_FirstName(id, firstName);
                                                else
                                                    Console.WriteLine("Error, you entered wrong value");

                                                break;

                                            case "2":
                                                Console.WriteLine("Enter new Last name");

                                                var lastName = Console.ReadLine();

                                                if (Validation.FirstOrLastName(lastName))
                                                    studentService.Edit_LastName(id, lastName);
                                                else
                                                    Console.WriteLine("Error, you entered wrong value");

                                                break;

                                            case "3":
                                                Console.WriteLine("Enter new Age");

                                                int age;

                                                try
                                                {
                                                    age = int.Parse(Console.ReadLine());
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered wrong value");

                                                    break;
                                                }

                                                if (age is >= 5 and <= 18)
                                                    studentService.Edit_Age(id, age);
                                                else
                                                    Console.WriteLine("Error, you entered wrong value");

                                                break;

                                            case "4":
                                                Console.WriteLine("Enter new Gender: \n" +
                                                                  "Male             0\n" +
                                                                  "Female           1\n" +
                                                                  "Other            2\n");

                                                int gender;

                                                try
                                                {
                                                    gender = int.Parse(Console.ReadLine());
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered wrong value");

                                                    break;
                                                }

                                                if (gender != 0 && gender != 1)
                                                    gender = 2;

                                                studentService.Edit_Gender(id, (GenderDto)gender);

                                                break;

                                            case "5":
                                                Console.WriteLine("Classes: \n");

                                                var classes = studentService.GetClasses();

                                                foreach (var c in classes)
                                                    Console.WriteLine($"Id: {c.Id}\t\tName: {c.Name}");

                                                Console.WriteLine("\nEnter class id:");

                                                int classChoice;

                                                try
                                                {
                                                    classChoice = int.Parse(Console.ReadLine());
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered wrong value");

                                                    break;
                                                }

                                                try
                                                {
                                                    studentService.Edit_Class(id, classChoice);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered wrong id");
                                                }

                                                break;

                                            case "6":
                                                Console.WriteLine("Subjects: \n");

                                                var subjects = studentService.GetSubjects();

                                                foreach (var subject in subjects)
                                                    Console.WriteLine($"Id: {subject.Id}\t\tName: {subject.Name}");

                                                var subjectIds = new List<int>();

                                                Console.WriteLine("Enter subjects id, pls input only correct value)");

                                                while (true)
                                                {
                                                    int subId;
                                                    try
                                                    {
                                                        subId = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception)
                                                    {
                                                        continue;
                                                    }

                                                    if (subId == 0) break;

                                                    subjectIds.Add(subId);
                                                }

                                                try
                                                {
                                                    studentService.Edit_Subjects(id, subjectIds);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered some wrong subjects id");
                                                }

                                                break;
                                        }
                                    } while (choiceEdit != "0");

                                    break;

                                case "9":
                                    return;
                            }
                        } while (choiceMainStudent1 != "0");

                        break;

                    case "2":
                        Console.WriteLine("\nStudents:\n");
                        foreach (var student in studentService.GetStudents())
                            Console.WriteLine($"{student.Id}\t\t{student.FullName}");

                        Console.WriteLine("\n");

                        Console.WriteLine("Enter your Id ;)\n");

                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("\nYpu entered wrong value\n");

                            break;
                        }

                        Console.WriteLine();

                        try
                        {
                            if (studentService.GetClassmates(id)?.Count >= 1)
                            {
                                Console.WriteLine("\n\n");

                                foreach (var student in studentService.GetClassmates(id))
                                    Console.WriteLine(student.FullName);

                                Console.WriteLine("\n\n");
                            }
                            else
                            {
                                Console.WriteLine("\n\nNo classmates (:\n\n");
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You entered wrong id");

                            break;
                        }

                        Console.WriteLine("Enter any key to continue use my app))))");

                        Console.ReadKey();

                        break;

                    case "3":
                        Console.WriteLine("\nStudents:\n");
                        foreach (var student in studentService.GetStudents())
                            Console.WriteLine($"{student.Id}\t\t{student.FullName}");

                        Console.WriteLine("\n");

                        Console.WriteLine("Enter your Id ;)\n");

                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You entered wrong value");

                            break;
                        }

                        Console.WriteLine();

                        TeacherDto t;

                        try
                        {
                            t = studentService.GetMyClassTeacher(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("We dont found teacher");

                            break;
                        }

                        if (t != null)
                            Console.WriteLine($"FullName: {t.FullName}\n" +
                                              $"Age:      {t.Age}\n");
                        else
                            Console.WriteLine("This student doesn't have teacher\\class");

                        Console.WriteLine("Enter any key to continue use my app))))");

                        Console.ReadKey();

                        break;
                }
            } while (choiceMainStudent != "0");
        }

        private static void AdminInterface(MainService main)
        {
            var adminService = new AdminService(main);

            string choiceMain;

            Console.WriteLine("You can make CRUD operation with every table:\n");

            do
            {
                Console.WriteLine("Students:            1\n" +
                                  "Subjects:            2\n" +
                                  "Teachers:            3\n" +
                                  "Classes:             4\n" +
                                  "Exit                 0\n" +
                                  "Repeat               any key");

                choiceMain = Console.ReadLine();

                switch (choiceMain)
                {
                    case "1":
                        Console.WriteLine("\n");

                        string choiceCrud;

                        do
                        {
                            Console.WriteLine("Get all info         1\n" +
                                              "Get for id           2\n" +
                                              "Edit                 3\n" +
                                              "Create               4\n" +
                                              "Delete               5\n" +
                                              "Back                 0\n" +
                                              "Exit                 9\n" +
                                              "Repeat               any key");

                            Console.WriteLine();

                            choiceCrud = Console.ReadLine();

                            Console.WriteLine();

                            int? classId;

                            switch (choiceCrud)
                            {
                                case "1":
                                    foreach (var student in adminService.Students_GetAll())
                                    {
                                        Console.WriteLine(
                                            "\n\n----------------------------------------------------------\n\n");

                                        var @class = student.ClassId != null
                                            ? adminService
                                                .Classes_GetForId(student.ClassId).Name
                                            : "no class";

                                        Console.WriteLine($"Full name:      {student.FullName}\n" +
                                                          $"Age:            {student.Age}\n" +
                                                          $"Gender:         {student.Gender}\n" +
                                                          $"Class:          {@class}\n" +
                                                          "Subjects:\n");

                                        foreach (var subject in adminService.Students_GetSubjectsForId(student.Id))
                                            Console.WriteLine(subject);

                                        Console.WriteLine();
                                    }

                                    Console.WriteLine("\n");

                                    break;

                                case "2":
                                    Console.WriteLine();

                                    foreach (var studentDto in adminService.Students_GetAll())
                                        Console.WriteLine($"{studentDto.Id}\t\t{studentDto.FullName}");

                                    Console.WriteLine();

                                    Console.Write("\nEnter student id:\t");

                                    int id;

                                    try
                                    {
                                        id = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered wrong value");

                                        break;
                                    }

                                    Console.WriteLine();

                                    StudentDto studentForId;

                                    try
                                    {
                                        studentForId = adminService.Students_GetForId(id);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered wrong value");

                                        break;
                                    }

                                    var class1 = studentForId.ClassId != null
                                        ? adminService
                                            .Classes_GetForId(studentForId.ClassId)
                                            .Name
                                        : "no class";

                                    Console.WriteLine($"Full name:      {studentForId.FullName}\n" +
                                                      $"Age:            {studentForId.Age}\n" +
                                                      $"Gender:         {studentForId.Gender}\n" +
                                                      $"Class:          {class1}\n" +
                                                      "Subjects:\n");

                                    foreach (var subject in adminService.Students_GetSubjectsForId(studentForId.Id))
                                        Console.WriteLine(subject);

                                    Console.WriteLine("\n");

                                    break;

                                case "3":
                                    Console.WriteLine();

                                    foreach (var studentDto in adminService.Students_GetAll())
                                        Console.WriteLine($"{studentDto.Id}\t\t{studentDto.FullName}");

                                    Console.WriteLine();

                                    string isContinue;

                                    string editChoice;

                                    Console.Write("Enter student number whose you want edit:");

                                    int idEdit;

                                    try
                                    {
                                        idEdit = int.Parse(Console.ReadLine());

                                        _ = adminService.Students_GetForId(idEdit);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered wrong value!!\n");

                                        break;
                                    }

                                    Console.WriteLine();

                                    do
                                    {
                                        Console.WriteLine("\nWhat do you want edit?\n" +
                                                          "First name           -   1\n" +
                                                          "Last name            -   2\n" +
                                                          "Age                  -   3\n" +
                                                          "Gender               -   4\n" +
                                                          "Class                -   5\n" +
                                                          "Subjects             -   6\n" +
                                                          "Back                 -   0\n" +
                                                          "Exit                 -   9\n" +
                                                          "Repeat               -   any key\n");

                                        editChoice = Console.ReadLine();

                                        switch (editChoice)
                                        {
                                            case "1":
                                                Console.WriteLine("Enter first name:\n");

                                                var firstNameEdit = Console.ReadLine();

                                                if (Validation.FirstOrLastName(firstNameEdit))
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    adminService.Students_Edit_FirstName(idEdit, firstNameEdit);

                                                    Console.WriteLine("First name was edit)");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Some error with validation");
                                                }

                                                break;

                                            case "2":
                                                Console.WriteLine("Enter last name:\n");

                                                var lastNameEdit = Console.ReadLine();

                                                if (Validation.FirstOrLastName(lastNameEdit))
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    adminService.Students_Edit_LastName(idEdit, lastNameEdit);

                                                    Console.WriteLine("Last name was edit)");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Some error with validation");
                                                }

                                                break;

                                            case "3":
                                                Console.WriteLine("Enter age:\n");

                                                int ageEdit;

                                                try
                                                {
                                                    ageEdit = int.Parse(Console.ReadLine());
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered wrong value");

                                                    break;
                                                }

                                                if (ageEdit is >= 5 and <= 18)
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    adminService.Students_Edit_Age(idEdit, ageEdit);

                                                    Console.WriteLine("Age was edit)");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Error with validation");
                                                }

                                                break;

                                            case "4":
                                                Console.WriteLine("Enter gender(0 - male, 1 - female, 2 - other:\n");

                                                int genderEdit;

                                                try
                                                {
                                                    genderEdit = int.Parse(Console.ReadLine());
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered wrong value");

                                                    break;
                                                }

                                                if (genderEdit is not 0 and not 1)
                                                    genderEdit = 2;

                                                try
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    adminService.Students_Edit_Gender(idEdit, (GenderDto)genderEdit);

                                                    Console.WriteLine("Gender was edit)");
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("Some error");
                                                }

                                                break;

                                            case "5":
                                                Console.WriteLine("Classes: \n");

                                                foreach (var c in adminService.Classes_GetAll())
                                                    Console.WriteLine($"{c.Id}\t\t{c.Name}");

                                                Console.WriteLine("\nEnter class number:");

                                                try
                                                {
                                                    classId = int.Parse(Console.ReadLine());
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered incorrect data");

                                                    break;
                                                }

                                                try
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    adminService.Students_Edit_Class(idEdit, classId);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("Class not found, you entered wrong id");

                                                    break;
                                                }

                                                Console.WriteLine("\nClass was edit\n");

                                                break;

                                            case "6":

                                                Console.WriteLine("Subjects: \n");

                                                var subs = adminService.Subjects_GetAll();

                                                foreach (var subject in subs)
                                                    Console.WriteLine($"{subject.Id}\t\t{subject.Name}");

                                                var subjectIds = new List<int>();

                                                Console.WriteLine(
                                                    "Enter subjects numbers, pls input only correct value)" +
                                                    "\nEnter 0 to stop\n");

                                                while (true)
                                                {
                                                    int subId;

                                                    try
                                                    {
                                                        subId = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception)
                                                    {
                                                        continue;
                                                    }

                                                    if (subId == 0) break;

                                                    subjectIds.Add(subId);
                                                }

                                                try
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    adminService.Students_Edit_Subjects(idEdit, subjectIds);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("\nYou entered wrong value");

                                                    break;
                                                }

                                                Console.WriteLine("Subjects was edit");

                                                break;

                                            case "9":
                                                return;
                                        }
                                    } while (editChoice != "0");

                                    break;

                                case "4":
                                    string firstName;

                                    string lastName;

                                    int age;

                                    GenderDto gender;

                                    var subjects = new List<int>();

                                    Console.Write("Enter first name:\t");

                                    firstName = Console.ReadLine();

                                    if (!Validation.FirstOrLastName(firstName))
                                        break;

                                    Console.WriteLine();

                                    Console.Write("Enter last name:\t");

                                    lastName = Console.ReadLine();

                                    if (!Validation.FirstOrLastName(lastName))
                                        break;

                                    Console.WriteLine();

                                    Console.Write("Enter age:\t");

                                    age = int.Parse(Console.ReadLine());

                                    if (age is < 5 or > 18)
                                        break;

                                    Console.WriteLine();

                                    Console.Write("Enter gender:(" +
                                                  "\n0 - Male" +
                                                  "\n1 - Female" +
                                                  "\n2 - Other)\n");

                                    var tmp1 = int.Parse(Console.ReadLine());

                                    if (tmp1 is not (0 or 1 or 2))
                                        break;

                                    gender = (GenderDto)tmp1;

                                    Console.WriteLine();

                                    if (adminService.Classes_GetAll().Any())
                                    {
                                        Console.WriteLine("Classes: \n");

                                        foreach (var c in adminService.Classes_GetAll())
                                            Console.WriteLine($"{c.Id}\t\t{c.Name}");

                                        Console.WriteLine("\n0\t\tNo class\n" +
                                                          "Enter class id:");

                                        try
                                        {
                                            classId = int.Parse(Console.ReadLine());
                                        }
                                        catch (Exception)
                                        {
                                            Console.WriteLine("You entered wrong value");

                                            break;
                                        }

                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        classId = 0;
                                    }

                                    if (classId == 0)
                                        classId = null;

                                    Console.WriteLine("Subjects:\n");

                                    foreach (var subject in adminService.Subjects_GetAll())
                                        Console.WriteLine($"{subject.Id}\t\t{subject.Name}");

                                    Console.WriteLine("Please enter 0 to stop)\"" +
                                                      "Please input only correct id");

                                    Console.WriteLine();

                                    while (true)
                                    {
                                        int tmp;

                                        try
                                        {
                                            tmp = int.Parse(Console.ReadLine());
                                        }
                                        catch (Exception)
                                        {
                                            Console.WriteLine("You entered incorrect data format\n");

                                            break;
                                        }

                                        if (tmp == 0) break;

                                        subjects.Add(tmp);
                                    }

                                    try
                                    {
                                        Console.WriteLine("Dou you want continue to create?");

                                        Console.WriteLine("Yes\t-\ty");

                                        Console.WriteLine("No\t-\tany key");

                                        isContinue = Console.ReadLine();

                                        if (isContinue != "y") break;

                                        adminService.Student_Create(new StudentDto
                                        {
                                            Age = age,
                                            ClassId = classId,
                                            FirstName = firstName,
                                            LastName = lastName,
                                            Gender = gender,
                                            SubjectIds = subjects
                                        });
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Some error, you can't create new student");

                                        break;
                                    }

                                    Console.WriteLine("Student was created");

                                    break;

                                case "5":
                                    foreach (var studentDto in adminService.Students_GetAll())
                                        Console.WriteLine($"{studentDto.Id}\t\t{studentDto.FullName}");

                                    Console.WriteLine();

                                    Console.Write("Input number:\t");

                                    try
                                    {
                                        id = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered wrong data");

                                        break;
                                    }

                                    Console.WriteLine();

                                    try
                                    {
                                        Console.WriteLine("Dou you want continue to delete?");

                                        Console.WriteLine("Yes\t-\ty");

                                        Console.WriteLine("No\t-\tany key");

                                        isContinue = Console.ReadLine();

                                        if (isContinue != "y") break;

                                        Console.WriteLine();

                                        adminService.Student_Delete(id);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered wrong value!!!\n\n");

                                        break;
                                    }

                                    Console.WriteLine("\nData was deleted\n");

                                    break;

                                case "9":
                                    return;
                            }
                        } while (choiceCrud != "0");

                        break;

                    case "2":

                        do
                        {
                            Console.WriteLine("Get all info         1\n" +
                                              "Get for id           2\n" +
                                              "Edit                 3\n" +
                                              "Create               4\n" +
                                              "Delete               5\n" +
                                              "Back                 0\n" +
                                              "Exit                 9\n" +
                                              "Repeat               any key");

                            Console.WriteLine();

                            choiceCrud = Console.ReadLine();

                            Console.WriteLine();

                            switch (choiceCrud)
                            {
                                case "1":
                                    foreach (var subject in adminService.Subjects_GetAll())
                                    {
                                        Console.WriteLine(
                                            "\n\n----------------------------------------------------------\n\n");
                                        Console.WriteLine($"Name:            {subject.Name}\n" +
                                                          "Students:\n");

                                        foreach (var student in adminService.Subjects_GetStudentsForId(subject.Id))
                                            Console.WriteLine(student);

                                        Console.WriteLine("\nTeachers:\n");

                                        foreach (var teacher in adminService.Subjects_GetTeachersForId(subject.Id))
                                            Console.WriteLine(teacher);
                                    }

                                    Console.WriteLine();
                                    Console.WriteLine();

                                    break;

                                case "2":
                                    Console.WriteLine();

                                    foreach (var subjectDto in adminService.Subjects_GetAll())
                                        Console.WriteLine($"{subjectDto.Id}\t\t{subjectDto.Name}");

                                    Console.WriteLine("\nEnter subject number");

                                    int iD;

                                    try
                                    {
                                        iD = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered incorrect format");

                                        break;
                                    }

                                    SubjectDto subjectForId;

                                    try
                                    {
                                        subjectForId = adminService.Subjects_GetForId(iD);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Subject don't found, maybe you entered wrong id\n");

                                        break;
                                    }

                                    Console.WriteLine($"Name:            {subjectForId.Name}\n" +
                                                      "Students:\n");

                                    foreach (var student in adminService.Subjects_GetStudentsForId(subjectForId.Id))
                                        Console.WriteLine(student);

                                    Console.WriteLine("\nTeachers:\n");

                                    foreach (var teacher in adminService.Subjects_GetTeachersForId(subjectForId.Id))
                                        Console.WriteLine(teacher);

                                    Console.WriteLine();

                                    break;

                                case "3":
                                    string isContinue;

                                    foreach (var subjectDto in adminService.Subjects_GetAll())
                                        Console.WriteLine($"{subjectDto.Id}\t\t{subjectDto.Name}");

                                    int idSubject;

                                    Console.Write("\nEnter subject id to edit:\t");

                                    try
                                    {
                                        idSubject = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered incorrect format\n");

                                        break;
                                    }

                                    try
                                    {
                                        _ = adminService.Subjects_GetForId(idSubject);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Subject do not found\n");

                                        break;
                                    }

                                    string editChoice;

                                    do
                                    {
                                        Console.WriteLine("What do you want edit?\n" +
                                                          "Name         -   1\n" +
                                                          "Students     -   2\n" +
                                                          "Teachers     -   3\n" +
                                                          "Back         -   0\n" +
                                                          "Exit         -   9\n" +
                                                          "Repeat       -   any key\n");

                                        editChoice = Console.ReadLine();

                                        switch (editChoice)
                                        {
                                            case "1":
                                                Console.Write("Enter name:\t");

                                                var nameEditSubject = Console.ReadLine();

                                                if (!Validation.SubjectName(nameEditSubject))
                                                    break;

                                                try
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    Console.WriteLine();

                                                    adminService.Subject_Edit_Name(idSubject, nameEditSubject);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("Any error");

                                                    break;
                                                }

                                                Console.WriteLine("Name was edit)\n");

                                                break;

                                            case "2":

                                                Console.WriteLine("\nStudents:\n");

                                                foreach (var studentDto in adminService.Students_GetAll())
                                                    Console.WriteLine($"{studentDto.Id}\t\t{studentDto.FullName}");

                                                Console.WriteLine();

                                                var sIds = new List<int>();

                                                Console.WriteLine(
                                                    "Enter student numbers to add subject, pls input only correct value\n");

                                                while (true)
                                                {
                                                    int sId;

                                                    try
                                                    {
                                                        sId = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception)
                                                    {
                                                        continue;
                                                    }

                                                    if (sId == 0) break;

                                                    sIds.Add(sId);
                                                }

                                                try
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    Console.WriteLine();

                                                    adminService.Subjects_Edit_Students(idSubject, sIds);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered wrong subject numbers");

                                                    break;
                                                }

                                                Console.WriteLine("Students was edit\n");

                                                break;

                                            case "3":
                                                Console.WriteLine("\nTeachers:\n");

                                                foreach (var teacherDto in adminService.Teachers_GetAll())
                                                    Console.WriteLine($"{teacherDto.Id}\t\t{teacherDto.FullName}");

                                                Console.WriteLine();

                                                var tIds = new List<int>();

                                                Console.WriteLine(
                                                    "Enter teachers numbers to add subject, pls input only correct value\n");

                                                while (true)
                                                {
                                                    int tId;

                                                    try
                                                    {
                                                        tId = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception)
                                                    {
                                                        continue;
                                                    }

                                                    if (tId == 0) break;

                                                    tIds.Add(tId);
                                                }

                                                try
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    Console.WriteLine();

                                                    adminService.Subjects_Edit_Teachers(idSubject, tIds);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered wrong teachers number\n");

                                                    break;
                                                }

                                                Console.WriteLine("Teachers was edited\n");

                                                break;

                                            case "9":
                                                return;
                                        }
                                    } while (editChoice != "0");

                                    break;

                                case "4":
                                    Console.WriteLine("Dou you want continue to create?");

                                    Console.WriteLine("Yes\t-\ty");

                                    Console.WriteLine("No\t-\tany key");

                                    isContinue = Console.ReadLine();

                                    if (isContinue != "y") break;

                                    Console.WriteLine();

                                    Console.WriteLine("Enter name: ");

                                    var name = Console.ReadLine();

                                    if (!Validation.SubjectName(name))
                                        break;

                                    Console.WriteLine("\nStudents:\n");

                                    foreach (var studentDto in adminService.Students_GetAll())
                                        Console.WriteLine($"{studentDto.Id}\t\t{studentDto.FullName}");

                                    Console.WriteLine();

                                    var studIds = new List<int>();

                                    Console.WriteLine(
                                        "Enter student numbers to add subject, pls input only correct value\n");

                                    while (true)
                                    {
                                        int sId;

                                        try
                                        {
                                            sId = int.Parse(Console.ReadLine());
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }

                                        if (sId == 0) break;

                                        studIds.Add(sId);
                                    }

                                    Console.WriteLine("\nTeachers:\n");

                                    foreach (var teacherDto in adminService.Teachers_GetAll())
                                        Console.WriteLine($"{teacherDto.Id}\t\t{teacherDto.FullName}");

                                    Console.WriteLine();

                                    var teachIds = new List<int>();

                                    Console.WriteLine(
                                        "Enter teachers numbers to add subject, pls input only correct value\n");

                                    while (true)
                                    {
                                        int tId;

                                        try
                                        {
                                            tId = int.Parse(Console.ReadLine());
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }

                                        if (tId == 0) break;

                                        teachIds.Add(tId);
                                    }

                                    try
                                    {
                                        Console.WriteLine("Dou you want continue to create?");

                                        Console.WriteLine("Yes\t-\ty");

                                        Console.WriteLine("No\t-\tany key");

                                        isContinue = Console.ReadLine();

                                        if (isContinue != "y") break;

                                        Console.WriteLine();

                                        adminService.Subject_Create(new SubjectDto
                                        {
                                            Name = name,
                                            TeacherIds = teachIds,
                                            StudentIds = studIds
                                        });
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Error, Subject wasn't create\n" +
                                                          "You entered wrong value\n");

                                        break;
                                    }

                                    Console.WriteLine("New subject was created");

                                    break;

                                case "5":

                                    foreach (var subjectDto in adminService.Subjects_GetAll())
                                        Console.WriteLine($"{subjectDto.Id}\t\t{subjectDto.Name}");

                                    Console.Write("Input number:\t");

                                    int id;

                                    try
                                    {
                                        id = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered incorrect format id\n");

                                        break;
                                    }

                                    Console.WriteLine();

                                    try
                                    {
                                        Console.WriteLine("Dou you want continue to delete?");

                                        Console.WriteLine("Yes\t-\ty");

                                        Console.WriteLine("No\t-\tany key");

                                        isContinue = Console.ReadLine();

                                        if (isContinue != "y") break;

                                        Console.WriteLine();

                                        adminService.Subject_Delete(id);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You can't delete subject\n" +
                                                          "You entered wrong id or subject do not found\n");

                                        break;
                                    }

                                    Console.WriteLine("\nData was deleted\n");

                                    break;

                                case "9":
                                    return;
                            }
                        } while (choiceCrud != "0");

                        break;

                    case "3":
                        do
                        {
                            Console.WriteLine("Get all info         1\n" +
                                              "Get for id           2\n" +
                                              "Edit                 3\n" +
                                              "Create               4\n" +
                                              "Delete               5\n" +
                                              "Back                 0\n" +
                                              "Exit                 9\n" +
                                              "Repeat               any key");

                            Console.WriteLine();

                            choiceCrud = Console.ReadLine();

                            Console.WriteLine();

                            string @class;

                            int? classId;

                            int age;

                            GenderDto gender;

                            switch (choiceCrud)
                            {
                                case "1":
                                    Console.WriteLine();

                                    foreach (var teacher in adminService.Teachers_GetAll())
                                    {
                                        Console.WriteLine(
                                            "\n\n----------------------------------------------------------\n\n");

                                        @class = adminService
                                            .Classes_GetAll()
                                            .FirstOrDefault(c =>
                                                c.TeacherId == teacher.Id)
                                            ?.Name ?? "no class";

                                        Console.WriteLine($"Full name:      {teacher.FullName}\n" +
                                                          $"Age:            {teacher.Age}\n" +
                                                          $"Gender:         {teacher.Gender}\n" +
                                                          $"Class:          {@class}\n" +
                                                          "Subjects:\n");

                                        foreach (var subject in adminService.Teachers_GetSubjectsForId(teacher.Id))
                                            Console.WriteLine(subject);
                                    }

                                    Console.WriteLine();
                                    Console.WriteLine();

                                    break;

                                case "2":
                                    Console.WriteLine();

                                    foreach (var teacherDto in adminService.Teachers_GetAll())
                                        Console.WriteLine($"{teacherDto.Id}\t\t{teacherDto.FullName}");

                                    Console.WriteLine();

                                    Console.Write("Enter number to find teacher:\t");

                                    int teacherId;

                                    try
                                    {
                                        teacherId = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered wrong format id\n");

                                        break;
                                    }

                                    TeacherDto teachersGetForId;

                                    try
                                    {
                                        teachersGetForId = adminService.Teachers_GetForId(teacherId);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Teacher not found!");

                                        break;
                                    }

                                    @class = adminService
                                        .Classes_GetAll()
                                        .FirstOrDefault(c =>
                                            c.TeacherId == teacherId)
                                        ?.Name ?? "no class";

                                    Console.WriteLine($"Full name:      {teachersGetForId.FullName}\n" +
                                                      $"Age:            {teachersGetForId.Age}\n" +
                                                      $"Gender:         {teachersGetForId.Gender}\n" +
                                                      $"Class:          {@class}\n" +
                                                      "Subjects:\n");

                                    foreach (var subject in adminService.Teachers_GetSubjectsForId(teachersGetForId.Id))
                                        Console.WriteLine(subject);

                                    Console.WriteLine();

                                    break;

                                case "3":
                                    string isContinue;

                                    foreach (var teacherDto in adminService.Teachers_GetAll())
                                        Console.WriteLine($"{teacherDto.Id}\t\t{teacherDto.FullName}");

                                    string editChoice;

                                    Console.Write("Enter teacher id to edit:");

                                    int idEdit;

                                    try
                                    {
                                        idEdit = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered wrong format id\n");

                                        break;
                                    }

                                    try
                                    {
                                        _ = adminService.Teachers_GetForId(idEdit);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered wrong id, teacher do not found\n");

                                        break;
                                    }

                                    Console.WriteLine();

                                    do
                                    {
                                        Console.WriteLine("\nWhat do you want edit?\n" +
                                                          "First name           -   1\n" +
                                                          "Last name            -   2\n" +
                                                          "Age                  -   3\n" +
                                                          "Gender               -   4\n" +
                                                          "Class                -   5\n" +
                                                          "Subjects             -   6\n" +
                                                          "Back                 -   0\n" +
                                                          "Exit                 -   9\n" +
                                                          "Repeat               -   any key\n");

                                        editChoice = Console.ReadLine();

                                        switch (editChoice)
                                        {
                                            case "1":
                                                Console.Write("\nEnter first name:\t");

                                                var fName = Console.ReadLine();

                                                if (!Validation.FirstOrLastName(fName))
                                                    break;

                                                Console.WriteLine("Dou you want continue to edit?");

                                                Console.WriteLine("Yes\t-\ty");

                                                Console.WriteLine("No\t-\tany key");

                                                isContinue = Console.ReadLine();

                                                if (isContinue != "y") break;

                                                Console.WriteLine();

                                                adminService.Teachers_Edit_FirstName(idEdit, fName);

                                                Console.WriteLine("First name was edit");

                                                break;

                                            case "2":
                                                Console.Write("\nEnter last name:\t");

                                                var lName = Console.ReadLine();

                                                if (!Validation.FirstOrLastName(lName))
                                                    break;

                                                Console.WriteLine("Dou you want continue to edit?");

                                                Console.WriteLine("Yes\t-\ty");

                                                Console.WriteLine("No\t-\tany key");

                                                isContinue = Console.ReadLine();

                                                if (isContinue != "y") break;

                                                Console.WriteLine();

                                                adminService.Teachers_Edit_LastName(idEdit, lName);

                                                Console.WriteLine("Last name was edit");

                                                break;

                                            case "3":
                                                Console.Write("\nEnter age:\t");

                                                try
                                                {
                                                    age = int.Parse(Console.ReadLine());
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered wrong value\n");

                                                    break;
                                                }

                                                if (age is < 18 or > 80)
                                                    break;

                                                Console.WriteLine("Dou you want continue to edit?");

                                                Console.WriteLine("Yes\t-\ty");

                                                Console.WriteLine("No\t-\tany key");

                                                isContinue = Console.ReadLine();

                                                if (isContinue != "y") break;

                                                Console.WriteLine();

                                                adminService.Teachers_Edit_Age(idEdit, age);

                                                Console.WriteLine("Age was edit");

                                                break;

                                            case "4":
                                                Console.WriteLine("\nEnter gender:\n" +
                                                                  "Male  -  0\n" +
                                                                  "Female    - 1\n" +
                                                                  "Other  -   2)\n");

                                                try
                                                {
                                                    gender = (GenderDto)int.Parse(Console.ReadLine());
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered wrong value\n");

                                                    break;
                                                }

                                                Console.WriteLine("Dou you want continue to edit?");

                                                Console.WriteLine("Yes\t-\ty");

                                                Console.WriteLine("No\t-\tany key");

                                                isContinue = Console.ReadLine();

                                                if (isContinue != "y") break;

                                                Console.WriteLine();

                                                adminService.Teachers_Edit_Gender(idEdit, gender);

                                                Console.WriteLine("Gender was edit");

                                                break;

                                            case "5":
                                                Console.WriteLine("Do you want add class?\n" +
                                                                  "Yes  -   1\n" +
                                                                  "No   -   any key\n");

                                                var choiceClassAdd = Console.ReadLine();

                                                classId = null;

                                                if (choiceClassAdd == "1")
                                                {
                                                    Console.WriteLine();
                                                    if (adminService.GetClassWithOutTeacher().Any())
                                                    {
                                                        foreach (var classDto in adminService.GetClassWithOutTeacher())
                                                            Console.WriteLine($"{classDto.Id}\t\t{classDto.Name}");

                                                        Console.Write("\nEnter id:\t");

                                                        try
                                                        {
                                                            classId = int.Parse(Console.ReadLine());
                                                        }
                                                        catch (Exception)
                                                        {
                                                            Console.WriteLine("You entered incorrect format id\n");

                                                            break;
                                                        }
                                                    }
                                                }

                                                try
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    Console.WriteLine();

                                                    adminService.Teachers_Edit_Class(idEdit, classId);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("Some error\n");

                                                    break;
                                                }

                                                Console.WriteLine("Class was edit");

                                                break;

                                            case "6":

                                                Console.WriteLine("Subjects:\n");

                                                foreach (var subjectDto in adminService.Subjects_GetAll())
                                                    Console.WriteLine($"{subjectDto.Id}\t\t{subjectDto.Name}");

                                                var subjectIds = new List<int>();

                                                Console.WriteLine("\nEnter subject ids:");

                                                do
                                                {
                                                    int subId;

                                                    try
                                                    {
                                                        subId = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.WriteLine("Some format error\n");

                                                        break;
                                                    }

                                                    if (subId == 0) break;

                                                    subjectIds.Add(subId);
                                                } while (true);

                                                try
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    Console.WriteLine();

                                                    adminService.Teachers_Edit_Subjects(idEdit, subjectIds);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("Error!!! Subjects wasn't edit");

                                                    break;
                                                }

                                                Console.WriteLine("Subjects was edit");

                                                break;

                                            case "9":
                                                return;
                                        }
                                    } while (editChoice != "0");

                                    break;

                                case "4":
                                    string firstName;

                                    string lastName;

                                    Console.Write("Enter first name:\t");

                                    firstName = Console.ReadLine();

                                    if (!Validation.FirstOrLastName(firstName))
                                        break;

                                    Console.Write("Enter last name:\t");

                                    lastName = Console.ReadLine();

                                    if (!Validation.FirstOrLastName(lastName))
                                        break;

                                    Console.Write("Enter age:\t");

                                    age = int.Parse(Console.ReadLine());

                                    if (age is < 18 or > 80)
                                        break;

                                    Console.Write("Enter gender\n" +
                                                  "Male - 0\n" +
                                                  "Female - 1\n" +
                                                  "Other - 2):\n");

                                    gender = (GenderDto)int.Parse(Console.ReadLine());

                                    if ((int)gender is not (0 or 1 or 2))
                                        break;

                                    Console.WriteLine("Do you want add class to teacher?" +
                                                      "Yes      -   1\n" +
                                                      "No       -   other key\n");

                                    var choiceClass = Console.ReadLine();

                                    if (choiceClass == "1")
                                    {
                                        Console.Write("Enter class:\t");

                                        var classes = adminService.GetClassWithOutTeacher().ToList();

                                        if (!classes.Any())
                                        {
                                            Console.WriteLine("We dont find free classes\n");

                                            classId = null;
                                        }
                                        else
                                        {
                                            foreach (var classDto in classes)
                                                Console.WriteLine($"{classDto.Id}\t\t{classDto.Name}");

                                            Console.WriteLine("\nEnter class id:");

                                            try
                                            {
                                                classId = int.Parse(Console.ReadLine());
                                            }
                                            catch (Exception)
                                            {
                                                Console.WriteLine("You entered wrong format id\n");

                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        classId = null;
                                    }

                                    Console.WriteLine("\nSubjects:\n");

                                    foreach (var subject in adminService.Subjects_GetAll())
                                        Console.WriteLine($"{subject.Id}\t\t{subject.Name}");

                                    Console.WriteLine();

                                    var subIds = new List<int>();

                                    Console.WriteLine("Enter subjects");

                                    do
                                    {
                                        int subId;

                                        try
                                        {
                                            subId = int.Parse(Console.ReadLine());
                                        }
                                        catch (Exception)
                                        {
                                            Console.WriteLine("You entered wrong format id");

                                            break;
                                        }

                                        if (subId == 0) break;

                                        subIds.Add(subId);
                                    } while (true);

                                    try
                                    {
                                        Console.WriteLine("Do you want continue to create?");

                                        Console.WriteLine("Yes\t-\ty");

                                        Console.WriteLine("No\t-\tany key");

                                        isContinue = Console.ReadLine();

                                        if (isContinue != "y") break;

                                        adminService.Teacher_Create(new TeacherDto
                                        {
                                            FirstName = firstName,
                                            LastName = lastName,
                                            Age = age,
                                            ClassId = classId,
                                            Gender = gender,
                                            SubjectIds = subIds
                                        });
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine(
                                            "Error!!! Teacher wasn't created!!! Maybe you entered wrong data");

                                        break;
                                    }

                                    Console.WriteLine("Teacher was added\n");

                                    break;

                                case "5":
                                    foreach (var teacherDto in adminService.Teachers_GetAll())
                                        Console.WriteLine($"{teacherDto.Id}\t\t{teacherDto.FullName}");

                                    Console.Write("Input number:\t");

                                    int id;

                                    try
                                    {
                                        id = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered incorrect format id\n");

                                        break;
                                    }

                                    Console.WriteLine();

                                    try
                                    {
                                        Console.WriteLine("Dou you want continue to delete?");

                                        Console.WriteLine("Yes\t-\ty");

                                        Console.WriteLine("No\t-\tany key");

                                        isContinue = Console.ReadLine();

                                        if (isContinue != "y") break;

                                        Console.WriteLine();

                                        adminService.Teacher_Delete(id);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine(
                                            "You entered incorrect id and we can't found teacher to delete\n");

                                        break;
                                    }

                                    Console.WriteLine("\nData was deleted\n");

                                    break;

                                case "9":
                                    return;
                            }
                        } while (choiceCrud != "0");

                        break;

                    case "4":
                        do
                        {
                            Console.WriteLine("Get all info         1\n" +
                                              "Get for id           2\n" +
                                              "Edit                 3\n" +
                                              "Create               4\n" +
                                              "Delete               5\n" +
                                              "Back                 0\n" +
                                              "Exit                 9\n" +
                                              "Repeat               any key");

                            Console.WriteLine();

                            choiceCrud = Console.ReadLine();

                            Console.WriteLine();

                            switch (choiceCrud)
                            {
                                case "1":
                                    foreach (var @class in adminService.Classes_GetAll())
                                    {
                                        Console.WriteLine(
                                            "\n\n----------------------------------------------------------\n\n");

                                        var t1 = @class.TeacherId != null
                                            ? adminService
                                                .Teachers_GetForId(@class.TeacherId)?
                                                .FullName
                                            : "no teacher";

                                        Console.WriteLine($"Name:            {@class.Name}\n" +
                                                          $"Teacher:         {t1}\n" +
                                                          "Students:\n");

                                        var students1 = adminService
                                            .Classes_GetStudentsForId(@class.Id).ToList();

                                        if (students1.Any())
                                            foreach (var student in students1)
                                                Console.WriteLine(student);
                                        else
                                            Console.WriteLine("no students");
                                    }

                                    Console.WriteLine();
                                    Console.WriteLine();

                                    break;

                                case "2":
                                    Console.WriteLine();

                                    foreach (var classDto in adminService.Classes_GetAll())
                                        Console.WriteLine($"{classDto.Id}\t\t{classDto.Name}");

                                    Console.Write("Enter class number:\t");

                                    int classId;

                                    try
                                    {
                                        classId = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered incorrect format id\n");

                                        break;
                                    }

                                    ClassDto claws;

                                    try
                                    {
                                        claws = adminService.Classes_GetForId(classId);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Class not found!");

                                        break;
                                    }

                                    var t = claws.TeacherId != null
                                        ? adminService
                                            .Teachers_GetForId(claws.TeacherId)?
                                            .FullName
                                        : "no teacher";

                                    Console.WriteLine($"Name:            {claws.Name}\n" +
                                                      $"Teacher:         {t}\n" +
                                                      "Students:\n");

                                    var students2 = adminService
                                        .Classes_GetStudentsForId(claws.Id).ToList();

                                    if (students2.Any())
                                        foreach (var student in students2)
                                            Console.WriteLine(student);
                                    else
                                        Console.WriteLine("no students");

                                    break;

                                case "3":
                                    string isContinue;

                                    foreach (var classDto in adminService.Classes_GetAll())
                                        Console.WriteLine($"{classDto.Id}\t\t{classDto.Name}");

                                    int idClass;

                                    Console.Write("Enter number:\t");

                                    try
                                    {
                                        idClass = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered incorrect format id\n");

                                        break;
                                    }

                                    try
                                    {
                                        _ = adminService.Classes_GetForId(idClass);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Class not found!!!");

                                        break;
                                    }

                                    string choiceEditClass;

                                    do
                                    {
                                        Console.WriteLine();

                                        Console.WriteLine("Name         -   1\n" +
                                                          "Teacher      -   2\n" +
                                                          "Students     -   3\n" +
                                                          "Back         -   0\n" +
                                                          "Exit         -   9\n" +
                                                          "Repeat       -   any key\n");

                                        choiceEditClass = Console.ReadLine();

                                        switch (choiceEditClass)
                                        {
                                            case "1":

                                                Console.Write("Enter name:\t");

                                                var n = Console.ReadLine();

                                                if (!Validation.ClassName(n))
                                                    break;

                                                Console.WriteLine("Dou you want continue to edit?");

                                                Console.WriteLine("Yes\t-\ty");

                                                Console.WriteLine("No\t-\tany key");

                                                isContinue = Console.ReadLine();

                                                if (isContinue != "y") break;

                                                Console.WriteLine();

                                                adminService.Class_Edit_Name(idClass, n);

                                                Console.WriteLine("Name was edit");

                                                break;

                                            case "2":

                                                var teachers = adminService
                                                    .GetTeachersWithoutClass()
                                                    .ToList();

                                                if (adminService.Classes_GetForId(idClass).TeacherId != null)
                                                {
                                                    var teacherDto = adminService.Teachers_GetForId(idClass);

                                                    teachers.Add(teacherDto);
                                                }

                                                foreach (var teacher in teachers)
                                                    Console.WriteLine($"{teacher.Id}\t\t{teacher.FullName}");

                                                Console.WriteLine("\nEnter teacher number:");

                                                int tId;

                                                try
                                                {
                                                    tId = int.Parse(Console.ReadLine());
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("You entered incorrect format id\n");

                                                    break;
                                                }

                                                try
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    Console.WriteLine();

                                                    adminService.Class_Edit_Teacher(idClass, tId);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("Error, teacher wasn't edited\n");

                                                    break;
                                                }

                                                Console.WriteLine("Teacher was edit");

                                                break;

                                            case "3":

                                                Console.WriteLine("\nStudents:\n");

                                                foreach (var studentDto in adminService.Students_GetAll())
                                                    Console.WriteLine($"{studentDto.Id}\t\t{studentDto.FullName}");

                                                Console.WriteLine();

                                                var studentIds = new List<int>();

                                                Console.WriteLine("Enter student numbers, if you want stop? enter 0");

                                                do
                                                {
                                                    int i;

                                                    try
                                                    {
                                                        i = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.WriteLine("You entered wrong format id\n");

                                                        break;
                                                    }

                                                    if (i == 0) break;

                                                    studentIds.Add(i);
                                                } while (true);

                                                try
                                                {
                                                    Console.WriteLine("Dou you want continue to edit?");

                                                    Console.WriteLine("Yes\t-\ty");

                                                    Console.WriteLine("No\t-\tany key");

                                                    isContinue = Console.ReadLine();

                                                    if (isContinue != "y") break;

                                                    Console.WriteLine();

                                                    adminService.Class_Edit_Students(idClass, studentIds);
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("Students wasn't edited.\n" +
                                                                      "You entered wrong ids");

                                                    break;
                                                }

                                                Console.WriteLine("Students was edit");

                                                break;

                                            case "9":
                                                return;
                                        }
                                    } while (choiceEditClass != "0");

                                    break;

                                case "4":
                                    string name;

                                    var students = new List<int>();

                                    int? teacherId = null;

                                    Console.Write("\tEnter name:\t");

                                    name = Console.ReadLine();

                                    if (!Validation.ClassName(name))
                                        break;

                                    Console.WriteLine("Teachers:");

                                    Console.WriteLine("Do you want add teacher?\n" +
                                                      "Yes      -   1\n" +
                                                      "No       -   any key");

                                    var choice = Console.ReadLine();

                                    if (choice == "1")
                                    {
                                        var teachers = adminService
                                            .GetTeachersWithoutClass().ToList();

                                        if (teachers.Any())
                                        {
                                            foreach (var teacher in teachers)
                                                Console.WriteLine($"{teacher.Id}\t\t{teacher.FullName}");

                                            Console.WriteLine("Enter teacher number");

                                            try
                                            {
                                                teacherId = int.Parse(Console.ReadLine());
                                            }
                                            catch (Exception)
                                            {
                                                Console.WriteLine("You entered wrong format value\n");

                                                break;
                                            }
                                        }
                                    }

                                    Console.WriteLine("\nStudents:\n");

                                    foreach (var studentDto in adminService.Students_GetAll())
                                        Console.WriteLine($"{studentDto.Id}\t\t{studentDto.FullName}");

                                    Console.WriteLine();

                                    Console.WriteLine("Enter student ids, if you want stop? enter 0");
                                    do
                                    {
                                        int i1;

                                        try
                                        {
                                            i1 = int.Parse(Console.ReadLine());
                                        }
                                        catch (Exception)
                                        {
                                            Console.WriteLine("You entered incorrect format id\n");

                                            break;
                                        }

                                        if (i1 == 0) break;

                                        students.Add(i1);
                                    } while (true);

                                    try
                                    {
                                        Console.WriteLine("Dou you want continue to create?");

                                        Console.WriteLine("Yes\t-\ty");

                                        Console.WriteLine("No\t-\tany key");

                                        isContinue = Console.ReadLine();

                                        if (isContinue != "y") break;

                                        adminService.Class_Create(new ClassDto
                                        {
                                            Name = name,
                                            TeacherId = teacherId,
                                            StudentIds = new List<int>()
                                        });
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Error, Class wasn't created!!!\n");

                                        break;
                                    }

                                    var idClassFoundForName = adminService
                                        .GetClassForName(name).Id;

                                    try
                                    {
                                        adminService.Class_Edit_Students(idClassFoundForName, students);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered wrong data");
                                    }

                                    Console.WriteLine("Classes was created");

                                    break;

                                case "5":
                                    foreach (var classDto in adminService.Classes_GetAll())
                                        Console.WriteLine($"Id: {classDto.Id}\t\tName: {classDto.Name}");

                                    Console.WriteLine();

                                    Console.Write("Input id:\t");

                                    int id;

                                    try
                                    {
                                        id = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("You entered wrong format class id\n");

                                        break;
                                    }

                                    Console.WriteLine();

                                    try
                                    {
                                        Console.WriteLine("Dou you want continue to delete?");

                                        Console.WriteLine("Yes\t-\ty");

                                        Console.WriteLine("No\t-\tany key");

                                        isContinue = Console.ReadLine();

                                        if (isContinue != "y") break;

                                        Console.WriteLine();

                                        adminService.Class_Delete(id);
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Error, Class wasn't deleted\n");

                                        break;
                                    }

                                    Console.WriteLine("\nData was deleted\n");

                                    break;

                                case "9":
                                    return;
                            }
                        } while (choiceCrud != "0");

                        break;
                }
            } while (choiceMain != "0");
        }

        private static void VisitorInterface(MainService main)
        {
            var visitorService = new VisitorService(main);

            string choice1;

            do
            {
                Console.WriteLine("\n\nTeachers        -    1\n" +
                                  "Classes         -    2\n" +
                                  "Subjects        -    3\n" +
                                  "Exit            -    0\n" +
                                  "Repeat          -    any key\n\n");

                choice1 = Console.ReadLine();

                switch (choice1)
                {
                    case "1":
                        var teachers = visitorService.GetTeachers();

                        foreach (var t in teachers)
                            Console.WriteLine($"{t.Id}\t\t{t.FullName}");

                        Console.WriteLine(
                            "\nIf you want to know more about the teacher, enter his number, if you want to go back, press 0, repeat any key");

                        int id;

                        do
                        {
                            try
                            {
                                id = Convert.ToInt32(Convert.ToUInt32(Console.ReadLine()));
                            }
                            catch (Exception)
                            {
                                id = int.MinValue;
                            }
                        } while (id == int.MinValue);

                        if (id == 0) break;

                        TeacherDto teacher;

                        try
                        {
                            teacher = visitorService.GetTeacher(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You entered wrong id, teacher not found");

                            break;
                        }

                        var c = visitorService.GetTeachersClass(id) != null
                            ? visitorService.GetTeachersClass(id)
                            : "no class";

                        Console.WriteLine($"\nFull name: {teacher.FullName}\n" +
                                          $"Age:         {teacher.Age}\n" +
                                          $"Gender:      {teacher.Gender}\n" +
                                          $"Class:       {c}\n" +
                                          "Subjects:\n");

                        foreach (var s in visitorService.GetSubjectsForTeacher(id))
                            Console.WriteLine(s);

                        Console.WriteLine();

                        break;

                    case "2":
                        foreach (var classDto in visitorService.GetClasses())
                            Console.WriteLine($"{classDto.Id}\t\t{classDto.Name}");

                        Console.WriteLine("\nIf you want to know more about the class," +
                                          " enter its number, if you want to go back, press 0,repeat any key");

                        int idClass;

                        do
                        {
                            try
                            {
                                idClass = Convert.ToInt32(Convert.ToUInt32(Console.ReadLine()));
                            }
                            catch (Exception)
                            {
                                idClass = int.MinValue;
                            }
                        } while (idClass == int.MinValue);

                        if (idClass == 0) break;

                        Console.WriteLine();

                        try
                        {
                            var @class = visitorService.GetClass(idClass);

                            Console.WriteLine($"Name:           {@class.Name}\n" +
                                              $"Teacher:        {visitorService.GetTeacher(@class.TeacherId).FullName}\n" +
                                              "Students:\n");

                            foreach (var student in visitorService.GetStudents(@class.Id))
                                Console.WriteLine(student);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You input wrong value");
                        }

                        Console.WriteLine();

                        break;

                    case "3":
                        Console.WriteLine();

                        foreach (var subject in visitorService.GetSubjects())
                            Console.WriteLine($"{subject.Id}\t\t{subject.Name}");

                        Console.WriteLine(
                            "\nIf you want to know more about the subject, enter its number, if you want to go back, press 0, repeat any key");

                        int subjectId;

                        do
                        {
                            try
                            {
                                subjectId = int.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                subjectId = int.MinValue;
                            }
                        } while (subjectId == int.MinValue);

                        Console.WriteLine();

                        SubjectDto sub;

                        try
                        {
                            sub = visitorService.GetSubject(subjectId);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You entered wrong id");

                            break;
                        }

                        Console.WriteLine(sub?.Name);

                        Console.WriteLine();

                        Console.WriteLine("Teachers:\n");

                        foreach (var t in visitorService.TeachersForSubjectId(sub.Id))
                            Console.WriteLine(t);

                        Console.WriteLine();

                        Console.WriteLine("Students:\n");

                        foreach (var s in visitorService.StudentsForSubjectId(sub.Id))
                            Console.WriteLine(s);

                        break;
                }
            } while (choice1 != "0");
        }
    }
}