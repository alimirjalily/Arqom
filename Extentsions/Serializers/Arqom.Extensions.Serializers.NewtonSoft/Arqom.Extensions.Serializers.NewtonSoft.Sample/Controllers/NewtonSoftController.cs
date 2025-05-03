using Microsoft.AspNetCore.Mvc;
using Arqom.Extensions.Serializers.NewtonSoft.Sample.Models;
using Arqom.Extensions.Serializers.Abstractions;

namespace Arqom.Extensions.Serializers.NewtonSoft.Sample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewtonSoftController : ControllerBase
{
    private readonly IJsonSerializer _jsonSerializer;

    public NewtonSoftController(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    [HttpGet("Serilize")]
    public IActionResult Serilize([FromQuery] Person person) => Ok(_jsonSerializer.Serialize(person));

    [HttpGet("Deserialize")]
    public IActionResult Deserialize([FromQuery] string input) => Ok(_jsonSerializer.Deserialize<Person>(input));
}
