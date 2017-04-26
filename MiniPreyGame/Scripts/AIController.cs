using System;
using System.Linq;
using KokoEngine;
using Random = KokoEngine.Random;

namespace MiniPreyGame
{
    class AIController : Player
    {
        public GameController GameController;
        private float _decisionTimer;

        void Start()
        {

        }

        void Update()
        {
            if (IsNeutral)
                return;

            _decisionTimer += Time.DeltaTime;

            // Make a decision every 8-15 seconds
            if (_decisionTimer > Random.Range(8, 20))
            {
                // Decide between offensive and defensive strategies
                if (Random.value >= 0.4f)
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

            float r = Random.value;

            // 15% chance to do nothing
            if (r < 0.15f)
            {
                Debug.Log("AI -- Did nothing...");
                return;
            }

            bool done = false;

            // TRY TO CAPTURE A NEUTRAL STRUCTURE FIRST
            // Find a neutral structure sorted by least pop
            // TODO: sort by distance also?
            bool neutralChoosen = false;
            foreach (var neutralStructure in GameController.Structures.Where(s => s.Owner.IsNeutral).OrderBy(s => s.AvailablePopulation))
            {
                // Find an owned structure sorted by most pop
                foreach (var structure in GameController.Structures.Where(s => s.Owner == this).OrderByDescending(s => s.AvailablePopulation))
                {
                    float percentage = 0.25f;
                    while (percentage <= 1)
                    {
                        // Try 25%, then 50%, then 75%, then 100%
                        if (structure.AvailablePopulation * percentage > neutralStructure.AvailablePopulation)
                        {
                            Debug.Log("AI -- Launched " + Mathf.CeilToInt(structure.AvailablePopulation * percentage) + " citizens (" +
                                      (int)percentage * 100 + ") to try to capture a neutral structure!");
                            LaunchCitizens(structure, neutralStructure, percentage);
                            return;
                        }
                        percentage += 0.25f;
                    }
                }
            }

            // TRY TO CAPTURE AN ENEMY STRUCTURE
            // Find an enemy structure sorted by least pop
            if (Random.value < 0.5f)
                return;
            foreach (var enemyStructure in GameController.Structures.Where(s => !s.Owner.IsNeutral && s.Owner != this).OrderBy(s => s.AvailablePopulation))
            {
                // Find an owned structure sorted by most pop
                foreach (var structure in GameController.Structures.Where(s => s.Owner == this).OrderByDescending(s => s.AvailablePopulation))
                {
                    float percentage = 0.25f;
                    while (percentage <= 1)
                    {
                        // Try 25%, then 50%, then 75%, then 100%
                        if (structure.AvailablePopulation * percentage > enemyStructure.AvailablePopulation)
                        {
                            Debug.Log("AI -- Launched " + Mathf.CeilToInt(structure.AvailablePopulation * percentage) + " citizens (" +
                                      (int)percentage * 100 + ") to try to capture an enemy structure!");
                            LaunchCitizens(structure, enemyStructure, percentage);
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

            float r = Random.value;

            // 15% chance to do nothing
            if (r < 0.15f)
            {
                Debug.Log("AI -- Did nothing...");
                return;
            }

            // Check convert
            if (r < 0.5f)
            {
                foreach (var structure in GameController.Structures.Where(s => s.Owner == this).OrderByDescending(s => s.AvailablePopulation))
                {
                    // Skip if max upgraded
                    if (structure.CurrentUpgradeLevel == structure.Types[structure.CurrentType].UpgradeLevels.Length - 1)
                        continue;

                    if (structure.AvailablePopulation >= structure.Types[structure.CurrentType].UpgradeLevels[structure.CurrentUpgradeLevel + 1].Cost)
                    {
                        structure.StartUpgrade();
                        break;
                    }
                }
            }

            // REDISTRIBUTIVE STRATEGY
            // Find an owned structure sorted by most pop
            foreach (var structure in GameController.Structures.Where(s => s.Owner == this).OrderByDescending(s => s.AvailablePopulation))
            {
                // Find another owned structure sorted by least pop
                foreach (var targetStructure in GameController.Structures.Where(s => s.Owner == this).OrderBy(s => s.AvailablePopulation))
                {
                    // Exclude same structure (obviously)
                    if (targetStructure == structure)
                        continue;

                    // Skip structures with room to generate more population
                    if (targetStructure.Population < targetStructure.CurrentUpgrade.PopGenerationLimit)
                        continue;

                    float amountToReinforce = structure.AvailablePopulation * 0.75f;
                    if (targetStructure.AvailablePopulation + amountToReinforce < structure.AvailablePopulation - amountToReinforce)
                    {
                        Debug.Log("AI -- Launched " + Mathf.CeilToInt(amountToReinforce) + " citizens (75%) to reinforce a friendly structure!");
                        LaunchCitizens(structure, targetStructure, 0.75f);
                        return;
                    }

                    amountToReinforce = structure.AvailablePopulation * 0.5f;
                    if (targetStructure.AvailablePopulation + amountToReinforce < structure.AvailablePopulation - amountToReinforce)
                    {
                        Debug.Log("AI -- Launched " + Mathf.CeilToInt(amountToReinforce) + " citizens (50%) to reinforce a friendly structure!");
                        LaunchCitizens(structure, targetStructure, 0.5f);
                        return;
                    }

                    // Random skip
                    if (Random.value < 0.5f)
                        continue;

                    amountToReinforce = structure.AvailablePopulation * 0.25f;
                    Debug.Log("AI -- Launched " + Mathf.CeilToInt(amountToReinforce) + " citizens (25%) to reinforce a friendly structure!");
                    LaunchCitizens(structure, targetStructure, 0.25f);
                    return;
                }
            }
        }

        void LaunchCitizens(Structure source, Structure target, float percentage)
        {
            source.LaunchCitizens(target, percentage);
        }
    }
}
