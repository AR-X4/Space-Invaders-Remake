using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveBombEvent : Command
    {
        //data
        private Bomb pBomb;

        public RemoveBombEvent()
        {


            this.pBomb = null;

        }
        //public RemoveAlienEvent(GameObject pAlien) {

        //    Debug.Assert(pAlien != null);
        //    this.pAlien = pAlien;

        //}
        public void SetBomb(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);
            this.pBomb = pBomb;
        }

        public override void Execute(float deltaTime)
        {
            BombManager.Reset(this.pBomb);
        }
    }
}
