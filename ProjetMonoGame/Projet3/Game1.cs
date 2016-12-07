using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Projet3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    
        
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();
        GameObjectPerso rambo;
        GameObject start;
        Texture2D fond;
        Rectangle fenetre;
        GameObject[] enemy;
        SpriteFont font;
        GameObject ball;
        int nbEnemy = 1;
        int nbBall = 1;

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
            font = Content.Load<SpriteFont>("Font");
            start = new GameObject();
            start.position.X = 0;
            start.position.Y = 0;
            start.estVivant = true;
            start.sprite = Content.Load<Texture2D>("BackBlack.jpg");
            fond = Content.Load<Texture2D>("grass.jpg");

            spriteBatch = new SpriteBatch(GraphicsDevice);
            rambo = new GameObjectPerso();
            rambo.direction = Vector2.Zero;
            rambo.vitesse.X = 2;
            rambo.vitesse.Y = 2;
            rambo.objectState = GameObjectPerso.etats.attentDroite;
            rambo.position = new Rectangle(500, 253, 167, 256); //Position initial de Rambo
            rambo.sprite = Content.Load<Texture2D>("SpriteSheet.png");
            rambo.spriteAfficher = rambo.tabAttenteDroite[rambo.waitState];

            enemy = new GameObject[4];
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i] = new GameObject();
                enemy[i].estVivant = true;
                enemy[i].position.X = -50;
                enemy[i].position.Y = 300;
                enemy[i].vitesse.X = 1;
                enemy[i].vitesse.Y =0 ;
                enemy[i].sprite = Content.Load<Texture2D>("spacestation.png");
            }
           
                ball = new GameObject();
                ball.estVivant = true;
                ball.position.X = 0;
                ball.position.Y = 0;
                ball.vitesse.X = 500;
                ball.sprite = Content.Load<Texture2D>("fart04.png");
            
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic 
            //On appelle la méthode Update de Rambo qui permet de gérer les états
            UpdateStart();
            UpdateBall();
            if (start.estVivant == false)
            {
                UpdateEnemy(gameTime);
                UpdateFenetre();
                UpdateRambo(gameTime);
                UpdateColision();
                
            }
            previousKeys = keys;
            base.Update(gameTime);
        }
        //Crée ici les fonctions pour bien organiser
        public void UpdateRambo(GameTime gameTime)
        {
            keys = Keyboard.GetState();
            rambo.position.X += (int)(rambo.vitesse.X * rambo.direction.X);
            rambo.position.Y += (int)(rambo.vitesse.Y * rambo.direction.Y);
            //Aller courir à droite
            if (Keyboard.GetState().IsKeyDown(Keys.D) && previousKeys.IsKeyDown(Keys.D))
            {
                if (ball.estVivant == true)
                {
                    ball.vitesse.X = 25;
                }
                rambo.direction.X = 4;
                rambo.objectState = GameObjectPerso.etats.runDroite;
                rambo.spriteAfficher = rambo.tabRunDroit[rambo.runState];
            }
            if (Keyboard.GetState().IsKeyUp(Keys.D) && previousKeys.IsKeyDown(Keys.D))
            {
                rambo.direction.X = 0;
                rambo.objectState = GameObjectPerso.etats.attentDroite;
                rambo.spriteAfficher = rambo.tabAttenteDroite[rambo.waitState];
            }
            //Aller courir à gauche
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (ball.estVivant == true)
                {
                    ball.vitesse.X = -25;
                }

                rambo.direction.X = -4;
                rambo.objectState = GameObjectPerso.etats.RunGauche;
                rambo.spriteAfficher = rambo.tabRunGauche[rambo.runState];
            }
            if (keys.IsKeyUp(Keys.A)&& previousKeys.IsKeyDown(Keys.A))
            {
                
                rambo.direction.X = 0;
                rambo.objectState = GameObjectPerso.etats.attenteGauche;
                rambo.spriteAfficher = rambo.tabAttenteGauche[rambo.waitState];
            }
            //Aller courir en haut
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {

                rambo.direction.Y = -2;
                rambo.objectState = GameObjectPerso.etats.RunHaut;
                rambo.spriteAfficher = rambo.tabRunHaut[rambo.runState];
            }
            if (keys.IsKeyUp(Keys.W) && previousKeys.IsKeyDown(Keys.W))
            {
                rambo.direction.Y = 0;
                rambo.objectState = GameObjectPerso.etats.attenteHaut;
                rambo.spriteAfficher = rambo.tabAttenteHaut[rambo.waitState];
            }
            //Courir vers le bas
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                rambo.direction.Y = 2;
                rambo.objectState = GameObjectPerso.etats.RunHaut;
                rambo.spriteAfficher = rambo.tabRunBas[rambo.runState];
            }
            if (keys.IsKeyUp(Keys.S) && previousKeys.IsKeyDown(Keys.S))
            {
                rambo.direction.Y = 0;
                rambo.objectState = GameObjectPerso.etats.attenteBas;
                rambo.spriteAfficher = rambo.tabAttenteBas[rambo.waitState];
            }

            //Compteur permettant de gérer le changement d'images
            rambo.cpt++;
                if (rambo.cpt == 6)//vitesse de déplacement
                {
                    //gestionde la course
                    rambo.runState++;
                    if (rambo.runState == rambo.nbEtatRun)
                    {
                        rambo.runState = 0;
                    }
                    rambo.cpt = 0;
                }
            }
        public void UpdateBall()
        {
                ball.estVivant = false;
            if (ball.estVivant == false)

            {
                ball.position += ball.vitesse;
            }
                if (ball.position.X < fenetre.Left)
                {
                ball.position.X = rambo.position.X;
                ball.position.Y = rambo.position.Y;
                ball.estVivant = true;
                }
                if (ball.position.X > fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width)
                {
                ball.position.X = rambo.position.X;
                ball.position.Y = rambo.position.Y;
                ball.estVivant = true;
                }
            
            }
        public void UpdateStart()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                start.estVivant = false;
            }
        }
        public void UpdateFenetre()
        {
          if (rambo.position.Y < fenetre.Top)
            {
                rambo.position.Y = fenetre.Top;
            }
          if (rambo.position.Y > fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height-250)
            {
                rambo.position.Y = fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height - 250;
            }
          if (rambo.position.X < fenetre.Left)
            {
                rambo.position.X = fenetre.Left;
            }
          if (rambo.position.X > fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width - 160)
            {
                rambo.position.X = fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width - 160;
            }
        }
        public void UpdateEnemy(GameTime gameTime)
        {
            Random de = new Random();
            for (int  i = 0;  i < enemy.Length; i++)
            {
                if (nbEnemy * 2 < gameTime.TotalGameTime.Seconds && nbEnemy < enemy.Length)
                {
                    enemy[nbEnemy].position.X = fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width;
                    enemy[nbEnemy].position.Y = 300;
                    enemy[nbEnemy].vitesse.X = -2;
                    enemy[nbEnemy].vitesse.Y = 0;
                    nbEnemy++; 
                }
            }
            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].position.X < fenetre.Left)
                {
                    enemy[i].position.X = fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width;
                }
                
                enemy[i].position.X += enemy[i].vitesse.X;
            }
        }
        public void UpdateColision()
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                if (ball.GetRect().Intersects(enemy[i].GetRect()))
                {
                    
                    enemy[i].vitesse.Y = 2;
                    enemy[i].vitesse.X = 0;
                    
                }
            }
        }
        public void UpdateRespawn()
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].position.X < fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height)
                {
                    enemy[i].position.X = fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width;
                    enemy[i].position.Y = 300;
                    enemy[i].vitesse.X = -10;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            

            spriteBatch.Draw(fond, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);

            if (ball.estVivant == false)
            {
                spriteBatch.Draw(ball.sprite, ball.position);
            }
            
            spriteBatch.Draw(rambo.sprite, rambo.position, rambo.spriteAfficher, Color.White );
            for (int i = 0; i < nbEnemy; i++)
            {
                spriteBatch.Draw(enemy[i].sprite, enemy[i].position += enemy[i].vitesse, Color.White);
            }
           

            if (start.estVivant == true)
            {
                spriteBatch.DrawString(font, "Press 'ENTER' to start!", new Vector2(100,100), Color.White);
            }

            if (ball.estVivant == false)
            {
                spriteBatch.Draw(ball.sprite, ball.position);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
