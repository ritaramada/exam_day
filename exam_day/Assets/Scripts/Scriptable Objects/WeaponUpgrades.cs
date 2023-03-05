using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "WeaponUpgrades")]
public class WeaponUpgrades : ScriptableObject
{
    public enum UpgradeType
    {
        DAMAGE,
        AMMO_SPEED,
        WEAPON_SPREAD,
        WEAPON_RANGE
        
    }

    public Dictionary<UpgradeType, int> upgrades = new Dictionary<UpgradeType, int>(){
        {UpgradeType.DAMAGE, 1},
        {UpgradeType.AMMO_SPEED, 1},
        {UpgradeType.WEAPON_SPREAD, 1},
        {UpgradeType.WEAPON_RANGE, 1}
    };


}
