using KokoEngine;

class HUD : Behaviour
{
    public GameController GameController { get; set; }
    public ILineRenderer LeftBar { get; set; }
    public ILineRenderer RightBar { get; set; }
    public ITextRenderer LeftText { get; set; }
    public ITextRenderer RightText { get; set; }
    public Font UIFont { get; set; }

    protected override void Update()
    {
        Player p1 = GameController.Players[0];
        Player p2 = GameController.Players[1];
        float p1Progress = (float) GameController.PlayerPopulations[p1] / GameController.TotalPopulation;
        float p2Progress = (float) GameController.PlayerPopulations[p2] / GameController.TotalPopulation;

        LeftBar.Color = p1.TeamColor;
        LeftBar.Start = new Vector2(Screen.Width / 2f - 200, 15);
        LeftBar.End = new Vector2(Screen.Width / 2f - 200 + p1Progress * 400, 15);
        LeftBar.Size = 15;

        RightBar.Color = p2.TeamColor;
        RightBar.Start = new Vector2(Screen.Width / 2f + 200 - p2Progress * 400, 15);
        RightBar.End = new Vector2(Screen.Width / 2f + 200, 15);
        RightBar.Size = 15;

        LeftText.Color = p1.TeamColor;
        LeftText.Text = GameController.PlayerPopulations[p1].ToString();
        LeftText.Transform.Position = new Vector3(Screen.Width / 2f - 220, 25);
        LeftText.Font = UIFont;
        LeftText.Size = 0.6f;

        RightText.Color = p2.TeamColor;
        RightText.Text = GameController.PlayerPopulations[p2].ToString();
        RightText.Transform.Position = new Vector3(Screen.Width / 2f + 220, 25);
        RightText.Font = UIFont;
        RightText.Size = 0.6f;
    }
}
