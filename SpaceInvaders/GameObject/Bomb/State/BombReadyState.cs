using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class BombReadyState : BombState
    {
        public override void Handle(AlienColumn pCol) {
            BombManager.SetState(BombManager.StateName.BombDropping, pCol);
        }

        public override void DropBomb(AlienColumn pCol) {


            BombManager.ActivateBomb(pCol, pCol.x, pCol.GetBottom());

            this.Handle(pCol);
        }
    }
}
