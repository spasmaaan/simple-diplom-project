namespace SimpleDiplomBackend.Domain.Entities
{
    public class BookingDish
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int DishId { get; set; }
        public int Count { get; set; }
        // public Booking Booking { get; set; }
        // public Dish Dish { get; set; }
    }
}
