using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour {

    [SerializeField]
    private GameObject Player1, Player2, Player3;
    

    public enum Select
    {
        Left,
        Center,
        Right
    }

    public Select Status;

	// Use this for initialization
	void Start () {
        PlayerInit();
        Status = Select.Left;
	}
	
	// Update is called once per frame
	void Update () {
        switch (Status)
        {
            case Select.Left:
                Player1.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                Player2.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Player3.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Status = Select.Right;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Status = Select.Center;
                }
                break;
            case Select.Center:
                Player1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Player2.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                Player3.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Status = Select.Left;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Status = Select.Right;
                }
                break;
            case Select.Right:
                Player1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Player2.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Player3.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Status = Select.Center;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Status = Select.Left;
                }
                break;
        }
	}
    void PlayerInit()
    {
        Instantiate(Player1, new Vector3(-5.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(Player2, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(Player3, new Vector3(5.0f, 0.0f, 0.0f), Quaternion.identity);
        Player1 = GameObject.Find("Character1(Clone)");
        Player2 = GameObject.Find("Character2(Clone)");
        Player3 = GameObject.Find("Character3(Clone)");
    }
}
