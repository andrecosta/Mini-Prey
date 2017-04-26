using System.Collections;
using System.Collections.Generic;
using KokoEngine;

namespace MiniPreyGame
{

    public class Citizen : Behaviour
    {
        public GameController GameController;
        public Player Owner;
        public float Speed = 1;

        public Structure Source { get; set; }
        public Structure Target { get; private set; }

        private Rigidbody _rb;
        private Vector3 _velocity;
        private float _randDeviation;
        private float _timer;

        void Start()
        {
            if (GameController)
                GameController.Citizens.Add(this);
            _rb = GetComponent<Rigidbody>();
            _randDeviation = Random.Range(0f, 1f);
            //_timer = _randDeviation;
            //    GetComponent<Animator>().enabled = false;
        }

        void Update()
        {
            //_timer -= Time.deltaTime;

            //if (_timer < 0)
            //    GetComponent<Animator>().enabled = true;

            _velocity = Vector3.zero;

            if (Target)
            {
                Vector3 dir = (Target.transform.position - transform.position).normalized;
                //transform.Translate(dir * Speed * Time.deltaTime);
                _velocity = dir * Speed * Time.deltaTime;
            }
        }

        void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _velocity);
        }

        public void SetTarget(Structure target)
        {
            Target = target;
        }

        void OnDisable()
        {
            GameController.Citizens.Remove(this);
        }

        public void Kill()
        {
            Source.Population--;
            Destroy(gameObject);
        }

        /*void OnTriggerEnter(Collider other)
        {
            var shot = other.transform.GetComponent<Shot>();
            if (shot)
            {
                Debug.Log("Trigger");
                Kill();
            }
        }*/
    }
}
