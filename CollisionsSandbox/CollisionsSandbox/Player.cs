using InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollisionsSandbox
{
    class Player : Sprite
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
            _speed = new Vector2(120, 100);
        }

        public override void Update(double time)
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

            // Switch player animation based on its movement
            if (_velocity.Length() > 0)
                SetAnimation("walking");
            else
                SetAnimation("default");

            // Execute common code in base (Sprite) class
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
