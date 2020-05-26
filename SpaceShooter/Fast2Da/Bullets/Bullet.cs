using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    abstract class Bullet : GameObject
    {
        protected Vector2 shootVelocity;
        protected float damage;
        public BulletManager.BulletType Type { get; protected set; }
        public Bullet(string spritesheetName="bullets") : base(Vector2.Zero, spritesheetName,DrawManager.Layer.Playground)
        {
            sprite.pivot = new Vector2(Width / 2, Height / 2);
            shootVelocity = new Vector2(400, 0);
            IsActive = false;
            RigidBody = new RigidBody(sprite.position, this);
            damage = 10;
        }

        public virtual void Shoot(Vector2 startPos)
        {
            IsActive = true;
            Position = startPos;
            Velocity = shootVelocity;
        }

        public virtual void OnDie()
        {
            IsActive = false;
            //restore bullet
            BulletManager.RestoreBullet(this);
        }

        
    }
}
