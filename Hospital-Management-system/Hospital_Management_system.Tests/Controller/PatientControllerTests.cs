using AutoMapper;
using FluentAssertions;
using Hospital_Management_system.Api.Controllers;
using Hospital_Management_system.Models.Domain;
using Hospital_Management_system.Models.Dto;
using Hospital_Management_system.Services.PatientRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Hospital_Management_system.Tests.Controllers
{
    public class PatientControllerTests
    {
        private readonly Mock<IPatientService> _patientServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<PatientController>> _loggerMock;
        private readonly PatientController _controller;

        public PatientControllerTests()
        {
            _patientServiceMock = new Mock<IPatientService>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<PatientController>>();
            _controller = new PatientController(_patientServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction_WhenPatientIsCreatedSuccessfully()
        {
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

            _patientServiceMock
                .Setup(s => s.ExistsAsync(It.IsAny<Expression<Func<Patient, bool>>>()))
                .ReturnsAsync(false);

            _mapperMock
                .Setup(m => m.Map<Patient>(dto))
                .Returns(domainModel);

            _patientServiceMock
                .Setup(s => s.AddAsync(domainModel))
                .ReturnsAsync(serviceResult);

            _mapperMock
                .Setup(m => m.Map<PatientResponseDto>(serviceResult))
                .Returns(responseDto);

            var result = await _controller.Create(dto);

            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult.ActionName.Should().Be(nameof(PatientController.GetById));
            createdAtActionResult.RouteValues["id"].Should().Be(1);
            createdAtActionResult.Value.Should().BeEquivalentTo(responseDto);
        }

        [Fact]
        public async Task Create_ShouldReturnConflict_WhenEmailAlreadyExists()
        {
            var dto = new CreatePatient
            {
                FullName = "John Doe",
                Email = "john.doe@hospital.com"
            };

            _patientServiceMock
                .Setup(s => s.ExistsAsync(It.IsAny<Expression<Func<Patient, bool>>>()))
                .ReturnsAsync(true);

            var result = await _controller.Create(dto);

            var conflictResult = result.Should().BeOfType<ConflictObjectResult>().Subject;
            conflictResult.Value.Should().BeEquivalentTo(new { message = "A patient with this email address already exists within the system." });
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenPatientExists()
        {
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
                .Setup(s => s.GetByIdAsync(patientId))
                .ReturnsAsync(domainModel);

            _mapperMock
                .Setup(m => m.Map<PatientResponseDto>(domainModel))
                .Returns(responseDto);

            var result = await _controller.GetById(patientId);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(responseDto);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenPatientDoesNotExist()
        {
            int patientId = 999;

            _patientServiceMock
                .Setup(s => s.GetByIdAsync(patientId))
                .ReturnsAsync((Patient?)null);

            var result = await _controller.GetById(patientId);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenPatientsAreRetrieved()
        {
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
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(domainList);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<PatientResponseDto>>(domainList))
                .Returns(responseList);

            var result = await _controller.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(responseList);
        }
    }
}