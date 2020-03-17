using System;


namespace SpaceInvaders
{
    public class ChangeP2StateObserver : CollisionObserver
    {
        private SwitchStateEvent pEvent;

        public ChangeP2StateObserver()
        {
            this.pEvent = new SwitchStateEvent();
        }


        public override void Notify()
        {
           
            TimerManager.Add(TimeEvent.Name.SwitchState, this.pEvent, 1.4f);
            
        }
    }
}
