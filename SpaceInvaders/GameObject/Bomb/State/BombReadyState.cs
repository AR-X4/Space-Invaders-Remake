using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class BombReadyState : BombState
    {
        public override void Handle(AlienColumn pCol) {
            BombManager.SetState(BombManager.StateName.BombDropping, pCol);
            //Debug.WriteLine("BOMB DROPPING STATE\n");
        }

        public override void DropBomb(AlienColumn pCol) {


            BombManager.ActivateBomb(pCol);

            this.Handle(pCol);
        }
    }
}
