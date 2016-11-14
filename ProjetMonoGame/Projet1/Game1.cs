using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using System.Timers;

namespace Projet1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject heros;
        GameObject bad;
        GameObject bullet;
        GameObject background;
        GameObject explosion;
        Rectangle fenetre;

        Random de = new Random();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight  = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = new GameObject();
            background.estVivant = true;
            background.position.X = 0;
            background.position.Y = 0;
            background.sprite = Content.Load<Texture2D>("firewatch.jpg");

            heros = new GameObject();
            heros.estVivant = true;
            heros.position.X = 500;
            heros.position.Y = 500;
            heros.sprite = Content.Load<Texture2D>("spaceShips_007.png");

            bad = new GameObject();
            bad.estVivant = true;
            bad.vitesse.X = -3;
            bad.position.X = 500; 
            bad.sprite = Content.Load<Texture2D>("Boss.png");

            bullet = new GameObject();
            bullet.estVivant = false;
            bullet.vitesse.Y = -3;
            bullet.sprite = Content.Load<Texture2D>("spaceMissiles_003.png");

            explosion = new GameObject();
            explosion.estVivant = false;
            explosion.position.X = -500;
            explosion.position.Y = -500;
            explosion.sprite = Content.Load<Texture2D>("Explosion/explosion00.png");
            
           

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            #region mouvement heros
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                heros.position.Y += -2;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    heros.position.Y += -10;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                heros.position.Y += 2;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    heros.position.Y += 10;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                heros.position.X += -2;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    heros.position.X += -10;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                heros.position.X += 2;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    heros.position.X += 10;
                }
            }
            #endregion

            // TODO: Add your update logic here
            #region Limite de fenetre
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Left;
            }
            if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Top;
            }
            if (heros.position.X > (fenetre.Right +graphics.GraphicsDevice.DisplayMode.Width-183))
            {
                heros.position.X = fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width-183;
            }
            if (heros.position.Y > (fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height-142))
            {
                heros.position.Y = fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height-142;
            }
            #endregion
            Respawn();
            UpdateBad();
            UpdateColision();
            
        }
        //Colision entre mon héros et mon ennemi et ma balle
        public void UpdateColision()
        {
            //Quand mon héros touche mon ennemi
            if (heros.GetRect().Intersects(bad.GetRect()))
            {
                bad.vitesse.X = de.Next(-4, 4);
                bad.vitesse.X = 0;
                explosion.estVivant = true;
                bad.estVivant = false;
            }
            //Quand ma balle touche mon ennemi
           if (bullet.GetRect().Intersects(bad.GetRect()))
            {
                bad.vitesse.X = de.Next(-4,4);
                bad.estVivant = false;   
            }
           //Si mon ennemi est touché
            if (bad.estVivant == false)
            {
                bad.vitesse.Y = 2;
                //Création d'une explosion
                #region Explosion
                int random = 0;
                Random de = new Random();
                random = de.Next(0, 9);
                if (random == 0)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion00.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y-50;
                }
                else if (random == 1)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion01.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y-50;
                }
                else if (random == 2)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion02.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y-50;
                }
                else if (random == 3)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion03.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y-50;
                }
                else if (random == 4)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion04.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y-50;
                }
                else if (random == 5)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion05.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y-50;
                }
                else if (random == 6)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion06.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y-50;
                }
                else if (random == 7)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion07.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y-50;
                }
                #endregion
            }
        }
        //faire respawn mon ennemi
        public void Respawn()
        {
            if (bad.position.Y > (fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height - 300))
            {
                bad.estVivant = true;
                bad.vitesse.X = -3;
                bad.vitesse.Y = 0;
                bad.position.Y = 0;
                bad.position.X = 500;
                explosion.position.X = -500;
                explosion.position.Y = -500;
            }
        }

        public void UpdateBad()
        {
            if (bad.position.X > fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width - 300)
            {
                bad.vitesse.X = -3;
            }
            if (bad.position.X < fenetre.Left)
            {
                bad.vitesse.X = 3;
            }
            bad.position.X += bad.vitesse.X;
            bad.position.Y += bad.vitesse.Y;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(background.sprite, background.position, Color.White);
            spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            spriteBatch.Draw(bad.sprite, bad.position, Color.WhiteSmoke);
            spriteBatch.Draw(bullet.sprite, bullet.position += bullet.vitesse, Color.White);
            spriteBatch.Draw(explosion.sprite, explosion.position, Color.FloralWhite);

            //Quand j'appuis sur ma touche t
            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                bullet.estVivant = false;
                bullet.vitesse.Y = -25;
                if (bullet.estVivant == false)
                {
                    bullet.position.Y += -15;
                    //affiche la trajectoire de ma balle en temps réel
                    spriteBatch.Draw(bullet.sprite, bullet.position += bullet.vitesse, Color.White);
                    //si ma balle arrive en haut de ma fenetre
                    if (bullet.position.Y < fenetre.Top)
                    {
                        //Affiche ma balle a la position de mon heros
                        spriteBatch.Draw(bullet.sprite, bullet.position = heros.position, Color.White);
                        bullet.estVivant = true;
                    }
                }
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
