
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentEnrolment.API.Controllers;
using StudentEnrolment.Core.Models;
using StudentEnrolment.Core.Services.Interfaces;
using Xunit;

namespace StudentEnrolment.API.Tests.Controllers
{
    public class StudentDetailsControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IStudentDetailsService> _serviceMock;
        private readonly StudentDetailsController _sut;

        public StudentDetailsControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IStudentDetailsService>>();
            _sut = new StudentDetailsController(_serviceMock.Object);
        }

        [Fact]
        public void GetStudentDetails_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var response = _fixture.Create<IEnumerable<StudentDetailsModel>>().ToList();
            _serviceMock.Setup(x => x.GetAllStudentDetails()).Returns(response);

            // Act
            var result = _sut.GetStudentDetails();

            // Assert
            result.Result.As<OkObjectResult>().Value.As<IEnumerable<StudentDetailsModel>>()
                .Should()
                .NotBeNull()
                .And.BeOfType(response.GetType());

            // moqs way of verification. 
            _serviceMock.Verify(x => x.GetAllStudentDetails(), Times.Once());
        }

        [Fact]
        public void GetStudentDetails_ShouldReturnNoResultFound_WhenDataNotFound()
        {
            // Arrange
            List<StudentDetailsModel> response = new List<StudentDetailsModel>();
            _serviceMock.Setup(x => x.GetAllStudentDetails()).Returns(response);
            ProblemDetails o = new ProblemDetails
            {
                Detail = "Either the db is empty or some error occurred",
                Status = 404
            };

            // Act
            var result = _sut.GetStudentDetails();

            // Assert
            result.Result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<ObjectResult>();
            Assert.Equal(o.Status.ToString(), result.Result.As<ObjectResult>().Value.As<ProblemDetails>().Status.ToString());
            Assert.Equal(o.Detail.ToString(), result.Result.As<ObjectResult>().Value.As<ProblemDetails>().Detail.ToString());
            // moqs way of verification. 
            _serviceMock.Verify(x => x.GetAllStudentDetails(), Times.Once());
        }

        [Fact]
        public void AddStudentDetails_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var studentModel = _fixture.Create<StudentDetailsModel>();
            _serviceMock.Setup(x => x.AddStudentDetails(studentModel)).Returns(true);
            var response = "Student details added successfully";

            // Act
            var result = _sut.AddStudentDetails(studentModel);

            // Assert
            result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull();
            Assert.Equal(response, result.As<OkObjectResult>().Value);

            // moqs way of verification. 
            _serviceMock.Verify(x => x.AddStudentDetails(studentModel), Times.Once());
        }

        // we will change the test when return type is changed to bool
        [Fact]
        public void AddStudentDetails_ShouldReturnNoResultFound_WhenDataNotFound()
        {
            // Arrange
            var request = _fixture.Create<StudentDetailsModel>();
            _serviceMock.Setup(x => x.AddStudentDetails(request)).Returns(false);
            ProblemDetails o = new ProblemDetails
            {
                Detail = "Some error occured while adding student details",
                Status = 500
            };

            // Act
            var result = _sut.AddStudentDetails(request);

            // Assert
            result.As<ObjectResult>().Value
                .Should()
                .NotBeNull();
            Assert.Equal(o.Status.ToString(), result.As<ObjectResult>().Value.As<ProblemDetails>().Status.ToString());
            Assert.Equal(o.Detail.ToString(), result.As<ObjectResult>().Value.As<ProblemDetails>().Detail.ToString());

            // moqs way of verification. 
            _serviceMock.Verify(x => x.AddStudentDetails(request), Times.Once());
        }

        [Fact]
        public void UpdateStudentDetails_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var request = _fixture.Create<StudentDetailsModel>();
            _serviceMock.Setup(x => x.UpdateStudentDetails(request)).Returns(true);
            var response = "Student details updated successfully";

            // Act
            var result = _sut.UpdateStudentDetails(request);

            // Assert
            result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull();
            Assert.Equal(response, result.As<OkObjectResult>().Value);

            // moqs way of verification. 
            _serviceMock.Verify(x => x.UpdateStudentDetails(request), Times.Once());
        }

        // we will change the test when return type is changed to bool
        [Fact]
        public void UpdateStudentDetails_ShouldReturnNoResultFound_WhenDataNotFound()
        {
            // Arrange
            var request = _fixture.Create<StudentDetailsModel>();
            _serviceMock.Setup(x => x.UpdateStudentDetails(request)).Returns(false);
            ProblemDetails o = new ProblemDetails
            {
                Detail = "Some error occured while updating student details",
                Status = 500
            };

            // Act
            var result = _sut.UpdateStudentDetails(request);

            // Assert
            result.As<ObjectResult>().Value
                .Should()
                .NotBeNull();
            Assert.Equal(o.Status.ToString(), result.As<ObjectResult>().Value.As<ProblemDetails>().Status.ToString());
            Assert.Equal(o.Detail.ToString(), result.As<ObjectResult>().Value.As<ProblemDetails>().Detail.ToString());

            // moqs way of verification. 
            _serviceMock.Verify(x => x.UpdateStudentDetails(request), Times.Once());
        }

    }
}

