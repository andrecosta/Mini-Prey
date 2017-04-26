using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KokoEngine;

namespace MiniPreyGame
{
    public class Structure : Behaviour
    {
        [Serializable]
        public struct Type
        {
            public Upgrade[] UpgradeLevels;
        }

        [Serializable]
        public struct Upgrade
        {
            public GameObject ModelPrefab;
            public int Cost;
            public float PopGenerationRate;
            public int PopGenerationLimit;
            public float Defense;
            public float FireRate;
        }

        public GameController GameController;

        // Structure Properties
        public Player Owner;
        public int Population;
        public GameObject Model;

        // Types & Upgrades
        public Type[] Types;

        // Visual
        public GameObject HoverOutline;
        public GameObject SelectedOutline;
        //public ParticleSystem ConvertedEffect;
        public Shot ShotPrefab;

        //public UpgradeMenu UpgradeMenu;

        public Action<Structure, Structure> LaunchCitizenHandler { get; set; }
        public Action OnPopulationChange { get; set; }
        public bool IsHovered { get; set; }
        public bool IsUnderAttack { get; set; }
        public bool IsUpgrading { get; set; }
        public bool IsConverting { get; set; }

        public int AvailablePopulation { get { return Population - _queuedToLaunch.Count; } }

        private List<Structure> _queuedToLaunch;
        private float _citizenGenerationTimer;
        private float _fireTimer;
        private float _launchTimer;
        private float _upgradeTimer;
        private float _underAttackTimer;
        //private TextMeshPro _text;
        private Animator _animator;


        public int CurrentType { get; set; }
        public int CurrentUpgradeLevel { get; set; }
        public Upgrade CurrentUpgrade { get { return Types[CurrentType].UpgradeLevels[CurrentUpgradeLevel]; } }

        //private Material OldOwnerMaterial;
        //private Material NewOwnerMaterial;

        //private Renderer[] _modelRenderers;
        private bool _pendingStructureUpgradeLevel;
        private float _hp = 1;

        private bool _isUpgradeMenuOpen;
        private int _targetType;

        void Awake()
        {
            //_text = GetComponentInChildren<TextMeshPro>();
            _animator = GetComponent<Animator>();
            _queuedToLaunch = new List<Structure>();
        }

        public void Init()
        {
            //OldOwnerMaterial = Owner.TeamMaterial;
            ReplaceModel();
            OnPopulationChange += UpdatePopulationText;
            OnPopulationChange();
        }

        protected override void Update()
        {
            // Citizen generation
            if (CurrentUpgrade.PopGenerationRate > 0 && Population < CurrentUpgrade.PopGenerationLimit)
            {
                _citizenGenerationTimer += Time.DeltaTime;
                if (_citizenGenerationTimer > CurrentUpgrade.PopGenerationRate)
                {
                    Population++;
                    _citizenGenerationTimer = 0;

                    if (OnPopulationChange != null)
                        OnPopulationChange();
                }
            }

            // Fire
            if (CurrentUpgrade.FireRate > 0)
            {
                _fireTimer += Time.DeltaTime;
                if (_fireTimer > CurrentUpgrade.FireRate)
                {
                    Shoot();
                    _fireTimer = 0;
                }
            }

            // Citizen launch
            ProcessCitizenLaunch();

            if (_underAttackTimer > 0.3f)
                IsUnderAttack = false;
            else
                _underAttackTimer += Time.DeltaTime;

            // Upgrade
            if (IsUpgrading && _upgradeTimer > 10)
            {
                if (IsConverting)
                    ChangeStructureType(_targetType);
                else
                    UpgradeStructure();

                _upgradeTimer = 0;
            }
            else
                _upgradeTimer += Time.DeltaTime;

            // Animation
            _animator.SetBool("IsHovered", IsHovered);
            _animator.SetBool("IsUnderAttack", IsUnderAttack);
            _animator.SetBool("IsUpgrading", IsUpgrading);
        }

        private void Shoot()
        {
            foreach (var citizen in GameController.Citizens.Where(x => x.Owner != Owner))
            {
                if ((citizen.transform.position - Transform.Position).SqrMagnitude < 10 * 10)
                {
                    var shot = Instantiate(ShotPrefab, Transform.position + Transform.up, Quaternion.identity);
                    shot.Target = citizen;
                    break;
                }
            }
        }

        void ProcessCitizenLaunch()
        {
            if (_queuedToLaunch.Count == 0)
                return;

            if (Population == 0)
            {
                _queuedToLaunch.Clear();
                return;
            }

            // Increment the timer
            _launchTimer += Time.deltaTime;

            // When the timer is activated, launch citizen (one for each different target)
            if (_launchTimer > Random.Range(0.05f, 0.5f))
            {
                // Get target
                foreach (var target in _queuedToLaunch.Distinct().ToList())
                {
                    //Structure target = QueuedToLaunch[Random.Range(0, QueuedToLaunch.Count)];
                    if (LaunchCitizenHandler != null)
                    {
                        Population--;
                        UpdatePopulationText();
                        LaunchCitizenHandler(this, target);
                    }
                    _queuedToLaunch.Remove(target);
                }

                // Reset timer
                _launchTimer = 0;
            }
        }

        public void LaunchCitizens(Structure target, float percentage)
        {
            int n = Mathf.CeilToInt(AvailablePopulation * percentage);
            //Debug.Log("Launching " + n + " citizens");
            for (int i = 0; i < n; i++)
                _queuedToLaunch.Add(target);
        }

        public void InsertCitizen(Citizen citizen)
        {
            if (citizen.Owner != Owner)
            {
                IsUnderAttack = true;
                _underAttackTimer = 0;
                Population--;
                //if (OnPopulationChange != null)
                //    OnPopulationChange();
                if (Population <= 0)
                {
                    Population = 0;
                    //Owner = citizen.Owner;
                    ConvertStructure(citizen.Owner);
                }
            }
            else
            {
                Population++;
            }

            OnPopulationChange();
        }

        void UpdatePopulationText()
        {
            _text.text = Population.ToString();
        }

        void ConvertStructure(Player newOwner)
        {
            Owner = newOwner;
            //Population = 0;
            _queuedToLaunch.Clear();
            HideUpgradeMenu();

            _pendingStructureUpgradeLevel = true;
            DowngradeStructure();
            IsUpgrading = false;
            IsConverting = false;

            IsHovered = false;

            NewOwnerMaterial = newOwner.TeamMaterial;
            _animator.SetTrigger("Convert");
        }

        public void StartUpgrade()
        {
            Debug.Log("Start upgrade!");
            int cost = Types[CurrentType].UpgradeLevels[CurrentUpgradeLevel + 1].Cost;
            if (AvailablePopulation < cost) return;

            IsUpgrading = true;
            _upgradeTimer = 0;


            //int nPop = _queuedToLaunch.Count;
            Population -= cost;

            if (OnPopulationChange != null)
                OnPopulationChange();

            HideUpgradeMenu();
        }

        public void StartConversion(int newType)
        {
            int cost = Types[newType].UpgradeLevels[0].Cost;
            if (AvailablePopulation < cost) return;

            Debug.Log("Start conversion!");
            IsUpgrading = true;
            IsConverting = true;
            _targetType = newType;
            _upgradeTimer = 0;

            //int nPop = _queuedToLaunch.Count;
            Population -= cost;

            if (OnPopulationChange != null)
                OnPopulationChange();

            HideUpgradeMenu();
        }

        void UpgradeStructure()
        {
            if (CurrentUpgradeLevel == Types[CurrentType].UpgradeLevels.Length - 1)
                return;

            CurrentUpgradeLevel++;
            IsUpgrading = false;

            if (!_pendingStructureUpgradeLevel)
                ReplaceModel();
        }

        void DowngradeStructure()
        {
            if (CurrentUpgradeLevel == 0)
                return;

            CurrentUpgradeLevel--;
            IsUpgrading = false;

            if (!_pendingStructureUpgradeLevel)
                ReplaceModel();
        }

        void ReplaceModel()
        {
            // Destroy current model
            var currentModel = Model.transform.GetChild(0).gameObject;
            if (currentModel)
                Destroy(currentModel);

            // Instantiate the new one
            var newModel = Types[CurrentType].UpgradeLevels[CurrentUpgradeLevel].ModelPrefab;
            var go = Instantiate(newModel, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(Model.transform, false);

            // Update renderer reference
            _modelRenderers = go.GetComponentsInChildren<MeshRenderer>();
            foreach (var r in _modelRenderers.Where(x => x.sharedMaterial.name != "Default-Material"))
            {
                r.material = OldOwnerMaterial;
            }
        }

        void ChangeStructureType(int newType)
        {
            if (newType < 0 || newType > Types.Length - 1)
                return;

            CurrentType = newType;
            IsUpgrading = false;
            IsConverting = false;

            if (!_pendingStructureUpgradeLevel)
                ReplaceModel();
        }

        public void PlayParticleEffect()
        {
            ConvertedEffect.Play();
        }

        public void LerpMaterial(float t)
        {
            if (t == 0.5f)
            {
                ReplaceModel();
                _pendingStructureUpgradeLevel = false;
            }
            foreach (var r in _modelRenderers.Where(x => x.sharedMaterial.name != "Default-Material"))
                r.material.Lerp(OldOwnerMaterial, NewOwnerMaterial, t);
            if (t >= 1)
                OldOwnerMaterial = NewOwnerMaterial;
        }

        public void OnAnimationFinished()
        {
            //OldOwnerMaterial = NewOwnerMaterial;
        }

        public void ToggleUpgradeMenu()
        {
            UpgradeMenu.Toggle();
        }

        public void ShowUpgradeMenu()
        {
            UpgradeMenu.Show();
        }

        public void HideUpgradeMenu()
        {
            UpgradeMenu.Hide();
        }
    }
}
