using KokoEngine;

class PlayerController : Behaviour
{
    public float Speed { get; set; } = 3;

    private IRigidbody _rb;

    protected override void Awake()
    {
        _rb = GetComponent<IRigidbody>();

        // Set the player's position to the centre of the screen
        Transform.Position = new Vector3(Screen.Width / 2f, Screen.Height / 2f);
    }

    protected override void Update(float dt)
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(-x, -y);
        
        //_rb.AddForce(dir * Speed);
        Transform.Position += new Vector3(dir.X, dir.Y) * 3;

        // Continuous rotation effect
        if (Input.GetAction("Fire"))
        Transform.Rotation += dt;
    }
}
