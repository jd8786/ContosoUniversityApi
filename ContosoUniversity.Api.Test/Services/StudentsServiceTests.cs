//using AutoFixture;
//using AutoMapper;
//using ContosoUniversity.Api.Models;
//using ContosoUniversity.Api.Services;
//using ContosoUniversity.Api.Validators;
//using ContosoUniversity.Data.EntityModels;
//using ContosoUniversity.Data.Repositories;
//using FluentAssertions;
//using Moq;
//using System.Collections.Generic;
//using Xunit;

//namespace ContosoUniversity.Api.Test.Services
//{
//    [Trait("Category", "Uint Test: Api.Services.Students")]
//    public class StudentsServiceTests
//    {
//        private readonly Mock<IStudentsRepository> _studentsRepository;

//        private readonly Mock<IStudentValidator> _studentValidator;

//        private readonly Mock<ICourseValidator> _courseValidator;

//        private readonly Mock<IMapper> _mapper;

//        private readonly IStudentsService _studentsService;

//        private readonly List<StudentEntity> _studentEntities;

//        private readonly List<Student> _students;

//        private readonly Fixture _fixture = new Fixture();

//        public StudentsServiceTests()
//        {
//            _studentsRepository = new Mock<IStudentsRepository>();
//            _studentValidator = new Mock<IStudentValidator>();
//            _courseValidator = new Mock<ICourseValidator>();
//            _mapper = new Mock<IMapper>();

//            _studentsService = new StudentsesService(_studentsRepository.Object, _studentValidator.Object,
//                _courseValidator.Object, _mapper.Object);

//            _studentEntities = new List<StudentEntity>
//            {
//                new StudentEntity {StudentId = 1},
//                new StudentEntity {StudentId = 2}
//            };

//            _students = new List<Student>
//            {
//                new Student {StudentId = 1},
//                new Student {StudentId = 2}
//            };

//            _studentsRepository.Setup(s => s.GetAll()).Returns(_studentEntities);

//            _mapper.Setup(m => m.Map<List<Student>>(_studentEntities)).Returns(_students);

//            _mapper.Setup(m => m.Map<Student>(_studentEntities[0])).Returns(_students[0]);

//            _mapper.Setup(m => m.Map<List<StudentEntity>>(_students)).Returns(_studentEntities);

//            _mapper.Setup(m => m.Map<StudentEntity>(_students[0])).Returns(_studentEntities[0]);
//        }

//        [Fact]
//        public void ShouldReturnAllExistingStudentsWhenCallingGetAll()
//        {
//            var students = _studentsService.GetAll();

//            students.Should().AllBeEquivalentTo(_students);
//        }

//        [Fact]
//        public void ShouldReturnExistingStudentWhenCallingGetStudentById()
//        {
//            var studentEntity = _studentEntities[0];

//            var mappedStudent = _students[0];

//            var student = _studentsService.Get(studentEntity.StudentId);

//            _studentValidator.Verify(s => s.Validate(studentEntity.StudentId), Times.Exactly(1));

//            student.Should().BeEquivalentTo(mappedStudent);
//        }

//        [Fact]
//        public void ShouldReturnNewStudentWhenCallingAdd()
//        {
//            var newStudent = new Student();

//            var mappedStudent = new StudentEntity {StudentId = 1, Enrollments = new List<EnrollmentEntity>()};

//            _mapper.Setup(m => m.Map<StudentEntity>(newStudent)).Returns(mappedStudent);

//            var returnedStudent = _studentsService.Add(newStudent);

//            mappedStudent.Enrollments.Should().BeNull();

//            _studentValidator.Verify(s => s.ValidatePostStudent(newStudent), Times.Exactly(1));

//            _studentsRepository.Verify(s => s.Add(mappedStudent), Times.Exactly(1));

//            _studentsRepository.Verify(s => s.Save(), Times.Exactly(1));

//            returnedStudent.Should().BeEquivalentTo(_students[0]);
//        }

//        [Fact]
//        public void ShouldReturnUpdatedStudentWhenCallingUpdate()
//        {
//            var updatedStudent = _fixture.Build<Student>()
//                .With(s => s.StudentId, 1)
//                .Create();

//            var enrollmentEntities = new List<EnrollmentEntity>();

//            _enrollmentsRepository.Setup(e => e.GetByStudentId(updatedStudent.StudentId)).Returns(enrollmentEntities);

//            _mapper.Setup(m => m.Map<List<Enrollment>>(enrollmentEntities)).Returns(new List<Enrollment>());

//            var existingStudent = _students[0];

//            var returnedStudent = _studentsService.Update(updatedStudent);

//            existingStudent.Courses.Should().AllBeEquivalentTo(new List<Enrollment>());

//            existingStudent.EnrollmentDate.Should().Be(updatedStudent.EnrollmentDate);

//            existingStudent.FirstMidName.Should().Be(updatedStudent.FirstMidName);

//            existingStudent.LastName.Should().Be(updatedStudent.LastName);

//            existingStudent.OriginCountry.Should().Be(updatedStudent.OriginCountry);

//            _studentValidator.Verify(s => s.ValidatePutStudent(updatedStudent), Times.Exactly(1));

//            _studentsRepository.Verify(s => s.Update(_studentEntities[0]), Times.Exactly(1));

//            _studentsRepository.Verify(s => s.Save(), Times.Exactly(1));

//            returnedStudent.Should().BeEquivalentTo(_students[0]);
//        }

//        [Fact]
//        public void ShouldDeleteStudentWhenCallingDelete()
//        {
//            var existingStudent = _students[0];

//            var isRemoved = _studentsService.Remove(existingStudent.StudentId);

//            _studentsRepository.Verify(s => s.Remove(_studentEntities[0]), Times.Exactly(1));

//            _studentsRepository.Verify(s => s.Save(), Times.Exactly(1));

//            isRemoved.Should().BeTrue();
//        }
//    }
//}
