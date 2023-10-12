//using System.Threading.Tasks.Dataflow;
using System;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    public float swipeThreshold = 50.0f; // フリックを検知する閾値
    public float slideDuration = 0.5f;   // スライドにかける時間

    public float fixedSlideAmount = 1.8f; // 一定量の移動

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float swipeDistance;

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float slideTimer;

    private bool isMoving = false;

    private void Update()
    {   
        //ここからはobjectの大きさ変換の話,ステージ１用
        // オブジェクトの位置を取得
        Vector3 objectPosition = transform.position;
        // 位置に基づいてサイズを計算
        float newSize = 1-(0.2f*Math.Abs(objectPosition.x));
        // オブジェクトのスケールを変更
        if (newSize>0){
        transform.localScale = new Vector3(1.5f*newSize,3.0f*newSize, 1);
        }else{
            transform.localScale=new Vector3(0,0,0);
        }
        //ここからはフリック動作の話
        if (isMoving)
        {
            SlideContent();
            return;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    touchEndPos = touch.position;
                    swipeDistance = Vector2.Distance(touchStartPos, touchEndPos);

                    if (swipeDistance > swipeThreshold)
                    {
                        if (touchStartPos.x < touchEndPos.x)
                        {
                            // Right swipe (move to the previous stage)
                            // Move content to the right
                            MoveContent(-1);
                        }
                        else
                        {
                            // Left swipe (move to the next stage)
                            // Move content to the left
                            MoveContent(1);
                        }
                    }
                    break;
            }
        }
    }


    private void MoveContent(int direction)
    {
        float xOffset = direction * fixedSlideAmount; 
        targetPosition = transform.position + new Vector3(xOffset, 0f, 0f);
        startPosition = transform.position;
        slideTimer = 0f;
        isMoving = true;
    }

    private void SlideContent()
    {
        slideTimer += Time.deltaTime;

        float t = Mathf.Clamp01(slideTimer / slideDuration);
        transform.position = Vector3.Lerp(startPosition, targetPosition, t);

        if (t >= 1.0f)
        {
            isMoving = false;
        }
    }
}
