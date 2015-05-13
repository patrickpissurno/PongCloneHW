using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class GameObject
    {
        public string Name;
        public Vector2 Position;
        public Vector2 Velocity = new Vector2(0,0);
        public Texture2D Texture;
        public Color ColorBlend = Color.White;

        public GameObject()
        {
            Game1.GameObjects.Add(this);
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
            Move();
        }

        public static int RandomRange(int min, int max)
        {
            Random r = new Random();
            return r.Next(min, max);
        }

        public virtual void Move()
        {
            Position += Velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, ColorBlend);
        }

        public bool CheckCollision(GameObject gameObject)
        {
            return (Position.X < gameObject.Position.X + gameObject.Texture.Width && Position.X + Texture.Width > gameObject.Position.X && Position.Y < gameObject.Position.Y + gameObject.Texture.Height && Texture.Height + Position.Y > gameObject.Position.Y);
        }


        public static List<T> Find<T>(string name)
        {
            List<T> temp = new List<T>();
            foreach (GameObject go in Game1.GameObjects)
                if (go.Name == name)
                    temp.Add((T)Convert.ChangeType(go, typeof(T)));
            return temp;
        }
    }
}
