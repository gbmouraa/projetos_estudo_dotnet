namespace ProductClientHub.Communication.Requests.Products
{
    public class RequestProductJson
    {
        public string Name { get; set; } = String.Empty;
        public string Brand { get; set; } = String.Empty;
        public decimal Price { get; set; }
    }
}
