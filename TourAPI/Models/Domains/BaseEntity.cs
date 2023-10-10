using System.ComponentModel.DataAnnotations;

namespace TourAPI.Models.Domains
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString("N");
            CreateTimes = DateTime.Now;
            LastUpdateTimes = DateTime.Now;
        }
        [Key]
        public string Id { get; set; }
        public DateTime CreateTimes { get; set; }
        public DateTime LastUpdateTimes { get; set; }
    }
}
