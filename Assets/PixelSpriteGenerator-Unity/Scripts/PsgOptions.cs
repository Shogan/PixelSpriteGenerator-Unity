using System;

namespace PixelSpriteGenerator
{
	/// <summary>
	/// Data class to store options for Unity Pixel Sprite Generator mask usage
	/// </summary>
	public class PsgOptions {

		public bool Colored { get; set; }

		public float EdgeBrightness { get; set; }

		public float ColorVariations { get; set; }

		public float BrightnessNoise { get;	set; }

		public float Saturation { get; set;	}
	}
}