using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonSeen : MonoBehaviour
{
    public Camera mTPSCamarea;
    public Transform mLookPt;
    public float fMoveSpeed;
    public float mMinLookDistance;
    public float mLookupLimit;
    public float mLookDownLimit;
    public float mCurrentLookDistance;
    private Vector3 mHorizontalVector;
    private float mTotalRoateVertical;
    // Start is called before the first frame update
    void Start()
    {
        mTotalRoateVertical = 0.0f;
        mHorizontalVector = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {

        float fH = Input.GetAxis("Horizontal");
        float fV = Input.GetAxis("Vertical");

        if (Mathf.Abs(fV) > 0)
        {
            transform.forward = Vector3.RotateTowards(transform.forward, mHorizontalVector,0.01f,0.01f);
        }
        // Debug.Log(fH);
        float fMoveAmount = fMoveSpeed * Time.deltaTime;
        Vector3 vMoveH = transform.right * fH;
        Vector3 vMoveV = transform.forward * fV;
        transform.position = transform.position + (vMoveH + vMoveV) * fMoveAmount;

        float fMX = Input.GetAxis("Mouse X");
        float fMY = -Input.GetAxis("Mouse Y");
        mHorizontalVector = Quaternion.AngleAxis(fMX,Vector3.up)* mHorizontalVector;
        Vector3 vTempR= Vector3.Cross(Vector3.up, mHorizontalVector);
        mTotalRoateVertical += fMY;
        if (mTotalRoateVertical > mLookDownLimit)
        {
            mTotalRoateVertical = mLookDownLimit;
        }
        else if (mTotalRoateVertical < -mLookupLimit) 
        {
            mTotalRoateVertical = -mLookupLimit;
        }
        Vector3 vRoatateVector= Quaternion.AngleAxis(mTotalRoateVertical, vTempR) * mHorizontalVector;
        vRoatateVector.Normalize();
        Vector3 vCameraMoveTarget = mLookPt.position - vRoatateVector * mCurrentLookDistance;
        mTPSCamarea.transform.position = vCameraMoveTarget;
        mTPSCamarea.transform.forward = mLookPt.position - mTPSCamarea.transform.position;
    }
}
