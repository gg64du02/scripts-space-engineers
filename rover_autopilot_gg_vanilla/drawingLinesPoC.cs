
IMyTextSurface _drawingSurface;
RectangleF _viewport;


MySpriteDrawFrame spriteFrame;

//whip's code
public void DrawLine(ref MySpriteDrawFrame frame, Vector2 point1, Vector2 point2, float width, Color color)
{
    Vector2 position = 0.5f * (point1 + point2);
    Vector2 diff = point1 - point2;
    float length = diff.Length();
    if (length > 0)
        diff /= length;

    Vector2 size = new Vector2(length, width);
    float angle = (float)Math.Acos(Vector2.Dot(diff, Vector2.UnitX));
    angle *= Math.Sign(Vector2.Dot(diff, Vector2.UnitY));

    MySprite sprite = MySprite.CreateSprite("SquareSimple", position, size);
    sprite.RotationOrScale = angle;
    sprite.Color = color;
    frame.Add(sprite);
}

//wiki's code
// Drawing Sprites
public void DrawSprites(ref MySpriteDrawFrame frame)
{
    // Set up the initial position - and remember to add our viewport offset
    var position = new Vector2(256, 20) + _viewport.Position;
    
    // Create our first line
    var sprite = new MySprite()
    {
        Type = SpriteType.TEXT,
        Data = "Line 1",
        Position = position,
        RotationOrScale = 0.8f /* 80 % of the font's default size */,
        Color = Color.Red,
        Alignment = TextAlignment.CENTER /* Center the text on the position */,
        FontId = "White"
    };
    // Add the sprite to the frame
    frame.Add(sprite);
    
    // Move our position 20 pixels down in the viewport for the next line
    position += new Vector2(0, 20);

    // Create our second line, we'll just reuse our previous sprite variable - this is not necessary, just
    // a simplification in this case.
    sprite = new MySprite()
    {
        Type = SpriteType.TEXT,
        Data = "Line 2",
        Position = position,
        RotationOrScale = 0.8f,
        Color = Color.Blue,
        Alignment = TextAlignment.CENTER,
        FontId = "White"
    };
    // Add the sprite to the frame
    frame.Add(sprite);
}

public Program()
{
    // Set the continuous update frequency of this script
    Runtime.UpdateFrequency = UpdateFrequency.Update100;
	

	
    _drawingSurface = Me.GetSurface(0);
	
    // Calculate the viewport offset by centering the surface size onto the texture size
    _viewport = new RectangleF(
        (_drawingSurface.TextureSize - _drawingSurface.SurfaceSize) / 2f,
        _drawingSurface.SurfaceSize
    );
}

public void Save()
{
}

public void Main(string argument, UpdateType updateSource)
{
	
	

	spriteFrame = _drawingSurface.DrawFrame();
	
	DrawLine(ref spriteFrame, new Vector2(256,100), new Vector2(256,160), 30.0f, Color.DarkRed);
	DrawLine(ref spriteFrame, new Vector2(0,96), new Vector2(512,416), 30.0f, Color.DarkRed);
	
	Echo("_viewport:"+_viewport);
	
	DrawSprites(ref spriteFrame);
	
	spriteFrame.Dispose();
	


}
