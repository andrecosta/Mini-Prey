using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MiniPrey
{
    class SpriteAnimation
    {
        protected static Random random = new Random();

        //protected Vector2 _position;                        // » Sprite position coordinates
        //protected Vector2 _velocity;                        // » Velocity of the sprite
        // Images
        protected Dictionary<string, string> _spritesheets; // » Name of the the spritesheet image resource(s)
        protected Dictionary<string, Texture2D> _animations;// » Loaded spritesheet textures
        protected string _activeAnimation;                  // » Name of currently active animation
        // Animation
        protected int _animationFrames;                     // » Number of animation frames for this sprite (1 means no animation)
        protected int _currentFrame;                        // » Current frame of the sprite animation
        protected double _animationTimer;                    // » Used to control the animation cycle speed
        // Draw variables
        protected Rectangle _sourceRectangle;               // » Area of the image from where the texture will be loaded
        protected Color _color;
        protected float _rotation;
        protected Vector2 _origin;
        protected float _size;
        protected SpriteEffects _spriteEffects;
        protected float _layerDepth;

        // Properties
        public Vector2 Position { get; set; }
        //public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
        public Texture2D Image { get { return _animations[_activeAnimation]; } set { _animations["default"] = value; } }

        // Default constructor
        public SpriteAnimation()
        {
            _spritesheets = new Dictionary<string, string>();
            _animations = new Dictionary<string, Texture2D>();
            //_velocity = Vector2.Zero;
            _color = Color.White;
            _rotation = 0;
            _origin = Vector2.Zero;
            _size = 1;
            _spriteEffects = SpriteEffects.None;
            _layerDepth = 0;
            _animationFrames = 1; // » If no object sets this number to greater than 1, it means it's a single image (or a "one frame spritesheet")
            _currentFrame = 0;
        }
        // Constructor with position
        public SpriteAnimation(Vector2 position)
            : this()
        {
            //_position = position;
        }

        // Methods
        public virtual void Load(ContentManager content)
        {
            foreach (KeyValuePair<string, string> imageName in _spritesheets)
                _animations[imageName.Key] = content.Load<Texture2D>(imageName.Value);

            if (string.IsNullOrEmpty(_activeAnimation))
                SetAnimation("default");
            else
                SetAnimation(_activeAnimation);
        }

        public virtual void Update(float time)
        {
            // Update the sprite's position
            //_position += _velocity * time;

            // Animate the sprite
            Animate(time);
        }

        public virtual void Draw(SpriteBatch sb)
        {
            if (Position != Vector2.Zero)
                sb.Draw(_animations[_activeAnimation], Position, _sourceRectangle, _color, _rotation, _origin, _size, _spriteEffects, _layerDepth);
        }

        protected void Animate(double time)
        {
            _animationTimer += time;
            if (_animationTimer > 0.15)
            {
                if (_currentFrame < _animationFrames - 1)
                    _currentFrame++;
                else
                    _currentFrame = 0;
                _animationTimer = 0;
            }
            _sourceRectangle.X = _currentFrame * _sourceRectangle.Width;
        }

        protected void SetAnimation(string animationName)
        {
            _activeAnimation = animationName;
            _sourceRectangle = new Rectangle(0, 0, _animations[_activeAnimation].Width / _animationFrames, _animations[_activeAnimation].Height);
        }

        public void AddSpritesheet(string name, string animation)
        {
            _spritesheets.Add(name, animation);
        }
    }
}
