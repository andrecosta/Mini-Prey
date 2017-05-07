using KokoEngine;

public class CustomCursor : Behaviour
{
    public ISprite Percent25Sprite { get; set; }
    public ISprite Percent50Sprite { get; set; }
    public ISprite Percent75Sprite { get; set; }
    public ISprite Percent100Sprite { get; set; }

    private ISpriteRenderer _sr;

    protected override void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.Layer = 0;
    }

    protected override void Update()
    {
        Transform.Position = Input.GetMousePosition() + new Vector2(_sr.Sprite.SourceRect.Width / 2f, _sr.Sprite.SourceRect.Height / 2f);
    }

    public void SetCursor(int percentage)
    {
        switch (percentage)
        {
            case 25:
                _sr.Sprite = Percent25Sprite;
                break;
            case 50:
                _sr.Sprite = Percent50Sprite;
                break;
            case 75:
                _sr.Sprite = Percent75Sprite;
                break;
            case 100:
                _sr.Sprite = Percent100Sprite;
                break;
        }
    }
}
