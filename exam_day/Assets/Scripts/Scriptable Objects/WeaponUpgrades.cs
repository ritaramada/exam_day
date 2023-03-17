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
        WEAPON_EXPLOSIVE
        
    }

    
    public Dictionary<UpgradeType, int> upgrades = new Dictionary<UpgradeType, int>(){
        {UpgradeType.DAMAGE, 0},
        {UpgradeType.AMMO_SPEED, 0},
        {UpgradeType.WEAPON_SPREAD, 0},
        {UpgradeType.WEAPON_EXPLOSIVE, 0}
    };

    public Dictionary<UpgradeType, int> upgradeCost = new Dictionary<UpgradeType, int>(){
        {UpgradeType.DAMAGE, 1},
        {UpgradeType.AMMO_SPEED, 1},
        {UpgradeType.WEAPON_SPREAD, 3},
        {UpgradeType.WEAPON_EXPLOSIVE, 3}
    };

    public Dictionary<UpgradeType, int> upgradeMax = new Dictionary<UpgradeType, int>(){
        {UpgradeType.DAMAGE, 10},
        {UpgradeType.AMMO_SPEED, 10},
        {UpgradeType.WEAPON_SPREAD, 4},
        {UpgradeType.WEAPON_EXPLOSIVE, 2}
    };
    
}
