﻿using System.Collections.Generic;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Services
{
    public interface IStudentService
    {
        List<StudentInfo> GetStudentInfos();

        StudentInfo GetStudentInfoById(int studentInfoId);

        void CreateStudentInfo(StudentInfo studentInfo);

        bool UpdateStudentInfo(StudentInfo studentInfo);

        bool DeleteStudentInfo(int studentInfoId);
    }
}