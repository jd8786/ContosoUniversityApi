//using AutoMapper;
//using ContosoUniversity.Api.Models;
//using ContosoUniversity.Api.Services;
//using ContosoUniversity.Api.Validators;
//using ContosoUniversity.Data.EntityModels;
//using ContosoUniversity.Data.Exceptions;
//using ContosoUniversity.Data.Repositories;
//using FluentAssertions;
//using Moq;
//using System.Collections.Generic;
//using System.Linq;
//using Xunit;

//namespace ContosoUniversity.Api.Test.Services
//{
//    [Trait("Category", "Uint Test: Api.Services.Enrollments")]
//    public class EnrollmentsServiceTests
//    {
//        private readonly Mock<ICoursesRepository> _coursesRepository;

//        private readonly Mock<IEnrollmentsRepository> _enrollmentsRepository;

//        private readonly Mock<IMapper> _mapper;

//        private readonly IEnrollmentsService _enrollmentService;

//        public EnrollmentsServiceTests()
//        {
//            _coursesRepository = new Mock<ICoursesRepository>();
//            _enrollmentsRepository = new Mock<IEnrollmentsRepository>();
//            _mapper = new Mock<IMapper>();

//            _enrollmentService = new EnrollmentsService(_enrollmentsRepository.Object, _coursesRepository.Object,
//                _enrollmentValidator.Object, _mapper.Object);
//        }

//        [Fact]
//        public void ShouldThrowInvalidEnrollmentExceptionWhenCallingAddRangeWithNullEnrollments()
//        {
//            var exception = Assert.Throws<InvalidEnrollmentException>(() => _enrollmentService.AddRange(null));

//            exception.Message.Should().Be("Enrollments must be provided");
//        }

//        [Fact]
//        public void ShouldReturnNewEmptyListOfEnrollmentWhenCallingAddRangeWithEmptyEnrollments()
//        {
//            var enrollments = _enrollmentService.AddRange(new List<Enrollment>());

//            enrollments.Should().BeEquivalentTo(new List<Enrollment>());
//        }

//        [Fact]
//        public void ShouldReturnNewListOfEnrollmentWhenCallingAddRangeWithNonEmptyEnrollments()
//        {
//            var enrollments = new List<Enrollment>
//            {
//                new Enrollment(),
//                new Enrollment()
//            };

//            var enrollmentEntities = new List<EnrollmentEntity>
//            {
//                new EnrollmentEntity(),
//                new EnrollmentEntity()
//            };

//            _mapper.Setup(m => m.Map<IEnumerable<EnrollmentEntity>>(enrollments)).Returns(enrollmentEntities);

//            _mapper.Setup(m => m.Map<IEnumerable<Enrollment>>(enrollmentEntities)).Returns(enrollments);

//            var addedEnrollments = _enrollmentService.AddRange(enrollments);

//            _enrollmentValidator.Verify(e => e.ValidatePostEnrollment(It.IsAny<Enrollment>()), Times.Exactly(2));

//            _enrollmentsRepository.Verify(e => e.AddRange(enrollmentEntities), Times.Exactly(1));

//            _enrollmentsRepository.Verify(e => e.Save(), Times.Exactly(1));

//            addedEnrollments.Should().BeEquivalentTo(enrollments);
//        }

//        [Fact]
//        public void ShouldUpdateEnrollmentsWhenCallingUpdate()
//        {
//            var courseEntities = new List<CourseEntity>
//            {
//                new CourseEntity { CourseId = 1 },
//                new CourseEntity { CourseId = 2 },
//                new CourseEntity { CourseId = 3 }
//            };

//            var enrollmentEntities = new List<EnrollmentEntity>
//            {
//                new EnrollmentEntity { StudentId = 1, CourseId = 1 },
//                new EnrollmentEntity { StudentId = 2, CourseId = 2 },
//                new EnrollmentEntity { StudentId = 1, CourseId = 3}
//            };

//            var enrollments = new List<Enrollment>
//            {
//                new Enrollment { StudentId = 1, CourseId = 1 },
//                new Enrollment { StudentId = 1, CourseId = 2 },
                
//            };

//            var mappedEnrollmentEntity1 = new EnrollmentEntity {StudentId = 1, CourseId = 1, Grade = Grade.A};

//            var mappedEnrollmentEntity2 = new EnrollmentEntity { StudentId = 1, CourseId = 2 };

//            _coursesRepository.Setup(c => c.GetAll()).Returns(courseEntities);

//            _enrollmentsRepository.Setup(e => e.GetAll()).Returns(enrollmentEntities);

//            _mapper.Setup(m => m.Map<IEnumerable<Enrollment>>(enrollmentEntities.Where(e => e.StudentId == 1)))
//                .Returns(new List<Enrollment>());

//            _mapper.Setup(m => m.Map<EnrollmentEntity>(enrollments[0])).Returns(mappedEnrollmentEntity1);

//            _mapper.Setup(m => m.Map<EnrollmentEntity>(enrollments[1])).Returns(mappedEnrollmentEntity2);

//            var updatedEnrollments = _enrollmentService.Update(1, enrollments);

//            _enrollmentValidator.Verify(e => e.ValidatePutEnrollment(It.IsAny<Enrollment>()), Times.Exactly(2));

//            _enrollmentsRepository.Verify(e => e.UpdateEnrollmentGrade(1, 1, mappedEnrollmentEntity1), Times.Exactly(1));

//            _enrollmentsRepository.Verify(e => e.Add(mappedEnrollmentEntity2), Times.Exactly(1));

//            _enrollmentsRepository.Verify(e => e.Remove(enrollmentEntities[2]), Times.Exactly(1));

//            _enrollmentsRepository.Verify(e => e.Save(), Times.Exactly(1));

//            updatedEnrollments.Should().BeEquivalentTo(new List<Enrollment>());
//        }

//        [Fact]
//        public void ShouldThrowInvalidEnrollmentExceptionWhenAddingNewEnrollmentWithNonZeroId()
//        {
//            var courseEntities = new List<CourseEntity>
//            {
//                new CourseEntity { CourseId = 1 },
//                new CourseEntity { CourseId = 2 }
//            };

//            var enrollmentEntities = new List<EnrollmentEntity>
//            {
//                new EnrollmentEntity { StudentId = 2, CourseId = 2 }
//            };

//            var enrollments = new List<Enrollment>
//            {
//                new Enrollment { EnrollmentId = 1, StudentId = 1, CourseId = 2 },
//            };

//            _coursesRepository.Setup(c => c.GetAll()).Returns(courseEntities);

//            _enrollmentsRepository.Setup(e => e.GetAll()).Returns(enrollmentEntities);

//            var exception = Assert.Throws<InvalidEnrollmentException>(() => _enrollmentService.Update(1, enrollments));

//            _enrollmentValidator.Verify(e => e.ValidatePutEnrollment(enrollments[0]), Times.Exactly(1));

//            _enrollmentsRepository.Verify(
//                e => e.UpdateEnrollmentGrade(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<EnrollmentEntity>()),
//                Times.Never);

//            exception.Message.Should().Be("Enrollment Id must be 0");
//        }
//    }
//}
