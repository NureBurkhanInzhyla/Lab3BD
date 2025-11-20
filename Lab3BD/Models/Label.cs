using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3BD.Models
{
    [Table("Label")]
    public class Label
    {
        [Column("label_id")]
        public int LabelId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("foundation_year")]
        public int FoundationYear { get; set; }

    }
}
