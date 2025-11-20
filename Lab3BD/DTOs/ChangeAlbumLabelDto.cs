namespace Lab3BD.DTOs
{
    public class ChangeAlbumLabelDto
    {
        public int AlbumId { get; set; }
        public string AlbumTitle { get; set; }
        public int ArtistId { get; set; }
        public int NewLabelId { get; set; }
    }
}
