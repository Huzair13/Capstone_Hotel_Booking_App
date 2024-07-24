﻿using HotelBooking.Models;
using HotelServices.Exceptions;
using HotelServices.Interfaces;
using HotelServices.Models;
using HotelServices.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelServices.Controllers
{
    [Route("api/")]
    [ApiController]
    [EnableCors("MyCors")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelServices _hotelServices;
        private readonly IAzureBlobService _blobService;
        private readonly ILogger<HotelController> _logger;

        public HotelController(IHotelServices hotelServices, ILogger<HotelController> logger, IAzureBlobService blobService)
        {
            _hotelServices = hotelServices;
            _logger = logger;
            _blobService = blobService;
        }

        // Add Hotel
        [HttpPost("AddHotel")]
        [ProducesResponseType(typeof(HotelReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelReturnDTO>> AddHotel([FromForm] HotelDTO hotelDTO, IFormFileCollection files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.Name);

                    var imageUrls = new List<string>();
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            using (var stream = file.OpenReadStream())
                            {
                                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                                var imageUrl = await _blobService.UploadAsync(stream, fileName);
                                imageUrls.Add(imageUrl);
                            }
                        }
                    }

                    hotelDTO.HotelImages = imageUrls;

                    var result = await _hotelServices.AddHotelAsync(hotelDTO);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while adding the hotel.");
                    return StatusCode(500, new ErrorModel(500, "An error occurred while processing your request."));
                }
            }
            return BadRequest("All Details are not provided");
        }

        // Get All Hotels
        [HttpGet("GetAllHotels")]
        [ProducesResponseType(typeof(List<HotelReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<HotelReturnDTO>>> GetAllHotels()
        {
            try
            {
                var result = await _hotelServices.GetAllHotels();
                return Ok(result);
            }
            catch(NoSuchHotelException ex)
            {
                _logger.LogError(ex, "Hotels Not found");
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all hotels.");
                return StatusCode(500, new ErrorModel(500, "An error occurred while processing your request."));
            }
        }

        // Get All Hotels
        [HttpGet("GetAvailableHotelsRooms")]
        [ProducesResponseType(typeof(List<RoomDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<RoomDTO>>> GetAvailableHotelsRooms(DateTime checkInDate, DateTime checkOutDate, int numberOfGuests)
        {
            try
            {
                var result = await _hotelServices.GetAvailableRoomsAsync(checkInDate, checkOutDate, numberOfGuests);
                return Ok(result);
            }
            catch (NoSuchHotelException ex)
            {
                _logger.LogError(ex, "Rooms Not found");
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all Available rooms.");
                return StatusCode(500, new ErrorModel(500, $"An error occurred while processing your request. + {ex.Message}"));
            }
        }

        // Get Hotel by ID
        [HttpGet("GetHotelByID/{hotelId}")]
        [ProducesResponseType(typeof(HotelReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelReturnDTO>> GetHotelByID(int hotelId)
        {
            try
            {
                var result = await _hotelServices.GetHotelByID(hotelId);
                return Ok(result);
            }
            catch (NoSuchHotelException ex)
            {
                _logger.LogError(ex, $"Hotels Not found for the hotel ID : {hotelId}");
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the hotel by ID.");
                return StatusCode(500, new ErrorModel(500, "An error occurred while processing your request."));
            }
        }

        // Get Hotel by Name
        [HttpGet("GetHotelByName/{hotelName}")]
        [ProducesResponseType(typeof(HotelReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelReturnDTO>> GetHotelByName(string hotelName)
        {
            try
            {
                var result = await _hotelServices.GetHotelByName(hotelName);
                return Ok(result);
            }
            catch (NoSuchHotelException ex)
            {
                _logger.LogError(ex, $"Hotels Not found for the hotel Name : {hotelName}");
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the hotel by name.");
                return StatusCode(500, new ErrorModel(500, "An error occurred while processing your request."));
            }
        }

        // Add Room to Hotel
        [HttpPost("AddRoomToHotel/{hotelId}")]
        [ProducesResponseType(typeof(HotelReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelReturnDTO>> AddRoomToHotel(int hotelId, [FromBody] RoomDTO roomDTO)
        {
            if (roomDTO == null)
            {
                return BadRequest("Room details are not provided.");
            }

            try
            {
                var result = await _hotelServices.AddRoomToHotelAsync(hotelId, roomDTO);
                return Ok(result);
            }
            catch (NoSuchHotelException ex)
            {
                _logger.LogError(ex, $"Hotel not found for ID: {hotelId}");
                return NotFound(new ErrorModel(404, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the room to the hotel.");
                return StatusCode(500, new ErrorModel(500, $"An error occurred while processing your request {ex.Message}"));
            }
        }

    }
}