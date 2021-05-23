using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tr_Dispetcher
{
	class ClassTrip
	{
		public ushort Number { get; private set; }
		public string Station { get; private set; }
		public TimeSpan Dept_time { get; private set; }
		public TimeSpan Travel_time { get; private set; }
		public int Tickets { get; private set; }

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
