using UnityEngine;
using UnityEngine.UI;

public class ShipDisplay : MonoBehaviour {

    public Text shieldLabel, hullLabel;
    public Hull hull;

    void Display(Text label, float value){
        label.text = "" + (int) value;
    }

    void Update () {
        if(hull != null){
            Display(shieldLabel, hull.GetComponentInChildren<Shield>().shieldEnergy);
            Display(hullLabel, hull.hullIntegrity);
        }
        else{
            Display(shieldLabel, -1);
            Display(hullLabel, -1);
        }
    }
}
