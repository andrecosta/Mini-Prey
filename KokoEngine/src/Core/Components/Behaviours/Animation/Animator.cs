using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public class Animator : Behaviour, IAnimator
    {
        public bool IsPlaying => _isPlaying;
        public float Speed { get; set; } = 1;
        public bool Looping { get; set; } = true;

        private readonly Dictionary<string, IAnimationClip> _animationClips = new Dictionary<string, IAnimationClip>();
        private IAnimationClipInternal _currentAnimationClip;
        private ISpriteRenderer _sr;
        private bool _isPlaying;
        private float _animationTimer;
        private int _currentFrame;

        protected override void Awake()
        {
            _sr = GetComponent<ISpriteRenderer>();

            foreach (var animationClip in _animationClips.Values)
            {
                (animationClip as IAnimationClipInternal)?.CreateSpritesheet();
            }
        }

        protected override void Update()
        {
            if (_currentAnimationClip == null || !_isPlaying)
                return;

            _animationTimer += Time.DeltaTime;

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

            _sr.Sprite = _currentAnimationClip.Sprites[_currentFrame];
        }

        public void AddAnimation(string key, IAnimationClip clip)
        {
            _animationClips.Add(key, clip);
        }

        public void Play(string animationName)
        {
            Stop();
            _currentAnimationClip = _animationClips[animationName] as IAnimationClipInternal;
            _sr.Sprite = _currentAnimationClip?.Sprites[_currentFrame];
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