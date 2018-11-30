using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WarningPlayerUI : MonoBehaviour {
    [SerializeField]
    private List<Image> warningImages = new List<Image>();
    [SerializeField]
    private List<Sprite> disconnectSprite = new List<Sprite>();
    [SerializeField]
    private List<Sprite> connectSprite = new List<Sprite>();
	// Use this for initialization
	void Start () {
		for(int i = 0; i < warningImages.Count; ++i)
        {
            warningImages[i].gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Display(List<int> disconnectedList)
    {
        for(int i = 0; i < warningImages.Count; ++i)
        {
            warningImages[i].gameObject.SetActive(true);
            warningImages[i].sprite = connectSprite[i];
        }
        for (int j = 0; j < disconnectedList.Count; ++j)
        {
            Debug.Log("hhhh"+disconnectedList[j]);
            warningImages[disconnectedList[j]-1].sprite = disconnectSprite[j];
        }
        
    }
}
