using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjetFinal
{
    class GameObject
    {
        public bool estVivant;
        public bool estMort;
        public int vie;
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 vitesse;
        public Vector2 direction;
        public Rectangle Colision = new Rectangle();

        public Rectangle GetRect()
        {
            Colision.X = (int)this.position.X;
            Colision.Y = (int)this.position.Y;
            Colision.Height = this.sprite.Height;
            Colision.Width = this.sprite.Width;

            return Colision;
        }
        public double Interval { get; set; }
    }
}
