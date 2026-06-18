
using AutoMapper;
using Hospital_Management_system.Models.Domain;
using Hospital_Management_system.Models.Dto;
using Hospital_Management_system.Services.DoctorRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_system.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorService doctorService, IMapper mapper, ILogger<DoctorController> logger)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DoctorResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            _logger.LogInformation("Creating a new doctor record for: {Email}", dto.Email);

            bool emailExists = await _doctorService.ExistsAsync(d => d.Email == dto.Email);
            if (emailExists)
            {
                _logger.LogWarning("Creation rejected. A doctor record with email {Email} already exists.", dto.Email);
                return Conflict(new { message = "A doctor with this email address already exists within the system." });
            }

            var newDoctor = _mapper.Map<Doctor>(dto);
            var result = await _doctorService.AddAsync(newDoctor);
            var responseDto = _mapper.Map<DoctorResponseDto>(result);

            _logger.LogInformation("Doctor profile successfully created with ID: {DoctorId}", responseDto.Id);
            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DoctorResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching doctor record for ID: {DoctorId}", id);

            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
            {
                _logger.LogWarning("Doctor record with ID: {DoctorId} was not found.", id);
                return NotFound();
            }

            return Ok(_mapper.Map<DoctorResponseDto>(doctor));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DoctorResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Retrieving all registered doctors.");

            var doctors = await _doctorService.GetAllAsync();

            _logger.LogInformation("Successfully retrieved {Count} doctor records.", doctors.Count());

            return Ok(_mapper.Map<IEnumerable<DoctorResponseDto>>(doctors));
        }
    }
}
