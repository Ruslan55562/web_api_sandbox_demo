namespace sandbox_demo_API.Helpers
{
    public class ApiResponseWrapper<T>
    {
        public T Data { get; set; }
        public bool IsSuccessful { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}
