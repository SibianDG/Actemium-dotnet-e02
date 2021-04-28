using System.ComponentModel.DataAnnotations;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumTicketActemiumUser
    {
        public int TicketId { get; set; }
        public ActemiumTicket Ticket { get; set; }
        public int UserId { get; set; }
        public UserModel Technician { get; set; }

    }
}