namespace OrderManagementSystem.Dto
{
    public class OrderQueueDto
    {
        public int QueueId { get; set; }
        public int OrderId { get; set; }
        public DateTime QueueDate { get; set; }
        public string ProcessingStatus { get; set; }
    }
}
