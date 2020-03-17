using System;


namespace SpaceInvaders
{
    class RemoveAllP2LivesObserver : CollisionObserver
    {
        public override void Notify()
        {
            //-------RemoveLife-------

            ScenePlay2.ShipLives = 0;

            SpaceInvaders.pSceneContext.GetState().Handle();

        }
    }
}
