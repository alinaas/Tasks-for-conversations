using AwesomeLib.Cellphone;
using AwesomeLib.Eyes;
using AwesomeLib.Legs;
using AwesomeLib.Weapon;
using System.Collections.Generic;

namespace AwesomeLib
{
    public class Bot
    {
        private readonly ILegs _legs;
        private readonly IEyes _eyes;
        private readonly IWeapon _weapon;
        private readonly ICellphone _cellphone;

        public Bot(IWeapon weapon, ILegs legs, IEyes eyes, ICellphone cellphone)
        {
            _legs = legs;
            _eyes = eyes;
            _weapon = weapon;
            _cellphone = cellphone;
        }

        public bool KillMob()
        {
            var location = _eyes.CheckForMob();
            if (location == 0)
                return false;

            _legs.MoveTo(location);

            var damageList = new List<int>();
            for (var i = 0; i < 3; i++)
            {
                damageList.Add(_weapon.Strike());
            }

            _cellphone.ReportDamage(damageList.ToArray());
            
            var mobStatus = _eyes.CheckMobStatus();

            _cellphone.ReportResult(new Report
            {
                IsSuccess = mobStatus
            });

            return true;

            //_weapon.Ext(2);
            //WeaponExtensions.Ext(_weapon, 2);
        }
    }

    public static class WeaponExtensions
    {
        public static void Ext(this IWeapon a, int d)
        {

        }
    }
}
