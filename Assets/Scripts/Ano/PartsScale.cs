using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsScale : MonoBehaviour {
    //選択無し、左手、右手、左足、右足、両手両足
    public enum Parts {NotParts,LeftHand, RightHand,LeftCalf, RightCalf,AllParts}
    //変化なし、加算、減算
    public enum SubTraction { NoChange,AddChange,SubChange }
    //左手
    public Transform Hand_L;
    //右手
    public Transform Hand_R;
    //左足
    public Transform Calf_L;
    //右足
    public Transform Calf_R;
    //デフォルトサイズ用
    private Vector3 PartsSizeDef;
    //最大サイズ
    public Vector3 PartsSizeMin;
    //最小サイズ
    public Vector3 PartsSizeMax;
    //変化スピード(デバッグ用にpublicにしてるのでそのうちprivateに変えます)
    public float Speed = 0;
    //変化させる場所(デバッグ用にpublicにしてるのでそのうちprivateに変えます)
    public Parts SelectParts;
    //減少か加算か(デバッグ用にpublicにしてるのでそのうちprivateに変えます)
    public SubTraction Pattern;
    //デフォルトに戻すか(デバッグ用にpublicにしてるのでそのうちprivateに変えます)
    public bool DefaultMode = false;
    //デフォから動けばfalse
    private bool DefHand_L = true;
    private bool DefHand_R = true;
    private bool DefCalf_L = true;
    private bool DefCalf_R = true;
    private void Awake()
    {
        //基準値に合わせる
        PartsSizeDef.x = 1;
        PartsSizeDef.y = 1;
        PartsSizeDef.z = 1;
        Hand_L.localScale = PartsSizeDef;
        Hand_R.localScale = PartsSizeDef;
        Calf_L.localScale = PartsSizeDef;
        Calf_R.localScale = PartsSizeDef;

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PartsVariation();
        DefaultSize();
    }
    //これを呼び出して設定すれば拡縮全てできる
    public void PartsVariationSetting(Parts PartsSelect, SubTraction Subtraction,float speed)
    {
        SelectParts = PartsSelect;
        Pattern = Subtraction;
        Speed = speed;
    }
    //これを呼べば元に戻す処理が走る
    public void DefultSizeSetting()
    {
        DefaultMode = true;
    }
    //Defaultサイズに戻す処理
    void DefaultSize()
    {


        if (DefaultMode)
        {
            SelectParts = Parts.NotParts;
            Pattern = SubTraction.NoChange;
            //オーバー分
            if ((Hand_L.localScale.x > PartsSizeDef.x) &&
                (Hand_L.localScale.y > PartsSizeDef.y) &&
                (Hand_L.localScale.z > PartsSizeDef.z))
            {
                Hand_L.localScale = new Vector3(Hand_L.localScale.x - Speed, Hand_L.localScale.y - Speed, Hand_L.localScale.z - Speed);
                if ((Hand_L.localScale.x < PartsSizeDef.x) &&
                    (Hand_L.localScale.y < PartsSizeDef.y) &&
                    (Hand_L.localScale.z < PartsSizeDef.z))
                {
                    Hand_L.localScale = PartsSizeDef;
                    DefHand_L = true;
                }

            }
            if ((Hand_R.localScale.x > PartsSizeDef.x) &&
                (Hand_R.localScale.y > PartsSizeDef.y) &&
                (Hand_R.localScale.z > PartsSizeDef.z))
            {
                Hand_R.localScale = new Vector3(Hand_L.localScale.x - Speed, Hand_L.localScale.y - Speed, Hand_L.localScale.z - Speed);
                if ((Hand_R.localScale.x < PartsSizeDef.x) &&
                    (Hand_R.localScale.y < PartsSizeDef.y) &&
                    (Hand_R.localScale.z < PartsSizeDef.z))
                {
                    Hand_R.localScale = PartsSizeDef;
                    DefHand_R = true;
                }

            }
            if ((Calf_L.localScale.x > PartsSizeDef.x) &&
                (Calf_L.localScale.y > PartsSizeDef.y) &&
                (Calf_L.localScale.z > PartsSizeDef.z))
            {
                Calf_L.localScale = new Vector3(Hand_L.localScale.x - Speed, Hand_L.localScale.y - Speed, Hand_L.localScale.z - Speed);
                if ((Calf_L.localScale.x < PartsSizeDef.x) &&
                    (Calf_L.localScale.y < PartsSizeDef.y) &&
                    (Calf_L.localScale.z < PartsSizeDef.z))
                {
                    Calf_L.localScale = PartsSizeDef;
                    DefCalf_L = true;
                }
            }
            if ((Calf_R.localScale.x > PartsSizeDef.x) &&
                (Calf_R.localScale.y > PartsSizeDef.y) &&
                (Calf_R.localScale.z > PartsSizeDef.z))
            {
                Calf_R.localScale = new Vector3(Hand_L.localScale.x - Speed, Hand_L.localScale.y - Speed, Hand_L.localScale.z - Speed);
                if ((Calf_R.localScale.x < PartsSizeDef.x) &&
                    (Calf_R.localScale.y < PartsSizeDef.y) &&
                    (Calf_R.localScale.z < PartsSizeDef.z))
                {
                    Calf_R.localScale = PartsSizeDef;
                    DefCalf_R = true;
                }

            }
            //オーバー分終わり
            //未満分
            if ((Hand_L.localScale.x < PartsSizeDef.x) &&
                (Hand_L.localScale.y < PartsSizeDef.y) &&
                (Hand_L.localScale.z < PartsSizeDef.z))
            {
                Hand_L.localScale = new Vector3(Hand_L.localScale.x + Speed, Hand_L.localScale.y + Speed, Hand_L.localScale.z + Speed);
                if ((Hand_L.localScale.x > PartsSizeDef.x) &&
                    (Hand_L.localScale.y > PartsSizeDef.y) &&
                    (Hand_L.localScale.z > PartsSizeDef.z))
                {
                    Hand_L.localScale = PartsSizeDef;
                    DefHand_L = true;
                }

            }
            if ((Hand_R.localScale.x < PartsSizeDef.x) &&
                (Hand_R.localScale.y < PartsSizeDef.y) &&
                (Hand_R.localScale.z < PartsSizeDef.z))
            {
                Hand_R.localScale = new Vector3(Hand_L.localScale.x + Speed, Hand_L.localScale.y + Speed, Hand_L.localScale.z + Speed);
                if ((Hand_R.localScale.x > PartsSizeDef.x) &&
                    (Hand_R.localScale.y > PartsSizeDef.y) &&
                    (Hand_R.localScale.z > PartsSizeDef.z))
                {
                    Hand_R.localScale = PartsSizeDef;
                    DefHand_R = true;
                }

            }
            if ((Calf_L.localScale.x < PartsSizeDef.x) &&
                (Calf_L.localScale.y < PartsSizeDef.y) &&
                (Calf_L.localScale.z < PartsSizeDef.z))
            {
                Calf_L.localScale = new Vector3(Hand_L.localScale.x + Speed, Hand_L.localScale.y + Speed, Hand_L.localScale.z + Speed);
                if ((Calf_L.localScale.x > PartsSizeDef.x) &&
                    (Calf_L.localScale.y > PartsSizeDef.y) &&
                    (Calf_L.localScale.z > PartsSizeDef.z))
                {
                    Calf_L.localScale = PartsSizeDef;
                    DefCalf_L = true;
                }
            }
            if ((Calf_R.localScale.x < PartsSizeDef.x) &&
                (Calf_R.localScale.y < PartsSizeDef.y) &&
                (Calf_R.localScale.z < PartsSizeDef.z))
            {
                Calf_R.localScale = new Vector3(Hand_L.localScale.x + Speed, Hand_L.localScale.y + Speed, Hand_L.localScale.z + Speed);
                if ((Calf_R.localScale.x > PartsSizeDef.x) &&
                    (Calf_R.localScale.y > PartsSizeDef.y) &&
                    (Calf_R.localScale.z > PartsSizeDef.z))
                {
                    Calf_R.localScale = PartsSizeDef;
                    DefCalf_R = true;
                }

            }
            //未満分終わり
            if((DefHand_L)&&(DefHand_R)&&(DefCalf_L)&&(DefCalf_R))
            {
                DefaultMode = false;
            }
        }  
    }
    //サイズ変更処理
    public void PartsVariation()
    {
        //どのパーツを拡縮するか
        switch (SelectParts)
        {
            case Parts.NotParts:
                Pattern = SubTraction.NoChange;
                break;
            case Parts.LeftHand:
                DefHand_L = false;
                if (Pattern==SubTraction.AddChange)
                {
                    Hand_L.localScale = new Vector3(Hand_L.localScale.x + Speed, Hand_L.localScale.y + Speed, Hand_L.localScale.z + Speed);
                    if(MaxOrMinSizeCheck(Hand_L, Pattern))
                    {
                        Hand_L.localScale = PartsSizeMax;
                        SelectParts = Parts.NotParts;
                        Pattern = SubTraction.NoChange;
                    }
                }
                else if(Pattern == SubTraction.SubChange)
                {
                    Hand_L.localScale = new Vector3(Hand_L.localScale.x - Speed, Hand_L.localScale.y - Speed, Hand_L.localScale.z - Speed);
                    if (MaxOrMinSizeCheck(Hand_L, Pattern))
                    {
                        Hand_L.localScale = PartsSizeMin;
                        SelectParts = Parts.NotParts;
                        Pattern = SubTraction.NoChange;
                    }
                }
                break;
            case Parts.RightHand:
                DefHand_R =  false;
                if (Pattern == SubTraction.AddChange)
                {
                    Hand_R.localScale = new Vector3(Hand_R.localScale.x + Speed, Hand_R.localScale.y + Speed, Hand_R.localScale.z + Speed);
                    if (MaxOrMinSizeCheck(Hand_R, Pattern))
                    {
                        Hand_R.localScale = PartsSizeMax;
                        SelectParts = Parts.NotParts;
                        Pattern = SubTraction.NoChange;
                    }
                }
                else if (Pattern == SubTraction.SubChange)
                {
                    Hand_R.localScale = new Vector3(Hand_R.localScale.x - Speed, Hand_R.localScale.y - Speed, Hand_R.localScale.z - Speed);
                    if (MaxOrMinSizeCheck(Hand_R, Pattern))
                    {
                        Hand_R.localScale = PartsSizeMin;
                        SelectParts = Parts.NotParts;
                        Pattern = SubTraction.NoChange;
                    }
                }

                break;
            case Parts.LeftCalf:
                DefCalf_L = false;
                if (Pattern == SubTraction.AddChange)
                {
                    Calf_L.localScale = new Vector3(Calf_L.localScale.x + Speed, Calf_L.localScale.y + Speed, Calf_L.localScale.z + Speed);
                    if (MaxOrMinSizeCheck(Calf_L, Pattern))
                    {
                        Calf_L.localScale = PartsSizeMax;
                        SelectParts = Parts.NotParts;
                        Pattern = SubTraction.NoChange;
                    }
                }
                else if (Pattern == SubTraction.SubChange)
                {
                    Calf_L.localScale = new Vector3(Calf_L.localScale.x - Speed, Calf_L.localScale.y - Speed, Calf_L.localScale.z - Speed);
                    if (MaxOrMinSizeCheck(Calf_L, Pattern))
                    {
                        Calf_L.localScale = PartsSizeMin;
                        SelectParts = Parts.NotParts;
                        Pattern = SubTraction.NoChange;
                    }
                }

                break;
            case Parts.RightCalf:
                DefCalf_R = false;
                if (Pattern == SubTraction.AddChange)
                {
                    Calf_R.localScale = new Vector3(Calf_R.localScale.x + Speed, Calf_R.localScale.y + Speed, Calf_R.localScale.z + Speed);
                    if (MaxOrMinSizeCheck(Calf_R, Pattern))
                    {
                        Calf_R.localScale = PartsSizeMax;
                        SelectParts = Parts.NotParts;
                        Pattern = SubTraction.NoChange;
                    }
                }
                else if (Pattern == SubTraction.SubChange)
                {
                    Calf_R.localScale = new Vector3(Calf_R.localScale.x - Speed, Calf_R.localScale.y - Speed, Calf_R.localScale.z - Speed);
                    if (MaxOrMinSizeCheck(Calf_R, Pattern))
                    {
                        Calf_R.localScale = PartsSizeMin;
                        SelectParts = Parts.NotParts;
                        Pattern = SubTraction.NoChange;
                    }
                }

                break;
            case Parts.AllParts:
                DefHand_L = false;
                DefHand_R = false;
                DefCalf_L = false;
                DefCalf_R = false;
                if (Pattern == SubTraction.AddChange)
                {
                    Hand_L.localScale = new Vector3(Hand_L.localScale.x + Speed, Hand_L.localScale.y + Speed, Hand_L.localScale.z + Speed);
                    Hand_R.localScale = new Vector3(Hand_R.localScale.x + Speed, Hand_R.localScale.y + Speed, Hand_R.localScale.z + Speed);
                    Calf_L.localScale = new Vector3(Calf_L.localScale.x + Speed, Calf_L.localScale.y + Speed, Calf_L.localScale.z + Speed);
                    Calf_R.localScale = new Vector3(Calf_R.localScale.x + Speed, Calf_R.localScale.y + Speed, Calf_R.localScale.z + Speed);
                    if ((MaxOrMinSizeCheck(Hand_L, Pattern))&&
                        (MaxOrMinSizeCheck(Hand_R, Pattern)) &&
                        (MaxOrMinSizeCheck(Calf_L, Pattern)) &&
                        (MaxOrMinSizeCheck(Calf_R, Pattern)))
                    {
                        Hand_L.localScale = PartsSizeMax;
                        Hand_R.localScale = PartsSizeMax;
                        Calf_L.localScale = PartsSizeMax;
                        Calf_R.localScale = PartsSizeMax;
                        SelectParts = Parts.NotParts;
                        Pattern = SubTraction.NoChange;
                    }
                }
                else if (Pattern == SubTraction.SubChange)
                {
                    Hand_L.localScale = new Vector3(Hand_L.localScale.x - Speed, Hand_L.localScale.y - Speed, Hand_L.localScale.z - Speed);
                    Hand_R.localScale = new Vector3(Hand_R.localScale.x - Speed, Hand_R.localScale.y - Speed, Hand_R.localScale.z - Speed);
                    Calf_L.localScale = new Vector3(Calf_L.localScale.x - Speed, Calf_L.localScale.y - Speed, Calf_L.localScale.z - Speed);
                    Calf_R.localScale = new Vector3(Calf_R.localScale.x - Speed, Calf_R.localScale.y - Speed, Calf_R.localScale.z - Speed);
                    if ((MaxOrMinSizeCheck(Hand_L, Pattern)) &&
                        (MaxOrMinSizeCheck(Hand_R, Pattern)) &&
                        (MaxOrMinSizeCheck(Calf_L, Pattern)) &&
                        (MaxOrMinSizeCheck(Calf_R, Pattern)))
                    {
                        Hand_L.localScale = PartsSizeMin;
                        Hand_R.localScale = PartsSizeMin;
                        Calf_L.localScale = PartsSizeMin;
                        Calf_R.localScale = PartsSizeMin;
                        SelectParts = Parts.NotParts;
                        Pattern = SubTraction.NoChange;
                    }
                }
                break;
        }
    }
    //最大最小をオーバーしてないかの処理
    private bool MaxOrMinSizeCheck(Transform Trans, SubTraction Subtraction)
    {
        if(Subtraction==SubTraction.AddChange)
        {
            if ((Trans.localScale.x > PartsSizeMax.x) &&
                (Trans.localScale.y > PartsSizeMax.y) &&
                (Trans.localScale.z > PartsSizeMax.z))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (Subtraction == SubTraction.SubChange)
        {
            if ((Trans.localScale.x < PartsSizeMin.x) &&
                (Trans.localScale.y < PartsSizeMin.y) &&
                (Trans.localScale.z < PartsSizeMin.z))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    
}
