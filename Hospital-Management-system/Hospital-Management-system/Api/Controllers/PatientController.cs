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
        public PatientController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status201Created, Type = typeof(PatientResponseDto))]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] CreatePatient dto)
        {
            var newPatient = _mapper.Map<Patient>(dto);
            var result = _patientService.Add(newPatient);
            var responseDto = _mapper.Map<PatientResponseDto>(result);

            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, Type = typeof(PatientResponseDto))]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            var patient = _patientService.GetById(id);
            if (patient == null) return NotFound();

            return Ok(_mapper.Map<PatientResponseDto>(patient));
        }

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, Type = typeof(IEnumerable<PatientResponseDto>))]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var patients = _patientService.GetAll();
            return Ok(_mapper.Map<IEnumerable<PatientResponseDto>>(patients));
        }
    }
}