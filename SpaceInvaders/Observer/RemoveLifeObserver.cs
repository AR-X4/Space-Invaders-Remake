using System;


namespace SpaceInvaders
{
    class RemoveLifeObserver : CollisionObserver
    {
        private int count;
        private GameOverEvent pEvent;

        public RemoveLifeObserver() {
            this.count = 1;
            this.pEvent = new GameOverEvent();
        }

        public override void Notify()
        {
            //-------RemoveLife-------

            PlayerLivesComposite pNullObjs = (PlayerLivesComposite)GameObjectManager.Find(GameObject.Name.Null_Object);

            this.count++;
            if (this.count > 4)
            {
                //pNullObjs.ResetLives();
                this.count = 0;
                //SpaceInvaders.pSceneContext.GetState().Handle();// put into an event for 1.4 seconds?
                TimerManager.Add(TimeEvent.Name.GameOver, this.pEvent, 1.5f);
            }
            else {
                pNullObjs.RemoveLife(count);
            }
        }
    }
}
