using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace openbankapi.Models
{
    public class TransactionFilter
    {
        public DateTime? ToBookingDate { get; set; }
        public DateTime? FromBookingDate { get; set; }

        public long ToTicks(DateTime? dateTime) {
                if (dateTime == null)
                    return 0;
                return dateTime.Value.Ticks;
        }
    }
}
