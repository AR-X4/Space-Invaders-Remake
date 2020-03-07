using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveAlienGridObserver : AlienObserver
    {
        
        public override void Notify()
        {
            AlienGrid pGrid = (AlienGrid)GameObjectManager.Find(GameObject.Name.AlienGrid);
            pGrid.MoveGrid();
        }
    }
}
