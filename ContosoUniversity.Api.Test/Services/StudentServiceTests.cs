using AutoMapper;
using ContosoUniversity.Api.AutoMappers;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Api.Test.Services
{
    [Trait("Category", "Uint Test: Api.Services.Student")]
    public class StudentServiceTests
    {
        private readonly Mock<IStudentRepository> _studentRepository;

        private readonly Mock<IStudentValidator> _studentValidator;

        private readonly IStudentService _studentService;

        private readonly List<StudentEntity> _studentEntities;

        public StudentServiceTests()
        {
            _studentRepository = new Mock<IStudentRepository>();
            _studentValidator = new Mock<IStudentValidator>();

            var mapperConfig = new MapperConfiguration(ctg => ctg.AddProfile(new StudentProfile()));

            var mapper = new Mapper(mapperConfig);

            _studentService = new StudentService(_studentRepository.Object, _studentValidator.Object, mapper);

            _studentEntities = new List<StudentEntity>
            {
                new StudentEntity {StudentId = 1},
                new StudentEntity {StudentId = 2}
            };

            _studentRepository.Setup(s => s.GetAll()).Returns(_studentEntities);
        }

        [Fact]
        public void ShouldReturnAllStudentsWhenCallingGetAll()
        {
            var students = _studentService.GetAll();

            _studentRepository.Verify(sr => sr.GetAll(), Times.Exactly(1));

            students.Count().Should().Be(2);
        }

        [Fact]
        public void ShouldReturnStudentWhenCallingGetStudentById()
        {
            var student = _studentService.Get(1);

            _studentValidator.Verify(s => s.CommonValidator.ValidateStudentById(1), Times.Exactly(1));

            _studentRepository.Verify(sr => sr.GetAll(), Times.Exactly(1));

            student.StudentId.Should().Be(1);
        }

        [Fact]
        public void ShouldAddStudentWhenCallingAdd()
        {
            var newStudent = new Student { StudentId = 3 };

            _studentRepository.Setup(sr => sr.Add(It.IsAny<StudentEntity>()))
                .Callback<StudentEntity>(s => _studentEntities.Add(s));

            var addedStudent = _studentService.Add(newStudent);

            _studentValidator.Verify(sv => sv.ValidatePostStudent(newStudent), Times.Exactly(1));

            _studentRepository.Verify(sr => sr.Add(It.Is<StudentEntity>(s => s.StudentId == 3)), Times.Exactly(1));

            _studentRepository.Verify(s => s.Save(), Times.Exactly(1));

            addedStudent.StudentId.Should().Be(3);
        }

        [Fact]
        public void ShouldUpdateStudentWhenCallingUpdate()
        {
            var studentToUpdate = new Student { StudentId = 1 };

            var updatedStudent = _studentService.Update(studentToUpdate);

            _studentValidator.Verify(sv => sv.ValidatePutStudent(studentToUpdate), Times.Exactly(1));

            _studentRepository.Verify(sr => sr.Update(It.Is<StudentEntity>(s => s.StudentId == 1)), Times.Exactly(1));

            _studentRepository.Verify(s => s.Save(), Times.Exactly(1));

            updatedStudent.StudentId.Should().Be(1);
        }

        [Fact]
        public void ShouldDeleteStudentWhenCallingDelete()
        {
            var isRemoved = _studentService.Remove(1);

            _studentValidator.Verify(sv => sv.CommonValidator.ValidateStudentById(1), Times.Exactly(1));

            _studentRepository.Verify(sr => sr.Remove(It.Is<StudentEntity>(s => s.StudentId == 1)), Times.Exactly(1));

            _studentRepository.Verify(sr => sr.Save(), Times.Exactly(1));

            isRemoved.Should().BeTrue();
        }
    }
}
