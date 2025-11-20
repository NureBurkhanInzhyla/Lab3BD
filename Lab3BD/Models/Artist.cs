using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3BD.Models
{
    [Table("Artist", Schema = "dbo")]
    public class Artist
    {
        [Column("artist_id")]
        public int ArtistId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("country")]
        public string Country { get; set; }

        public List<Album> Albums { get; set; }
    }
}
