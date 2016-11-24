using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet3
{
    class GameObjectPerso
    {
        public Texture2D sprite;
        public Vector2 vitesse;
        public Vector2 direction;
        public Rectangle position;
        public Rectangle soriteAfficher;//le rectangle affiché à l'écran
        public enum etats
        {
            attentDroite,attenteGauche,runDroite,RunGauche
        }
        public etats objectState;

        //Compteur qui changeras le sprite affiché
        private int cpt = 0;

        //Gestion des tableaux des sprite (chaque sprite est un rectangle dans le tableeau
        int runState = 0;//état de départ
        int nbEtatRun = 6; //combien il y a de rectangle pour l'état courir
        public Rectangle[] tabRunBas =
        {
            new Rectangle(0,0,450,590),
            new Rectangle(450,0,450,590),
            new Rectangle(940,0,450,590),
            new Rectangle(1383,0,450,590),
        };
        public Rectangle[] tabRunDroit =
        {
            new Rectangle(0,590,450,590),
            new Rectangle(450,590,450,590),
            new Rectangle(940,590,450,590),
            new Rectangle(1383,590,450,590),
        };//Y = 1200
        public Rectangle[] tabRunGauche =
        {
            new Rectangle(0,1200,450,590),
            new Rectangle(450,1200,450,590),
            new Rectangle(940,1200,450,590),
            new Rectangle(1383,1200,450,590),
        };
        public Rectangle[] tabRunHaut =
        {//1850
            new Rectangle(0,1850,450,590),
            new Rectangle(450,1850,450,590),
            new Rectangle(940,1850,450,590),
            new Rectangle(1383,1850,450,590),
        };

        int waitState = 0;
        public Rectangle[] tabAttenteDroite =
        {
            new Rectangle(450,590,450,590)
        };
        public Rectangle[] tabAttenteGauche =
        {
             new Rectangle(0,1200,450,590),
        };
        public Rectangle[] tabAttenteHaut =
        {
              new Rectangle(450,1850,450,590),
        };
        public Rectangle[] tabAttenteBas =
        {
              new Rectangle(450,0,450,590),
        };
    }
}
