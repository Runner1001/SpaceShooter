using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Fast2Da
{
    class RedLaserBullet : PlayerBullet
    {
        public RedLaserBullet(string spritesheetName = "bullets") : base(spritesheetName)
        {
            Type = BulletManager.BulletType.RedLaser;
        }
    }
}
