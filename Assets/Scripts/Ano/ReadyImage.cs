using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReadyImage : MonoBehaviour {
    public RelayTV Relay;
    private Image image;
    private WarmingSystem WarimingUp;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        try
        {
            WarimingUp = GameObject.Find("WarimingSystem").GetComponent<WarmingSystem>();
        }
        catch
        {
            WarimingUp = null;
            Debug.Log("WarimingSystemがありません");
        }

    }
	
	// Update is called once per frame
	void Update () {
	    if(WarimingUp.GetReadyFlag(Relay.GetPlayerNum()-1))
        {
            if(image.color.a<1)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.1f);
            }
            else
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
            }
        }	
	}
}
