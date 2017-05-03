using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Utilities
{
	public class Enumerations
	{
		public enum OctagonType { Empty, Normal, Endpoint }
		public enum OctagonAction { Turn, Swap }
		public enum OctagonColor { Default, Green, Red, Blue, Yellow }
		public enum Wall { N, NE, NW, E, W, SW, SE, S}
	}
}
