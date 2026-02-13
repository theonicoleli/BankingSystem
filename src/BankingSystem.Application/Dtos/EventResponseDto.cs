using System.Text.Json.Serialization;

namespace BankingSystem.Application.Dtos;

public class EventResponseDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AccountDto? Origin { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AccountDto? Destination { get; set; }
}