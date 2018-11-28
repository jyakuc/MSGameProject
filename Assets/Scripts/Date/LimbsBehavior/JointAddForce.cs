using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAddForce : MonoBehaviour {
    private Rigidbody rigidbody;
    private Vector3 vector;
    public Transform axis;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void InitParameter(Vector3 param)
    {
        vector = param;
    }

    public void Rotation(bool right)
    {
        Vector3 vc = vector;
        vc = right ? vc : new Vector3(-vc.x, -vc.y, vc.z);

        vc = axis.TransformDirection(vc);

        rigidbody.AddForce(vc, ForceMode.Force);
    }
}
