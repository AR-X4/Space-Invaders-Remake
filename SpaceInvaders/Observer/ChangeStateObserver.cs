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
            if (SpaceInvaders.Player1Mode == false || ScenePlay.ShipLives == 1)
            {
                TimerManager.Add(TimeEvent.Name.SwitchState, this.pEvent, 1.4f);
            }
        }
    }
}
