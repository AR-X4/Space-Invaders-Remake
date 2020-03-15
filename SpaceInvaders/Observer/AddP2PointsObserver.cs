using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AddP2PointsObserver : CollisionObserver
    {
        private GameObject pAlien;

        public AddP2PointsObserver()
        {
            this.pAlien = null;
        }

        public override void Notify()
        {

            this.pAlien = this.pSubject.pObjB;

            switch (this.pAlien.name) {
                case GameObject.Name.PurpleOctopus:
                    SpaceInvaders.pPlayer2Score += 10;
                    break;
                case GameObject.Name.BlueCrab:
                    SpaceInvaders.pPlayer2Score += 20;
                    break;
                case GameObject.Name.GreenSquid:
                    SpaceInvaders.pPlayer2Score += 30;
                    break;
                case GameObject.Name.OrangeSaucer:
                    SpaceInvaders.pPlayer2Score += 100;
                    break;
            }

        }
    }
}
