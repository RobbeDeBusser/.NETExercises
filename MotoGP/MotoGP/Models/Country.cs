using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace MotoGP.Models
{
    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountryID { get; set; }
        public string Name { get; set; }

        // Navigation properties
        public ICollection<Rider>? Riders { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
