using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using StudentEnrolment.Core.Models;
using StudentEnrolment.Core.Services;
using StudentEnrolment.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Core.Tests.Services
{
    public class CoursesServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IRepositoryService<CourseModel>> _serviceMock;
        private readonly Mock<ILogger<CoursesService>> _loggerServiceMock;
        private readonly CoursesService _sut;

        public CoursesServiceTest()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IRepositoryService<CourseModel>>>();
            _loggerServiceMock = _fixture.Freeze<Mock<ILogger<CoursesService>>>();
            _sut = new CoursesService(_serviceMock.Object, _loggerServiceMock.Object);
        }

        [Fact]
        public void GetAllCourses_ShouldReturnData_WhenDataFound()
        {
            // Arrange
            var response = _fixture.Create<IEnumerable<CourseModel>>();
            var fileName = "Courses";
            _serviceMock.Setup(x => x.Get(fileName)).Returns(response);

            // Act
            var result = _sut.GetAllCourses();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().BeAssignableTo<IEnumerable<CourseModel>>();

            // moqs way of verification. 
            _serviceMock.Verify(x => x.Get(fileName), Times.Once());
        }

        [Fact]
        public void GetAllCourses_ShouldReturnNoData_WhenNoDataFound()
        {
            // Arrange
            //var response = _fixture.Inject<IEnumerable<StudentDetailsModel>>(new StudentDetailsModel[0]);
            var response = new List<CourseModel>();
            //int itemCount = 0;

            var fileName = "Courses";
            _serviceMock.Setup(x => x.Get(fileName)).Returns(response);

            // Act
            var result = _sut.GetAllCourses();

            // Assert
            result.Should().BeEmpty();
            result.Should().BeAssignableTo<IEnumerable<CourseModel>>();
            //Assert.Equal(itemCount, result.Count());

            // moqs way of verification. 
            _serviceMock.Verify(x => x.Get(fileName), Times.Once());
        }

        [Fact]
        public void GetAllCourses_ShouldReturnNoData_WhenIOExceptionThrown()
        {
            // Arrange

            var fileName = "Courses";
            _serviceMock.Setup(x => x.Get(fileName)).Throws(new IOException());

            // Act
            Func<IEnumerable<CourseModel>> exception = () => _sut.GetAllCourses();

            // Assert
            exception.Should().Throw<IOException>();

            // moqs way of verification. 
            _serviceMock.Verify(x => x.Get(fileName), Times.Once());
        }

        [Fact]
        public void GetAllStudentDetails_ShouldReturnNoData_WhenJsonExceptionThrown()
        {
            // Arrange
            var fileName = "Courses";
            _serviceMock.Setup(x => x.Get(fileName)).Throws(new JsonException());

            // Act
            Func<IEnumerable<CourseModel>> result = () => _sut.GetAllCourses();

            // Assert
            result.Should().Throw<JsonException>();

            // moqs way of verification. 
            _serviceMock.Verify(x => x.Get(fileName), Times.Once());
        }
    }
}
