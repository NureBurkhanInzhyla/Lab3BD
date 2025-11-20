using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3BD.Models
{
    [Table("Genre", Schema = "dbo")]
    public class Genre
    {
        [Column("genre_id")]
        public int GenreId { get; set; }

        [Column("name")]
        public string Name { get; set; }

    }
}
