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
        public Rectangle spriteAfficher;//le rectangle affiché à l'écran
        public enum etats
        {
            attentDroite,attenteGauche,runDroite,RunGauche
        }
        public etats objectState;

        //Compteur qui changeras le sprite affiché
        public int cpt = 0;

        //Gestion des tableaux des sprite (chaque sprite est un rectangle dans le tableeau
        public int runState = 0;//état de départ
        public int nbEtatRun = 4; //combien il y a de rectangle pour l'état courir
        public Rectangle[] tabRunBas =
        {
            new Rectangle(0,0,196,240),
            new Rectangle(210,0,196,240),
            new Rectangle(430,0,196,240),
            new Rectangle(630,0,196,240),
        };
        public Rectangle[] tabRunDroit =
        {
            new Rectangle(0,253,167,256),
            new Rectangle(209,253,167,256),
            new Rectangle(434,253,167,256),
            new Rectangle(629,253,167,256),
        };
        public Rectangle[] tabRunGauche =
        {
            new Rectangle(0,777,167,226),
            new Rectangle(209,777,167,226),
            new Rectangle(434,777,167,226),
            new Rectangle(629,777,167,226),
        };
        public Rectangle[] tabRunHaut =
        {//1850
            new Rectangle(0,1030,196,227),
            new Rectangle(209,1030,212,227),
            new Rectangle(434,1030,196,227),
            new Rectangle(629,1030,212,227),
        };

        public int waitState = 0;
        public Rectangle[] tabAttenteDroite =
        {
            new Rectangle(209,253,167,256)
        };
        public Rectangle[] tabAttenteGauche =
        {
              new Rectangle(0,777,167,226)
        };
        public Rectangle[] tabAttenteHaut =
        {
               new Rectangle(209,1030,212,227)
        };
        public Rectangle[] tabAttenteBas =
        {
              new Rectangle(210,0,196,240)
        };
    }
}
