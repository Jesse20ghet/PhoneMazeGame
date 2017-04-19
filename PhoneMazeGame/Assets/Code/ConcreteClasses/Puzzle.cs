using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Code.ConcreteClasses
{
	[Serializable]
	[System.Xml.Serialization.XmlRoot("Puzzle")]
	public class Puzzle
	{
		[XmlArray("OctagonCollection")]
		[XmlArrayItem("Octagon", typeof(Octagon))]
		public Octagon[] Octagons { get; set; }

		[System.Xml.Serialization.XmlElement("PuzzleName")]
		public string PuzzleName { get; set; }
	}
}
