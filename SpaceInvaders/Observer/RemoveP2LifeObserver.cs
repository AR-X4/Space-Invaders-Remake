using System;


namespace SpaceInvaders
{
    class RemoveP2LifeObserver : CollisionObserver
    {
        private int count;

        public RemoveP2LifeObserver()
        {
            this.count = 1;
        }


        public override void Notify()
        {
            //-------RemoveLife-------
            ScenePlay2.ShipLives--;
            this.count++;

            PlayerLivesComposite pNullObjs = (PlayerLivesComposite)GameObjectManager.Find(GameObject.Name.Null_Object);
            pNullObjs.RemoveLife(count);

            if (this.count > 2)
            {
                this.count = 1;
            }
        }
    }
}
