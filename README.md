# PixelSpriteGenerator-Unity
A port of the Pixel Sprite Generator to C# for use with the Unity3D game engine. 

Ported from the JavaScript / HTML version done by zfedoran https://github.com/zfedoran/pixel-sprite-generator to work with Unity3D.

## Examples

### Space ship sprites colored
![alt text](http://i.imgur.com/nRYrV61.png "Space ship sprites colored")

### Space ship sprites low saturation
![alt text](http://i.imgur.com/1tZ44QZ.png "Space ship sprites low saturation")

### Space ship sprites many color variations
![alt text](http://i.imgur.com/NuQ8lRH.png "Space ship sprites many color variations")

### Dragon sprites colored
![alt text](http://i.imgur.com/X4Rf4es.png "Dragon sprites colored")

### Tree sprites colored
![alt text](http://i.imgur.com/9xKU0Sb.png "Tree sprites colored")

### Shrub sprites colored
![alt text](http://i.imgur.com/gxj2Xn5.png "Shrub sprites colored")

### Robot sprites B&W
![alt text](http://i.imgur.com/ZG5cY6a.png "Robot sprites B&W")

## Usage in Unity3D

To use Pixel Sprite Generator for Unity, you should first of all add the three main scripts/classes to your Unity project:

- PsgSprite.cs
- PsgMask.cs
- PsgOptions.cs

Once these are added, to generate a sprite you need to define a template. The template is an integer array. Take a look at the template examples in the included PixeSpriteGeneratorDemo.cs for some ideas. Create an instance of the PsgMask class and pass in your template int[] and also pass in parameters to indicate your template size dimensions (x, y) as well as whether or not it is mirrored on the X or Y axes.

Next, create an instance of PsgOptions and define the options you would like to use. For example, colored or not, saturation, edge brightness etc... These options effect the look of your generated sprites.

Finally, create a new PsgSprite instance and pass in the mask and options (PsgMask and PsgOptions instances you created before).

The constructor of PsgSprite instance will do everything for you, and afterwards you can access the PsgSprite texture property to get a handle on the Texture2D that is created of the sprite. Use point mode filtering for the texture and set the wrap mode to Clamp for best results and usage as the texture for a Sprite.
