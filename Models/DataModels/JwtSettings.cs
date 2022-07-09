namespace Holamundo.Models.DataModels
{
    public class JwtSettings
    {
        public bool ValidateIssuerSingKey { get; set; }
        public string? IssuerSigningKey { get; set; } = string.Empty;

        public bool ValidateIssuer { get; set; }
        public string? VAlidIssuer { get; set; }

        public bool ValididateAudience { get; set; }
        public string? ValidAudience { get; set; }

        public bool RequireValidationTime { get; set; }
        public bool ValidateLifetime { get; set; }

    }
}
