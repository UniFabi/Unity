using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public float maximumShieldEnergy;
    //Recharge Rate in Units per Second
    public float shieldRechargeRate;
    public float shieldEnergy;

    //Reduces shieldEnergy by given amount, returns amount of extra damage if shield is broken
    public float reduceShield(float damage){
        if(shieldEnergy - damage < 0){
            shieldEnergy = 0;
            return damage - shieldEnergy;
        }
        else{
            shieldEnergy -= damage;
            return 0;
        }
    }

    void FixedUpdate(){
        if(shieldEnergy < maximumShieldEnergy){
            float recharge = Time.deltaTime * shieldRechargeRate;
            if(shieldEnergy + recharge > maximumShieldEnergy){
                shieldEnergy = maximumShieldEnergy;
            }
            else{
                shieldEnergy += recharge;
            }
        }
    }
}
