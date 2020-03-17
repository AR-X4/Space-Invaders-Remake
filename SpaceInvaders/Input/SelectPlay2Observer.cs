using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SelectPlay2Observer : InputObserver
    {
        public override void Notify()
        {
            SpaceInvaders.Player1Mode = false;
            SpaceInvaders.pSceneContext.pSceneState.Handle();
        }
    }
}
