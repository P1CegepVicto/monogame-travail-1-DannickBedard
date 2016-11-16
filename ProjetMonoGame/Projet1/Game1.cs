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
        GameObject badBullet;
        GameObject background;
        GameObject explosion;
        GameObject explosion2;
        GameObject coeur;
        GameObject coeurE;
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
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            //this.graphics.ApplyChanges();
            this.graphics.ToggleFullScreen();
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

            #region Background
            background = new GameObject();
            background.estVivant = true;
            background.position.X = 0;
            background.position.Y = 0;
            background.sprite = Content.Load<Texture2D>("firewatch.jpg");
            #endregion
            #region Heros
            heros = new GameObject();
            heros.estVivant = true;
            heros.position.X = 500;
            heros.position.Y = 500;
            heros.vie = 100;
            heros.sprite = Content.Load<Texture2D>("spaceShips_007.png");
            #endregion
            #region bad
            bad = new GameObject();
            bad.estVivant = true;
            bad.vitesse.X = -3;
            bad.position.X = 500;
            bad.vie = 100;
            bad.sprite = Content.Load<Texture2D>("Boss.png");
            #endregion
            #region Bullet
            bullet = new GameObject();
            bullet.estVivant = false;
            bullet.vitesse.Y = -3;
            bullet.sprite = Content.Load<Texture2D>("spaceMissiles_003.png");
            #endregion
            #region BulletBad
            badBullet = new GameObject();
            badBullet.estVivant = false;
            badBullet.vitesse.Y = 10;
            badBullet.position = bad.position;
            badBullet.sprite = Content.Load<Texture2D>("balleBad.png");
            #endregion
            #region Explosion
            explosion = new GameObject();
            explosion.estVivant = false;
            explosion.position.X = -500;
            explosion.position.Y = -500;
            explosion.sprite = Content.Load<Texture2D>("Explosion/explosion00.png");

            explosion2 = new GameObject();
            explosion2.estVivant = false;
            explosion2.position.X = -500;
            explosion2.position.Y = -500;
            explosion2.sprite = Content.Load<Texture2D>("Explosion/explosion00.png");
            #endregion
            #region Coeur
            coeur = new GameObject();
            coeur.estVivant = true;
            coeur.position.X = 0;
            coeur.position.Y = 500;
            coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeur10px.png");
            #endregion
            #region coeur mechant
            coeurE = new GameObject();
            coeurE.estVivant = true;
            coeurE.position.X = 0;
            coeurE.position.Y = 200;
            coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurMchant.png");
            #endregion
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
                Environment.Exit(0);

            // TODO: Add your update logic here
            UpdateHeros();
            Respawn();
            UpdateBad();
            UpdateColision();

        }
        //Heros
        public void UpdateHeros()
        {
            #region mouvement heros
            if (heros.estVivant == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    heros.vitesse.Y = -2;
                    heros.position.Y += heros.vitesse.Y;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        heros.vitesse.Y = -10;
                        heros.position.Y += heros.vitesse.Y;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    heros.vitesse.Y = 2;
                    heros.position.Y += heros.vitesse.Y;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        heros.vitesse.Y = 10;
                        heros.position.Y += heros.vitesse.Y;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    heros.vitesse.X = -2;
                    heros.position.X += heros.vitesse.X;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        heros.vitesse.X = -10;
                        heros.position.X += heros.vitesse.X;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    heros.vitesse.X = 2;
                    heros.position.X += heros.vitesse.X;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        heros.vitesse.X = 10;
                        heros.position.X += heros.vitesse.X;
                    }
                }
            }
            #endregion

            //Limite de fentre pour mon héros
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Left;
            }
            if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Top;
            }
            if (heros.position.X > (fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width - 183))
            {
                heros.position.X = fenetre.Right + graphics.GraphicsDevice.DisplayMode.Width - 183;
            }
            if (heros.position.Y > (fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height - 142))
            {
                heros.position.Y = fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height - 142;
            }
            
        }
        public void UpdateTireHeros()
        {
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
                    if (bullet.GetRect().Intersects(bad.GetRect()))
                    {
                        bad.vie = bad.vie - 1;
                        //Lorsque ma balle touhe mon ennemi elle retourne sur mon héros
                        bullet.position = heros.position;
                    }
                }
            }
        }
        //Ennemi
        public void UpdateBad()
        {
            if (badBullet.estVivant == true)
            {
                badBullet.position = bad.position;
                badBullet.vitesse.Y = -25;
            }

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
        public void UpdateBulletEnnemi()
        {
            if (badBullet.position.Y > (fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height - 128))
            {
                badBullet.estVivant = true;
                if (badBullet.estVivant == true)
                {
                    badBullet.position = bad.position;
                    spriteBatch.Draw(badBullet.sprite, badBullet.position = bad.position, Color.White);
                    badBullet.estVivant = false;

                }

            }
        }
        public void UpdateCoeurHerosColision()
        {
            if (heros.GetRect().Intersects(badBullet.GetRect()))
            {
                //Lorsque la balle touche le heros elle vas tout de suite sur mon ennemi
                badBullet.position = bad.position;
                //Des dégat de -10 vie a chaque hit pas l'ennemi
                heros.vie = heros.vie - 10;
                //Annimation pour le coeur
                if (heros.vie == 100)
                {
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeur10px.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500;
                }
                if (heros.vie == 90)
                {
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeur910.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500;
                }
                if (heros.vie == 80)
                {
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeur810.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500;
                }
                if (heros.vie == 70)
                {
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeur710.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500;
                }
                if (heros.vie == 60)
                {
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeur610.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500;
                }
                if (heros.vie == 50)
                {
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/CoeurMoitier.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500;
                }
                if (heros.vie == 40)
                {
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeur410.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500;
                }
                if (heros.vie == 30)
                {
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeur310.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500;
                }
                if (heros.vie == 20)
                {
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeur210.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500;
                }
                if (heros.vie == 10)
                {
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeur110.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500;
                }
                if (heros.vie <= 0)
                {
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeurvide.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500;
                    heros.estVivant = false;
                }
            }
        }
        public void UpdateBadCoeur()
        {
            /// <summary
            /// Pour changer les degats des ballse du héros sa vas être dans "UpdateTireHeros()"
            /// </summary

            if (bad.vie == 100)
            {
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurMchant.png");
                coeurE.position.X = 0;
                coeurE.position.Y = 200;
            }
            if (bad.vie == 90)
            {
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurM910.png");
                coeurE.position.X = 0;
                coeurE.position.Y = 200;
            }
            if (bad.vie == 80)
            {
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurM810.png");
                coeurE.position.X = 0;
                coeurE.position.Y = 200;
            }
            if (bad.vie == 70)
            {
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurM710.png");
                coeurE.position.X = 0;
                coeurE.position.Y = 200;
            }
            if (bad.vie == 60)
            {
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurM610.png");
                coeurE.position.X = 0;
                coeurE.position.Y = 200;
            }
            if (bad.vie == 50)
            {
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurM510.png");
                coeurE.position.X = 0;
                coeurE.position.Y = 200;
            }
            if (bad.vie == 40)
            {
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurM410.png");
                coeurE.position.X = 0;
                coeurE.position.Y = 200;
            }
            if (bad.vie == 30)
            {
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurM310.png");
                coeurE.position.X = 0;
                coeurE.position.Y = 200;
            }
            if (bad.vie == 20)
            {
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurM210.png");
                coeurE.position.X = 0;
                coeurE.position.Y = 200;
            }
            if (bad.vie == 10)
            {
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurM110.png");
                coeurE.position.X = 0;
                coeurE.position.Y = 200;
            }

            if (bad.vie <= 0)
            {
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurMV.png");
                coeurE.position.X = 0;
                coeurE.position.Y = 200;
                bad.estVivant = false;
            }
        }

        //Colision entre tour les choses (balle,heros,ennemi,etc)
        public void UpdateColision()
        {
            int random = 0;
            //Création d'un logo vie + Colision entre les balles de mon ennemi et mon héros
            UpdateCoeurHerosColision();

            if (heros.GetRect().Intersects(bad.GetRect()))
            {
                //Si mon heros touche mon ennemi, coup critique de 100 point de dommage
                coeur.position.X = 0;
                coeur.position.Y = 500;
                coeur.sprite = Content.Load<Texture2D>("CoeurHeros/CoeurVide.png");
                heros.estVivant = false;
            }

            #region ExplosionHéros
            if (heros.estVivant == false)
            {
                //Crée un random pour donner une animation a mon explosion
                random = de.Next(0, 9);
                heros.vitesse.X = de.Next(-3,3);
                heros.vitesse.Y = 4;
                heros.position.X += heros.vitesse.X;
                heros.position.Y += heros.vitesse.Y;
               
                if (random == 0)
                {
                    explosion2.sprite = Content.Load<Texture2D>("Explosion/explosion00.png");
                    explosion2.position.X = heros.position.X - 180;
                    explosion2.position.Y = heros.position.Y - 80;
                }
                else if (random == 1)
                {
                    explosion2.sprite = Content.Load<Texture2D>("Explosion/explosion01.png");
                    explosion2.position.X = heros.position.X - 180;
                    explosion2.position.Y = heros.position.Y - 80;
                }
                else if (random == 2)
                {
                    explosion2.sprite = Content.Load<Texture2D>("Explosion/explosion02.png");
                    explosion2.position.X = heros.position.X - 180;
                    explosion2.position.Y = heros.position.Y - 80;
                }
                else if (random == 3)
                {
                    explosion2.sprite = Content.Load<Texture2D>("Explosion/explosion03.png");
                    explosion2.position.X = heros.position.X - 180;
                    explosion2.position.Y = heros.position.Y - 80;
                }
                else if (random == 4)
                {
                    explosion2.sprite = Content.Load<Texture2D>("Explosion/explosion04.png");
                    explosion2.position.X = heros.position.X - 180;
                    explosion2.position.Y = heros.position.Y - 80;
                }
                else if (random == 5)
                {
                    explosion2.sprite = Content.Load<Texture2D>("Explosion/explosion05.png");
                    explosion2.position.X = heros.position.X - 180;
                    explosion2.position.Y = heros.position.Y - 80;
                }
                else if (random == 6)
                {
                    explosion2.sprite = Content.Load<Texture2D>("Explosion/explosion06.png");
                    explosion2.position.X = heros.position.X - 180  ;
                    explosion2.position.Y = heros.position.Y - 80;
                }
                else if (random == 7)
                {
                    explosion2.sprite = Content.Load<Texture2D>("Explosion/explosion07.png");
                    explosion2.position.X = heros.position.X - 180;
                    explosion2.position.Y = heros.position.Y - 80;
                }
                
               
            } 
            #endregion



            //Coeur de mon ennemi Pour modification "UpdatebadCoeur()"
            UpdateBadCoeur();
            //Si mon ennemi est Mort
            if (bad.estVivant == false)
            {
                //vitesse.Y est mise a 2 pour faire descendre mon ennemi lentement
                bad.vitesse.Y = 2;

                //Création d'une explosion
                #region Explosion
                
                Random de = new Random();
                random = de.Next(0, 9);
                bad.vitesse.X = de.Next(-4, 4);
                if (random == 0)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion00.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y - 50;
                }
                else if (random == 1)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion01.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y - 50;
                }
                else if (random == 2)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion02.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y - 50;
                }
                else if (random == 3)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion03.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y - 50;
                }
                else if (random == 4)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion04.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y - 50;
                }
                else if (random == 5)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion05.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y - 50;
                }
                else if (random == 6)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion06.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y - 50;
                }
                else if (random == 7)
                {
                    explosion.sprite = Content.Load<Texture2D>("Explosion/explosion07.png");
                    explosion.position.X = bad.position.X - 150;
                    explosion.position.Y = bad.position.Y - 50;
                }
                #endregion
                
            }
        }

        //faire respawn mon ennemi && Heros
        public void Respawn()
        {
            if (bad.position.Y > (fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height - 300))
            {
                bad.estVivant = true;
                //Remet ma vitesse pour qu'il puisse bouger
                bad.vitesse.X = -3;
                bad.vitesse.Y = 0;
                //Place mon ennemi a sa place d'origine
                bad.position.Y = 0;
                bad.position.X = 500;
                //Cache l'image de l'explosion
                explosion.position.X = -500;
                explosion.position.Y = -500;
               
                //Remet la vie de mon ennemi a 100%
                 bad.vie = 100;
                coeurE.sprite = Content.Load<Texture2D>("CoeurNoir/CoeurMchant.png");
            }
            if (heros.estVivant == false)
            {
                if (heros.position.Y > (fenetre.Bottom + graphics.GraphicsDevice.DisplayMode.Height - 150))
                {
                    heros.estVivant = true;
                    //Remet ma vitesse pour qu'il puisse bouger
                    heros.vitesse.X = 3;
                    heros.vitesse.Y = 3;
                    //Replace mon heros a sa place d'origine
                    heros.position.X = 500;
                    heros.position.Y = 500;
                    //Cache mon image d'explosion
                    explosion2.position.X = -500;
                    explosion2.position.Y = -500;
                    heros.vie = 100;
                    //Fait remmetre mon coeur à 100%
                    coeur.sprite = Content.Load<Texture2D>("CoeurHeros/Coeur10px.png");
                    coeur.position.X = 0;
                    coeur.position.Y = 500 ;
                }
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here

            spriteBatch.Draw(background.sprite, background.position, Color.White);
            spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            spriteBatch.Draw(bad.sprite, bad.position, Color.WhiteSmoke);
            spriteBatch.Draw(bullet.sprite, bullet.position += bullet.vitesse, Color.White);
            spriteBatch.Draw(explosion.sprite, explosion.position, Color.FloralWhite);
            spriteBatch.Draw(explosion2.sprite, explosion2.position, Color.FloralWhite);
            spriteBatch.Draw(badBullet.sprite, badBullet.position += badBullet.vitesse, Color.White);
            spriteBatch.Draw(coeur.sprite, coeur.position, Color.White);
            spriteBatch.Draw(coeurE.sprite, coeurE.position, Color.White);
            
            UpdateBulletEnnemi();
            //Quand j'appuis sur ma touche t
            UpdateTireHeros();
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}