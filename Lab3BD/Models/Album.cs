using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3BD.Models
{
    [Table("Album", Schema = "dbo")]
    public class Album
    {
        [Column("album_id")]
        public int AlbumId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("release_year")]
        public int ReleaseYear { get; set; }

        [Column("artist_id")]
        public int ArtistId { get; set; }  
        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }

        [Column("label_id")]
        public int LabelId { get; set; }
        [ForeignKey("LabelId")]
        public Label Label { get; set; }
        public List<Track> Tracks { get; set; }
    }

}
