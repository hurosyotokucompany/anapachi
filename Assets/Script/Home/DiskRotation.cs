using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskRotation : MonoBehaviour
{
    private float minFlickDistance = 1.0f; // 45度回転を起動するための最小フリック距離
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                UnityEngine.Debug.Log("1");
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                float flickDistance = Vector2.Distance(touchStartPos, touchEndPos);
                UnityEngine.Debug.Log("2");

                if (flickDistance >= minFlickDistance)
                {
                    RotateDisc();
                    UnityEngine.Debug.Log("3");
                }
            }
        }
    }

    void RotateDisc()
    {
        // 45度回転する
        transform.Rotate(Vector3.forward, 45.0f);
        UnityEngine.Debug.Log("回った");
    }
}
