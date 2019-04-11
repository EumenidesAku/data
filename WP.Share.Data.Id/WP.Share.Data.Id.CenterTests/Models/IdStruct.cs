using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Id.CenterTests.Models
{
    public class IdStruct
    {
        public long Sign { get; set; }

        public DateTime Time { get; set; }

        public long WorkNode { get; set; }
        public long Sequence { get; set; }
    }
}
