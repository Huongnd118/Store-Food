namespace Store_Food.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public string UserId { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
    public class OrderDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public int FoodId { get; set; }

        public string? FoodName { get; set; }
        public string? FoodDescription { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }


        public decimal? FoodPrice { get; set; }
        public decimal? FoodDiscount { get; set; }
        public string? FoodPhoto { get; set; }

        public int SizeId { get; set; }
        public Size? Size { get; set; }


        public int ColorId { get; set; }
        public Color? Color { get; set; }
        public bool IsTrandy { get; set; }
        public bool IsArriver { get; set; }
    }
}
