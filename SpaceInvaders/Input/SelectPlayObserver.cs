using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SelectPlayObserver : InputObserver
    {
        public override void Notify()
        {
            SpaceInvaders.pSceneContext.pSceneState.Handle();
        }
    }
}
