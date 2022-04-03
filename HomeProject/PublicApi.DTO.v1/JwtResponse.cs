namespace PublicApi.DTO.v1
{
    public class JwtResponse
    {
        public string Token { get; set; } = default!;
        public string Username { get; set; } = default!;
    }
}