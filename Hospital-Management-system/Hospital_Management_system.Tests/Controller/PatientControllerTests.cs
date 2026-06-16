using AutoMapper;
using FluentAssertions;
using Hospital_Management_system.Api.Controllers;
using Hospital_Management_system.Models.Domain;
using Hospital_Management_system.Models.Dto;
using Hospital_Management_system.Services.PatientRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Hospital_Management_system.Tests.Controllers
{
    public class PatientControllerTests
    {
        private readonly Mock<IPatientService> _patientServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<AuthController>> _loggerMock;
        private readonly PatientController _controller;

        public PatientControllerTests()
        {
            _patientServiceMock = new Mock<IPatientService>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<AuthController>>();
            _controller = new PatientController(_patientServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void Create_ShouldReturnCreatedAtAction_WhenPatientIsCreatedSuccessfully()
        {
            // Arrange
            var dto = new CreatePatient
            {
                FullName = "John Doe",
                Email = "john.doe@hospital.com"
            };

            var domainModel = new Patient
            {
                FullName = "John Doe",
                Email = "john.doe@hospital.com"
            };

            var serviceResult = new Patient
            {
                Id = 1,
                FullName = "John Doe",
                Email = "john.doe@hospital.com"
            };

            var responseDto = new PatientResponseDto
            {
                Id = 1,
                FullName = "John Doe",
                Email = "john.doe@hospital.com"
            };

            _mapperMock
                .Setup(m => m.Map<Patient>(dto))
                .Returns(domainModel);

            _patientServiceMock
                .Setup(s => s.Add(domainModel))
                .Returns(serviceResult);

            _mapperMock
                .Setup(m => m.Map<PatientResponseDto>(serviceResult))
                .Returns(responseDto);

            // Act
            var result = _controller.Create(dto);

            // Assert
            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult.ActionName.Should().Be(nameof(PatientController.GetById));
            createdAtActionResult.RouteValues["id"].Should().Be(1);
            createdAtActionResult.Value.Should().BeEquivalentTo(responseDto);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenPatientExists()
        {
            // Arrange
            int patientId = 1;
            var domainModel = new Patient
            {
                Id = patientId,
                FullName = "John Doe",
                Email = "john.doe@hospital.com"
            };

            var responseDto = new PatientResponseDto
            {
                Id = patientId,
                FullName = "John Doe",
                Email = "john.doe@hospital.com"
            };

            _patientServiceMock
                .Setup(s => s.GetById(patientId))
                .Returns(domainModel);

            _mapperMock
                .Setup(m => m.Map<PatientResponseDto>(domainModel))
                .Returns(responseDto);

            // Act
            var result = _controller.GetById(patientId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(responseDto);
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenPatientDoesNotExist()
        {
            // Arrange
            int patientId = 999;

            _patientServiceMock
                .Setup(s => s.GetById(patientId))
                .Returns((Patient)null);

            // Act
            var result = _controller.GetById(patientId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetAll_ShouldReturnOk_WhenPatientsAreRetrieved()
        {
            // Arrange
            var domainList = new List<Patient>
            {
                new Patient { Id = 1, FullName = "John Doe", Email = "john@hospital.com" },
                new Patient { Id = 2, FullName = "Jane Smith", Email = "jane@hospital.com" }
            };

            var responseList = new List<PatientResponseDto>
            {
                new PatientResponseDto { Id = 1, FullName = "John Doe", Email = "john@hospital.com" },
                new PatientResponseDto { Id = 2, FullName = "Jane Smith", Email = "jane@hospital.com" }
            };

            _patientServiceMock
                .Setup(s => s.GetAll())
                .Returns(domainList);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<PatientResponseDto>>(domainList))
                .Returns(responseList);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(responseList);
        }
    }
}