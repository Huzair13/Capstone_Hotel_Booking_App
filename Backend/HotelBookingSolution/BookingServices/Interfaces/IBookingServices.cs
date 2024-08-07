﻿using BookingServices.Models.DTOs;

namespace BookingServices.Interfaces
{
    public interface IBookingServices
    {
        public Task<BookingReturnDTO> AddBookingAsync(BookingDTO bookingDTO,int currUserID);
        public Task<List<BookingReturnDTO>> GetAllBookings();
        public Task<BookingByIdReturnDTO> GetBookingByID(int bookingId);
        public Task<List<BookingReturnDTO>> GetBookingByUser(int userId);
        public Task<List<int>> GetBookedRoomNumbersAsync(DateTime checkInDate, DateTime checkOutDate);
        public Task<CancelReturnDTO> CancelBookingByBookingId(int BookingID);
        public Task RevertBookingAsync(int bookingId, int currUserId);
        public Task<CalculateAmountDTO> CalculateTotalAmountAsync(BestCombinationDTO bestCombinationDTO,int userId);
        public Task<int> GetCancellationCount(int userId);
    }
}
