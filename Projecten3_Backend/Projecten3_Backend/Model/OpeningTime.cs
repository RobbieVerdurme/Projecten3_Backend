using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model
{
    public class OpeningTime
    {
        #region Properties

        public int OpeningTimeId { get; set; }

        public DayOfWeek Day { get; set; }

        public string OpeningHourMorning { get; set; }

        public string ClosingHourMorning { get; set; }

        public string OpeningHourAfternoon { get; set; }

        public string ClosingHourAfternoon { get; set; }


        #endregion

        #region Constructors

        public OpeningTime()
        {
        }

        public OpeningTime(DayOfWeek day, string openingHourMorning, string closingHourMorning, string openingHourAfternoon, string closingHourAfternoon)
        {
            Day = day;
            OpeningHourMorning = openingHourMorning;
            ClosingHourMorning = closingHourMorning;
            OpeningHourAfternoon = openingHourAfternoon;
            ClosingHourAfternoon = closingHourAfternoon;
        }


        #endregion
    }
}
