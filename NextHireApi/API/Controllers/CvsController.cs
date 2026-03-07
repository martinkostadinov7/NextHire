using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Shared.DTOs.Cvs;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CvsController(ICvService cvService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CvReadDto>> CreateCv(CvCreateDto request)
        {
            CvReadDto result = await cvService.CreateCvAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CvReadDto>> GetCvById(int id)
        {
            CvReadDto result = await cvService.GetCvById(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<CvReadDto>>> GetAll()
        {
            List<CvReadDto> result = await cvService.GetAllCvs();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CvReadDto>> DeleteCv(int id)
        {
            await cvService.DeleteCvAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CvReadDto>> UpdateCv(int id, [FromBody] CvUpdateDto request)
        {
            CvReadDto result = await cvService.UpdateCvAsync(id, request);
            return Ok(result);
        }
    }
}
