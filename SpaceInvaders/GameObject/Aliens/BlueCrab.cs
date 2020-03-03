using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class BlueCrab : Leaf
    {
        public BlueCrab(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        ~BlueCrab()
        {

        }
        
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitBlueCrab(this);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            // ALien vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs ALien
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Alien
            Debug.WriteLine("-------> Done  <--------");

            //m.Hit();
        }

        public override void Update()
        {
            
            base.Update();
        }
        // this is just a placeholder, who knows what data will be stored here

    }
}
