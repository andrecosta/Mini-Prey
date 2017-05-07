using KokoEngine;

class UpgradeMenu : Behaviour
{
    public ITextRenderer UpgradeText { get; set; }
    public ITextRenderer Convert1Text { get; set; }
    public ITextRenderer Convert2Text { get; set; }
    public Font UIFont { get; set; }

    private Planet _planet;

    protected override void Awake()
    {
        _planet = GetComponent<Planet>();
    }

    protected override void Update()
    {
        UpgradeText.Transform.Position = _planet.Transform.Position + new Vector2(0, 53);
        UpgradeText.Font = UIFont;
        UpgradeText.Size = 0.32f;

        Convert1Text.Transform.Position = _planet.Transform.Position + new Vector2(0, 65);
        Convert1Text.Font = UIFont;
        Convert1Text.Size = 0.32f;

        Convert2Text.Transform.Position = _planet.Transform.Position + new Vector2(0, 65);
        Convert2Text.Font = UIFont;
        Convert2Text.Size = 0.32f;

        if (_planet.CurrentUpgradeLevel + 1 < _planet.GameController.PlanetTypes[_planet.CurrentType].UpgradeLevels.Length)
        {
            int cost = _planet.GameController.PlanetTypes[_planet.CurrentType].UpgradeLevels[_planet.CurrentUpgradeLevel + 1].Cost;

            if (_planet.AvailablePopulation < cost)
                UpgradeText.Color = Color.Red;
            else
                UpgradeText.Color = new Color(236, 196, 73);

            UpgradeText.Text = "[W] UPGRADE TO LEVEL " + (_planet.CurrentUpgradeLevel + 1) + " (" + cost + ")";
        }
        else
        {
            UpgradeText.Text = "UPGRADED (MAX)";
            UpgradeText.Color = Color.Grey;
        }

        if (_planet.CurrentType != 0)
        {
            int cost = _planet.GameController.PlanetTypes[0].UpgradeLevels[0].Cost;

            if (_planet.AvailablePopulation < cost)
                Convert1Text.Color = Color.Red;
            else
                Convert1Text.Color = new Color(236, 196, 73);

            Convert1Text.Text = "[A] Change to Colony (" + cost + ")";
        }
        else
            Convert1Text.GameObject.SetActive(false);

        if (_planet.CurrentType != 1)
        {
            int cost = _planet.GameController.PlanetTypes[1].UpgradeLevels[0].Cost;

            if (_planet.AvailablePopulation < cost)
                Convert2Text.Color = Color.Red;
            else
                Convert2Text.Color = new Color(236, 196, 73);

            Convert2Text.Text = "[D] Change to Sentry (" + cost + ")";
        }
        else
            Convert2Text.GameObject.SetActive(false);
    }

    public void Hide()
    {
        UpgradeText.GameObject.SetActive(false);
        Convert1Text.GameObject.SetActive(false);
        Convert2Text.GameObject.SetActive(false);
    }

    public void Show()
    {
        UpgradeText.GameObject.SetActive(true);
        Convert1Text.GameObject.SetActive(true);
        Convert2Text.GameObject.SetActive(true); 
    }
}
