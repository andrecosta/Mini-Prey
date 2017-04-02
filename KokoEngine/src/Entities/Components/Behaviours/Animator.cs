using System.Collections.Generic;

namespace KokoEngine
{
    public class Animator : Behaviour
    {
        public bool IsPlaying => _isPlaying;
        public float Speed { get; set; } = 1;
        public bool Looping { get; set; } = true;

        private readonly Dictionary<string, AnimationClip> _animationClips = new Dictionary<string, AnimationClip>();
        private AnimationClip _currentAnimationClip;
        private ISpriteRenderer _sr;
        private IFSM _fsm;
        private bool _isPlaying;
        private float _animationTimer;
        private int _currentFrame;

        protected override void Awake()
        {
            _sr = GetComponent<ISpriteRenderer>();
            _fsm = GetComponent<IFSM>();
        }

        protected override void Update(float dt)
        {
            if (_currentAnimationClip == null || !_isPlaying)
                return;

            _animationTimer += dt;

            if (_animationTimer > Speed)
            {
                if (_currentFrame < _currentAnimationClip.NumFrames - 1)
                    _currentFrame++;
                else if (Looping)
                    _currentFrame = 0;
                else
                    Stop();

                _animationTimer = 0;
            }

            _sr.sprite = _currentAnimationClip.Sprites[_currentFrame];
        }

        public void AddAnimation(string key, AnimationClip clip)
        {
            _animationClips.Add(key, clip);
        }

        public void Play(string animationName)
        {
            Stop();
            _currentAnimationClip = _animationClips[animationName];
            _isPlaying = true;
        }

        public void Play(string animationName, float speed)
        {
            Speed = speed;
            Play(animationName);
        }

        public void Stop()
        {
            _currentFrame = 0;
            _animationTimer = 0;
            _isPlaying = false;
        }
    }
}