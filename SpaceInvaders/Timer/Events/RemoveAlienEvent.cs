using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveAlienEvent : Command
    {
        //data
        private GameObject pAlien;

        public RemoveAlienEvent()
        {

            
            this.pAlien = null;

        }
        public void SetAlien(GameObject pAlien) {
            Debug.Assert(pAlien != null);
            this.pAlien = pAlien;
        }

        public override void Execute(float deltaTime)
        {
            Debug.Assert(this.pAlien != null);
            this.pAlien.Remove();

            if (this.pAlien.name != GameObject.Name.OrangeSaucer)
            {
                AlienGrid pGrid = (AlienGrid)GameObjectManager.Find(GameObject.Name.AlienGrid);
                Debug.Assert(pGrid != null);
                pGrid.DecreaseAlienCount();
            }
        }
    }
}
