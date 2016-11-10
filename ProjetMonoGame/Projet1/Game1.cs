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
        Rectangle fenetre;
        int compteur = 0;
        int random = 0;

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
            bad.position.X = 3; 
            bad.sprite = Content.Load<Texture2D>("Boss.png");

            bullet = new GameObject();
            bullet.estVivant = true;
            bullet.vitesse.Y = -150;
            bullet.position.X = 500;
            bullet.position.Y = 500;
            bullet.sprite = Content.Load<Texture2D>("spaceMissiles_003.png");



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

            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                bullet.position.Y += bullet.vitesse.Y;
            }
            
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

            UpdateBad();
        }
        public void UpdateBullete()
        {

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
            spriteBatch.Draw(bullet.sprite, heros.position + bullet.position, Color.White);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
