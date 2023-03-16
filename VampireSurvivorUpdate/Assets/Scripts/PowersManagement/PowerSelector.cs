using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSelector : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Canvas of the power selector")]
    private Canvas canvas;
    [SerializeField]
    private GameObject informationsPower_02;
    [SerializeField]
    private GameObject informationsPower_07;
    [SerializeField]
    private GameObject informationsPower_09;
    [SerializeField]
    private GameObject informationsPower_12;

    [SerializeField]
    private GameObject Power_02;
    [SerializeField]
    private GameObject Power_07;
    [SerializeField]
    private GameObject Power_09;
    [SerializeField]
    private GameObject Power_12;


    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PowersManager.instance.hasPower("Ray Of Light")){
            informationsPower_09.GetComponentInChildren<Image>().color = new Color(0, 1, 0, 1);
        } else {
            informationsPower_09.GetComponentInChildren<Image>().color = new Color(1, 0, 0, 1);
        }



        if(Input.GetKeyDown(KeyCode.P)){
            if(canvas.enabled){
                canvas.enabled = false;
            } else {
                canvas.enabled = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            if(!canvas.enabled) return;
            if(PowersManager.instance.hasPower("Ray Of Light")){
                foreach(Power _power in PowersManager.instance.getPowers()){
                    if(_power.PowerData.Name == "Ray Of Light"){
                        PowersManager.instance.removePower(_power);
                    }
                }
            } else {
                Power _power = PowersManager.instance.InstantiateAndRegisterPower(Power_09);
            }
        }
    }
}
