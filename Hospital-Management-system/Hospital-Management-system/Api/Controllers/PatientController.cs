using AutoMapper;
using Hospital_Management_system.Models.Domain;
using Hospital_Management_system.Models.Dto;
using Hospital_Management_system.Services.PatientRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_system.Api.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientController> _logger;

        public PatientController(IPatientService patientService, IMapper mapper, ILogger<PatientController> logger)
        {
            _patientService = patientService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreatePatient dto)
        {
            _logger.LogInformation("Creating a new patient record for: {Email}", dto.Email);

            bool emailExists = await _patientService.ExistsAsync(p => p.Email == dto.Email);
            if (emailExists)
            {
                _logger.LogWarning("Creation rejected. A patient record with email {Email} already exists.", dto.Email);
                return Conflict(new { message = "A patient with this email address already exists within the system." });
            }

            var newPatient = _mapper.Map<Patient>(dto);
            var result = await _patientService.AddAsync(newPatient);
            var responseDto = _mapper.Map<PatientResponseDto>(result);

            _logger.LogInformation("Patient record successfully created with ID: {PatientId}", responseDto.Id);
            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching patient record for ID: {PatientId}", id);

            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
            {
                _logger.LogWarning("Patient record with ID: {PatientId} was not found.", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved patient record for ID: {PatientId}", id);
            return Ok(_mapper.Map<PatientResponseDto>(patient));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PatientResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Retrieving all patient records.");

            var patients = await _patientService.GetAllAsync();

            _logger.LogInformation("Successfully retrieved {Count} patient records.", patients.Count());

            return Ok(_mapper.Map<IEnumerable<PatientResponseDto>>(patients));
        }
    }
}