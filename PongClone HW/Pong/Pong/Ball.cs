using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Ball : GameObject
    {
        public Random random = new Random();
        public List<Player> players;

        public Ball()
        {
            Name = "Ball";
        }

        public override void Start()
        {
            base.Start();

            players = GameObject.Find<Player>("Player");

            Launch(2f);
        }

        public override void Update()
        {
            //Velocity *= 1.001f;
            CheckWallCollision();
            CheckPlayerCollision();
            //Update the object
            base.Update();
        }

        private void Launch(float speed)
        {
            Position = new Vector2(Game1.screenWidth / 2 - Texture.Width / 2, Game1.screenHeight / 2 - Texture.Height / 2);
            float rotation = (float) (Math.PI/2 + (random.NextDouble() * (Math.PI/1.5f) - Math.PI/3));
            Velocity.X = (float)Math.Sin(rotation) * speed;
            Velocity.Y = (float)Math.Cos(rotation) * speed;

            if (random.Next(2) == 1)
                Velocity.X *= -1;
            if (random.Next(2) == 1)
                Velocity.Y *= -1;
        }

        private void CheckPlayerCollision()
        {
            if (players != null)
            {
                foreach (Player player in players)
                {
                    if (player != null)
                    {
                        if (player.CheckCollision(this))
                        {
                            //Edges
                            if (Position.Y + Texture.Height < player.Position.Y + player.Texture.Height / 8)
                            {
                                Velocity.Y = -Math.Abs(Velocity.Y);
                                Velocity.Y += player.Velocity.Y;
                                if ((player.playerID == 0 && Position.X > player.Position.X + player.Texture.Width * 0.5) || (player.playerID == 1 && Position.X + Texture.Width < player.Position.X + player.Texture.Width * 0.5))
                                {
                                    Velocity.X *= -1;
                                    Velocity.X += Math.Sign(Velocity.X) * GameObject.RandomRange(2, 10) / 3;
                                    Position.X = player.Position.X + Math.Sign(Velocity.X) * player.Texture.Width;
                                }
                            }
                            else if (Position.Y > player.Position.Y + player.Texture.Height * 0.875f)
                            {
                                Velocity.Y = +Math.Abs(Velocity.Y);
                                Velocity.Y += player.Velocity.Y;
                                if ((player.playerID == 0 && Position.X > player.Position.X + player.Texture.Width * 0.5) || (player.playerID == 1 && Position.X + Texture.Width < player.Position.X + player.Texture.Width * 0.5))
                                {
                                    Velocity.X *= -1;
                                    Velocity.X += Math.Sign(Velocity.X) * GameObject.RandomRange(2, 10) / 3;
                                    Position.X = player.Position.X + Math.Sign(Velocity.X) * player.Texture.Width;
                                }
                            }
                            //Normal
                            else
                            {
                                Velocity.X *= -1;
                                Position.X = player.Position.X + Math.Sign(Velocity.X) * player.Texture.Width;
                            }
                            Game1.sound2.Play();
                        }
                    }
                }
            }
        }

        private void CheckWallCollision()
        {
            if (Position.X < 0)
            {
                Game1.score[1]++;
                Launch(2f);
            }
            else if (Position.X + Texture.Width > Game1.screenWidth)
            {
                Game1.score[0]++;
                Launch(2f);
            }
            if (Position.Y < 0 || Position.Y + Texture.Height > Game1.screenHeight)
            {
                Velocity.Y *= -1;
                Game1.sound1.Play();
            }
        }
    }
}
