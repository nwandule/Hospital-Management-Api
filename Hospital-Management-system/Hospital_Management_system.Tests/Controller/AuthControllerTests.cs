/*=============================================================================
 * Author:       Vikash
 * Description:  Unit tests for AuthController.
 * Validates endpoint routing, service interaction, and response status codes.
 * Created Date: June 2026
 *=============================================================================*/

using FluentAssertions;
using Hospital_Management_system.Api.Controllers;
using Hospital_Management_system.Models.Dto;
using Hospital_Management_system.Services.AuthRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Hospital_Management_system.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<ILogger<AuthController>> _loggerMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _loggerMock = new Mock<ILogger<AuthController>>();
            _controller = new AuthController(_authServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new LoginRequestDto { Email = "test@hospital.com", Password = "Password123" };
            var expectedResponse = new LoginResponseDto { Token = "mock-token", Email = "test@hospital.com" };

            _authServiceMock
                .Setup(s => s.LoginAsync(loginRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task RegisterPatient_ShouldReturnOk_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var registerDto = new RegisterPatientDto { Email = "new@hospital.com", Password = "Pass" };

            _authServiceMock
                .Setup(s => s.RegisterPatientAsync(registerDto))
                .ReturnsAsync("User registered successfully");

            // Act
            var result = await _controller.RegisterPatient(registerDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}