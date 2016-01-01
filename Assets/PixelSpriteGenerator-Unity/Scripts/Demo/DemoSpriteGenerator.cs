using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace PixelSpriteGenerator
{
	public class DemoSpriteGenerator : MonoBehaviour {

		[HideInInspector]
		public int[] templateData;

		[HideInInspector]
		public int width = 8;

		[HideInInspector]
		public int height = 8;

		[HideInInspector]
		public bool mirrorX;

		[HideInInspector]
		public bool mirrorY;

		public enum SpriteTemplate
		{
			spaceShipColored,
			spaceShipColoredLowSat,
			spaceShipManyColor,
			treeColored,
			dragonColored,
			shrubColored,
			robotBW
		}

		public SpriteTemplate SpriteTemplateSelection;

		private SpriteRenderer sr;

		private PsgOptions options;

		private PsgMask mask;

		private float spritePadding;

		// Use this for initialization
		void Start () {
			GenerateSprites ();
		}

		private void CheckTemplateSelection()
		{
			switch (SpriteTemplateSelection) {

			case SpriteTemplate.spaceShipColored:

				mask = new PsgMask (new int[] {
					0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 1, 1,
					0, 0, 0, 0, 1, -1,
					0, 0, 0, 1, 1, -1,
					0, 0, 0, 1, 1, -1,
					0, 0, 1, 1, 1, -1,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 1, -1,
					0, 0, 0, 1, 1, 1,
					0, 0, 0, 0, 0, 0
				}, 6, 12, true, false);

				spritePadding = 1f;

				options = new PsgOptions () {
					Colored = true,
					EdgeBrightness = 0.3f,
					ColorVariations = 0.2f,
					BrightnessNoise = 0.3f,
					Saturation = 0.5f
				};

				break;

			case SpriteTemplate.spaceShipColoredLowSat:

				mask = new PsgMask (new int[] {
					0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 1, 1,
					0, 0, 0, 0, 1, -1,
					0, 0, 0, 1, 1, -1,
					0, 0, 0, 1, 1, -1,
					0, 0, 1, 1, 1, -1,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 1, -1,
					0, 0, 0, 1, 1, 1,
					0, 0, 0, 0, 0, 0
				}, 6, 12, true, false);

				spritePadding = 1f;

				options = new PsgOptions () {
					Colored = true,
					EdgeBrightness = 0.3f,
					ColorVariations = 0.2f,
					BrightnessNoise = 0.3f,
					Saturation = 0.2f
				};

				break;

			case SpriteTemplate.spaceShipManyColor:

				mask = new PsgMask (new int[] {
					0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 1, 1,
					0, 0, 0, 0, 1, -1,
					0, 0, 0, 1, 1, -1,
					0, 0, 0, 1, 1, -1,
					0, 0, 1, 1, 1, -1,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 1, -1,
					0, 0, 0, 1, 1, 1,
					0, 0, 0, 0, 0, 0
				}, 6, 12, true, false);

				spritePadding = 1f;

				options = new PsgOptions () {
					Colored = true,
					EdgeBrightness = 0.3f,
					ColorVariations = 0.85f,
					BrightnessNoise = 0.3f,
					Saturation = 0.5f
				};

				break;

			case SpriteTemplate.shrubColored:

				mask = new PsgMask (new int[] {
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 1, 1, 2,
					0, 0, 0, 0, 0, 1, 0, 0, 2,
					0, 0, 0, 0, 1, 0, 0, -1, 2,
					0, 0, 0, 1, 0, 0, -1, 0, 2,
					0, 0, 0, 0, 0, -1, 0, 0, 2,
					0, 0, 0, 0, 0, 0, 0, 1, 2,
					0, 0, 0, 0, -1, -1, 2, 2, 2,
					0, 0, -1, -1, 0, 0, -1, -1, 2,
					0, -1, 0, -1, 0, 0, -1, 0, -1,
					0, 0, 0, 0, 0, -1, 0, 0, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 2,
					0, 0, 0, 0, 0, 0, 0, 0, 2,
					0, 0, 0, 0, 0, 0, 0, 0, 2,
					0, 0, -1, -1, 2, 0, 0, 0, 2,
					0, -1, 0, 0, -1, -1, 2, 0, 2,
					-1, 0, 0, 0, 0, 0, -1, -1, 2,
					0, 0, 0, 0, 0, 0, 0, 0, 2,
					0, 0, 0, 0, 0, 0, 0, -1, 2,
					0, 0, 0, 0, 0, 0, 0, -1, 2,
					0, 0, 0, 0, 0, 0, -1, -2, 2
				}, 9, 21, true, false);

				spritePadding = 1.7f;

				options = new PsgOptions () {
					Colored = true,
					EdgeBrightness = 0.3f,
					ColorVariations = 0.2f,
					BrightnessNoise = 0.3f,
					Saturation = 0.5f
				};

				break;

			case SpriteTemplate.treeColored:

				mask = new PsgMask (new int[] {
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1,
					0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, -1,
					0, 0, 0, 0, 0, 0, -1, -1, 0, 0, 0, 0, -1,
					0, 0, 0, 0, 0, 0, 0, -1, -1, -1, 0, 0, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1, -1,
					0, 0, 1, -1, 0, 0, 0, 0, 0, 0, 0, -1, -1,
					0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, 1, -1,
					0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 1, -1,
					0, 0, 0, 1, 1, -1, -1, -1, 0, 0, 0, 1, -1,
					0, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1, 1, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1, -1,
					0, 0, 0, -1, 0, 0, 0, 0, 0, 0, 0, 1, -1,
					0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, 1, -1,
					0, 0, 1, -1, -1, -1, -1, 0, 0, 0, 0, 1, -1,
					0, 1, 0, 0, 0, 1, -1, -1, -1, 0, 0, 1, -1,
					0, 0, 0, 0, 0, 0, 0, 1, -1, -1, 0, -1, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, -1, -1
				}, 13, 26, true, false);

				spritePadding = 2.1f;

				options = new PsgOptions () {
					Colored = true,
					EdgeBrightness = 0.3f,
					ColorVariations = 0.2f,
					BrightnessNoise = 0.3f,
					Saturation = 0.5f
				};

				break;

			case SpriteTemplate.dragonColored:

				mask = new PsgMask (new int[] {
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
					0, 0, 0, 1, 1, 2, 2, 1, 1, 0, 0, 0,
					0, 0, 1, 1, 1, 2, 2, 1, 1, 1, 0, 0,
					0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0,
					0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0,
					0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0,
					0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0,
					0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0,
					0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0,
					0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
				}, 12, 12, false, false);

				spritePadding = 1f;

				options = new PsgOptions () {
					Colored = true,
					EdgeBrightness = 0.3f,
					ColorVariations = 0.2f,
					BrightnessNoise = 0.3f,
					Saturation = 0.5f
				};

				break;

			case SpriteTemplate.robotBW:

				mask = new PsgMask (new int[] {
					0, 0, 0, 0,
					0, 1, 1, 1,
					0, 1, 2, 2,
					0, 0, 1, 2,
					0, 0, 0, 2,
					1, 1, 1, 2,
					0, 1, 1, 2,
					0, 0, 0, 2,
					0, 0, 0, 2,
					0, 1, 2, 2,
					1, 1, 0, 0
				}, 4, 11, true, false);

				spritePadding = 1f;

				options = new PsgOptions () {
					Colored = false,
					EdgeBrightness = 0.3f,
					ColorVariations = 0.2f,
					BrightnessNoise = 0.3f,
					Saturation = 0.5f
				};

				break;

			default:

				mask = new PsgMask (new int[] {
					0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 1, 1,
					0, 0, 0, 0, 1, -1,
					0, 0, 0, 1, 1, -1,
					0, 0, 0, 1, 1, -1,
					0, 0, 1, 1, 1, -1,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 2, 2,
					0, 1, 1, 1, 1, -1,
					0, 0, 0, 1, 1, 1,
					0, 0, 0, 0, 0, 0
				}, 6, 12, true, false);

				spritePadding = 1f;

				options = new PsgOptions () {
					Colored = true,
					EdgeBrightness = 0.3f,
					ColorVariations = 0.2f,
					BrightnessNoise = 0.3f,
					Saturation = 0.5f
				};

				break;
			}
		}

		public void DemoButtonPressed(string param)
		{
			switch (param) {

			case "spaceShipColored":
				SpriteTemplateSelection = SpriteTemplate.spaceShipColored;
				break;

			case "spaceShipColoredLowSat":
				SpriteTemplateSelection = SpriteTemplate.spaceShipColoredLowSat;
				break;
			
			case "spaceShipColoredManyColor":
				SpriteTemplateSelection = SpriteTemplate.spaceShipManyColor;
				break;

			case "dragonColored":
				SpriteTemplateSelection = SpriteTemplate.dragonColored;
				break;

			case "treeColored":
				SpriteTemplateSelection = SpriteTemplate.treeColored;
				break;

			case "shrubColored":
				SpriteTemplateSelection = SpriteTemplate.shrubColored;
				break;

			case "robotBw":
				SpriteTemplateSelection = SpriteTemplate.robotBW;
				break;

			default:
				SpriteTemplateSelection = SpriteTemplate.spaceShipColored;
				break;
			}

			var allSprites = GameObject.FindObjectsOfType<SpriteRenderer> ();
			if (allSprites != null) {
				foreach (var sprite in allSprites) {
					GameObject.Destroy (sprite.gameObject);
				}
			}
			GenerateSprites ();
		}

		private void GenerateSprites()
		{
			CheckTemplateSelection ();

			for (var y = 0; y < 10; y++) {
				for (var x = 0; x < 10; x++) {
					var psgSprite = new PsgSprite (mask, options);

					if (mask.mirrorX) {
						width = mask.width * 2;

					} else {
						width = mask.width;
					}

					if (mask.mirrorY) {
						height = mask.height * 2;
					} else {
						height = mask.height;
					}

					mirrorX = mask.mirrorX;
					mirrorY = mask.mirrorY;

					var tex = psgSprite.texture;
					tex.wrapMode = TextureWrapMode.Clamp;
					tex.filterMode = FilterMode.Point;

					var cmpts = new Type[1]{ typeof(SpriteRenderer) };
					var go = new GameObject (((x + 1) * y).ToString (), cmpts);
					var theSr = go.GetComponent<SpriteRenderer> ();

					theSr.sprite = Sprite.Create(tex, new Rect(0, 0, (float)width, (float)height), new Vector2(0.5f, 0.5f), 32f);

					theSr.transform.localScale = new Vector3 (-2.2f, -2.2f, -2.2f);
					theSr.transform.position = new Vector2 (x * spritePadding, y * spritePadding);
				}
			}
		}
	}	
}