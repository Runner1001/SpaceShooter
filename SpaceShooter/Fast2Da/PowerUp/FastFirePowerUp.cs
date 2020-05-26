using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Fast2Da
{
    class FastFirePowerUp : PowerUp
    {
        public FastFirePowerUp(Vector2 spritePosition) : base(spritePosition, "powerUp_Nrg")
        {
            sprite.SetAdditiveTint(0, -0.6f, 0, 0);
        }

        protected override void OnAttach(Player player)
        {
            attachedPlayer = player;
            attachedPlayer.ShootDelay = 0.2f;
            duration = 0;
            IsActive = false;
        }

        public override void Update()
        {
            base.Update();
            if (attachedPlayer!=null)
            {//he's attached (and not active!)
                duration += Game.DeltaTime;
                if (duration >= 5)
                {
                    OnDetach();
                }
            }
        }
        protected override void OnDetach()
        {
            attachedPlayer.ShootDelay = 0.45f;
            base.OnDetach();
        }
    }
}
