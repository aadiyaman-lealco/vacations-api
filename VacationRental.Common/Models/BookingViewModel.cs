namespace VacationRental.Common.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }

        public int PreparationTimeInDays { get; set; }
        public int Units { get; set; }
    }
}
