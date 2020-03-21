using System;


namespace SpaceInvaders
{
    class RemoveAllP1LivesObserver : CollisionObserver
    {
        public override void Notify()
        {
            //-------RemoveLife-------

            ScenePlay.ShipLives = 0;
            SpaceInvaders.pSceneContext.GetState().Handle();

        }
    }
}
