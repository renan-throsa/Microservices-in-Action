namespace Invoice.Service
{
    public class LoyaltyProgramClient
    {
        private readonly HttpClient httpClient;

        public LoyaltyProgramClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<HttpResponseMessage> QueryUser(int userId)
        {
            return await httpClient.GetAsync($"/users/{userId}");
        }
    }
}
