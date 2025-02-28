using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Piles.Data
{
    public class PileEntity
    {
        [Key]
        public int Id { get; set; }
        public string Justification { get; set; }

        public virtual ICollection<RuminationEntity> Ruminations { get; }
    }
}
