using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public int duration;
    public abstract void ActivatePowerUp();
    public abstract void DeactivatePowerUp();


}
