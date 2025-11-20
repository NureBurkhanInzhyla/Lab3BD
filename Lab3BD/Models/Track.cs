using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3BD.Models
{
    [Table("Track", Schema = "dbo")]
    public class Track
    {
        [Column("track_id")]
        public int TrackId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("duration")]
        public int Duration { get; set; }

        [Column("album_id")]
        public int AlbumId { get; set; }
        [ForeignKey("AlbumId")]
        public Album Album { get; set; }
    }

}
