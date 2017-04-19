using Assets.Code.ConcreteClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Code
{
	public class TextPuzzleParser
	{
		/// <summary>
		/// Creates a list of puzzles in specified directory
		/// </summary>
		/// <returns>Puzzle from XML file, throws error if unparsable</returns>
		public List<Puzzle> ParsePuzzles(string directory)
		{
			var puzzles = Resources.LoadAll(directory);

			var puzzleList = new List<Puzzle>();
			foreach (var puzzle in puzzles)
			{ 
				var parsedPuzzle = ParsePuzzle(puzzle.ToString());
				puzzleList.Add(parsedPuzzle);
			}

			return puzzleList;
		}

		public Puzzle ParsePuzzle(string puzzleXML)
		{
			//XmlDocument doc = new XmlDocument();
			//doc.Load(m_filePath);
			//string xmlDocumentText = doc.InnerXml;

			XmlSerializer serializer = new XmlSerializer(typeof(Puzzle));
			var reader = new StringReader(puzzleXML);
			return (Puzzle)serializer.Deserialize(reader);
		}
	}
}
