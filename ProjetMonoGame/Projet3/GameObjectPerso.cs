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
        public Rectangle postion;
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

    }
}
