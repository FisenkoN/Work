﻿using System.Linq;
using School.BLL.Dto;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.BLL.Mapper
{
    public class Map
    {
        private readonly IUnitOfWork _unitOfWork;

        public Map(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ClassDto To(Class c) =>
            new()
            {
                Id = c.Id,
                Name = c.Name,
                TeacherId = c.TeacherId,
                StudentIds = c.Students
                    .Select(s => s.Id)
            };

        public StudentDto To(Student s) =>
            new()
            {
                Id = s.Id,
                Age = s.Age,
                ClassId = s.ClassId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Gender = (GenderDto)s.Gender,
                Image = s.Image,
                SubjectIds = s.Subjects
                    .Select(subject => subject.Id)
            };

        public TeacherDto To(Teacher t) =>
            new()
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Age = t.Age,
                Image = t.Image,
                ClassId = t.ClassId,
                Gender = (GenderDto)t.Gender,
                SubjectIds = t.Subjects?
                    .Select(s => s.Id)
            };

        public SubjectDto To(Subject s) =>
            new()
            {
                Id = s.Id,
                Name = s.Name,
                StudentIds = s.Students.Select(z => z.Id),
                TeacherIds = s.Teachers.Select(t => t.Id)
            };

        public Subject To(SubjectDto s) =>
            new()
            {
                Id = s.Id,
                Name = s.Name,
                Students = s.StudentIds != null
                    ? _unitOfWork.Students
                        .GetAll()
                        .Where(i =>
                            s.StudentIds
                                .ToList()
                                .Exists(t =>
                                    t == i.Id))
                        .ToList()
                    : null,

                Teachers = s.TeacherIds != null
                    ? _unitOfWork.Teachers
                        .GetAll()
                        .Where(i =>
                            s.TeacherIds
                                .ToList()
                                .Exists(t =>
                                    t == i.Id))
                        .ToList()
                    : null
            };

        public Class To(ClassDto c) =>
            new()
            {
                Id = c.Id,
                Name = c.Name,
                TeacherId = c.TeacherId,
                Students = _unitOfWork.Students
                    .GetAll()
                    .Where(i =>
                        c.StudentIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
            };

        public Student To(StudentDto s) =>
            new()
            {
                Id = s.Id,
                Age = s.Age,
                ClassId = s.ClassId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Image = s.Image,
                Gender = (Gender)s.Gender,
                Subjects = _unitOfWork.Subjects
                    .GetAll()
                    .Where(i =>
                        s.SubjectIds
                            .ToList()
                            .Exists(t =>
                                t == i.Id))
                    .ToList()
            };

        public Teacher To(TeacherDto t) =>
            new()
            {
                Id = t.Id,
                Age = t.Age,
                ClassId = t.ClassId,
                FirstName = t.FirstName,
                Image = t.Image,
                Gender = (Gender)t.Gender,
                LastName = t.LastName,
                Subjects = t.SubjectIds != null
                    ? _unitOfWork.Subjects
                        .GetAll()
                        .Where(i =>
                            t.SubjectIds
                                .ToList()
                                .Exists(s =>
                                    s == i.Id))
                        .ToList()
                    : null
            };
    }
}