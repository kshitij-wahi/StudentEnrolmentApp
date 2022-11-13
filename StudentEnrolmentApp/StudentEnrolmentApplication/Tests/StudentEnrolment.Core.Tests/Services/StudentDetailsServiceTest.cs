using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using StudentEnrolment.Core.Models;
using StudentEnrolment.Core.Services;
using StudentEnrolment.Core.Services.Interfaces;
using StudentEnrolment.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Core.Tests.Services
{
    public class StudentDetailsServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IRepositoryService<StudentDetailsModel>> _serviceMock;
        private readonly Mock<ILogger<StudentDetailsService>> _loggerServiceMock;
        private readonly StudentDetailsService _sut;
        public StudentDetailsServiceTest()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IRepositoryService<StudentDetailsModel>>>();
            _loggerServiceMock = _fixture.Freeze<Mock<ILogger<StudentDetailsService>>>();
            _sut = new StudentDetailsService(_serviceMock.Object, _loggerServiceMock.Object);
        }


        [Fact]
        public void GetAllStudentDetails_ShouldReturnData_WhenDataFound()
        {
            // Arrange
            var response = _fixture.Create<IEnumerable<StudentDetailsModel>>();
            var fileName = "Students";
            _serviceMock.Setup(x => x.Get(fileName)).Returns(response);

            // Act
            var result = _sut.GetAllStudentDetails();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().BeAssignableTo<IEnumerable<StudentDetailsModel>>();

            // moqs way of verification. 
            _serviceMock.Verify(x => x.Get(fileName), Times.Once());
        }

        [Fact]
        public void GetAllStudentDetails_ShouldReturnNoData_WhenNoDataFound()
        {
            // Arrange
            //var response = _fixture.Inject<IEnumerable<StudentDetailsModel>>(new StudentDetailsModel[0]);
            var response = new List<StudentDetailsModel>();
            //int itemCount = 0;

            var fileName = "Students";
            _serviceMock.Setup(x => x.Get(fileName)).Returns(response);

            // Act
            var result = _sut.GetAllStudentDetails();

            // Assert
            result.Should().BeEmpty();
            result.Should().BeAssignableTo<IEnumerable<StudentDetailsModel>>();
            //Assert.Equal(itemCount, result.Count());

            // moqs way of verification. 
            _serviceMock.Verify(x => x.Get(fileName), Times.Once());
        }

        [Fact]
        public void GetAllStudentDetails_ShouldReturnNoData_WhenIOExceptionThrown()
        {
            // Arrange

            var fileName = "Students";
            _serviceMock.Setup(x => x.Get(fileName)).Throws(new IOException());

            // Act
            Func<IEnumerable<StudentDetailsModel>> exception = () => _sut.GetAllStudentDetails();

            // Assert
            exception.Should().Throw<IOException>();

            // moqs way of verification. 
            _serviceMock.Verify(x => x.Get(fileName), Times.Once());
        }

        [Fact]
        public void GetAllStudentDetails_ShouldReturnNoData_WhenJsonExceptionThrown()
        {
            // Arrange
            var fileName = "Students";
            _serviceMock.Setup(x => x.Get(fileName)).Throws(new JsonException());

            // Act
            Func<IEnumerable<StudentDetailsModel>> result = () => _sut.GetAllStudentDetails();

            // Assert
            result.Should().Throw<JsonException>();

            // moqs way of verification. 
            _serviceMock.Verify(x => x.Get(fileName), Times.Once());
        }

        [Fact]
        public void UpdateStudentDetails_ShouldUpdateStudentRecord()
        {
            // Arrange
            var dbListOfStudenDetails = _fixture.Create<List<StudentDetailsModel>>();
            var updateStudenDetails = _fixture.Create<StudentDetailsModel>();
            dbListOfStudenDetails[0] = updateStudenDetails;

            var fileName = "Students";
            _serviceMock.Setup(x => x.Get(fileName)).Returns(dbListOfStudenDetails);
            _serviceMock.Setup(x => x.Update(fileName, dbListOfStudenDetails));
            

            // Act
            var result =  _sut.UpdateStudentDetails(updateStudenDetails);

            // Assert
            result.Should().BeTrue();
            Assert.Equal(updateStudenDetails, dbListOfStudenDetails[0]);

            // moqs way of verification. 
            _serviceMock.Verify(x => x.Update(fileName, dbListOfStudenDetails), Times.Once());
        }

        [Fact]
        public void AddStudentDetails_ShouldAddStudentRecord()
        {
            // Arrange
            var dbListOfStudenDetails = _fixture.Create<List<StudentDetailsModel>>();
            var addStudenDetails = _fixture.Create<StudentDetailsModel>();
            dbListOfStudenDetails.Add(addStudenDetails);

            var fileName = "Students";
            _serviceMock.Setup(x => x.Get(fileName)).Returns(dbListOfStudenDetails);
            _serviceMock.Setup(x => x.Add(fileName, addStudenDetails));


            // Act
            var result = _sut.AddStudentDetails(addStudenDetails);

            // Assert
            result.Should().BeTrue();
            Assert.Equal(addStudenDetails, dbListOfStudenDetails.Last());

            // moqs way of verification. 
            _serviceMock.Verify(x => x.Add(fileName, addStudenDetails), Times.Once());
        }
    }
}
