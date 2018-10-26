using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKController : MonoBehaviour {

    protected Animator animator;

    public bool ikActive = false;
    [Header("ControllerObject")]
    public Transform rightHandObj = null;
    public Transform leftHandObj = null;
    public Transform rightFootObj = null;
    public Transform leftFootObj = null;
    public Transform lookObj = null;

    [Header("LookWeight")]
    [SerializeField, Range(0.0f,1.0f)]
    private float lookTotalWeight = 0.0f;
    [SerializeField, Range(0.0f, 1.0f)]
    private float bodyWeight = 0.0f;
    [SerializeField, Range(0.0f, 1.0f)]
    private float headWeight = 0.0f;
    [SerializeField, Range(0.0f, 1.0f)]
    private float eyeWeight = 0.0f;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnAnimatorIK(int layerIndex)
    {
        if (!animator) return;

        if (ikActive)
        {
            // 指定されている場合、視線のターゲット位置を設定します
            if(lookObj != null)
            {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(lookObj.position);
            }
            // 指定されている場合、右手のターゲット位置と回転を設定します
            if(rightHandObj != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                
            }
            if(leftHandObj != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            }
            if(rightFootObj != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootObj.position);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootObj.rotation);
            }
            if(leftFootObj != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootObj.position);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootObj.rotation);
            }
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            animator.SetLookAtWeight(0);
        }


        if(lookObj != null)
        {
            animator.SetLookAtWeight(lookTotalWeight, bodyWeight, headWeight, eyeWeight);
            animator.SetLookAtPosition(lookObj.transform.position);
        }
    }
}
