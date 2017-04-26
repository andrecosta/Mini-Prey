using System;
using System.Collections.Generic;
using System.Linq;
using KokoEngine;

namespace MiniPreyGame
{
    class GameController : Behaviour
    {
        public Citizen CitizenPrefab;
        public Structure StructurePrefab;
        public GameObject StructureSpawns;
        public Player[] Players;

        public static GameController Instance { get; private set; }
        public List<Structure> Structures { get; private set; }
        public List<Citizen> Citizens { get; private set; }
        public Dictionary<Player, int> PlayerPopulations { get; set; }
        public int TotalPopulation { get { return PlayerPopulations.Values.Sum(); } }

        // Callbacks
        public Action<Structure> OnStructureCreated { get; set; }
        public Action<Structure> OnStructureUpgraded { get; set; }
        public Action<Structure> OnStructureAttacked { get; set; }
        public Action<Structure, int> OnStructureCaptured { get; set; }
        public Action<Citizen> OnCitizenCreated { get; set; }
        public Action<Citizen> OnCitizenLaunched { get; set; }
        public Action<Citizen> OnCitizenKilled { get; set; }

        public Action<Structure> OnStructureHovered { get; set; }
        public Action<Structure> OnStructureSelected { get; set; }

        public Action OnPopulationChange { get; set; }

        //public Action<Structure, Structure> OnCitizenLaunched { get; set; }

        void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            Structures = new List<Structure>();
            Citizens = new List<Citizen>();
            PlayerPopulations = new Dictionary<Player, int>();
            foreach (var player in Players)
            {
                PlayerPopulations.Add(player, 0);
            }
        }

        void Start()
        {
            //var structure = new City(10);
            //Structures.Add(structure);
            //OnStructureCreated(structure);

            foreach (var spawn in StructureSpawns.GetComponentsInChildren<StructureSpawn>())
            {
                Structure structure = Instantiate(StructurePrefab, spawn.transform.position, 0);
                structure.Owner = spawn.Player;
                structure.CurrentType = spawn.StructureType;
                structure.CurrentUpgradeLevel = spawn.StructureUpgradeLevel;
                structure.Population = spawn.Population;
                structure.GameController = this;

                structure.LaunchCitizenHandler += LaunchCitizen;
                structure.OnPopulationChange += UpdateTotalPopulation;

                Structures.Add(structure);
                structure.GameObject.SetActive(true);
                structure.Init();

                if (OnStructureCreated != null)
                    OnStructureCreated(structure);
            }

            // foreach structure location
            // Create neutral structures


            // Create player structures
            // foreach team
        }

        void Update()
        {
            /*foreach (var structure in Structures)
            {
                structure.Update(Time.deltaTime);
            }*/
        }

        public void LaunchCitizen(Structure source, Structure target)
        {
            var citizen = Instantiate(CitizenPrefab, source.transform.position, 0);
            citizen.transform.SetParent(Transform);
            citizen.GameController = this;
            citizen.Owner = source.Owner;
            citizen.SetTarget(target);
            citizen.Source = source;
            //Citizens.Add(citizen);
        }

        void UpdateTotalPopulation()
        {
            foreach (var player in PlayerPopulations.Keys.ToList())
            {
                var p = player;

                // Reset player populations
                PlayerPopulations[p] = 0;

                // Find population inside structures
                foreach (var structure in Structures.Where(s => s.Owner == p))
                    PlayerPopulations[p] += structure.Population;

                // Find population on the field
                int ownedCitizens = FindObjectsOfType<Citizen>().Count(c => c.Owner == p);
                PlayerPopulations[p] += ownedCitizens;
            }

            OnPopulationChange?.Invoke();
        }

        public void Quit()
        {
            //Application.Quit();
        }
    }
}
