﻿using KokoEngine;

public class PlayerController : Player
{
    public GameController GameController;
    public CustomCursor CustomCursor;

    //private LineRenderer _lineRenderer;
    private Planet _selectedPlanet;
    private Planet _lastHoveredPlanet;

    private ILineRenderer _lineRenderer;
    private float _selectedPercentage = 0.25f;

    protected override void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    protected override void Start()
    {
        UpdateMouseTexture();
    }

    protected override void Update()
    {
        Planet planet = null;

        // Detect if there is a planet under the mouse cursor
        foreach (var p in GameController.Planets)
        {
            if (Vector3.Distance(Input.GetMousePosition(), p.Transform.Position) < 30)
            {
                planet = p;
            }
        }

        if (planet)
        {
            _lastHoveredPlanet = planet;

            if (planet.Owner == this)
            {
                // Show hover
                if (!_selectedPlanet || (_selectedPlanet != null && _selectedPlanet != planet))
                    planet.Hover();

                // Select structure
                if (Input.GetActionUp("PrimaryAction"))
                {
                    Debug.Log("Selected structure");
                    if (_selectedPlanet)
                        _selectedPlanet.DeSelect();

                    UnHover();

                    _selectedPlanet = planet;
                    _selectedPlanet.Select();
                    //_selectedPlanet.HideUpgradeMenu();
                }

                if (_selectedPlanet != null && _selectedPlanet == planet)
                {
                    // Toggle upgrade menu
                    if (Input.GetActionDown("SecondaryAction"))
                    {
                        //planet.ToggleUpgradeMenu();
                    }
                }
            }

            if (_selectedPlanet != null && _selectedPlanet != planet)
            {
                // Show line
                _lineRenderer.Start = _selectedPlanet.Transform.Position;
                _lineRenderer.End = planet.Transform.Position;
                _lineRenderer.Size = 3;
                _lineRenderer.Color = new Color(236, 196, 73);

                if (Input.GetActionDown("SecondaryAction") && _selectedPlanet != null)
                {
                    Debug.Log("Selected target structure");
                    _selectedPlanet.LaunchShips(planet, _selectedPercentage);
                    _selectedPlanet.DeSelect();
                    //_selectedPlanet.HideUpgradeMenu();
                    _selectedPlanet = null;
                    UnHover();
                }
            }
        }
        else
        {
            UnHover();

            if (Input.GetActionUp("PrimaryAction"))
            {
                if (_selectedPlanet)
                {
                    _selectedPlanet.DeSelect();
                    //_selectedPlanet.HideUpgradeMenu();
                    _selectedPlanet = null;
                }
            }
        }

        if (Input.GetMouseScrollDelta() > 0 || Input.GetActionUp("Fire"))
        {
            _selectedPercentage += 0.25f;
            if (_selectedPercentage > 1)
                _selectedPercentage = 1;
            UpdateMouseTexture();
        }
        else if (Input.GetMouseScrollDelta() < 0)
        {
            _selectedPercentage -= 0.25f;
            if (_selectedPercentage < 0.25f)
                _selectedPercentage = 0.25f;
            UpdateMouseTexture();
        }
    }

    void UnHover()
    {
        _lineRenderer.Start = Vector2.Zero;
        _lineRenderer.End = Vector2.Zero;
        _lineRenderer.Size = 0;

        if (_lastHoveredPlanet != null && _lastHoveredPlanet != _selectedPlanet)
            _lastHoveredPlanet.UnHover();

        //if (_selectedPlanet)
        //    _selectedPlanet.UnHover();
    }

    void UpdateMouseTexture()
    {
        if (_selectedPercentage == 0.25f)
            CustomCursor.SetCursor(25);
        else if (_selectedPercentage == 0.5f)
            CustomCursor.SetCursor(50);
        else if (_selectedPercentage == 0.75f)
            CustomCursor.SetCursor(75);
        else if (_selectedPercentage == 1f)
            CustomCursor.SetCursor(100);
    }
}
