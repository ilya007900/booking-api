using Booking.API.RequestModels;
using Booking.Application.Hotels.Commands.BookHotelCommand;
using Booking.Application.Hotels.Queries.SearchHotelsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    public class HotelsController : BaseController
    {
        public HotelsController(IMediator mediator) : base(mediator) { }

        [HttpGet("search")]
        public Task<IReadOnlyList<SearchHotelsQueryResult>> Search(
            [FromQuery] int cityId,
            [FromQuery] DateTime start,
            [FromQuery] DateTime end,
            CancellationToken cancellationToken = default)
        {
            return Mediator.Send(new SearchHotelsQuery(cityId, start, end), cancellationToken);
        }

        [HttpPost("{hotelId}/occupancy")]
        public Task CreateOccupancy([FromRoute] int hotelId, [FromBody] CreateOccupancyRequest request)
        {
            var command = new BookHotelCommand(hotelId, request.Start, request.End);
            return Mediator.Send(command);
        }
    }
}
