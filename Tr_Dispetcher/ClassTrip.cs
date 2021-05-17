using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tr_Dispetcher
{
	class ClassTrip
	{
		private ushort Number { get; set; }
		private string Station { get; set; }
		private TimeSpan Dept_time { get; set; }
		private TimeSpan Travel_time { get; set; }
		private int Tickets { get; set; }

		public ClassTrip(ushort number, string station, TimeSpan dept_time, TimeSpan travel_time, int tickets)
		{
			this.Number = number;
			this.Station = station;
			this.Dept_time = dept_time;
			this.Travel_time = travel_time;
			this.Tickets = tickets;
		}
	}
}
