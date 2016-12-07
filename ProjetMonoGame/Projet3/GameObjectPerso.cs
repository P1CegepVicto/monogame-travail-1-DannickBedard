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
            attentDroite,attenteGauche,attenteHaut, attenteBas,runDroite,RunGauche,RunHaut, RunBas
        }
        public etats objectState;

        //Compteur 
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
            new Rectangle(14,253,167,256),
            new Rectangle(209,253,167,256),
            new Rectangle(434,253,167,256),
            new Rectangle(629,253,167,256),
        };
        public Rectangle[] tabRunGauche =
        {
            new Rectangle(14,521,167,250),
            new Rectangle(209,521,167,250),
            new Rectangle(434,521,167,250),
            new Rectangle(629,521,167,250),
        };
        public Rectangle[] tabRunHaut =
        {//1850
            new Rectangle(0,803,210,250),
            new Rectangle(210,789,210,250),
            new Rectangle(435,803,195,250),
            new Rectangle(630,789,210,250),
        };

        public int waitState = 0;
        public Rectangle[] tabAttenteDroite =
        {
            new Rectangle(209,253,167,256)
        };
        public Rectangle[] tabAttenteGauche =
        {
              new Rectangle(14,521,167,250)
        };
        public Rectangle[] tabAttenteHaut =
        {
               new Rectangle(210,789,210,250)
        };
        public Rectangle[] tabAttenteBas =
        {
              new Rectangle(210,0,196,240)
        };
    }
}
