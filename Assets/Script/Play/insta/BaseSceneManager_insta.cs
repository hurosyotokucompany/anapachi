using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BaseSceneManager_insta : MonoBehaviour
{
    [SerializeField] private GameObject StageText;
    [SerializeField] private GameObject BackGroundImage;
    [SerializeField] private AudioSource StartSound;
    [SerializeField] private AudioSource BGM;

    [SerializeField] private GameObject OverSound;
    [SerializeField] private GameObject Walls;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject HomeButton;
    [SerializeField] private GameObject RetryButton;
    [SerializeField] private GameObject GameEnd;
    [SerializeField] TextMeshProUGUI DeflectionCountText; // はじき返した回数表示用
    [SerializeField] TextMeshProUGUI BestRecordText; // ベストレコード表示用

    [SerializeField] private GameObject Over1;
    [SerializeField] private GameObject Over2;
    [SerializeField] private GameObject Over3;
    [SerializeField] private GameObject Over4;

    private int deflectionCount = 0;
    
    

    private void Start()
    {
        StartCoroutine(Enlarge(StageText, 2.5f, 0.5f));
        StartCoroutine(FadeIn(BackGroundImage, 0.5f));
        StartSound.PlayOneShot(StartSound.clip);
        StartCoroutine(StartSequence());

        DeflectionCountText.gameObject.SetActive(false);   
        Walls.SetActive(false);
        Player.SetActive(false);
        Ball.SetActive(false);
        HomeButton.SetActive(false);
        RetryButton.SetActive(false);
        GameEnd.SetActive(false);
        OverSound.SetActive(false);

        string recordKey = "BestCount_" + SceneManager.GetActiveScene().name;
        if (PlayerPrefs.HasKey(recordKey))
        {
            int BestRecord = PlayerPrefs.GetInt(recordKey);
            BestRecordText.text = "Best: " + BestRecord.ToString();  
        }
    }

    private IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(1f);

        // BGMを開始し、背景画像を拡大
        BGM.Play();
        StartCoroutine(EnlargeBackgroundImage());

        // 背景画像の拡大が完了するのを待つ
        yield return new WaitForSeconds(1);

        Walls.SetActive(true);
        Player.SetActive(true);

        // 0.5秒待ってからボールをアクティブにする
        yield return new WaitForSeconds(1.5f);
        Ball.SetActive(true);
        DeflectionCountText.gameObject.SetActive(true);
    }

    private IEnumerator EnlargeBackgroundImage()
    {
        float duration = 0.5f;
        float currentTime = 0f;
        Vector3 startSize = BackGroundImage.transform.localScale;
        Vector3 endSize = startSize * 4 / 3;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            BackGroundImage.transform.localScale = Vector3.Lerp(startSize, endSize, t);
            yield return null;
        }
    }

    private IEnumerator Enlarge(GameObject target, float ratio, float duration)
    {
        // GameObject object, 
        float currentTime = 0f;
        Vector3 startSize = target.transform.localScale;
        Vector3 endSize = startSize * ratio;

        CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            target.transform.localScale = Vector3.Lerp(startSize, endSize, t);

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f - (currentTime / duration);
            }

            yield return null;
        }

        canvasGroup.alpha = 0; // 確実にアルファ値を0に設定する
        target.SetActive(false);

        // FadeIn(target, duration);
    }

     private void Update()
    {
        // ゲームオーバーの条件をチェック
        if (Ball.transform.position.y < -15 && !Overed)
        {
            Overed = true;
            StartCoroutine(GameOverSequence());
        }
    }

    private bool Overed = false;

    // プレイヤーがボールをはじき返したときに呼び出すメソッド
    public void IncrementDeflectionCount()
    {
        deflectionCount++;
        DeflectionCountText.text = deflectionCount.ToString();
    }

    private IEnumerator GameOverSequence()
    {
        GameEnd.SetActive(true);
        HomeButton.SetActive(true);
        RetryButton.SetActive(true);
        BGM.Stop();

        Ball.SetActive(false);
        Walls.SetActive(false);
        BackGroundImage.SetActive(false);
        Player.SetActive(false);
        DeflectionCountText.gameObject.SetActive(false);

        string recordKey = "BestCount_" + SceneManager.GetActiveScene().name;
        if (!PlayerPrefs.HasKey(recordKey) || PlayerPrefs.GetInt(recordKey) < deflectionCount)
        {
            PlayerPrefs.SetInt(recordKey, deflectionCount);
            // BestRecordText.text = "Best Record ! " + deflectionCount.ToString("F2");  
        }

        int rnd = UnityEngine.Random.Range(1, 101);
        if (rnd <= 33)
        {
            StartCoroutine(FadeIn(Over1, 0.5f));
        }
        else if (rnd <= 66)
        {
            StartCoroutine(FadeIn(Over2, 0.5f));
        }
        else if (rnd <= 99)
        {
            StartCoroutine(FadeIn(Over3, 0.5f));
        }
        else
        {
            StartCoroutine(FadeIn(Over4, 0.5f));
        }

        yield return new WaitForSeconds(1f);
        OverSound.SetActive(true);
    }


    private IEnumerator FadeIn(GameObject target, float duration)
    {
        target.SetActive(true);
        CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            
            // アルファ値を0（完全に透明）から1（完全に不透明）まで変化させる
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                canvasGroup.alpha = t / duration;
                yield return null;
            }
            canvasGroup.alpha = 1; // 確実にアルファ値を1に設定する
        }
        else
        {
            Debug.LogError("CanvasGroup component is not found on " + target.name);
        }
    }

    private IEnumerator FadeOut(GameObject target, float duration)
    {
        CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            // アルファ値を1（完全に不透明）から0（完全に透明）まで変化させる
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                canvasGroup.alpha = 1 - (t / duration);
                yield return null;
            }
            canvasGroup.alpha = 0; // 確実にアルファ値を0に設定する
            target.SetActive(false); // 完全に透明になった後、オブジェクトを非アクティブにする
        }
        else
        {
            Debug.LogError("CanvasGroup component is not found on " + target.name);
        }
    }



}