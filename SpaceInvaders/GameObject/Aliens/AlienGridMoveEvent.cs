using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienGridMoveEvent : Command
    {
        private int soundIndex;

        public AlienGridMoveEvent() {
            this.soundIndex = 1;
        }


        public override void Execute(float deltaTime)
        {
            AlienGrid pGrid = (AlienGrid)GameObjectManager.Find(GameObject.Name.AlienGrid);
            pGrid.MoveGrid();

            //CHANGE THIS... move to its own timer event?
            Sound test = SoundManager.Find(Sound.Name.Invader1);
            switch (this.soundIndex) {
                case 1: test = SoundManager.Find(Sound.Name.Invader1);
                    break;
                case 2: test = SoundManager.Find(Sound.Name.Invader2);
                    break;
                case 3: test = SoundManager.Find(Sound.Name.Invader3);
                    break;
                case 4: test = SoundManager.Find(Sound.Name.Invader4);
                    break;
                default: Debug.Assert(false);
                    break;
            }
            this.soundIndex++;
            if (this.soundIndex > 4) {
                this.soundIndex = 1;
            }
            test.PlaySound();

            // Add itself back to timer
            TimerManager.Add(TimeEvent.Name.MoveAlienGrid, this, 0, deltaTime);
        }
    }
}
