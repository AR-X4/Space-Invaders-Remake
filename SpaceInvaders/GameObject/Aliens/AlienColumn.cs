﻿using System.Diagnostics;

namespace SpaceInvaders
{
    //Specific type of composite
    public class AlienColumn : Composite
    {
        public BombState state;
        public Bomb pBomb;

        public AlienColumn(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
        : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            BombManager.SetState(BombManager.StateName.Ready, this);
            this.pBomb = BombManager.CreateBomb();

            this.poColObj.pColSprite.SetLineColor(0, 1, 1);
        }
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an BirdColumn
            // Call the appropriate collision reaction            
            other.VisitColumn(this);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdColumn vs MissileGroup
            // Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            CollisionPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);

            base.Update();
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
            
            //remove missile from composite... missile only has one parent..need to find root for others? 
            pParent.Remove(this);
            pParent.Update();

            // Now remove it
            base.Remove();
        }

        public float GetBottom() {
            return this.poColObj.poColRect.y - this.poColObj.poColRect.height / 2;
        }

        public void DropBomb() {
            this.state.DropBomb(this);
         
        }

        public void Handle()
        {
            this.state.Handle(this);
        }
    }
}
