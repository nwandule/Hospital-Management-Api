
using AutoMapper;
using FluentAssertions;
using Hospital_Management_system.Api.Controllers;
using Hospital_Management_system.Models.Domain;
using Hospital_Management_system.Models.Dto;
using Hospital_Management_system.Services.DoctorRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Hospital_Management_system.Tests.Controller
{
    public class DoctorControllerTests
    {
        private readonly Mock<IDoctorService> _doctorServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<DoctorController>> _loggerMock;
        private readonly DoctorController _controller;

        public DoctorControllerTests()
        {
            _doctorServiceMock = new Mock<IDoctorService>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<DoctorController>>();
            _controller = new DoctorController(_doctorServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction_WhenDoctorIsCreatedSuccessfully()
        {
            var dto = new CreateDoctorDto
            {
                FullName = "Dr. Jane Doe",
                Email = "jane.doe@hospital.com",
                Specialization = "Cardiology",
                PhoneNumber = "+1234567890"
            };

            var domainModel = new Doctor
            {
                FullName = "Dr. Jane Doe",
                Email = "jane.doe@hospital.com",
                Specialization = "Cardiology",
                PhoneNumber = "+1234567890"
            };

            var serviceResult = new Doctor
            {
                Id = 1,
                FullName = "Dr. Jane Doe",
                Email = "jane.doe@hospital.com",
                Specialization = "Cardiology",
                PhoneNumber = "+1234567890",
                IsAvailable = true
            };

            var responseDto = new DoctorResponseDto
            {
                Id = 1,
                FullName = "Dr. Jane Doe",
                Email = "jane.doe@hospital.com",
                Specialization = "Cardiology",
                PhoneNumber = "+1234567890",
                IsAvailable = true
            };

            _doctorServiceMock
                .Setup(s => s.ExistsAsync(It.IsAny<Expression<Func<Doctor, bool>>>()))
                .ReturnsAsync(false);

            _mapperMock
                .Setup(m => m.Map<Doctor>(dto))
                .Returns(domainModel);

            _doctorServiceMock
                .Setup(s => s.AddAsync(domainModel))
                .ReturnsAsync(serviceResult);

            _mapperMock
                .Setup(m => m.Map<DoctorResponseDto>(serviceResult))
                .Returns(responseDto);

            var result = await _controller.Create(dto);

            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult.ActionName.Should().Be(nameof(DoctorController.GetById));
            createdAtActionResult.RouteValues["id"].Should().Be(1);
            createdAtActionResult.Value.Should().BeEquivalentTo(responseDto);
        }

        [Fact]
        public async Task Create_ShouldReturnConflict_WhenEmailAlreadyExists()
        {
            var dto = new CreateDoctorDto
            {
                FullName = "Dr. Jane Doe",
                Email = "jane.doe@hospital.com"
            };

            _doctorServiceMock
                .Setup(s => s.ExistsAsync(It.IsAny<Expression<Func<Doctor, bool>>>()))
                .ReturnsAsync(true);

            var result = await _controller.Create(dto);

            var conflictResult = result.Should().BeOfType<ConflictObjectResult>().Subject;
            conflictResult.Value.Should().BeEquivalentTo(new { message = "A doctor with this email address already exists within the system." });
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenDoctorExists()
        {
            int doctorId = 1;
            var domainModel = new Doctor
            {
                Id = doctorId,
                FullName = "Dr. Jane Doe",
                Email = "jane.doe@hospital.com",
                Specialization = "Cardiology",
                PhoneNumber = "+1234567890"
            };

            var responseDto = new DoctorResponseDto
            {
                Id = doctorId,
                FullName = "Dr. Jane Doe",
                Email = "jane.doe@hospital.com",
                Specialization = "Cardiology",
                PhoneNumber = "+1234567890",
                IsAvailable = true
            };

            _doctorServiceMock
                .Setup(s => s.GetByIdAsync(doctorId))
                .ReturnsAsync(domainModel);

            _mapperMock
                .Setup(m => m.Map<DoctorResponseDto>(domainModel))
                .Returns(responseDto);

            var result = await _controller.GetById(doctorId);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(responseDto);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenDoctorDoesNotExist()
        {
            int doctorId = 999;

            _doctorServiceMock
                .Setup(s => s.GetByIdAsync(doctorId))
                .ReturnsAsync((Doctor?)null);

            var result = await _controller.GetById(doctorId);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenDoctorsAreRetrieved()
        {
            var domainList = new List<Doctor>
            {
                new Doctor { Id = 1, FullName = "Dr. One", Email = "one@hospital.com", Specialization = "General" },
                new Doctor { Id = 2, FullName = "Dr. Two", Email = "two@hospital.com", Specialization = "Neuro" }
            };

            var responseList = new List<DoctorResponseDto>
            {
                new DoctorResponseDto { Id = 1, FullName = "Dr. One", Email = "one@hospital.com", Specialization = "General", IsAvailable = true },
                new DoctorResponseDto { Id = 2, FullName = "Dr. Two", Email = "two@hospital.com", Specialization = "Neuro", IsAvailable = true }
            };

            _doctorServiceMock
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(domainList);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<DoctorResponseDto>>(domainList))
                .Returns(responseList);

            var result = await _controller.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(responseList);
        }
    }
}

