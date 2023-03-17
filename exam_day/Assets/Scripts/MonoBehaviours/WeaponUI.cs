using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public GameObject weaponUI;
    public WeaponUpgrades weaponUpgrades;

    public CoinCounter coinCounter;

    public List<GameObject> weaponUIList = new List<GameObject>();

    public Sprite damageSprite;
    public Sprite ammoSpeedSprite;
    public Sprite weaponSpreadSprite;
    public Sprite weaponExplosiveSprite;

    // Start is called before the first frame update
    void Start()
    {
        int maxUpgrades = weaponUpgrades.upgrades.Count;
        int count = 0;
        //Go through all the child objects of the weaponUI
        foreach(Transform child in weaponUI.transform){
            count+=1;
            if(count <= maxUpgrades){
                weaponUIList.Add(child.gameObject);
                child.gameObject.SetActive(true);
            }else{
                child.gameObject.SetActive(false);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        int counter = 0;
        foreach(GameObject weaponUI in weaponUIList){
            if(weaponUI.activeSelf == true){
                UpdateWeaponUI(weaponUI, (WeaponUpgrades.UpgradeType)counter);
                counter+=1;
            }
        }
    }

    void UpdateWeaponUI(GameObject weaponUI, WeaponUpgrades.UpgradeType upgrade){
        if(weaponUI == null){
            return;
        }

        //Go through all the child objects of the weaponUI
        foreach(Transform child in weaponUI.transform){
            float upgradeLevel = weaponUpgrades.upgrades[upgrade]+1;
            float upgradeCost = weaponUpgrades.upgradeCost[upgrade];
            float upgradeCostTotal = upgradeLevel * upgradeCost;

            bool isMaxLevel = (upgradeLevel -1 == weaponUpgrades.upgradeMax[upgrade]);

            if(child.gameObject.name == "LevelText"){
                child.gameObject.GetComponent<TextMeshProUGUI>().text = (upgradeLevel-1).ToString() + "/" + weaponUpgrades.upgradeMax[upgrade].ToString();

            }
            if(child.gameObject.name == "KeyText"){
                child.gameObject.GetComponent<TextMeshProUGUI>().text = upgradeCostTotal.ToString();
                continue;
            }
            if(child.gameObject.name == "UpgradeMask"){
                //check the child of the upgrade mask
                foreach(Transform upgradeMaskChild in child.transform){
                    if(upgradeMaskChild.gameObject.name == "UpgradeIcon"){
                        if(upgrade == WeaponUpgrades.UpgradeType.DAMAGE){
                            upgradeMaskChild.gameObject.GetComponent<Image>().sprite = damageSprite;
                        }
                        if(upgrade == WeaponUpgrades.UpgradeType.AMMO_SPEED){
                            upgradeMaskChild.gameObject.GetComponent<Image>().sprite = ammoSpeedSprite;
                        }
                        if(upgrade == WeaponUpgrades.UpgradeType.WEAPON_SPREAD){
                            upgradeMaskChild.gameObject.GetComponent<Image>().sprite = weaponSpreadSprite;
                        }
                        if(upgrade == WeaponUpgrades.UpgradeType.WEAPON_EXPLOSIVE){
                            upgradeMaskChild.gameObject.GetComponent<Image>().sprite = weaponExplosiveSprite;
                        }
                    }

                    float coinCount = coinCounter.value;
                    float fillAmount = 1;

                    if(fillAmount > 1){
                        fillAmount = 1;
                    }

                    if(isMaxLevel){
                        fillAmount = 1;
                    }
                    
                    upgradeMaskChild.gameObject.GetComponent<Image>().fillAmount = fillAmount;

                }

            }
        }
        

    }
}
