using System;
using System.Collections.Generic;
using System.Linq;
using KokoEngine;
using Random = KokoEngine.Random;

public class Planet : Behaviour
{
    public struct Type
    {
        public Upgrade[] UpgradeLevels;
    }

    public struct Upgrade
    {
        public ISprite Sprite;
        public int Cost;
        public float PopGenerationRate;
        public int PopGenerationLimit;
        public float FireRate;
        public float Range;
    }

    public GameController GameController;

    // Planet Properties
    public Player Owner;
    public int Population;
    
    //public UpgradeMenu UpgradeMenu;

    public Action<Planet, Planet> LaunchShipHandler { get; set; }
    public Action OnPopulationChange { get; set; }
    public bool IsHovered { get; set; }
    public bool IsUnderAttack { get; set; }
    public bool IsUpgrading { get; set; }
    public bool IsConverting { get; set; }

    public int AvailablePopulation => Population - _queuedToLaunch.Count;

    private List<Planet> _queuedToLaunch;
    private float _shipGenerationTimer;
    private float _fireTimer;
    private float _launchTimer;
    private float _upgradeTimer;
    private float _underAttackTimer;
    private ISpriteRenderer _sr;
    private ITextRenderer _text;
    private IAnimator _animator;


    public int CurrentType { get; set; }
    public int CurrentUpgradeLevel { get; set; }
    public Upgrade CurrentUpgrade { get { return GameController.PlanetTypes[CurrentType].UpgradeLevels[CurrentUpgradeLevel]; } }

    // Visual effects
    private ISpriteRenderer _planetOutline;
    private ISpriteRenderer _rangeOutline;

    private bool _pendingStructureUpgradeLevel;
    private float _hp = 1;

    private bool _isUpgradeMenuOpen;
    private int _targetType;

    protected override void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _text = GetComponent<TextRenderer>();
        _animator = GetComponent<Animator>();
        _queuedToLaunch = new List<Planet>();

        // Add outline visual elements
        _planetOutline = Instantiate<SpriteRenderer>("HoverOutline", Transform.Position);
        _planetOutline.Sprite = GameController.OutlineSprite;
        _planetOutline.Layer = 0.6f;
        _planetOutline.GameObject.SetActive(false);

        _rangeOutline = Instantiate<SpriteRenderer>("RangeOutline", Transform.Position);
        _rangeOutline.Sprite = GameController.RangeSprite;
        _rangeOutline.Color = new Color(38, 38, 38, 25);
        _rangeOutline.Layer = 0.61f;

        UpdateAppearance();

        OnPopulationChange += UpdatePopulationText;
        OnPopulationChange();
    }

    protected override void Update()
    {
        // Ship generation
        if (CurrentUpgrade.PopGenerationRate > 0 && Population < CurrentUpgrade.PopGenerationLimit)
        {
            _shipGenerationTimer += Time.DeltaTime;
            if (_shipGenerationTimer > CurrentUpgrade.PopGenerationRate)
            {
                Population++;
                _shipGenerationTimer = 0;

                OnPopulationChange?.Invoke();
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

        // Ship launch
        ProcessShipLaunch();

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

        // Animations
        if (!IsHovered)
            _planetOutline.Transform.Rotation += Time.DeltaTime * 0.25f;

        _rangeOutline.Transform.Rotation += Time.DeltaTime * 0.02f;
        /*_animator.SetBool("IsHovered", IsHovered);
        _animator.SetBool("IsUnderAttack", IsUnderAttack);
        _animator.SetBool("IsUpgrading", IsUpgrading);*/
    }

    private void Shoot()
    {
        foreach (var ship in GameController.Ships.Where(x => x.Owner != Owner && !x.IsBeingTargeted))
        {
            if (Vector2.Distance(ship.Transform.Position, Transform.Position) <= CurrentUpgrade.Range)
            {
                var shot = Instantiate<Shot>("Shot", Transform.Position);
                var sr = shot.AddComponent<SpriteRenderer>();
                var rb = shot.AddComponent<Rigidbody>();
                var v = shot.AddComponent<Vehicle>();
                var pursuit = shot.AddComponent<Pursuit>();

                sr.Sprite = GameController.ShotSprite;
                sr.Color = new Color(236, 196, 73);

                v.Behaviours.Add(pursuit);
                
                shot.Target = ship;
                ship.IsBeingTargeted = true;

                break;
            }
        }
    }

    void ProcessShipLaunch()
    {
        if (_queuedToLaunch.Count == 0)
            return;

        if (Population == 0)
        {
            _queuedToLaunch.Clear();
            return;
        }

        // Increment the timer
        _launchTimer += Time.DeltaTime;

        // When the timer is activated, launch ship (one for each different target)
        if (_launchTimer > Random.Range(0.05f, 0.15f))
        {
            // Get target
            foreach (var target in _queuedToLaunch.Distinct().ToList())
            {
                //Planet target = QueuedToLaunch[Random.Range(0, QueuedToLaunch.Count)];
                if (LaunchShipHandler != null)
                {
                    Population--;
                    UpdatePopulationText();
                    LaunchShipHandler(this, target);
                }
                _queuedToLaunch.Remove(target);
            }

            // Reset timer
            _launchTimer = 0;
        }
    }

    public void LaunchShips(Planet target, float percentage)
    {
        int n = (int)Math.Ceiling(AvailablePopulation * percentage);
        //Debug.Log("Launching " + n + " ships");
        for (int i = 0; i < n; i++)
            _queuedToLaunch.Add(target);
    }

    public void InsertShip(Ship ship)
    {
        if (ship.Owner != Owner)
        {
            IsUnderAttack = true;
            _underAttackTimer = 0;
            Population--;

            if (Population <= 0)
            {
                Population = 0;
                ConvertStructure(ship.Owner);
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
        _text.Text = Population.ToString();
    }

    void ConvertStructure(Player newOwner)
    {
        Owner = newOwner;
        _queuedToLaunch.Clear();
        //HideUpgradeMenu();

        _pendingStructureUpgradeLevel = true;
        DowngradeStructure();
        IsUpgrading = false;
        IsConverting = false;

        IsHovered = false;

        UpdateAppearance();
    }

    public void StartUpgrade()
    {
        Debug.Log("Start upgrade!");
        int cost = GameController.PlanetTypes[CurrentType].UpgradeLevels[CurrentUpgradeLevel + 1].Cost;
        if (AvailablePopulation < cost) return;

        IsUpgrading = true;
        _upgradeTimer = 0;

        Population -= cost;

        OnPopulationChange?.Invoke();

        //HideUpgradeMenu();
    }

    public void StartConversion(int newType)
    {
        int cost = GameController.PlanetTypes[newType].UpgradeLevels[0].Cost;
        if (AvailablePopulation < cost) return;

        Debug.Log("Start conversion!");
        IsUpgrading = true;
        IsConverting = true;
        _targetType = newType;
        _upgradeTimer = 0;

        Population -= cost;

        OnPopulationChange?.Invoke();

        //HideUpgradeMenu();
    }

    void UpgradeStructure()
    {
        if (CurrentUpgradeLevel == GameController.PlanetTypes[CurrentType].UpgradeLevels.Length - 1)
            return;

        CurrentUpgradeLevel++;
        IsUpgrading = false;

        UpdateAppearance();
    }

    void DowngradeStructure()
    {
        if (CurrentUpgradeLevel == 0)
            return;

        CurrentUpgradeLevel--;
        IsUpgrading = false;

        UpdateAppearance();
    }

    void ChangeStructureType(int newType)
    {
        if (newType < 0 || newType > GameController.PlanetTypes.Length - 1)
            return;

        CurrentType = newType;
        IsUpgrading = false;
        IsConverting = false;

        UpdateAppearance();
    }

    void UpdateAppearance()
    {
        _sr.Sprite = CurrentUpgrade.Sprite;
        _sr.Color = Owner.TeamColor;

        if (CurrentType == 0)
            _rangeOutline.GameObject.SetActive(false);
        else
        {
            _rangeOutline.GameObject.SetActive(true);
            _rangeOutline.Transform.Scale = Vector2.One * (CurrentUpgrade.Range * 2 / 253f);
        }
    }

    public void Hover()
    {
        _planetOutline.Color = new Color(230, 230, 230);
        _planetOutline.GameObject.SetActive(true);
        IsHovered = true;
    }

    public void UnHover()
    {
        _planetOutline.GameObject.SetActive(false);
        IsHovered = false;
    }

    public void Select()
    {
        _planetOutline.Color = new Color(236, 196, 73);
        _planetOutline.Transform.Rotation = 0;
        _planetOutline.GameObject.SetActive(true);
    }

    public void DeSelect()
    {
        _planetOutline.GameObject.SetActive(false);
    }

    /*public void ToggleUpgradeMenu()
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
    }*/
}
