using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Domain.Entity
{
    public class BaseEntity
    {
        [Column(Order = 0), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public long? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int RecVersion { get; set; } = 0;
        public long? DbTableId { get; set; }
    }
}