using System;

namespace PixelSpriteGenerator
{
	public class PsgMask {

		public int width;
		public int height;
		public int[] data;
		public bool mirrorX;
		public bool mirrorY;

		public PsgMask(int[] data, int width, int height, bool mirrorX, bool mirrorY) {
			this.width   = width;
			this.height  = height;
			this.data    = data;
			this.mirrorX = mirrorX;
			this.mirrorY = mirrorY;
		}
	}
}
