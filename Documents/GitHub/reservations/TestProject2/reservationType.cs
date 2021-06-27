using Moq;
using reservation_system.Controllers;
using reservation_system.Models;
using reservation_system.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject2
{
    public class reservationType
    {
        public Mock<IReservationTypeService> mock = new Mock<IReservationTypeService>();


        [Fact]
        public async void GetReservationTypeById()
        {
            mock.Setup(p => p.GetReservationTypebyId(1)).ReturnsAsync("JK");
            ReservationsTypeController resv = new ReservationsTypeController(mock.Object);
            string result = await resv.GetReservationTypeById(1);
            Assert.Equal("JK", result);
        }

        [Fact]
        public async void GetReservationTypeDetails()
        {
            var reservationDTO = new ReservationType()
            {
                id = 1,
               type = "JK",
               nbrPlace = 10
            };
            mock.Setup(p => p.GetReservationTypeDetails(1)).ReturnsAsync(reservationDTO);
            ReservationsTypeController resv = new ReservationsTypeController(mock.Object);
            var result = await resv.GetReservationTypeDetails(1);
            Assert.True(reservationDTO.Equals(result));
        }
    }
}
