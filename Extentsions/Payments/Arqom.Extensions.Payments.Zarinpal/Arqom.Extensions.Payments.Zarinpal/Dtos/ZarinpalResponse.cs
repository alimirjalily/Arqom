using System.Text.Json.Serialization;

namespace Arqom.Extensions.Payments.Zarinpal.Dtos;

public class ZarinpalResponse
{
    public ZarinpalData? Data { get; set; }
}

public class ZarinpalData
{
    public int Code { get; set; }
    public string Authority { get; set; } = string.Empty;

    [JsonPropertyName("ref_id")]
    public long RefID { get; set; }
    public string? Message { get; set; }
}
