using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class BombFallingState : BombState
    {

        public override void Handle(AlienColumn pCol) {
            BombManager.SetState(BombManager.StateName.Ready, pCol);
            //Debug.WriteLine("BOMB READY STATE\n");
        }

        public override void DropBomb(AlienColumn pCol) { 
            //do nothing
        }
    }
}
