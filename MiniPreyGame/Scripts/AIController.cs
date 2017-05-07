using System;
using System.Linq;
using KokoEngine;
using Random = KokoEngine.Random;

public class AIController : Player
{
    public GameController GameController;
    private float _decisionTimer;

    protected override void Start()
    {

    }

    protected override void Update()
    {
        if (IsNeutral)
            return;

        _decisionTimer += Time.DeltaTime;

        // Make a decision every 8-15 seconds
        if (_decisionTimer > Random.Range(8, 20))
        {
            // Decide between offensive and defensive strategies
            if (Random.Value >= 0.4f)
            {
                // Do an offensive maneuver between 1-2 times at once
                for (int i = 0; i < Random.Range(1, 3); i++)
                    OffensiveStrategy();
            }
            else
            {
                // Do a defensive maneuver between 1-3 times at once
                for (int i = 0; i < Random.Range(1, 4); i++)
                    DefensiveStrategy();
            }

            _decisionTimer = 0;
        }
    }

    void OffensiveStrategy()
    {
        Debug.Log("AI -------- OFFENSIVE STRATEGY --------");

        float r = Random.Value;

        // 15% chance to do nothing
        if (r < 0.15f)
        {
            Debug.Log("AI -- Did nothing...");
            return;
        }

        bool done = false;

        // TRY TO CAPTURE A NEUTRAL PLANET FIRST
        // Find a neutral planet sorted by least pop
        // TODO: sort by distance also?
        bool neutralChoosen = false;
        foreach (var neutralPlanet in GameController.Planets.Where(s => s.Owner.IsNeutral).OrderBy(s => s.AvailablePopulation))
        {
            // Find an owned planet sorted by most pop
            foreach (var planet in GameController.Planets.Where(s => s.Owner == this).OrderByDescending(s => s.AvailablePopulation))
            {
                float percentage = 0.25f;
                while (percentage <= 1)
                {
                    // Try 25%, then 50%, then 75%, then 100%
                    if (planet.AvailablePopulation * percentage > neutralPlanet.AvailablePopulation)
                    {
                        Debug.Log("AI -- Launched " + (int)Math.Ceiling(planet.AvailablePopulation * percentage) + " ships (" +
                                    (int)percentage * 100 + ") to try to capture a neutral planet!");
                        LaunchShips(planet, neutralPlanet, percentage);
                        return;
                    }
                    percentage += 0.25f;
                }
            }
        }

        // TRY TO CAPTURE AN ENEMY PLANET
        // Find an enemy planet sorted by least pop
        if (Random.Value < 0.5f)
            return;
        foreach (var enemyPlanet in GameController.Planets.Where(s => !s.Owner.IsNeutral && s.Owner != this).OrderBy(s => s.AvailablePopulation))
        {
            // Find an owned planet sorted by most pop
            foreach (var planet in GameController.Planets.Where(s => s.Owner == this).OrderByDescending(s => s.AvailablePopulation))
            {
                float percentage = 0.25f;
                while (percentage <= 1)
                {
                    // Try 25%, then 50%, then 75%, then 100%
                    if (planet.AvailablePopulation * percentage > enemyPlanet.AvailablePopulation)
                    {
                        Debug.Log("AI -- Launched " + (int)Math.Ceiling(planet.AvailablePopulation * percentage) + " ships (" +
                                    (int)percentage * 100 + ") to try to capture an enemy planet!");
                        LaunchShips(planet, enemyPlanet, percentage);
                        return;
                    }
                    percentage += 0.25f;
                }
            }
        }
    }

    void DefensiveStrategy()
    {
        Debug.Log("AI -------- DEFENSIVE STRATEGY --------");

        float r = Random.Value;

        // 15% chance to do nothing
        if (r < 0.15f)
        {
            Debug.Log("AI -- Did nothing...");
            return;
        }

        // Check convert
        if (r < 0.5f)
        {
            foreach (var planet in GameController.Planets.Where(s => s.Owner == this).OrderByDescending(s => s.AvailablePopulation))
            {
                // Skip if max upgraded
                if (planet.CurrentUpgradeLevel == GameController.PlanetTypes[planet.CurrentType].UpgradeLevels.Length - 1)
                    continue;

                if (planet.AvailablePopulation >= GameController.PlanetTypes[planet.CurrentType].UpgradeLevels[planet.CurrentUpgradeLevel + 1].Cost)
                {
                    planet.StartUpgrade();
                    break;
                }
            }
        }

        // REDISTRIBUTIVE STRATEGY
        // Find an owned planet sorted by most pop
        foreach (var planet in GameController.Planets.Where(s => s.Owner == this).OrderByDescending(s => s.AvailablePopulation))
        {
            // Find another owned planet sorted by least pop
            foreach (var targetPlanet in GameController.Planets.Where(s => s.Owner == this).OrderBy(s => s.AvailablePopulation))
            {
                // Exclude same planet (obviously)
                if (targetPlanet == planet)
                    continue;

                // Skip planets with room to generate more population
                if (targetPlanet.Population < targetPlanet.CurrentUpgrade.PopGenerationLimit)
                    continue;

                float amountToReinforce = planet.AvailablePopulation * 0.75f;
                if (targetPlanet.AvailablePopulation + amountToReinforce < planet.AvailablePopulation - amountToReinforce)
                {
                    Debug.Log("AI -- Launched " + (int)Math.Ceiling(amountToReinforce) + " ships (75%) to reinforce a friendly planet!");
                    LaunchShips(planet, targetPlanet, 0.75f);
                    return;
                }

                amountToReinforce = planet.AvailablePopulation * 0.5f;
                if (targetPlanet.AvailablePopulation + amountToReinforce < planet.AvailablePopulation - amountToReinforce)
                {
                    Debug.Log("AI -- Launched " + (int)Math.Ceiling(amountToReinforce) + " ships (50%) to reinforce a friendly planet!");
                    LaunchShips(planet, targetPlanet, 0.5f);
                    return;
                }

                // Random skip
                if (Random.Value < 0.5f)
                    continue;

                amountToReinforce = planet.AvailablePopulation * 0.25f;
                Debug.Log("AI -- Launched " + (int)Math.Ceiling(amountToReinforce) + " ships (25%) to reinforce a friendly planet!");
                LaunchShips(planet, targetPlanet, 0.25f);
                return;
            }
        }
    }

    void LaunchShips(Planet source, Planet target, float percentage)
    {
        source.LaunchShips(target, percentage);
    }
}
