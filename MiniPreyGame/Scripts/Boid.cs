using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KokoEngine;

namespace MiniPreyGame
{
    class Boid : Script
    {
        public float Speed = 5;
        public GameObject Target;

        private Rigidbody _rb;

        public override void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public override void Start()
        {
        }

        public override void Update(float dt)
        {
            Vector3 dir = (Target.Transform.Position - Transform.Position).Normalized;

            _rb.AddForce(dir * Speed);

            Transform.Rotation = (float) Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);

            foreach (Rigidbody rb in Rigidbody.All)
            {
                Vector3 diff = rb.Transform.Position - Transform.Position;
                /*Debug.Log("Transform: " + Transform.position.ToString());
                Debug.Log("Other Transform: " + rb.Transform.position.ToString());
                Debug.Log("Diff: " + diff.ToString());*/
                if (Vector3.Magnitude(diff) < 50)
                {
                    if (rb.GetComponent<PlayerController>() != null)
                        continue;

                    _rb.AddForce(-diff.Normalized * Speed * 0.2f);
                }
            }

            Debug.Track(GameObject);
        }
    }
}
