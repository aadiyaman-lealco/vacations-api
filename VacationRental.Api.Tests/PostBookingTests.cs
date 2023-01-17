using System;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Api.Models;
using VacationRental.Common.Models;
using Xunit;

namespace VacationRental.Api.Tests
{
    [Collection("Integration")]
    public class PostBookingTests
    {
        private readonly HttpClient _client;

        public PostBookingTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Theory]
        [InlineData(4, 2, 3)]
        [InlineData(1, 1, 4)]
        [InlineData(2, 2, 2)]
        public async Task GivenCompleteRequest_WhenPostBooking_ThenAGetReturnsTheCreatedBooking(int units, int prepDays, int nights)
        {
            var postRentalRequest = new RentalBindingModel
            {
                Units = units,
                PreparationTimeInDays = prepDays
            };

            ResourceIdViewModel postRentalResult;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                postRentalResult = await postRentalResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }

            var postBookingRequest = new BookingBindingModel
            {
                RentalId = postRentalResult.Id,
                Nights = nights,
                Start = new DateTime(2001, 01, 01)
            };

            ResourceIdViewModel postBookingResult;
            using (var postBookingResponse = await _client.PostAsJsonAsync($"/api/v1/bookings", postBookingRequest))
            {
                Assert.True(postBookingResponse.IsSuccessStatusCode);
                postBookingResult = await postBookingResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }

            using (var getBookingResponse = await _client.GetAsync($"/api/v1/bookings/{postBookingResult.Id}"))
            {
                Assert.True(getBookingResponse.IsSuccessStatusCode);

                var getBookingResult = await getBookingResponse.Content.ReadAsAsync<BookingViewModel>();
                Assert.Equal(postBookingRequest.RentalId, getBookingResult.RentalId);
                Assert.Equal(postBookingRequest.Nights, getBookingResult.Nights);
                Assert.Equal(postBookingRequest.Start, getBookingResult.Start);
            }
        }

        [Theory]
        [InlineData(1, 1, 3, 1)]
        [InlineData(1, 1, 4, 2)]
        [InlineData(2, 1, 5, 3)]
        [InlineData(5, 2, 10, 15)]
        public async Task GivenCompleteRequest_WhenPostBooking_ThenAPostReturnsErrorWhenThereIsOverbooking(int units, int prepDays, int nights1, int nights2)
        {
            var postRentalRequest = new RentalBindingModel
            {
                Units = units,
                PreparationTimeInDays = prepDays
            };

            ResourceIdViewModel postRentalResult;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                postRentalResult = await postRentalResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }

            // Over booking for each unit
            for (int i = 0; i < units; i++)
            {
                var postBooking1Request = new BookingBindingModel
                {
                    RentalId = postRentalResult.Id,
                    Nights = nights1,
                    Start = new DateTime(2002, 01, 01)
                };

                using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
                {
                    Assert.True(postBooking1Response.IsSuccessStatusCode);
                }
            }

            var postBooking2Request = new BookingBindingModel
            {
                RentalId = postRentalResult.Id,
                Nights = nights2,
                Start = new DateTime(2002, 01, 02)
            };

            await Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                using var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request);
            });
        }
    }
}
