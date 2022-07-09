namespace Holamundo.Models.DataModels
{
    public class UserTokens
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string token { get; set; }
        public TimeSpan validity { get; set; }
        public string RefreshToken { get; set; }
        public string EmailId { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
