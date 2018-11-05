using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    private float num;
    private float Count;
    [SerializeField]
    private GameObject MainCamera;
    [SerializeField]
    private GameObject SubCamera;
    [SerializeField]
    private GameObject Cannons;

    private float Rate;
	// Use this for initialization
	void Start () {
        num = 2;
        Rate = 0;
        Count = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Count < 360)
        {
            this.transform.Rotate(0, num, 0);
            Count += num;
        }
        else
        {
            transform.position = Vector3.Lerp(this.transform.position, MainCamera.transform.position, Rate);
            transform.rotation = Quaternion.Lerp(this.transform.rotation, MainCamera.transform.rotation, Rate);
            Rate += 0.05f;
        }
        if (Rate > 1 && !MainCamera.activeSelf)
        {
            MainCamera.SetActive(!MainCamera.activeSelf);
            SubCamera.SetActive(!SubCamera.activeSelf);
            Cannons.transform.Find("Cannon6Arc").gameObject.SetActive(true);
            Cannons.transform.Find("CannonMng").gameObject.SetActive(true);
        }
	}
}
