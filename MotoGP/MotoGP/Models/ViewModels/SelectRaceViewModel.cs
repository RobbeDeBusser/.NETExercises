using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotoGP.Models
{
    public class SelectRaceViewModel
    {
        public int SelectedRaceID { get; set; }
        public List<SelectListItem> Races { get; set; }
        public Race SelectedRace { get; set; }
    }
}
