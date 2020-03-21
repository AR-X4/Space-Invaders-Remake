using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class OrangeSaucer : Leaf
    {
        // Data: ---------------
        private float delta;
        
       

        public OrangeSaucer(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY)
        {
            this.x = posX;
            this.y = posY;

            this.delta = 0.0f;

        }

        ~OrangeSaucer()
        {

        }
        
        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitOrangeSaucer(this);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            if (this.bMarkForDeath == false)// to fix bug with collision with null objs
            {
                // MissileRoot vs WallRoot
                GameObject pGameObj = (GameObject)Iterator.GetChild(m);
                CollisionPair.Collide(pGameObj, this);
            }
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair pColPair = CollisionPairManager.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }
        public override void Update()
        {
            base.Update();

            this.x += delta;

            if (this.x < 20.0f || this.x > SpaceInvaders.ScreenWidth)
            {
                this.Remove();
            }
        }
        public override void Remove()
        {
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            this.pProxySprite.Set(GameSprite.Name.NullObject);
            this.delta = 0.0f;
            this.x = 0.0f;
        }
        public void SetDelta(float inDelta)
        {
            this.delta = inDelta;
        }

        private void Reset(float x, float delta) {
            this.pProxySprite.Set(this.pSpriteName);
            this.poColObj.poColRect.Set(this.pProxySprite.pSprite.GetScreenRect());
            this.x = x;
            this.delta = delta;

            this.bMarkForDeath = false;
        }

        public void RandomizeDirection() {
            float NewDelta = 0.0f;
            float NewX = 0.0f;
            float randF = RandomManager.RandomInt(1, 3);
            switch (randF) {
                case 1: 
                    NewX = 20f;
                    NewDelta = 1f;
                    break;
                case 2:
                    NewX = SpaceInvaders.ScreenWidth - 20f;
                    NewDelta = -1f;
                    break;
            
            }
            Debug.Assert(NewX != 0.0f);
            Debug.Assert(NewDelta != 0.0f);
            this.Reset(NewX, NewDelta);
        }
    }
}
