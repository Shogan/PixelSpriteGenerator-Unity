using UnityEngine;
using System.Collections;

namespace PixelSpriteGenerator
{
	public class PsgSprite {

		public int width;

		public int height;

		public PsgMask mask;

		public int[] data;

		public PsgOptions options;

		public Texture2D texture;

		public PsgSprite(PsgMask mask, PsgOptions options) {

			this.width = mask.width * (mask.mirrorX ? 2 : 1);
			this.height = mask.height * (mask.mirrorY ? 2 : 1);

			this.mask = mask;
			this.data = new int[this.width * this.height];

			var defaults = new PsgOptions () {
				Colored = true,
				EdgeBrightness = 0.3f,
				ColorVariations = 0.2f,
				BrightnessNoise = 0.3f,
				Saturation = 0.5f
			};

			if (options == null) { 
				this.options = defaults;
			} 
			else {
				this.options = options;
			}

			this.Init ();
		}

		private void Init()
		{
			this.InitContext ();
			this.InitData ();
			this.ApplyMask ();
			this.GenerateRandomSample ();

			if (this.mask.mirrorX) {
				this.MirrorX ();
			}

			if (this.mask.mirrorY) {
				this.MirrorY ();
			}

			this.GenerateEdges ();
			this.RenderPixelData ();
		}

		private void InitContext()
		{
			var newWidth = 0;
			var newHeight = 0;

			if (this.mask.mirrorX) {
				newWidth = this.width * 2;
			} else {
				newWidth = this.mask.width;
			}

			if (this.mask.mirrorY) {
				newHeight = this.height * 2;
			} else {
				newHeight = this.mask.height;
			}

			this.texture = new Texture2D (newWidth, newHeight, TextureFormat.ARGB32, false);
		}

		/// <summary>
		/// The GetData method returns the sprite template data at location (x, y)
		///
		///      -1 = Always border (black)
		///       0 = Empty
		///       1 = Randomly chosen Empty/Body
		///       2 = Randomly chosen Border/Body
		/// </summary>
		/// <returns>The data at the x,y co-ordinate.</returns>
		private int GetData(int x, int y)
		{
			return this.data[y * this.width + x];
		}

		/// <summary>
		/// *   The SetData method sets the sprite template data at location (x, y)
		/// *
		/// *      -1 = Always border (black)
		/// *       0 = Empty
		/// *       1 = Randomly chosen Empty/Body
		/// *       2 = Randomly chosen Border/Body
		/// *
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="value">Value.</param>
		private void SetData(int x, int y, int value)
		{
			this.data[y * this.width + x] = value;
		}

		/// <summary>
		/// The InitData method initializes the sprite data to completely solid.
		/// </summary>
		private void InitData()
		{
			var h = this.height;
			var w = this.width;
			for (var y = 0; y < h; y++) {
				for (var x = 0; x < w; x++) {
					this.SetData(x, y, -1);
				}
			}
		}

		/// <summary>
		/// The MirrorX method mirrors the template data horizontally.
		/// </summary>
		private void MirrorX()
		{
			var h = this.height;
			var w = Mathf.FloorToInt(this.width/2);

			for (var y = 0; y < h; y++) {
				for (var x = 0; x < w; x++) {
					this.SetData(this.width - x - 1, y, this.GetData(x, y));
				}
			}
		}

		/// <summary>
		/// The MirrorY method mirrors the template data vertically.
		/// </summary>
		private void MirrorY()
		{
			var h = Mathf.FloorToInt(this.height/2);
			var w = this.width;

			for (var y = 0; y < h; y++) {
				for (var x = 0; x < w; x++) {
					this.SetData(x, this.height - y - 1, this.GetData(x, y));
				}
			}
		}

		/// <summary>
		/// The applyMask method copies the mask data into the template data array at
		/// *   location (0, 0).
		/// *
		/// *   (note: the mask may be smaller than the template data array)
		/// </summary>
		private void ApplyMask()
		{
			var h = this.mask.height;
			var w = this.mask.width;

			for (var y = 0; y < h; y++) {
				for (var x = 0; x < w; x++) {
					this.SetData(x, y, this.mask.data[y * w + x]);
				}
			}
		}

		/// <summary>
		/// *   Apply a random sample to the sprite template.
		/// *
		/// *   If the template contains a 1 (internal body part) at location (x, y), then
		/// *   there is a 50% chance it will be turned empty. If there is a 2, then there
		/// *   is a 50% chance it will be turned into a body or border.
		/// *
		/// *   (feel free to play with this logic for interesting results)
		/// </summary>
		private void GenerateRandomSample()
		{
			var h = this.height;
			var w = this.width;
			var x = 0;
			var y = 0;
			var val = 0;

			for (y = 0; y < h; y++)
			{
				for (x = 0; x < w; x++)
				{
					val = this.GetData(x, y);

					if (val == 1)
					{
						val = val * Mathf.RoundToInt(Random.Range(0f, 1f));
					}
					else if (val == 2)
					{
						if (Random.Range(0f, 1f) > 0.5)
						{
							val = 1;
						}
						else
						{
							val = -1;
						}
					} 

					this.SetData(x, y, val);
				}
			}
		}

		private Color SetHSL(Color inputColor, float h, float s, float l)
		{
			int i;
			float f;
			float p;
			float q;
			float t;

			i = Mathf.FloorToInt(h * 6);
			f = h * 6 - i;
			p = l * (1 - s);
			q = l * (1 - f * s);
			t = l * (1 - (1 - f) * s);

			switch (i % 6) {
			case 0:
				inputColor.r = l;
				inputColor.g = t;
				inputColor.b = p;
				break;
			case 1: 
				inputColor.r = q; 
				inputColor.g = l; 
				inputColor.b = p; 
				break;
			case 2: 
				inputColor.r = p; 
				inputColor.g = l; 
				inputColor.b = t; 
				break;
			case 3:
				inputColor.r = p; 
				inputColor.g = q; 
				inputColor.b = l;
				break;
			case 4:
				inputColor.r = t; 
				inputColor.g = p; 
				inputColor.b = l; 
				break;
			case 5:
				inputColor.r = l; 
				inputColor.g = p; 
				inputColor.b = q; 
				break;

			default:
				Debug.LogError ("No case found!");
				break;
			}

			return inputColor;
		}

		/// <summary>
		/// *   This method applies edges to any template location that is positive in
		/// *   value and is surrounded by empty (0) pixels.
		/// </summary>
		private void GenerateEdges()
		{
			var h = this.height;
			var w = this.width;
			var x = 0;
			var y = 0;

			for (y = 0; y < h; y++)
			{
				for (x = 0; x < w; x++)
				{
					if (this.GetData(x, y) > 0)
					{
						if (y - 1 >= 0 && this.GetData(x, y - 1) == 0)
						{
							this.SetData(x, y - 1, -1);
						}
						if (y + 1 < height && this.GetData(x, y + 1) == 0)
						{
							this.SetData(x, y + 1, -1);
						}
						if (x - 1 >= 0 && this.GetData(x - 1, y) == 0)
						{
							this.SetData(x - 1, y, -1);
						}
						if (x + 1 < width && this.GetData(x + 1, y) == 0)
						{
							this.SetData(x + 1, y, -1);
						}
					}
				}
			}
		}

		/// <summary>
		/// *   This method renders out the template data to a Unity Texture2D to be used for the SpriteRenderer to display
		/// *   the sprite.
		/// *
		/// *   (note: only template locations with the values of -1 (border) are rendered)
		/// *
		/// </summary>
		private void RenderPixelData()
		{
			var isVerticalGradient = Random.Range(0f, 1f) > 0.5f;
			var saturation = Mathf.Max(Mathf.Min(Random.Range(0f, 1f) * this.options.Saturation, 1f), 0f);

			var hue = Random.Range(0f, 1f);

			var u = 0;
			var v = 0;
			var ulen = 0;
			var vlen = 0;

			var isNewColor = 0f;
			var val = 0;
			var color = new Color(0,0,0);
			var brightness = 0f;

			// Target XY of pixels for the SetPixel method on the Texture2D
			var x = 0;
			var y = 0;

			if (isVerticalGradient)
			{
				ulen = this.height;
				vlen = this.width;
			}
			else
			{
				ulen = this.width;
				vlen = this.height;
			}

			for (u = 0; u < ulen; u++)
			{
				// Create a non-uniform random number between 0 and 1 (lower numbers more likely)
				isNewColor = Mathf.Abs(((Random.Range(0f, 1f) * 2 - 1) 
					+ (Random.Range(0f, 1f) * 2 - 1) 
					+ (Random.Range(0f, 1f) * 2 - 1)) / 3);

				// Only change the color sometimes (values above 0.8 are less likely than others)
				if (isNewColor > (1f - this.options.ColorVariations))
				{
					hue = Random.Range(0f, 1f);
				}

				for (v = 0; v < vlen; v++)
				{
					if (isVerticalGradient)
					{
						val   = this.GetData(v, u);
						x     = v;
						y     = u;
					}
					else
					{
						val   = this.GetData(u, v);
						x     = u;
						y     = v;
					}

					color = new Color (1, 1, 1, 0); // transparent by default (0 alpha)

					if (val != 0)
					{
						if (this.options.Colored)
						{
							// Fade brightness away towards the edges
							brightness = Mathf.Sin((u / ulen) * Mathf.PI) * (1f - this.options.BrightnessNoise) 
								+ Random.Range(0f, 3f) * this.options.BrightnessNoise; // Original code uses random between 0 and 1.

							color = SetHSL(color, hue, saturation, brightness);

							// If this is an edge, then darken the pixel
							if (val == -1)
							{
								color.r *= this.options.EdgeBrightness;
								color.g *= this.options.EdgeBrightness;
								color.b *= this.options.EdgeBrightness;

							}

							color.a = 1;
						}
						else
						{
							// Not colored, simply output black
							if (val == -1)
							{
								color.r = 0;
								color.g = 0;
								color.b = 0;
								color.a = 1;
							}
						}
					}

					this.texture.SetPixel (x, y, color);
				}
			}

			// Apply the changes to the texture
			this.texture.Apply();
		}

		/// <summary>
		/// Used for debug purposes
		/// </summary>
		/// <returns>The to string.</returns>
		public string TemplateToString()
		{
			var h = height;
			var w = width;
			var x = 0;
			var y = 0;
			var output = string.Empty;

			for (y = 0; y < h; y++)
			{
				for (x = 0; x < w; x++)
				{
					var val = this.GetData(x, y);
					output += (val >= 0) ? " " + val : "" + val;
				}
				output += "\n";
			}
			return output;
		}
	}
}