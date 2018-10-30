//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ContosoUniversity.Data.EntityModels;
//using ContosoUniversity.Data.Repositories;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using Xunit;

//namespace ContosoUniversity.Data.Test.Repositories
//{
//    [Trait("Category", "Unit Test: Data.Repositories.Students")]
//    public class StudentRepositoryTests
//    {
//        private readonly IStudentsRepository _repository;

//        private readonly Mock<SchoolContext> _mockDbContext;

//        private readonly Mock<DbSet<StudentEntity>> _mockDbSet;

//        public StudentRepositoryTests()
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();

//            _mockDbContext = new Mock<SchoolContext>(optionsBuilder.Options);

//            _mockDbSet = new Mock<DbSet<StudentEntity>>();

//            var students = new List<StudentEntity>
//            {
//                new StudentEntity { StudentId = 1, LastName = "some-last-name1"},
//                new StudentEntity { StudentId = 2, LastName = "some-last-name2" },
//                new StudentEntity { StudentId = 3, LastName = "some-last-name3" }
//            }; ;

//            var queryableStudents = students.AsQueryable();

//            _mockDbSet.As<IQueryable<StudentEntity>>().Setup(m => m.Provider).Returns(queryableStudents.Provider);
//            _mockDbSet.As<IQueryable<StudentEntity>>().Setup(m => m.Expression).Returns(queryableStudents.Expression);
//            _mockDbSet.As<IQueryable<StudentEntity>>().Setup(m => m.ElementType).Returns(queryableStudents.ElementType);
//            _mockDbSet.As<IQueryable<StudentEntity>>().Setup(m => m.GetEnumerator()).Returns(queryableStudents.GetEnumerator());

//            _mockDbSet.Setup(dbSet => dbSet.Add(It.IsAny<StudentEntity>())).Callback((StudentEntity s) => students.Add(s));

//            _mockDbSet.Setup(dbSet => dbSet.Remove(It.IsAny<StudentEntity>())).Callback((StudentEntity s) => students.Remove(s));

//            _mockDbContext.Setup(c => c.Students).Returns(_mockDbSet.Object);

//            _repository = new StudentsRepository(_mockDbContext.Object);
//        }

//        [Fact]
//        public async Task ShouldReturnAllTheStudents()
//        {
//            var students = await _repository.GetStudentsAsync();

//            students.Count().Should().Be(3);
//        }

//        [Fact]
//        public async Task ShouldReturnStudentWhenIdExists()
//        {
//            var student = await _repository.GetStudentByIdAsync(2);

//            student.Should().NotBeNull();

//            student.LastName.Should().Be("some-last-name2");
//        }

//        [Fact]
//        public async Task ShouldReturnNullWhenIdDoesNotExist()
//        {
//            var student = await _repository.GetStudentByIdAsync(4);

//            student.Should().BeNull();
//        }

//        [Fact]
//        public async Task ShouldCreateStudent()
//        {
//            var student = new StudentEntity
//            {
//                LastName = "some-last-name4"
//            };

//            await _repository.CreateAsync(student);

//            _mockDbSet.Verify(x => x.Add(student), Times.Exactly(1));

//            _mockDbContext.Verify(c => c.SaveChanges(), Times.Exactly(1));

//            var addedStudent = _mockDbContext.Object.Students.FirstOrDefault(s => s.LastName == "some-last-name4");

//            addedStudent.Should().NotBeNull();
//        }

//        [Fact]
//        public void ShouldThrowExceptionWhenFailingToCreateStudent()
//        {
//            _mockDbContext.Setup(c => c.SaveChanges()).Throws<Exception>();

//            Action action = async () => await _repository.CreateAsync(new StudentEntity());

//            action.Should().Throw<Exception>()
//                .WithMessage("Student was failed to be saved in the database");
//        }

//        [Fact]
//        public async Task ShouldReturnFalseWhenNoStudentFoundToUpdate()
//        {
//            var student = new StudentEntity {StudentId = 4};

//            var result = await _repository.UpdateAsync(student);

//            result.Should().BeFalse();
//        }

//        [Fact]
//        public async Task ShouldReturnTrueWhenStudentFoundToUpdate()
//        {
//            var student = new StudentEntity
//                {
//                    StudentId = 3,
//                    LastName = "new-last-name",
//                    FirstMidName = "new-first-mid-name",
//                    EnrollmentDate = new DateTime(2000, 1, 2)
//                };

//            var result = await _repository.UpdateAsync(student);

//            _mockDbContext.Verify(dbSet => dbSet.SaveChanges(), Times.Exactly(1));

//            result.Should().BeTrue();

//            var updatedStudent = _mockDbContext.Object.Students.First(s => s.StudentId == 3);

//            updatedStudent.LastName.Should().Be("new-last-name");

//            updatedStudent.FirstMidName.Should().Be("new-first-mid-name");

//            updatedStudent.EnrollmentDate.Should().Be(new DateTime(2000, 1, 2));
//        }

//        [Fact]
//        public void ShouldThrowExceptionWhenFailingToUpdateStudent()
//        {
//            _mockDbContext.Setup(c => c.SaveChanges()).Throws<Exception>();

//            Action action = async () => await _repository.UpdateAsync(new StudentEntity { StudentId = 1 });

//            action.Should().Throw<Exception>()
//                .WithMessage("Student was failed to be updated in the database");
//        }

//        [Fact]
//        public async Task ShouldReturnFalseWhenNoStudentFoundToDelete()
//        {
//            var result = await _repository.DeleteAsync(4);

//            result.Should().BeFalse();
//        }

//        [Fact]
//        public async Task ShouldReturnTrueWhenStudentWasDeleted()
//        {
//            var result = await _repository.DeleteAsync(3);

//            _mockDbSet.Verify(dbSet => dbSet.Remove(It.IsAny<StudentEntity>()), Times.Exactly(1));

//            _mockDbContext.Verify(dbSet => dbSet.SaveChanges(), Times.Exactly(1));

//            var deletedStudent = _mockDbContext.Object.Students.FirstOrDefault(s => s.StudentId == 3);

//            deletedStudent.Should().BeNull();

//            result.Should().BeTrue();
//        }

//        [Fact]
//        public void ShouldThrowExceptionWhenFailingToDeleteStudent()
//        {
//            _mockDbContext.Setup(c => c.SaveChanges()).Throws<Exception>();

//            Action action = async () => await _repository.DeleteAsync(3);

//            action.Should().Throw<Exception>()
//                .WithMessage("Student was failed to be removed in the database");
//        }
//    }
//}
