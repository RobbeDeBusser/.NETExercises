using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotoGP.Models.ViewModels
{
    public class SelectTicketViewModel
    {
        public List<Ticket> TicketList;
        public SelectList Races { get; set; }
        public int raceID { get; set; }
    }
}
