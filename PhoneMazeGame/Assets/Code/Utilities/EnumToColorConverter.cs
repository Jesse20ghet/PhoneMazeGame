using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Utilities
{
	public static class EnumToColorConverter
	{
		private static Color BackgroundYellow = new Color(.5F, .5F, 0);
		private static Color WallYellow = new Color(1, 1, 0);

		private static Color BackgroundBlue = new Color(.39F, .39F, 1);
		private static Color WallBlue = new Color(0, 0, 1);

		private static Color BackgroundRed = new Color(.5F, 0, 0);
		private static Color WallRed = new Color(1, 0, 0);

		private static Color BackgroundGreen = new Color(0, .5F, 0);
		private static Color WallGreen = new Color(0, 1, 0);

		private static Color BackgroundLocked = new Color(.5F, .5F, .5F);
		private static Color WallLocked = new Color(0, 0, 0);

		private static Color BackgroundDefault = new Color(1, 1, 1);
		private static Color WallDefault = new Color(0, 0, 0);


		public static Color SwapBackgroundColor(Enumerations.OctagonColor colorToSwap)
		{
			switch(colorToSwap)
			{
				case Enumerations.OctagonColor.Locked:
					return BackgroundLocked;

				case Enumerations.OctagonColor.Default:
					return BackgroundDefault;

				case Enumerations.OctagonColor.Blue:
					return BackgroundBlue;

				case Enumerations.OctagonColor.Red:
					return BackgroundRed;

				case Enumerations.OctagonColor.Green:
					return BackgroundGreen;

				case Enumerations.OctagonColor.Yellow:
					return BackgroundYellow;

				default:
					return BackgroundDefault;
			}
		}

		public static Color SwapWallColor(Enumerations.OctagonColor colorToSwap)
		{
			switch (colorToSwap)
			{
				case Enumerations.OctagonColor.Locked:
					return WallLocked;

				case Enumerations.OctagonColor.Default:
					return WallDefault;

				case Enumerations.OctagonColor.Blue:
					return WallBlue;

				case Enumerations.OctagonColor.Red:
					return WallRed;

				case Enumerations.OctagonColor.Green:
					return WallGreen;

				case Enumerations.OctagonColor.Yellow:
					return WallYellow;

				default:
					return WallDefault;
			}
		}
	}
}
