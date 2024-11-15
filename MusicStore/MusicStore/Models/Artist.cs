namespace MusicStore.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }
        public string Name { get; set; }

        public ICollection<Album>? Albums { get; set; }
    }
}
