using ProductClientHub.Communication.Responses.Products;

namespace ProductClientHub.Communication.Responses.Clients
{
    public class ResponseClientJson
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<ResponseShortProductJson> Products { get; set; } = [];
    }
}
