using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.ConcreteClasses
{
	public class PuzzleCategoryDescriptor
	{
		private int _difficulty;

		public string CategoryName { get; set; }

		/// <summary>
		/// Difficulty is an integer from 1 - 5. Used to display the number of stars on canvas
		/// </summary>
		public int Difficulty
		{
			get { return _difficulty; }
			set
			{
				if (value > 5)
				{
					_difficulty = 5;
				}
				else if (value < 1)
					_difficulty = 1;
				else
					_difficulty = value;
			}
		}

		public int LevelsCompleted { get; set; }
		public int LevelsAvailable { get; set; }
	}
}
