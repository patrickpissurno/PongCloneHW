using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Player : GameObject
    {
        public int playerID;
        private float maxSpeed = 3;
        public Player(int id)
        {
            Name = "Player";
            this.playerID = id;
        }

        public override void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            switch (this.playerID)
            {
                case 0:
                    if (keyState.IsKeyDown(Keys.W))
                        this.Velocity.Y = -maxSpeed;
                    else if (keyState.IsKeyDown(Keys.S))
                        this.Velocity.Y = maxSpeed;
                    else
                        this.Velocity.Y = 0;
                    break;
                case 1:
                    if (keyState.IsKeyDown(Keys.Up))
                        this.Velocity.Y = -maxSpeed;
                    else if (keyState.IsKeyDown(Keys.Down))
                        this.Velocity.Y = maxSpeed;
                    else
                        this.Velocity.Y = 0;
                    break;
            }
            CheckWallCollision();
            base.Update();
        }

        private void CheckWallCollision()
        {
            if (Texture != null)
            {
                if (Position.Y + Velocity.Y < 0)
                    Position.Y = 0;
                else if (Position.Y + Texture.Height + Velocity.Y > Game1.screenHeight)
                    Position.Y = Game1.screenHeight - Texture.Height;
            }
        }
    }
}
