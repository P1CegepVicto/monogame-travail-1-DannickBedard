using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Projet2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject heros;
        GameObject coeur;
        GameObject bullet;
        GameObject[] bad;
        GameObject[] badBullet;
        GameObject background;
        GameObject[] enemy;
        SoundEffect son;
        SoundEffectInstance bombe;
        SpriteFont font;
        int nbenemy = 1;
        int compteurEnemyTuer = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ApplyChanges();
            //this.graphics.ToggleFullScreen();
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = new GameObject();
            background.position.X = 0;
            background.position.Y = 0;
            background.sprite = Content.Load<Texture2D>("rocheTailleEcran.jpg");
            //background.sprite = Content.Load<Texture2D>("roche.jpg");
            
            heros = new GameObject();
            heros.estVivant = true;
            heros.vie = 100;
            heros.position.X = 50;
            heros.position.Y = 500;
            heros.vitesse.Y = -3;
            heros.vitesse.X = 0;
            heros.sprite = Content.Load<Texture2D>("playerShip1_orange.png");

           
            bad = new GameObject[15];
            for (int i = 0; i < bad.Length; i++)
            {
                bad[i] = new GameObject();

                bad[i].estVivant = true;
                bad[i].position.X = -500;
                bad[i].position.Y = 0;
                bad[i].vitesse.X = 5;
                bad[i].sprite = Content.Load<Texture2D>("enemyBlack3.png");
            }
            badBullet = new GameObject[15];
            for (int i = 0; i < badBullet.Length; i++)
            {
                badBullet[i] = new GameObject();
                badBullet[i].estMort = false;
                badBullet[i].estVivant =false;
                badBullet[i].position.X = 0;
                badBullet[i].position.Y = 0;
                badBullet[i].vitesse.Y = 100;
                badBullet[i].sprite = Content.Load<Texture2D>("laserRed12.png");
            }
            bullet = new GameObject();
            bullet.estVivant = true;
            bullet.position.X = heros.position.X;
            bullet.position.Y = heros.position.Y;
            bullet.vitesse.Y = 0;
            bullet.sprite = Content.Load<Texture2D>("laserGreen02.png");

            coeur = new GameObject();
            coeur.position.X = 0;
            coeur.position.Y = 500;
            coeur.sprite = Content.Load<Texture2D>("coeur/Coeur10px.png");
            //bullet.sprite = Content.Load<Texture2D>("laserBlue12.png");

            son = Content.Load<SoundEffect>("highDown");
            bombe = son.CreateInstance();

            font = Content.Load<SpriteFont>("Font");

            enemy = new GameObject[2];
            // TODO: use this.Content to load your game content here
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
           
            MouvementHeros(gameTime);
            Mouvementbad(gameTime);
            LimiteFenetreHeros();
            Colision();
            Respawn();
            base.Update(gameTime);

        }
        public void LimiteFenetreHeros()
        {
            if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Top;
            }
            if (heros.position.Y > (fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height - 142))
            {
                heros.position.Y = fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height - 142;
            }
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Left;
            }
            if (heros.position.X > fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width - 100)
            {
                heros.position.X = fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width - 100;
            }
        }
        public void MouvementHeros(GameTime gameTime)
        {
            if (heros.estVivant == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    heros.vitesse.Y = -5;
                    heros.position.Y += heros.vitesse.Y;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        heros.vitesse.Y = -10;
                        heros.position.Y += heros.vitesse.Y;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    heros.vitesse.Y = 5;
                    heros.position.Y += heros.vitesse.Y;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        heros.vitesse.Y = 10;
                        heros.position.Y += heros.vitesse.Y;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    heros.vitesse.X = -5;
                    heros.position.X += heros.vitesse.X;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        heros.vitesse.X = -10;
                        heros.position.X += heros.vitesse.X;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    heros.vitesse.X = 5;
                    heros.position.X += heros.vitesse.X;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        heros.vitesse.X = 10;
                        heros.position.X += heros.vitesse.X;
                    }

                }
            }
        }
        public void Mouvementbad(GameTime gameTime)
        {
            Random de = new Random();

            for (int j = 0; j < bad.Length; j++)
            {
                if (nbenemy * 2 < gameTime.TotalGameTime.Seconds && nbenemy < bad.Length)
                {
                    bad[nbenemy].position.X = 1;
                    bad[nbenemy].position.Y = 0;
                    bad[nbenemy].vitesse.X = 10;
                    bad[nbenemy].vitesse.Y = 0;
                    bad[nbenemy].position += bad[nbenemy].vitesse;
                    bad[nbenemy].estMort = false;
                    nbenemy++;
                }
            }
            for (int i = 0; i < bad.Length; i++)
            {
                if (bad[i].position.X < fenetre.Left)
                {
                    bad[i].vitesse.X = de.Next(1,10);
                }
                if (bad[i].position.X > fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width)
                {
                    bad[i].vitesse.X = de.Next(-10,-1);
                }

                bad[i].position.X += bad[i].vitesse.X;
            }
        }
        public void BulletEnemy()
        {
            Random de = new Random();
            
            for (int i = 0; i < badBullet.Length; i++)
            {

                if (badBullet[i].position.Y > fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height)
                {
                    badBullet[i].estVivant = true;
                    if (badBullet[i].estVivant == true)
                    {
                        badBullet[i].position = bad[i].position;
                        badBullet[i].vitesse.Y = de.Next(8, 25);
                    }
                    if (badBullet[i].estVivant == true)
                    {
                        badBullet[i].position = bad[i].position;
                        spriteBatch.Draw(badBullet[i].sprite, badBullet[i].position = bad[i].position, Color.White);
                        badBullet[i].estVivant = false;

                    }
                }
            }
        }
        public void Bullet()

        {
            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                bombe.Play();
                bullet.estVivant = false;
                bullet.vitesse.Y = -10;
                if (bullet.estVivant == false)
                {
                    bullet.position.Y += bullet.vitesse.Y;
                    spriteBatch.Draw(bullet.sprite, bullet.position, Color.White);
                }
                if (bullet.position.Y < fenetre.Top)
                {
                    spriteBatch.Draw(bullet.sprite, bullet.position = heros.position, Color.White);
                    bullet.estVivant = true;
                }
                
            }
        }
        public void Colision()
        {
            Random de = new Random();
            for (int i = 0; i < nbenemy; i++)
            {
                //Colision entre balle heros et enemy
                if (bullet.GetRect().Intersects(bad[i].GetRect()))
                {
                    bad[i].estVivant = false;
                    bad[i].vitesse.X = 0;
                    bad[i].vitesse.Y = 2;
                    bad[i].estMort = true;
                    compteurEnemyTuer++;
                }
                if (bad[i].estVivant == false)
                {

                }
                if (bad[i].position.Y > fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Width)
                {
                    bad[i].position.X = 10;
                    bad[i].position.Y = 0;
                    bad[i].vitesse.Y = 0;
                    bad[i].vitesse.X = de.Next(3, 10);
                    bad[i].position = bad[i].vitesse;
                    bad[i].estVivant = true;
                    bad[i].estMort = false;
                }
                //Colision entre balle enemy et heros
                if (badBullet[i].GetRect().Intersects(heros.GetRect()))
                {
                    badBullet[i].position = bad[i].position;
                    heros.vie = heros.vie - 10;
                }
                if (heros.vie <= 0)
                {
                    heros.estVivant = false;

                    heros.vitesse.X = de.Next(-6, 6);
                   
                    heros.position.Y += heros.vitesse.Y;
                    heros.position.X += heros.vitesse.X;
                    heros.vitesse.Y = 6;
                   
                }
            }
        }
        public void Coeur()
        {
            if (heros.vie == 100)
            {

            }

        }
        public void Respawn()
        {
            if (heros.estVivant == false)
            {
                if (heros.position.Y > fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height - 142)
                {
                    heros.vitesse.X = 0;
                    heros.vitesse.Y = 0;
                    heros.position.X = 20;
                    heros.position.Y = 500;
                    heros.estVivant = true;
                    heros.vie = 100;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            
            BulletEnemy();
            Bullet();
            
            spriteBatch.Draw(background.sprite, background.position, Color.White);
            spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            
            spriteBatch.Draw(bullet.sprite, bullet.position += bullet.vitesse, Color.White);
            
            for (int i = 0; i < nbenemy; i++)
            {
                
                spriteBatch.Draw(bad[i].sprite, bad[i].position += bad[i].vitesse, Color.White);
            }
            for (int i = 0; i < nbenemy; i++)
            {
                if (bad[i].estMort == true)
                {
                    badBullet[i].position.X = -500;
                    badBullet[i].position.Y = -500;
                    spriteBatch.Draw(badBullet[i].sprite, badBullet[i].position, Color.White);
                }
                else
                {
                    spriteBatch.Draw(badBullet[i].sprite, badBullet[i].position += badBullet[i].vitesse, Color.White);
                }
            }

            //Écrire le temps dans le jeux
            #region Time
            spriteBatch.DrawString(font,gameTime.TotalGameTime.Minutes.ToString(), new Vector2(50, 100), Color.Black);
            spriteBatch.DrawString(font, ":".ToString(), new Vector2(80, 100), Color.Black);
            spriteBatch.DrawString(font,gameTime.TotalGameTime.Seconds.ToString(), new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(font, ":".ToString(), new Vector2(150, 100), Color.Black);
            spriteBatch.DrawString(font, gameTime.TotalGameTime.Milliseconds.ToString(), new Vector2(170, 100), Color.Black);
            #endregion
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
