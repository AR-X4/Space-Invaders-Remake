using System;


namespace SpaceInvaders
{
    public class ChangeStateObserver : CollisionObserver
    {
        private SwitchStateEvent pEvent;

        public ChangeStateObserver() {
            this.pEvent = new SwitchStateEvent();
        }


        public override void Notify()
        {
            if (ScenePlay.ShipLives == 1 || (ScenePlay2.ShipLives > 0 && SpaceInvaders.Player1Mode == false))
            {
                TimerManager.Add(TimeEvent.Name.SwitchState, this.pEvent, 1.4f);
            }
            
        }
    }
}
