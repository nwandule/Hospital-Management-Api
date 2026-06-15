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
        private readonly ILogger<AuthController> _logger;
        public PatientController(IPatientService patientService, IMapper mapper, ILogger<AuthController> logger)
        {
            _patientService = patientService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status201Created, Type = typeof(PatientResponseDto))]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] CreatePatient dto)
        {
            _logger.LogInformation("Creating a new patient record.");
            var newPatient = _mapper.Map<Patient>(dto);
            var result = _patientService.Add(newPatient);
            var responseDto = _mapper.Map<PatientResponseDto>(result);

            _logger.LogInformation("Patient record successfully created with ID: {PatientId}", responseDto.Id);
            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, Type = typeof(PatientResponseDto))]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("Fetching patient record for ID: {PatientId}", id);

            var patient = _patientService.GetById(id);
            if (patient == null)
            {
                _logger.LogWarning("Patient record with ID: {PatientId} was not found.", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved patient record for ID: {PatientId}", id);
            return Ok(_mapper.Map<PatientResponseDto>(patient));
        }

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, Type = typeof(IEnumerable<PatientResponseDto>))]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Retrieving all patient records.");

            var patients = _patientService.GetAll();

            _logger.LogInformation("Successfully retrieved {Count} patient records.", patients.Count());

            return Ok(_mapper.Map<IEnumerable<PatientResponseDto>>(patients));
        }
    }
}