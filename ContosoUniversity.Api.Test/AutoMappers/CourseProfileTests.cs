using AutoFixture;
using AutoMapper;
using ContosoUniversity.Api.AutoMappers;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Api.Test.AutoMappers
{
    [Trait("Category", "Unit Test: Api.AutoMappers.Course")]
    public class CourseProfileTests
    {
        private readonly IMapper _mapper;

        private readonly Fixture _fixture;

        public CourseProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CourseProfile());
                cfg.AddProfile(new DepartmentProfile());
            });

            _mapper = new Mapper(config);

            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldMapPrimaryPropertiesFromCourseEntityToCourse()
        {
            var courseEntity = _fixture.Build<CourseEntity>()
                .Without(c => c.Enrollments)
                .Without(c => c.CourseAssignments)
                .Without(c => c.Department)
                .Create();

            var course = _mapper.Map<Course>(courseEntity);

            course.CourseId.Should().Be(courseEntity.CourseId);
            course.Credits.Should().Be(courseEntity.Credits);
            course.Title.Should().Be(courseEntity.Title);
            course.Grade.Should().BeNull();
            course.Department.Should().BeNull();
            course.Instructors.Should().BeEmpty();
            course.Students.Should().BeEmpty();
        }

        [Fact]
        public void ShouldMapCoursesFromCourseEntityToCourse()
        {
            var studentEntity1 = new StudentEntity
            {
                FirstMidName = "first-mid-name1",
                LastName = "last-name1",
                EnrollmentDate = new DateTime(2010, 1, 1),
                OriginCountry = "origin-country1"
            };

            var studentEntity2 = new StudentEntity
            {
                FirstMidName = "first-mid-name2",
                LastName = "last-name2",
                EnrollmentDate = new DateTime(2010, 1, 2),
                OriginCountry = "origin-country2"
            };

            var enrollmentEntity1 = new EnrollmentEntity
            {
                Student = studentEntity1,
                StudentId = 1
            };

            var enrollmentEntity2 = new EnrollmentEntity
            {
                Student = studentEntity2,
                StudentId = 2,
            };

            var courseEntity = new CourseEntity
            {
                Enrollments = new List<EnrollmentEntity> { enrollmentEntity1, enrollmentEntity2 }
            };

            var course = _mapper.Map<Course>(courseEntity);

            course.Students.Count().Should().Be(2);
            course.Students.First().StudentId.Should().Be(1);
            course.Students.First().LastName.Should().Be("last-name1");
            course.Students.First().FirstMidName.Should().Be("first-mid-name1");
            course.Students.First().EnrollmentDate.Should().Be(new DateTime(2010, 1, 1));
            course.Students.First().OriginCountry.Should().Be("origin-country1");
            course.Students.Last().StudentId.Should().Be(2);
            course.Students.Last().LastName.Should().Be("last-name2");
            course.Students.Last().FirstMidName.Should().Be("first-mid-name2");
            course.Students.Last().EnrollmentDate.Should().Be(new DateTime(2010, 1, 2));
            course.Students.Last().OriginCountry.Should().Be("origin-country2");
        }

        [Fact]
        public void ShouldMapInstructorsFromCourseEntityToCourse()
        {
            var instructor1 = new InstructorEntity
            {
                FirstMidName = "first-mid-name1",
                LastName = "last-name1",
                HireDate = new DateTime(2000, 1, 1),
                OfficeAssignment = new OfficeAssignmentEntity { Location = "location1" }
            };

            var instructor2 = new InstructorEntity
            {
                FirstMidName = "first-mid-name2",
                LastName = "last-name2",
                HireDate = new DateTime(2000, 1, 2),
                OfficeAssignment = new OfficeAssignmentEntity { Location = "location2" }
            };

            var courseAssignment1 = new CourseAssignmentEntity
            {
                Instructor = instructor1,
                InstructorId = 1
            };

            var courseAssignment2 = new CourseAssignmentEntity
            {
                Instructor = instructor2,
                InstructorId = 2
            };

            var courseEntity = new CourseEntity
            {
                CourseAssignments = new List<CourseAssignmentEntity> { courseAssignment1, courseAssignment2 }
            };

            var course = _mapper.Map<Course>(courseEntity);

            course.Instructors.Count().Should().Be(2);
            course.Instructors.First().InstructorId.Should().Be(1);
            course.Instructors.First().LastName.Should().Be("last-name1");
            course.Instructors.First().FirstMidName.Should().Be("first-mid-name1");
            course.Instructors.First().HireDate.Should().Be(new DateTime(2000, 1, 1));
            course.Instructors.First().OfficeLocation.Should().Be("location1");
            course.Instructors.Last().InstructorId.Should().Be(2);
            course.Instructors.Last().LastName.Should().Be("last-name2");
            course.Instructors.Last().FirstMidName.Should().Be("first-mid-name2");
            course.Instructors.Last().HireDate.Should().Be(new DateTime(2000, 1, 2));
            course.Instructors.Last().OfficeLocation.Should().Be("location2");
        }

        [Fact]
        public void ShouldMapDepartmentFromCourseEntityToCourse()
        {
            var course = new CourseEntity { Department = new DepartmentEntity { DepartmentId = 1, Budget = 2312 } };

            var courseEntity = _mapper.Map<Course>(course);

            courseEntity.Department.Should().NotBeNull();
            courseEntity.Department.DepartmentId.Should().Be(1);
            courseEntity.Department.Budget.Should().Be(2312);
        }

        [Fact]
        public void ShouldMapPrimaryPropertiesFromCourseToCourseEntity()
        {
            var course = _fixture.Build<Course>()
                .Without(c => c.Department)
                .Without(c => c.Instructors)
                .Without(c => c.Students)
                .Create();

            var courseEntity = _mapper.Map<CourseEntity>(course);

            courseEntity.CourseId.Should().Be(courseEntity.CourseId);
            courseEntity.Credits.Should().Be(courseEntity.Credits);
            courseEntity.Title.Should().Be(courseEntity.Title);
            courseEntity.Department.Should().BeNull();
            courseEntity.CourseAssignments.Should().BeEmpty();
            courseEntity.Enrollments.Should().BeEmpty();
        }

        [Fact]
        public void ShouldMapEnrollmentsFromCourseToCourseEntity()
        {
            var student1 = new Student { StudentId = 1 };

            var student2 = new Student { StudentId = 2 };

            var course = new Course
            {
                CourseId = 1,
                Students = new List<Student> { student1, student2 }
            };

            var courseEntity = _mapper.Map<CourseEntity>(course);

            courseEntity.Enrollments.Count.Should().Be(2);
            courseEntity.Enrollments.First().CourseId.Should().Be(1);
            courseEntity.Enrollments.First().StudentId.Should().Be(1);
            courseEntity.Enrollments.Last().CourseId.Should().Be(1);
            courseEntity.Enrollments.Last().StudentId.Should().Be(2);
        }

        [Fact]
        public void ShouldMapCourseAssignmentsFromCourseToCourseEntity()
        {
            var instructor1 = new Instructor { InstructorId = 1 };

            var instructor2 = new Instructor { InstructorId = 2 };

            var course = new Course
            {
                CourseId = 1,
                Instructors = new List<Instructor> { instructor1, instructor2 }
            };

            var courseEntity = _mapper.Map<CourseEntity>(course);

            courseEntity.CourseAssignments.Count.Should().Be(2);
            courseEntity.CourseAssignments.First().CourseId.Should().Be(1);
            courseEntity.CourseAssignments.First().InstructorId.Should().Be(1);
            courseEntity.CourseAssignments.Last().CourseId.Should().Be(1);
            courseEntity.CourseAssignments.Last().InstructorId.Should().Be(2);
        }

        [Fact]
        public void ShouldMapDepartmentEntityFromCourseToCourseEntity()
        {
            var course = new Course { Department = new Department { DepartmentId = 1 } };

            var courseEntity = _mapper.Map<CourseEntity>(course);

            courseEntity.DepartmentId.Should().Be(1);
        }
    }
}
