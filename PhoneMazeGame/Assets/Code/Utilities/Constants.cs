using Assets.Code.ConcreteClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Utilities
{
	public static class Constants
	{
		public static PuzzleCategoryDescriptor[] PuzzleCategories =
		{
			new PuzzleCategoryDescriptor() {Difficulty = 1, CategoryName = "2x2", LevelsAvailable = 10, LevelsCompleted = 0 },
			new PuzzleCategoryDescriptor() {Difficulty = 1, CategoryName = "3x3", LevelsAvailable = 10, LevelsCompleted = 0 },
			new PuzzleCategoryDescriptor() {Difficulty = 1, CategoryName = "4x4", LevelsAvailable = 10, LevelsCompleted = 0 },
			new PuzzleCategoryDescriptor() {Difficulty = 1, CategoryName = "5x5", LevelsAvailable = 10, LevelsCompleted = 0 },
			new PuzzleCategoryDescriptor() {Difficulty = 2, CategoryName = "6x6", LevelsAvailable = 10, LevelsCompleted = 0 },
			new PuzzleCategoryDescriptor() {Difficulty = 3, CategoryName = "7x7", LevelsAvailable = 10, LevelsCompleted = 0 },
			new PuzzleCategoryDescriptor() {Difficulty = 4, CategoryName = "8x8", LevelsAvailable = 10, LevelsCompleted = 0 },
			new PuzzleCategoryDescriptor() {Difficulty = 5, CategoryName = "9x9", LevelsAvailable = 10, LevelsCompleted = 0 },
			new PuzzleCategoryDescriptor() {Difficulty = 5, CategoryName = "10x10", LevelsAvailable = 10, LevelsCompleted = 0 }
		};
	}
}
