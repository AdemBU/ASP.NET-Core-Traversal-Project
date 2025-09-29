namespace Traversal.CQRS.Results.DestinationResults
{
    public class GetAllDestinationQueryResult
    {
        // Result klasörü => bizim entity imizin karşılık gelen prop'larını tutuyor
        // Queries klasörü => bizim parametrelerimizi tutuyor 
        public int id { get; set; }
        public string city { get; set; }
        public string daynight { get; set; }
        public double price { get; set; }
        public int capacity { get; set; }
    }
}
