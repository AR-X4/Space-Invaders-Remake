
using System.Diagnostics;

namespace SpaceInvaders
{
    public class TimeEvent : DLink
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            MoveAlienGrid,
            SpriteAnimation,
            DropBomb,
            UFOSpawn,
            PlayUFOSound,
            RemoveAlien,
            RemoveShip,
            RemoveBomb,
            RemoveMissile,
            SwapShipExplosion,
            SwitchState,
            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private Name name;
        private Command pCommand;
        public float triggerTime;
        public float deltaTime;

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public TimeEvent()
            : base()
        {
            this.name = TimeEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------

        public void Set(TimeEvent.Name eventName, Command pCommand, float deltaTimeToTrigger)
        {
            Debug.Assert(pCommand != null);

            this.name = eventName;
            this.pCommand = pCommand;
            this.deltaTime = deltaTimeToTrigger;

            // set the trigger time
            this.triggerTime = TimerManager.GetCurrTime() + deltaTimeToTrigger;
        }
        public void Process()
        {
            // make sure the command is valid
            Debug.Assert(this.pCommand != null);
            // fire off command
            this.pCommand.Execute(deltaTime);
        }
        public new void Clear()   // the "new" is there to shut up warning - overriding at derived class
        {
            this.name = TimeEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }
        public void Wash()
        {
            // Wash - clear the entire hierarchy
            base.Clear();
            this.Clear();
        }
        public void SetName(TimeEvent.Name inName)
        {
            this.name = inName;
        }
        public TimeEvent.Name GetName()
        {
            return this.name;
        }
        public Command GetCommand() {
            return this.pCommand;
        }
        public void Dump()
        {

            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("      Command: {0}", this.pCommand);
            Debug.WriteLine("   Event Name: {0}", this.name);
            Debug.WriteLine(" Trigger Time: {0}", this.triggerTime);
            Debug.WriteLine("   Delta Time: {0}", this.deltaTime);

            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                TimeEvent pTmp = (TimeEvent)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                TimeEvent pTmp = (TimeEvent)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }
    }
}


