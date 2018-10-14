using System;
using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;

namespace ContosoUniversity.Api.Services
{
    public class EnrollmentsService: IEnrollmentsService
    {
        private readonly IEnrollmentsRepository _repository;

        private readonly IMapper _mapper;

        public EnrollmentsService(IEnrollmentsRepository repository, IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }

        public Enrollment Add(Enrollment enrollment)
        {
            if (enrollment == null)
            {
                throw new InvalidEnrollmentException("Enrollment must be provided");
            }

            if (enrollment.StudentId == 0 || enrollment.CourseId == 0)
            {
                throw new InvalidEnrollmentException("Student and course must be provided");
            }


        }
    }
}
