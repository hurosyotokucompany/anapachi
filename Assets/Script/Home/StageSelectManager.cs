using System.Diagnostics;
//using System.Threading.Tasks.Dataflow;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

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

    private int StageNumber=1;

    private int StageMax=8;

    AudioSource audioSource;

    private string gameId = "5511506"; // Andoroido 5511507
    private string placementId = "rewardedVideo"; // リワード広告のプレースメントIDを設定
    void Start()
    {
         audioSource = GetComponent<AudioSource>();
        // Advertisement.Initialize(gameId, true); // テストモードを有効にする場合は true を指定

        // // リワード広告の読み込み
        // Advertisement.Load(placementId);
    }


    private void Update()
    {   

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
                            
                            if (StageNumber==1){
                                StageNumber=1;
                            }else{
                                StageNumber=StageNumber-1;
                                MoveContent(1);
                            }
                            UnityEngine.Debug.Log(StageNumber);
                        }
                        else
                        {
                            // Left swipe (move to the next stage)
                            // Move content to the left
                            
                            if (StageNumber==StageMax){
                                StageNumber=StageMax;
                            }else{
                                StageNumber=StageNumber+1;
                                MoveContent(-1);
                            }
                            UnityEngine.Debug.Log(StageNumber);
                        }
                    }
                    break;
            }
        }
    }


    private void MoveContent(int direction)
    {
        // オブジェクトの名前を指定して取得
        GameObject targetObject = GameObject.Find("Canvas");
        // オブジェクトのTransformコンポーネントを取得
        Transform objTransform = targetObject.transform;
        // スケールの取得
        Vector3 scale = objTransform.localScale;
        // x方向のスケールのみを取得
        float scaleX = scale.x;
        // 取得したx方向のスケールの利用
        UnityEngine.Debug.Log("x方向のスケール: " + scaleX);

        float SamneScale=3.9f;
        int SamneBet=120;
        float xOffset=SamneBet*SamneScale*direction*scaleX;
        // float xOffset = direction * fixedSlideAmount; 
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
    public void OnClickStartButton()
    {
        audioSource.PlayOneShot(audioSource.clip);
        //RewardedAdsButton.LoadAd();
        if (StageNumber==1){
            SceneManager.LoadScene("SpinStage2");
            }
        else if (StageNumber==2){
            SceneManager.LoadScene("SpaceStage");
        }
    
    }


//以下広告の話
    // public void ShowRewardedAd()
    // {
    //     // リワード広告の表示
    //     if (Advertisement.IsReady(placementId))
    //     {
    //         ShowOptions options = new ShowOptions();
    //         options.resultCallback = HandleShowResult;

    //         Advertisement.Show(placementId, options);
    //     }
    //     else
    //     {
    //         UnityEngine.Debug.LogWarning("リワード広告が読み込まれていないか、表示できません。");
    //     }
    // }

    // private void HandleShowResult(ShowResult result)
    // {
    //     switch (result)
    //     {
    //         case ShowResult.Finished:
    //             // リワードを付与する処理をここに追加
    //             UnityEngine.Debug.Log("リワードを受け取りました。");
    //             break;
    //         case ShowResult.Skipped:
    //             UnityEngine.Debug.Log("広告がスキップされました。");
    //             break;
    //         case ShowResult.Failed:
    //             UnityEngine.Debug.LogError("広告の表示に失敗しました。");
    //             break;
    //     }
    // }
//ここまで広告の話

}
