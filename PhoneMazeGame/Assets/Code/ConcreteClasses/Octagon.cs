using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Utilities;

namespace Assets.Code.ConcreteClasses
{
	[Serializable]
	public class Octagon
	{
		[System.Xml.Serialization.XmlElement("Type")]
		public Enumerations.OctagonType Type { get; set; }


		[System.Xml.Serialization.XmlElement("Action")]
		public Enumerations.OctagonAction Action { get; set; }


		[System.Xml.Serialization.XmlElement("Color")]
		public Enumerations.OctagonColor Color { get; set; }


		[System.Xml.Serialization.XmlElement("X")]
		public int XCoordinate { get; set; }


		[System.Xml.Serialization.XmlElement("Y")]
		public int YCoordinate { get; set; }

		[System.Xml.Serialization.XmlElement("Rotation")]
		public int Rotation { get; set; }
	}
}
