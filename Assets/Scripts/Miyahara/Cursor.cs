using UnityEngine;

public class Cursor : MonoBehaviour 
{
    public int PlayerID;
    public float Speed;
    private Vector2 Slope;
    private Vector3 pos;
    private Vector3 Oldpos;
    //private Vector3 CenterPos;

    private Vector2 Decrease = new Vector2(1.0f, 1.0f);


    private bool fireflg;
    public bool FireFlg
    {
        get { return fireflg; }
        set { fireflg = value; }
    }

    [SerializeField]
    private MyInputManager myInputManager;

    //private bool SlopeXFlg;
    //private bool SlopeYFlg;

    void Start()
    {
        pos = transform.position;
        //CenterPos = pos;
        fireflg = true;
        //SlopeXFlg = false;
        //SlopeYFlg = false;
        myInputManager = GameObject.FindObjectOfType<MyInputManager>();
        if (myInputManager == null)
            Debug.LogError("MyInputManagerがシーンにありません");
    }

    void Update()
    {
        if (!fireflg)
            return;

        Slope.x = Input.GetAxis("Horizontal_Player" + myInputManager.joysticks[PlayerID - 1].ToString());
        Slope.y = Input.GetAxis("Vertical_Player" + myInputManager.joysticks[PlayerID - 1].ToString());

        //if (Mathf.FloorToInt(Slope.x) == 0)           //横方向入力されていないときフラグを立てる
        //{
        //    SlopeXFlg = true;
        //}
        //else
        //{
        //    SlopeXFlg = false;
        //    Decrease.x = 1.0f;
        //}

        //if (Mathf.FloorToInt(Slope.y) == 0)           //縦方向入力されていないときフラグを立てる
        //{
        //    SlopeYFlg = true;
        //}
        //else
        //{
        //    SlopeYFlg = false;
        //    Decrease.y = 1.0f;
        //}
        //Debug.Log("slope.x"+Slope.x);
        //Debug.Log("slope.x" + Slope.y);
        //Debug.Log("x"+SlopeXFlg);
        //Debug.Log("y"+SlopeYFlg);

        //if (SlopeXFlg && pos.x != CenterPos.x)              //縦方向入力されていないとき徐々に減少
        //{
        //    Decrease.x -= 0.1f;
        //    Slope.x = Decrease.x;
        //}
        //if (SlopeYFlg && pos.y != CenterPos.y)              //横方向入力されていないとき徐々に減少
        //{
        //    Decrease.y -= 0.1f;
        //    Slope.y = Decrease.y;
        //}

        Vector3 OldInputPos = pos;
        pos.x = Slope.x * Speed;
        pos.z = Slope.y * Speed;

        transform.position += pos;

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
