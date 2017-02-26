using InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPrey
{
    /*class Player : SpriteAnimation
    {
        private Vector2 _speed;
        private Vector2 _direction;

        public Player()
        {
            _spritesheets["default"] = "player_idle";
            _spritesheets["up"] = "player_up";
            _spritesheets["right"] = "player_right";
            _spritesheets["down"] = "player_down";
            _spritesheets["left"] = "player_left";
            _animationFrames = 6;
            _position = new Vector2(400, 400);
            _speed = new Vector2(50, 50);
        }

        public override void Update(float time)
        {
            _direction = Vector2.Zero;

            if (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up))
                _direction.Y = -1;

            if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
                _direction.Y = 1;

            if ((Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left)))
                _direction.X = -1;

            if (Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right))
                _direction.X = 1;

            _velocity = _direction;
            _position += _velocity * _speed * (float)time;

            // Switch player animation based on its movement
            if (_velocity.X > 0)
                SetAnimation("right");
            else if (_velocity.X < 0)
                SetAnimation("left");
            else if (_velocity.Y < 0)
                SetAnimation("up");
            else if (_velocity.Y > 0)
                SetAnimation("down");
            else
                SetAnimation("default");

            // Execute common code in base (Sprite) class
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }*/
}
