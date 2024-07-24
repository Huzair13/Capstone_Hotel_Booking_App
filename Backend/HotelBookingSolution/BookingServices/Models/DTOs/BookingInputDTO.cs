﻿namespace BookingServices.Models.DTOs
{
    public class bookingInputDTO
    {
        public int HotelId { get; set; }
        public int? RoomNumber {  get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
    }
}