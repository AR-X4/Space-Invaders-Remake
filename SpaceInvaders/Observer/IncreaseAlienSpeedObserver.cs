using System;


namespace SpaceInvaders
{
    public class IncreaseAlienSpeedObserver : CollisionObserver
    {
        //data
        AlienGrid pGrid;

        public IncreaseAlienSpeedObserver() {
            this.pGrid = (AlienGrid)GameObjectManager.Find(GameObject.Name.AlienGrid);
        }

        public override void Notify()
        {
            float rate = this.pGrid.GetMoveRate();
            rate -= this.pGrid.GetRateChange();
            this.pGrid.SetMoveRate(rate);
        }
    }
}
