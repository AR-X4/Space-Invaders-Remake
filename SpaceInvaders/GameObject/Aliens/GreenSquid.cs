﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GreenSquid : Leaf
    {
        public GreenSquid(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        ~GreenSquid()
        {

        }
        override public void Move()
        {
            this.x += delta;
            if (this.x < 0.0f || this.x > 800.0f)
            {
                delta *= -1;
            }
        }
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitGreenSquid(this);
        }

        public override void VisitMissile(Missile m)
        {
           
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Alien
            Debug.WriteLine("-------> Done  <--------");

            m.Hit();
        }
        public override void Update()
        {

            base.Update();
        }
        // this is just a placeholder, who knows what data will be stored here

    }
}
