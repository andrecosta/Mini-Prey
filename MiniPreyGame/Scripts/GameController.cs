using System;
using System.Collections.Generic;
using System.Linq;
using KokoEngine;

public class GameController : Behaviour
{
    // Properties populated from setup
    public ISprite ShipSprite { get; set; }
    public ISprite ShotSprite { get; set; }
    public ISprite OutlineSprite { get; set; }
    public ISprite RangeSprite { get; set; }
    public AudioClip PlanetConqueredSound { get; set; }
    public AudioClip PlanetSelectSound { get; set; }
    public AudioClip PlanetUpgradeSound { get; set; }
    public AudioClip ShipShotSound { get; set; }
    public Font PlanetPopulationFont { get; set; }
    public Player[] Players { get; set; }
    public Planet.Type[] PlanetTypes { get; set; }

    public List<Planet> Planets { get; private set; }
    public List<Ship> Ships { get; private set; }
    public Dictionary<Player, int> PlayerPopulations { get; set; }
    public int TotalPopulation { get { return PlayerPopulations.Values.Sum(); } }

    // Callbacks
    public Action<Planet> OnPlanetCreated { get; set; }
    public Action<Planet> OnStructureUpgraded { get; set; }
    public Action<Planet> OnStructureAttacked { get; set; }
    public Action<Planet, int> OnStructureCaptured { get; set; }
    public Action<Ship> OnCitizenCreated { get; set; }
    public Action<Ship> OnCitizenLaunched { get; set; }
    public Action<Ship> OnCitizenKilled { get; set; }

    public Action<Planet> OnStructureHovered { get; set; }
    public Action<Planet> OnStructureSelected { get; set; }

    public Action OnPopulationChange { get; set; }

    //public Action<Planet, Planet> OnCitizenLaunched { get; set; }

    protected override void Awake()
    {
        Planets = new List<Planet>();
        Ships = new List<Ship>();
        PlayerPopulations = new Dictionary<Player, int>();
        foreach (var player in Players)
        {
            PlayerPopulations.Add(player, 0);
        }
    }

    protected override void Start()
    {
        // Create the planets
        CreatePlanets();
    }

    protected override void Update()
    {
        if (Input.GetActionDown("ToggleFullScreen"))
            Screen.IsFullScreen = !Screen.IsFullScreen;

        if (Input.GetActionDown("ToggleDebugConsole"))
            Debug.Toggle();
    }

    private void CreatePlanets()
    {
        CreatePlanet(new Vector2(100, 100), Players[0], 0, 1, 15);
        CreatePlanet(new Vector2(100, 400), Players[2], 0, 2, 5);
        CreatePlanet(new Vector2(300, 200), Players[2], 0, 0, 5);
        CreatePlanet(new Vector2(300, 500), Players[2], 1, 1, 5);
        CreatePlanet(new Vector2(500, 300), Players[2], 1, 2, 5);
        CreatePlanet(new Vector2(500, 600), Players[2], 1, 0, 5);
        CreatePlanet(new Vector2(Screen.Width - 100, 100), Players[1], 0, 1, 10);
        CreatePlanet(new Vector2(Screen.Width - 100, 400), Players[1], 0, 1, 10);
        CreatePlanet(new Vector2(Screen.Width - 300, 200), Players[2], 0, 0, 5);
        CreatePlanet(new Vector2(Screen.Width - 300, 500), Players[2], 0, 0, 5);
        CreatePlanet(new Vector2(Screen.Width - 500, 300), Players[2], 0, 0, 5);
        CreatePlanet(new Vector2(Screen.Width - 500, 600), Players[2], 0, 0, 5);
    }

    private void CreatePlanet(Vector2 position, Player owner, int currentType, int currentUpgradeLevel, int population)
    {
        Planet planet = Instantiate<Planet>("Planet", position);
        planet.Owner = owner;
        planet.CurrentType = currentType;
        planet.CurrentUpgradeLevel = currentUpgradeLevel;
        planet.Population = population;
        planet.GameController = this;

        // Add SpriteRenderer component
        var sr = planet.AddComponent<SpriteRenderer>();
        sr.Sprite = planet.CurrentUpgrade.Sprite;

        // Add TextRenderer component
        var tr = planet.AddComponent<TextRenderer>();
        tr.Font = PlanetPopulationFont;
        tr.Offset = new Vector2(0, -54);
        tr.Size = 0.75f;

        // Add AudioSource component
        var au = planet.AddComponent<AudioSource>();

        // Bind callbacks
        planet.LaunchShipHandler += LaunchShip;
        planet.OnPopulationChange += UpdateTotalPopulation;

        // Add to list
        Planets.Add(planet);

        // Invoke callback
        OnPlanetCreated?.Invoke(planet);
    }

    private void LaunchShip(Planet source, Planet target)
    {
        var ship = Instantiate<Ship>("Ship", source.Transform.Position);
        var sr = ship.AddComponent<SpriteRenderer>();
        var rb = ship.AddComponent<Rigidbody>();
        var fsm = ship.AddComponent<FSM>();
        var v = ship.AddComponent<Vehicle>();
        var seek = ship.AddComponent<Seek>();

        sr.Sprite = ShipSprite;
        sr.Layer = 0.55f;
        sr.Color = source.Owner.TeamColor;

        v.Behaviours.Add(seek);

        ship.GameController = this;
        ship.Owner = source.Owner;
        ship.Source = source;
        ship.Target = target;

        Ships.Add(ship);
    }

    private void UpdateTotalPopulation()
    {
        foreach (var player in PlayerPopulations.Keys.ToList())
        {
            var p = player;

            // Reset player populations
            PlayerPopulations[p] = 0;

            // Find population inside structures
            foreach (var structure in Planets.Where(s => s.Owner == p))
                PlayerPopulations[p] += structure.Population;

            // Find population on the field
            int ownedCitizens = FindObjectsOfType<Ship>().Count(c => c.Owner == p);
            PlayerPopulations[p] += ownedCitizens;
        }

        OnPopulationChange?.Invoke();
    }

    public void Quit()
    {
        //Application.Quit();
    }
}
