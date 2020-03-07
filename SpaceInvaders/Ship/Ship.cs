using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class Ship : Leaf
    {
        // Data: --------------------
        public float shipSpeed;
        private ShipState state;
        public ShipManager.State CurrentStateName;

        public Ship(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
         : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.shipSpeed = 3.0f;
            this.state = null;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Bomb
            // Call the appropriate collision reaction
            other.VisitShip(this);
        }

        public void MoveRight()
        {
            this.state.MoveRight(this);
        }

        public void MoveLeft()
        {
            this.state.MoveLeft(this);
        }

        public void ShootMissile()
        {
            this.state.ShootMissile(this);
        }
        public void PlayShootSound()
        {
            this.state.PlayShootSound();
        }

        public void SetState(ShipManager.State inState)
        {
            this.state = ShipManager.GetState(inState);
            this.CurrentStateName = inState;
            Debug.WriteLine(this.state);
        }
        public void Handle()
        {
            this.state.Handle(this);
        }
        public ShipState GetState()
        {
            return this.state;
        }
    }
}
