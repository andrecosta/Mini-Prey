using System;
using KokoEngine;

class Shot : Behaviour
{
    public Ship Target { get; set; }
    public AudioClip ShipShotSound { get; set; }

    private IRigidbody _rb;
    private IAudioSource _au;
    private IVehicle _vehicle;
    private Pursuit _pursuit;

    protected override void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _au = GetComponent<AudioSource>();
        _vehicle = GetComponent<Vehicle>();
        _pursuit = GetComponent<Pursuit>();
        _pursuit.target = Target.GetComponent<Vehicle>();
        _vehicle.MaxSpeed = 130;

        Transform.Rotation = (float)Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);
    }

    protected override void Update()
    {
        Transform.Rotation = (float)Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);
        
        DrawPursuitLine();

        if (!Target.Enabled)
            Destroy(GameObject);

        if (Vector3.Distance(Transform.Position, Target.Transform.Position) <= 5)
        {
            Target.Kill();
            _au.Play(ShipShotSound);
            Destroy(GameObject);
        }
    }

    private void DrawPursuitLine()
    {
        Vector3 distanceToTarget = Target.Transform.Position - Transform.Position;
        float t = distanceToTarget.Magnitude / _vehicle.MaxSpeed;
        Vector3 futurePosition = Target.Transform.Position + _pursuit.target.Velocity * t;

        Debug.DrawLine(Transform.Position, futurePosition, Color.Red, 2);
    }
}
