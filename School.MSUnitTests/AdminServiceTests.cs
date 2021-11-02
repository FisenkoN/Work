// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Moq;
// using School.UI;
// using School.WEB.Data.Repository;
// using School.WEB.DTOs;
// using School.WEB.Models;
// using School.WEB.Services;
//
// namespace School.MSUnitTests
// {
//     [TestClass]
//     public class AdminServiceTests
//     {
//         private readonly Mock<MainService> _service;
//         private readonly Mock<IUnitOfWork> _unitOfWork;
//
//         public AdminServiceTests()
//         {
//             _unitOfWork = new Mock<IUnitOfWork>();
//
//             _service = new Mock<MainService>();
//         }
//
//         [DataRow(1)]
//         [DataRow(2)]
//         [DataRow(3)]
//         [DataRow(4)]
//         [TestMethod]
//         public void StudentDelete(int idFaceStudent)
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Students.Delete(idFaceStudent));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Student_Delete(idFaceStudent);
//
//             //Assert
//             Assert.IsNotNull(idFaceStudent);
//             _unitOfWork.Verify(unit => unit.Students.Delete(It.IsAny<int>()),
//                 Times.Once());
//         }
//
//         [TestMethod]
//         public void TeacherDelete()
//         {
//             //Arrange
//             const int idFaceTeacher = 1;
//
//             _unitOfWork.Setup(unit => unit.Teachers.Delete(idFaceTeacher));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Teacher_Delete(idFaceTeacher);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Teachers.Delete(It.IsAny<int>()),
//                 Times.Once());
//         }
//
//         [TestMethod]
//         public void ClassDelete()
//         {
//             //Arrange
//             const int idFaceClass = 1;
//
//             _unitOfWork.Setup(unit => unit.Classes.Delete(idFaceClass));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Class_Delete(idFaceClass);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Classes.Delete(It.IsAny<int>()),
//                 Times.Once());
//         }
//
//         [TestMethod]
//         public void SubjectDelete()
//         {
//             //Arrange
//             const int idFaceSubject = 1;
//
//             _unitOfWork.Setup(unit => unit.Subjects.Delete(idFaceSubject));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Subject_Delete(idFaceSubject);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Subjects.Delete(It.IsAny<int>()),
//                 Times.Once());
//         }
//
//         [TestMethod]
//         public void StudentAdd()
//         {
//             //Arrange
//             var newStudent = new Student
//             {
//                 Id = 100,
//                 Age = 18,
//                 ClassId = 2,
//                 FirstName = "Peter",
//                 LastName = "Griphin",
//                 Gender = Gender.Male,
//                 Subjects = new List<Subject>
//                 {
//                     new()
//                     {
//                         Id = 1,
//                         Name = "Sub_1"
//                     }
//                 }
//             };
//             _unitOfWork.Setup(unit => unit.Subjects.GetAll())
//                 .Returns(GetSubjects);
//
//             _unitOfWork.Setup(unit => unit.Students.Add(newStudent));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Student_Create(new StudentDto
//             {
//                 Id = newStudent.Id,
//                 FirstName = newStudent.FirstName,
//                 LastName = newStudent.LastName,
//                 Age = newStudent.Age,
//                 ClassId = newStudent.ClassId,
//                 Gender = (GenderDto)newStudent.Gender,
//                 SubjectIds = newStudent.Subjects.Select(s => s.Id)
//             });
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Students.Add(It.IsAny<Student>()),
//                 Times.Once());
//         }
//
//         [TestMethod]
//         public void TeacherAdd()
//         {
//             //Arrange
//             var newTeacher = new Teacher
//             {
//                 Id = 100,
//                 Age = 26,
//                 ClassId = 1,
//                 FirstName = "Peter",
//                 LastName = "Griphin",
//                 Gender = Gender.Male,
//                 Subjects = new List<Subject>
//                 {
//                     new()
//                     {
//                         Id = 1,
//                         Name = "Sub_1"
//                     }
//                 }
//             };
//             _unitOfWork.Setup(unit => unit.Subjects.GetAll())
//                 .Returns(GetSubjects);
//
//             _unitOfWork.Setup(unit => unit.Teachers.Add(newTeacher));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Teacher_Create(new TeacherDto
//             {
//                 Id = newTeacher.Id,
//                 FirstName = newTeacher.FirstName,
//                 LastName = newTeacher.LastName,
//                 Age = newTeacher.Age,
//                 ClassId = newTeacher.ClassId,
//                 Gender = (GenderDto)newTeacher.Gender,
//                 SubjectIds = newTeacher.Subjects.Select(s => s.Id)
//             });
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Teachers.Add(It.IsAny<Teacher>()),
//                 Times.Once());
//         }
//
//         [TestMethod]
//         public void ClassAdd()
//         {
//             //Arrange
//             var newClass = new Class
//             {
//                 Id = 100,
//                 Name = "7A",
//                 TeacherId = 1,
//                 Students = new List<Student>
//                 {
//                     new()
//                     {
//                         Id = 1,
//                         FirstName = "Olha",
//                         LastName = "Porechnikova",
//                         Age = 45,
//                         ClassId = 100,
//                         Gender = Gender.Female,
//                         Subjects = new List<Subject>()
//                     }
//                 }
//             };
//             _unitOfWork.Setup(unit => unit.Students.GetAll())
//                 .Returns(GetStudents);
//
//             _unitOfWork.Setup(unit => unit.Classes.Add(newClass));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Class_Create(new ClassDto
//             {
//                 Id = newClass.Id,
//                 Name = newClass.Name,
//                 TeacherId = newClass.TeacherId,
//                 StudentIds = newClass.Students.Select(s => s.Id)
//             });
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Classes.Add(It.IsAny<Class>()),
//                 Times.Once());
//         }
//
//         [TestMethod]
//         public void SubjectsAdd()
//         {
//             //Arrange
//             var newSubject = new Subject
//             {
//                 Id = 100,
//                 Name = "Ukraine History",
//                 Students = new List<Student>(),
//                 Teachers = new List<Teacher>()
//             };
//             _unitOfWork.Setup(unit => unit.Students.GetAll())
//                 .Returns(GetStudents);
//             _unitOfWork.Setup(unit => unit.Teachers.GetAll())
//                 .Returns(GetTeachers);
//             _unitOfWork.Setup(unit => unit.Subjects.Add(newSubject));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Subject_Create(new SubjectDto
//             {
//                 Id = newSubject.Id,
//                 Name = newSubject.Name,
//                 TeacherIds = newSubject.Teachers.Select(t => t.Id),
//                 StudentIds = newSubject.Students.Select(s => s.Id)
//             });
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Subjects.Add(It.IsAny<Subject>()),
//                 Times.Once());
//         }
//
//         [TestMethod]
//         public void StudentsGetAllReturnsAListOfStudents()
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Students.GetAll())
//                 .Returns(GetStudents);
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             var result = service.Students_GetAll();
//
//             //Assert
//             Assert.IsInstanceOfType(result,
//                 typeof(IEnumerable<StudentDto>));
//             Assert.AreEqual(result.Count(),
//                 GetStudents()
//                     .Count);
//         }
//
//         [TestMethod]
//         public void SubjectsGetAllReturnsAListOfSubjects()
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Subjects.GetAll())
//                 .Returns(GetSubjects);
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             var result = service.Subjects_GetAll();
//
//             //Assert
//             Assert.IsInstanceOfType(result,
//                 typeof(IEnumerable<SubjectDto>));
//             Assert.AreEqual(result.Count(),
//                 GetSubjects()
//                     .Count);
//         }
//
//         [TestMethod]
//         public void ClassesGetAllReturnsAListOfClasses()
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Classes.GetAll())
//                 .Returns(GetClasses);
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             var result = service.Classes_GetAll();
//
//             //Assert
//             Assert.IsInstanceOfType(result,
//                 typeof(IEnumerable<ClassDto>));
//             Assert.AreEqual(result.Count(),
//                 GetClasses()
//                     .Count);
//         }
//
//         [TestMethod]
//         public void TeachersGetAllReturnsAListOfTeachers()
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Teachers.GetAll())
//                 .Returns(GetTeachers);
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             var result = service.Teachers_GetAll();
//
//             //Assert
//             Assert.IsInstanceOfType(result,
//                 typeof(IEnumerable<TeacherDto>));
//             Assert.AreEqual(result.Count(),
//                 GetTeachers()
//                     .Count);
//         }
//
//         [DataRow(1)]
//         [DataRow(2)]
//         [DataRow(3)]
//         [DataRow(4)]
//         [DataRow(5)]
//         [TestMethod]
//         public void StudentsGetForIdReturnsStudent(int testStudentId)
//         {
//             //Arrange
//             var student = GetStudents()
//                 .Find(s => s.Id == testStudentId);
//
//             _unitOfWork.Setup(unit => unit.Students.GetOneRelated(testStudentId))
//                 .Returns(GetStudents()
//                     .FirstOrDefault(s => s.Id == testStudentId));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             var result = service.Students_GetForId(testStudentId);
//
//             //Assert
//             Assert.IsInstanceOfType(result,
//                 typeof(StudentDto));
//             Assert.IsNotNull(result);
//             Assert.IsNotNull(student);
//             Assert.AreEqual(student.Id,
//                 result.Id);
//             Assert.AreEqual(student.Age,
//                 result.Age);
//             Assert.AreEqual(student.FirstName,
//                 result.FirstName);
//             Assert.AreEqual(student.LastName,
//                 result.LastName);
//             Assert.AreEqual((int)student.Gender,
//                 (int)result.Gender);
//         }
//
//         [DataRow(1)]
//         [DataRow(2)]
//         [DataRow(3)]
//         [DataRow(4)]
//         [TestMethod]
//         public void SubjectsGetForIdReturnsSubject(int testSubjectId)
//         {
//             //Arrange
//             var subject = GetSubjects()
//                 .Find(s => s.Id == testSubjectId);
//
//             _unitOfWork.Setup(unit => unit.Subjects.GetOneRelated(testSubjectId))
//                 .Returns(GetSubjects()
//                     .FirstOrDefault(s => s.Id == testSubjectId));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             var result = service.Subjects_GetForId(testSubjectId);
//
//             //Assert
//             Assert.IsInstanceOfType(result,
//                 typeof(SubjectDto));
//             Assert.IsNotNull(result);
//             Assert.IsNotNull(subject);
//             Assert.AreEqual(subject.Id,
//                 result.Id);
//             Assert.AreEqual(subject.Name,
//                 result.Name);
//         }
//
//         [DataRow(1)]
//         [DataRow(2)]
//         [TestMethod]
//         public void ClassesGetForIdReturnsClass(int testClassId)
//         {
//             //Arrange
//             var form = GetClasses()
//                 .Find(c => c.Id == testClassId);
//             _unitOfWork.Setup(unit => unit.Classes.GetOneRelated(testClassId))
//                 .Returns(GetClasses()
//                     .FirstOrDefault(s => s.Id == testClassId));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             var result = service.Classes_GetForId(testClassId);
//
//             //Assert
//             Assert.IsInstanceOfType(result,
//                 typeof(ClassDto));
//             Assert.IsNotNull(result);
//             Assert.IsNotNull(form);
//             Assert.AreEqual(form.Id,
//                 result.Id);
//             Assert.AreEqual(form.Name,
//                 result.Name);
//         }
//
//         [DataRow(1)]
//         [DataRow(2)]
//         [DataRow(3)]
//         [TestMethod]
//         public void TeachersGetForIdReturnsTeacher(int testTeacherId)
//         {
//             //Arrange
//             var teacher = GetTeachers()
//                 .Find(t => t.Id == testTeacherId);
//             _unitOfWork.Setup(unit => unit.Teachers.GetOneRelated(testTeacherId))
//                 .Returns(GetTeachers()
//                     .FirstOrDefault(s => s.Id == testTeacherId));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             var result = service.Teachers_GetForId(testTeacherId);
//
//             //Assert
//             Assert.IsInstanceOfType(result,
//                 typeof(TeacherDto));
//             Assert.IsNotNull(result);
//             Assert.IsNotNull(teacher);
//             Assert.AreEqual(teacher.Id,
//                 result.Id);
//             Assert.AreEqual(teacher.FirstName,
//                 result.FirstName);
//             Assert.AreEqual(teacher.LastName,
//                 result.LastName);
//             Assert.AreEqual(teacher.Age,
//                 result.Age);
//             Assert.AreEqual((int)teacher.Gender,
//                 (int)result.Gender);
//         }
//
//         [TestMethod]
//         public void StudentsEditFirstName()
//         {
//             //Arrange
//             const int studentFaceId = 1;
//             const string newFaceFirstName = "newFName";
//             var newStudent = GetStudents()
//                 .FirstOrDefault(s => s.Id == studentFaceId);
//             _unitOfWork.Setup(unit => unit.Students.GetOne(studentFaceId))
//                 .Returns(newStudent);
//             _unitOfWork.Setup(repo => repo.Students.Update(newStudent));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Students_Edit_FirstName(studentFaceId,
//                 newFaceFirstName);
//
//             //Assert
//             Assert.IsTrue(Validation.FirstOrLastName(newFaceFirstName));
//             _unitOfWork.Verify(unit => unit.Students.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Students.Update(It.IsAny<Student>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void StudentsEditLastName()
//         {
//             //Arrange
//             const int studentFaceId = 1;
//             const string newFaceLastName = "newLName";
//             var newStudent = GetStudents()
//                 .FirstOrDefault(s => s.Id == studentFaceId);
//             _unitOfWork.Setup(unit => unit.Students.GetOne(studentFaceId))
//                 .Returns(newStudent);
//             _unitOfWork.Setup(unit => unit.Students.Update(newStudent));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Students_Edit_LastName(studentFaceId,
//                 newFaceLastName);
//
//             //Assert
//             Assert.IsTrue(Validation.FirstOrLastName(newFaceLastName));
//             _unitOfWork.Verify(unit => unit.Students.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Students.Update(It.IsAny<Student>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void StudentsEditAge()
//         {
//             //Arrange
//             const int studentFaceId = 1;
//             const int newFaceAge = 16;
//             var newStudent = GetStudents()
//                 .FirstOrDefault(s => s.Id == studentFaceId);
//             _unitOfWork.Setup(unit => unit.Students.GetOne(studentFaceId))
//                 .Returns(newStudent);
//             _unitOfWork.Setup(unit => unit.Students.Update(newStudent));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Students_Edit_Age(studentFaceId,
//                 newFaceAge);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Students.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Students.Update(It.IsAny<Student>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void StudentsEditGender()
//         {
//             //Arrange
//             const int studentFaceId = 1;
//             const GenderDto newFaceGender = GenderDto.Female;
//             var newStudent = GetStudents()
//                 .FirstOrDefault(s => s.Id == studentFaceId);
//             _unitOfWork.Setup(unit => unit.Students.GetOne(studentFaceId))
//                 .Returns(newStudent);
//             _unitOfWork.Setup(unit => unit.Students.Update(newStudent));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Students_Edit_Gender(studentFaceId,
//                 newFaceGender);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Students.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Students.Update(It.IsAny<Student>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void StudentsEditClass()
//         {
//             //Arrange
//             const int studentFaceId = 1;
//             const int newFaceClassId = 1;
//             var newStudent = GetStudents()
//                 .FirstOrDefault(s => s.Id == studentFaceId);
//             _unitOfWork.Setup(unit => unit.Students.GetOne(studentFaceId))
//                 .Returns(newStudent);
//             _unitOfWork.Setup(unit => unit.Students.Update(newStudent));
//             _unitOfWork.Setup(unit => unit.Classes.GetOne(newFaceClassId))
//                 .Returns(GetClasses()
//                     .FirstOrDefault(c => c.Id == newFaceClassId));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Students_Edit_Class(studentFaceId,
//                 newFaceClassId);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Classes.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Students.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Students.Update(It.IsAny<Student>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void StudentsEditSubjects()
//         {
//             //Arrange
//             const int studentFaceId = 1;
//             var newFaceListSubjectIds = new List<int>
//             {
//                 1,
//                 2
//             };
//             var newStudent = GetStudents()
//                 .FirstOrDefault(s => s.Id == studentFaceId);
//             _unitOfWork.Setup(unit => unit.Students.GetOneRelated(studentFaceId))
//                 .Returns(newStudent);
//             _unitOfWork.Setup(unit => unit.Students.Update(newStudent));
//             _unitOfWork.Setup(unit => unit.Subjects.GetOne(It.IsAny<int>()))
//                 .Returns(new Subject());
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Students_Edit_Subjects(studentFaceId,
//                 newFaceListSubjectIds);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Subjects.GetOne(It.IsAny<int>()),
//                 Times.AtLeastOnce());
//             _unitOfWork.Verify(unit => unit.Students.GetOneRelated(It.IsAny<int>()),
//                 Times.Exactly(2));
//             _unitOfWork.Verify(unit => unit.Students.Update(It.IsAny<Student>()),
//                 Times.Exactly(2));
//         }
//
//         [TestMethod]
//         public void TeachersEditFirstName()
//         {
//             //Arrange
//             const int teacherFaceId = 1;
//             const string newFaceFirstName = "newFName";
//             var newTeacher = GetTeachers()
//                 .FirstOrDefault(s => s.Id == teacherFaceId);
//             _unitOfWork.Setup(unit => unit.Teachers.GetOne(teacherFaceId))
//                 .Returns(newTeacher);
//             _unitOfWork.Setup(unit => unit.Teachers.Update(newTeacher));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Teachers_Edit_FirstName(teacherFaceId,
//                 newFaceFirstName);
//
//             //Assert
//             Assert.IsTrue(Validation.FirstOrLastName(newFaceFirstName));
//             _unitOfWork.Verify(unit => unit.Teachers.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Teachers.Update(It.IsAny<Teacher>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void TeachersEditLastName()
//         {
//             //Arrange
//             const int teacherFaceId = 1;
//             const string newFaceLastName = "newLName";
//             var newTeacher = GetTeachers()
//                 .FirstOrDefault(s => s.Id == teacherFaceId);
//             _unitOfWork.Setup(unit => unit.Teachers.GetOne(teacherFaceId))
//                 .Returns(newTeacher);
//             _unitOfWork.Setup(unit => unit.Teachers.Update(newTeacher));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Teachers_Edit_LastName(teacherFaceId,
//                 newFaceLastName);
//
//             //Assert
//             Assert.IsTrue(Validation.FirstOrLastName(newFaceLastName));
//             _unitOfWork.Verify(unit => unit.Teachers.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Teachers.Update(It.IsAny<Teacher>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void TeachersEditAge()
//         {
//             //Arrange
//             const int teacherFaceId = 1;
//             const int newFaceAge = 16;
//             var newTeacher = GetTeachers()
//                 .FirstOrDefault(s => s.Id == teacherFaceId);
//             _unitOfWork.Setup(unit => unit.Teachers.GetOne(teacherFaceId))
//                 .Returns(newTeacher);
//             _unitOfWork.Setup(unit => unit.Teachers.Update(newTeacher));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Teachers_Edit_Age(teacherFaceId,
//                 newFaceAge);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Teachers.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Teachers.Update(It.IsAny<Teacher>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void TeachersEditGender()
//         {
//             //Arrange
//             const int teacherFaceId = 1;
//             const GenderDto newFaceGender = GenderDto.Female;
//             var newTeacher = GetTeachers()
//                 .FirstOrDefault(s => s.Id == teacherFaceId);
//             _unitOfWork.Setup(unit => unit.Teachers.GetOne(teacherFaceId))
//                 .Returns(newTeacher);
//             _unitOfWork.Setup(unit => unit.Teachers.Update(newTeacher));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Teachers_Edit_Gender(teacherFaceId,
//                 newFaceGender);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Teachers.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Teachers.Update(It.IsAny<Teacher>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void TeachersEditClass()
//         {
//             //Arrange
//             const int teacherFaceId = 1;
//             const int newFaceClassId = 1;
//             var newTeacher = GetTeachers()
//                 .FirstOrDefault(s => s.Id == teacherFaceId);
//             _unitOfWork.Setup(unit => unit.Teachers.GetOne(teacherFaceId))
//                 .Returns(newTeacher);
//             _unitOfWork.Setup(unit => unit.Teachers.Update(newTeacher));
//             _unitOfWork.Setup(unit => unit.Classes.GetOne(newFaceClassId))
//                 .Returns(GetClasses()
//                     .FirstOrDefault(c => c.Id == newFaceClassId));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Teachers_Edit_Class(teacherFaceId,
//                 newFaceClassId);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Classes.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Teachers.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Teachers.Update(It.IsAny<Teacher>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void TeachersEditSubjects()
//         {
//             //Arrange
//             const int teacherFaceId = 1;
//             var newFaceListSubjectIds = new List<int>
//             {
//                 1,
//                 2
//             };
//             var newTeacher = GetTeachers()
//                 .FirstOrDefault(s => s.Id == teacherFaceId);
//
//             _unitOfWork.Setup(unit => unit.Teachers.GetOneRelated(teacherFaceId))
//                 .Returns(newTeacher);
//
//             _unitOfWork.Setup(unit => unit.Teachers.Update(newTeacher));
//
//             _unitOfWork.Setup(unit => unit.Subjects.GetOne(It.IsAny<int>()))
//                 .Returns(new Subject());
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Teachers_Edit_Subjects(teacherFaceId,
//                 newFaceListSubjectIds);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Subjects.GetOne(It.IsAny<int>()),
//                 Times.AtLeastOnce());
//             _unitOfWork.Verify(unit => unit.Teachers.GetOneRelated(It.IsAny<int>()),
//                 Times.Exactly(2));
//             _unitOfWork.Verify(unit => unit.Teachers.Update(It.IsAny<Teacher>()),
//                 Times.Exactly(2));
//         }
//
//         [TestMethod]
//         public void ClassesEditName()
//         {
//             //Arrange
//             const int classFaceId = 1;
//
//             const string newFaceName = "4A";
//
//             var newClass = GetClasses()
//                 .FirstOrDefault(s => s.Id == classFaceId);
//
//             _unitOfWork.Setup(unit => unit.Classes.GetOne(classFaceId))
//                 .Returns(newClass);
//
//             _unitOfWork.Setup(unit => unit.Classes.Update(newClass));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Class_Edit_Name(classFaceId,
//                 newFaceName);
//
//             //Assert
//             Assert.IsTrue(Validation.ClassName(newFaceName));
//             _unitOfWork.Verify(unit => unit.Classes.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Classes.Update(It.IsAny<Class>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void ClassesEditTeacher()
//         {
//             //Arrange
//             const int classFaceId = 1;
//
//             const int newFaceTeacherId = 1;
//
//             var newClass = GetClasses()
//                 .FirstOrDefault(s => s.Id == classFaceId);
//
//             _unitOfWork.Setup(unit => unit.Classes.GetOne(classFaceId))
//                 .Returns(newClass);
//
//             _unitOfWork.Setup(unit => unit.Teachers.GetOne(classFaceId))
//                 .Returns(new Teacher());
//
//             _unitOfWork.Setup(unit => unit.Classes.Update(newClass));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Class_Edit_Teacher(classFaceId,
//                 newFaceTeacherId);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Classes.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Teachers.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Classes.Update(It.IsAny<Class>()),
//                 Times.Once);
//         }
//
//         [TestMethod]
//         public void ClassesEditStudents()
//         {
//             //Arrange
//             const int classFaceId = 1;
//             var newStudents = new List<int>
//             {
//                 1,
//                 2
//             };
//             var newClass = GetClasses()
//                 .FirstOrDefault(s => s.Id == classFaceId);
//
//             _unitOfWork.Setup(unit => unit.Classes.GetOneRelated(classFaceId))
//                 .Returns(newClass);
//
//             _unitOfWork.Setup(unit => unit.Classes.GetOne(classFaceId))
//                 .Returns(newClass);
//
//             _unitOfWork.Setup(unit => unit.Students.GetOne(It.IsAny<int>()))
//                 .Returns(new Student());
//
//             _unitOfWork.Setup(unit => unit.Students.Update(new Student()));
//
//             _unitOfWork.Setup(unit => unit.Classes.Update(newClass));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Class_Edit_Students(classFaceId,
//                 newStudents);
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Classes.GetOneRelated(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Classes.GetOne(It.IsAny<int>()),
//                 Times.Exactly(2));
//             _unitOfWork.Verify(unit => unit.Students.GetOne(It.IsAny<int>()),
//                 Times.Exactly(2));
//             _unitOfWork.Verify(unit => unit.Classes.Update(It.IsAny<Class>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Students.Update(It.IsAny<Student>()),
//                 Times.Exactly(2));
//         }
//
//         [TestMethod]
//         public void SubjectsEditName()
//         {
//             //Arrange
//             const int subjectFaceId = 1;
//
//             const string newFaceName = "newName";
//
//             var newSubject = GetSubjects()
//                 .FirstOrDefault(s => s.Id == subjectFaceId);
//
//             _unitOfWork.Setup(unit => unit.Subjects.GetOne(subjectFaceId))
//                 .Returns(newSubject);
//
//             _unitOfWork.Setup(unit => unit.Subjects.Update(newSubject));
//
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             service.Subject_Edit_Name(subjectFaceId,
//                 newFaceName);
//
//             //Assert
//             Assert.IsTrue(Validation.SubjectName(newFaceName));
//             _unitOfWork.Verify(unit => unit.Subjects.GetOne(It.IsAny<int>()),
//                 Times.Once);
//             _unitOfWork.Verify(unit => unit.Subjects.Update(It.IsAny<Subject>()),
//                 Times.Once);
//         }
//
//         [DataRow("11T")]
//         [DataRow("10A")]
//         [TestMethod]
//         public void GetClassForName(string value)
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Classes.GetAll())
//                 .Returns(GetClasses());
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             var result = service.GetClassForName(value);
//
//             //Assert
//             Assert.IsNotNull(value);
//             _unitOfWork.Verify(unit => unit.Classes.GetAll(),
//                 Times.Once);
//             Assert.IsNotNull(result);
//             Assert.AreEqual(value,
//                 result.Name);
//         }
//
//         [TestMethod]
//         public void GetTeachersWithoutClass()
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Teachers.GetAll())
//                 .Returns(GetTeachers);
//             _unitOfWork.Setup(unit => unit.Classes.GetRelatedData())
//                 .Returns(GetClasses()
//                     .AsQueryable()
//                     .Include(c => c.Teacher));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             var result = service.GetTeachersWithoutClass();
//
//             //Assert
//             _unitOfWork.Verify(unit => unit.Teachers.GetAll(),
//                 Times.Exactly(2));
//             Assert.AreEqual(3,
//                 result.Count());
//         }
//
//         [DataRow(1)]
//         [DataRow(2)]
//         [DataRow(3)]
//         [TestMethod]
//         public void Students_GetSubjectsForId(int value)
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Students.GetOneRelated(value))
//                 .Returns(GetStudents()
//                     .Find(s => s.Id == value));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//             var test = GetStudents()
//                 .Find(s => s.Id == value);
//
//             //Act
//             var result = service.Students_GetSubjectsForId(value);
//
//             //Assert
//             Assert.AreEqual(test?.Subjects.Count,
//                 result.Count());
//             Assert.IsNotNull(result);
//         }
//
//         [DataRow(1)]
//         [DataRow(2)]
//         [TestMethod]
//         public void Subjects_GetTeachersForId(int value)
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Subjects.GetOneRelated(value))
//                 .Returns(GetSubjects()
//                     .Find(s => s.Id == value));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//             var test = GetSubjects()
//                 .Find(s => s.Id == value);
//
//             //Act
//             var result = service.Subjects_GetTeachersForId(value);
//
//             //Assert
//             Assert.AreEqual(test?.Teachers.Select(t => t.FirstName + " " + t.LastName)
//                     .Count(),
//                 result.Count());
//             Assert.IsNotNull(result);
//         }
//
//         [DataRow(1)]
//         [DataRow(2)]
//         [TestMethod]
//         public void Subjects_GetStudentsForId(int value)
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Subjects.GetOneRelated(value))
//                 .Returns(GetSubjects()
//                     .Find(s => s.Id == value));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//             var test = GetSubjects()
//                 .Find(s => s.Id == value);
//
//             //Act
//             var result = service.Subjects_GetStudentsForId(value);
//
//             //Assert
//             Assert.AreEqual(test?.Students.Count,
//                 result.Count());
//             Assert.IsNotNull(result);
//         }
//
//         [DataRow(1)]
//         [DataRow(2)]
//         [TestMethod]
//         public void Classes_GetStudentsForId(int value)
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Classes.GetOneRelated(value))
//                 .Returns(GetClasses()
//                     .Find(s => s.Id == value));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//             var test = GetClasses()
//                 .Find(s => s.Id == value);
//
//             //Act
//             var result = service.Classes_GetStudentsForId(value);
//
//             //Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(test?.Students.Select(s => s.FirstName + " " + s.LastName)
//                     .Count(),
//                 result.Count());
//         }
//
//         [DataRow(1)]
//         [DataRow(2)]
//         [TestMethod]
//         public void Teacher_GetSubjectsForId(int value)
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Teachers.GetOneRelated(value))
//                 .Returns(GetTeachers()
//                     .Find(s => s.Id == value));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//             var test = GetTeachers()
//                 .Find(s => s.Id == value);
//
//             //Act
//             var result = service.Teachers_GetSubjectsForId(value);
//
//             //Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(test?.Subjects.Select(s => s.Name)
//                     .Count(),
//                 result.Count());
//             _unitOfWork.Verify(unit => unit.Teachers.GetOneRelated(It.IsAny<int>()));
//         }
//
//         [TestMethod]
//         public void GetClassWithOutTeacher()
//         {
//             //Arrange
//             _unitOfWork.Setup(unit => unit.Classes.GetRelatedData())
//                 .Returns(GetClasses()
//                     .AsQueryable()
//                     .Include(c => c.Teacher));
//             _service.Setup(m =>
//                     m.UnitOfWork())
//                 .Returns(_unitOfWork.Object);
//
//             var service = new AdminService(_service.Object);
//
//             //Act
//             var result = service.GetClassWithOutTeacher();
//
//             //Assert
//             Assert.AreEqual(1,
//                 result.Count());
//             _unitOfWork.Verify(unit => unit.Classes.GetRelatedData(),
//                 Times.Once);
//         }
//
//         private static List<Student> GetStudents()
//         {
//             var students = new List<Student>
//             {
//                 new()
//                 {
//                     Id = 1,
//                     Age = 14,
//                     FirstName = "Nazar",
//                     LastName = "Uruck",
//                     Gender = Gender.Male,
//                     Subjects = new List<Subject>
//                     {
//                         new()
//                         {
//                             Id = 1,
//                             Name = "Sub_1"
//                         },
//                         new()
//                         {
//                             Id = 2,
//                             Name = "Sub_2"
//                         }
//                     }
//                 },
//                 new()
//                 {
//                     Id = 2,
//                     Age = 12,
//                     FirstName = "Anna",
//                     LastName = "Smith",
//                     Gender = Gender.Female
//                 },
//                 new()
//                 {
//                     Id = 3,
//                     Age = 11,
//                     FirstName = "Yura",
//                     LastName = "Shit",
//                     Gender = Gender.Male,
//                     ClassId = 1,
//                     Subjects = new List<Subject>
//                     {
//                         new()
//                         {
//                             Id = 1,
//                             Name = "Sub_1"
//                         },
//                         new()
//                         {
//                             Id = 2,
//                             Name = "Sub_2"
//                         }
//                     }
//                 },
//                 new()
//                 {
//                     Id = 4,
//                     Age = 7,
//                     FirstName = "Oleh",
//                     LastName = "Op",
//                     Gender = Gender.Male
//                 },
//                 new()
//                 {
//                     Id = 5,
//                     Age = 18,
//                     FirstName = "Igor",
//                     LastName = "Masluk",
//                     Gender = Gender.Male
//                 }
//             };
//             return students;
//         }
//
//         private static List<Subject> GetSubjects()
//         {
//             var subjects = new List<Subject>
//             {
//                 new()
//                 {
//                     Id = 1,
//                     Name = "Math"
//                 },
//                 new()
//                 {
//                     Id = 2,
//                     Name = "IT"
//                 },
//                 new()
//                 {
//                     Id = 3,
//                     Name = "English"
//                 },
//                 new()
//                 {
//                     Id = 4,
//                     Name = "History"
//                 }
//             };
//             return subjects;
//         }
//
//         private static List<Teacher> GetTeachers()
//         {
//             var teachers = new List<Teacher>
//             {
//                 new()
//                 {
//                     Id = 1,
//                     Age = 44,
//                     FirstName = "Oksana",
//                     Gender = Gender.Female,
//                     LastName = "Zelena"
//                 },
//                 new()
//                 {
//                     Id = 2,
//                     Age = 20,
//                     FirstName = "Olha",
//                     Gender = Gender.Female,
//                     LastName = "Ponovna"
//                 },
//                 new()
//                 {
//                     Id = 3,
//                     Age = 34,
//                     FirstName = "Olena",
//                     Gender = Gender.Female,
//                     LastName = "Ivani",
//                     Subjects = new List<Subject>()
//                 }
//             };
//             return teachers;
//         }
//
//         private static List<Class> GetClasses()
//         {
//             var classes = new List<Class>
//             {
//                 new()
//                 {
//                     Id = 1,
//                     Name = "10A",
//                     TeacherId = 1,
//                     Teacher = new Teacher
//                     {
//                         Id = 1
//                     }
//                 },
//                 new()
//                 {
//                     Id = 2,
//                     Name = "11T"
//                 }
//             };
//             return classes;
//         }
//     }
// }