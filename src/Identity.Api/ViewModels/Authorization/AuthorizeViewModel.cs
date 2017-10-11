namespace Identity.Api.ViewModels.Authorization
{
    public class AuthorizeViewModel
    {
        public string ApplicationName { get; set; }
        public string RequestId { get; set; }
        public string Scope { get; set; }
    }
}