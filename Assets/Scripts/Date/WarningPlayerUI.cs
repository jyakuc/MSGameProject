using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WarningPlayerUI : MonoBehaviour {
    private List<Image> warningImages = new List<Image>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Display(List<int> disconnectedList)
    {
        int num = disconnectedList.Count;
    }
}
