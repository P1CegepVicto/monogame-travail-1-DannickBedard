using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            rambo = new GameObjectPerso();
            rambo.direction = Vector2.Zero;
            
            rambo.objectState = GameObjectPerso.etats.attentDroite;
            rambo.position = new Rectangle(209, 253, 167, 256); //Position initial de Rambo
            rambo.sprite = Content.Load<Texture2D>("SpriteSheet.png");
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here


            //On appelle la méthode Update de Rambo qui permet de gérer les états

            UpdateRambo(gameTime);
            previousKeys = keys;
            base.Update(gameTime);
        }
        //Crée ici les fonctions pour bien organiser
        public void UpdateRambo(GameTime gameTime)
        {

            keys = Keyboard.GetState();
            rambo.position.X += (int)(rambo.vitesse.X * rambo.direction.X);

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                rambo.direction.X = 2;

                rambo.objectState = GameObjectPerso.etats.runDroite;
                rambo.spriteAfficher = rambo.tabAttenteDroite[rambo.runState];

            }
                if (keys.IsKeyUp(Keys.D) && previousKeys.IsKeyDown(Keys.D))
                {
                    rambo.direction.X = 0;
                    rambo.objectState = GameObjectPerso.etats.attentDroite;
                }

                if (rambo.objectState == GameObjectPerso.etats.attentDroite)
                {
                    rambo.spriteAfficher = rambo.tabAttenteDroite[rambo.waitState];

                }
                if (rambo.objectState == GameObjectPerso.etats.runDroite)
                {
                    if (rambo.runState == 1)
                        rambo.spriteAfficher = new Rectangle(60, 30, 65, 65);
                    if (rambo.runState == 2)
                        rambo.spriteAfficher = new Rectangle(130, 30, 65, 65);
                    if (rambo.runState == 3)
                        rambo.spriteAfficher = new Rectangle(193, 30, 65, 65);
                    if (rambo.runState == 4)
                        rambo.spriteAfficher = new Rectangle(260, 30, 65, 65);
                    if (rambo.runState == 5)
                        rambo.spriteAfficher = new Rectangle(320, 30, 65, 65);
                    if (rambo.runState == 6)
                        rambo.spriteAfficher = new Rectangle(385, 30, 65, 65);
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
        
        

        

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(rambo.sprite, rambo.position, rambo.spriteAfficher, Color.White );
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
