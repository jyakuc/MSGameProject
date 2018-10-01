using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour {

    public GameObject Player1, Player2, Player3;


    public enum Select
    {
        Left,
        Center,
        Right
    }

    public Select Status;

	// Use this for initialization
	void Start () {
        Instantiate(Player1, new Vector3(-5.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(Player2, new Vector3( 0.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(Player3, new Vector3( 5.0f, 0.0f, 0.0f), Quaternion.identity);
        Status = Select.Left;
	}
	
	// Update is called once per frame
	void Update () {
        switch (Status)
        {
            case Select.Left:
                Player1.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Status = Select.Right;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Status = Select.Center;
                }
                Debug.Log("player1");
                break;
            case Select.Center:
                Player2.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Status = Select.Left;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Status = Select.Right;
                }
                Debug.Log("player2");
                break;
            case Select.Right:
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
}
