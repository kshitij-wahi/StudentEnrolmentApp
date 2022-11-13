
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentEnrolment.API.Controllers;
using StudentEnrolment.Core.Models;
using StudentEnrolment.Core.Services.Interfaces;

namespace StudentEnrolment.API.Tests.Controllers
{
    public class CoursesControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<ICoursesService> _serviceMock;
        private readonly CoursesController _sut;

        public CoursesControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<ICoursesService>>();
            _sut = new CoursesController(_serviceMock.Object);
        }

        [Fact]
        public void GetCourses_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var coursesMock = _fixture.Create <List<CourseModel>>();
            _serviceMock.Setup(x => x.GetCourses()).Returns(coursesMock);

            // Act
            var result = _sut.GetCourses();

            // Assert
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(coursesMock.GetType());

            // moqs way of verification. 
            _serviceMock.Verify(x => x.GetCourses(), Times.Once());
        }

        [Fact]
        public void GetCourses_ShouldReturnNoResultFound_WhenDataNotFound()
        {
            // Arrange
            List<CourseModel>? response = null;
            _serviceMock.Setup(x => x.GetCourses()).Returns(response);

            // Act
            var result = _sut.GetCourses();

            // Assert
            result.Result.Should().BeAssignableTo<NotFoundResult>();

            // moqs way of verification. 
            _serviceMock.Verify(x => x.GetCourses(), Times.Once());
        }
    }
}
