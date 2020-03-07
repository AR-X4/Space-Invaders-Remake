using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienGridMoveEvent : Command
    {

        public override void Execute(float deltaTime)
        {

            AlienGrid pGrid = (AlienGrid)GameObjectManager.Find(GameObject.Name.AlienGrid);
            pGrid.NotifyListeners();
            // Add itself back to timer
            TimerManager.Add(TimeEvent.Name.MoveAlienGrid, this, deltaTime);
        }
    }
}
