using UnityEngine;

public class Cursor : MonoBehaviour 
{
    public int PlayerID;
    public float Speed;
    private Vector3 pos;
    private Vector3 Oldpos;
    private bool fireflg;
    public bool FireFlg
    {
        get { return fireflg; }
        set { fireflg = value; }
    }
    void Start()
    {
        pos = transform.position;
        fireflg = true;
    }

	void Update () 
	{
        if (!fireflg)
            return;

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 OldInputPos = pos;
        if (Input.GetAxis("GameController_Hori" + PlayerID.ToString()) < 0)
        {
            pos.x -= Speed;
            
        }
        else if (Input.GetAxis("GameController_Hori" + PlayerID.ToString()) > 0)
        {
            pos.x += Speed;
        }
        if (Input.GetAxis("GameController_Vert" + PlayerID.ToString()) < 0)
        {
            pos.z += Speed;
        }
        else if (Input.GetAxis("GameController_Vert" + PlayerID.ToString()) > 0)
        {
            pos.z -= Speed;
        }

        Ray ray = new Ray();
        ray.direction = Vector3.down;
        ray.origin = pos;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("RayCollider")))
        {
            transform.position = hit.point;
            Oldpos = transform.position;
        }
        else
        {
            if (OldInputPos.x != pos.x) //入力のポジション
            {
                transform.position = new Vector3(Oldpos.x, transform.position.y, transform.position.z);
                pos.x = OldInputPos.x;
            }
            if (OldInputPos.z != pos.z) //入力のポジション
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Oldpos.z);
                pos.z = OldInputPos.z;
            }

            
        }
	}
}
