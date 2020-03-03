using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : Leaf
    {

        // Data -------------------------------------
        private bool enable;
        public float delta;

        public Missile(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
            this.enable = false;
            this.delta = 5.0f;
        }

        public override void Update()
        {
            base.Update();
            this.y += delta;
        }

        ~Missile()
        {

        }
        public override void Remove()
        {
            // Keenan(delete.E)
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            base.Update();

            //// Update the parent (missile root)
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();
            //remove missile from composite... missile only has one parent..need to find root for others? 
            pParent.Remove(this);

            // Now remove it
            base.Remove();
        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Missile
            // Call the appropriate collision reaction            
            other.VisitMissile(this);
        }
        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }
        public void SetActive(bool state)
        {
            this.enable = state;
        }

    }
}