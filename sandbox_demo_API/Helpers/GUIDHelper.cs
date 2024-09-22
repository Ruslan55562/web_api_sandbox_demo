using System;

namespace web_api_sandbox_demo_API.Helpers
{
    public class GUIDHelper
    {
        public string GetRandomGUID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
