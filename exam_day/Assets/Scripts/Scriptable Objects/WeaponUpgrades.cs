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
        {UpgradeType.DAMAGE, 2},
        {UpgradeType.AMMO_SPEED, 20},
        {UpgradeType.WEAPON_SPREAD, 30},
        {UpgradeType.WEAPON_EXPLOSIVE, 100}
    };

    public Dictionary<UpgradeType, int> upgradeMax = new Dictionary<UpgradeType, int>(){
        {UpgradeType.DAMAGE, 10},
        {UpgradeType.AMMO_SPEED, 10},
        {UpgradeType.WEAPON_SPREAD, 2},
        {UpgradeType.WEAPON_EXPLOSIVE, 1}
    };
    
}
