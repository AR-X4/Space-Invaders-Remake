using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AddP1PointsObserver : CollisionObserver
    {
        private GameObject pAlien;

        public AddP1PointsObserver() {
            this.pAlien = null;
        }

        public override void Notify()
        {

            this.pAlien = this.pSubject.pObjB;

            switch (this.pAlien.name)
            {
                case GameObject.Name.PurpleOctopus:
                    SpaceInvaders.pPlayer1Score += 10;
                    break;
                case GameObject.Name.BlueCrab:
                    SpaceInvaders.pPlayer1Score += 20;
                    break;
                case GameObject.Name.GreenSquid:
                    SpaceInvaders.pPlayer1Score += 30;
                    break;
                case GameObject.Name.OrangeSaucer:
                    SpaceInvaders.pPlayer1Score += 100;
                    break;
            }

        }
    }
}
