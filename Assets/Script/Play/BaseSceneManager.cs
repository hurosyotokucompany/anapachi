using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class BaseSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject StageImage;
    [SerializeField] private GameObject StageText;
    [SerializeField] private GameObject BackGroundImage;
    [SerializeField] private AudioSource StartSound;
    [SerializeField] private AudioSource BGM;
    // [SerializeField] private AudioSource ClearSound;
    // [SerializeField] private AudioSource OverSound;
    
    [SerializeField] private GameObject ClearSound;
    [SerializeField] private GameObject OverSound;

    [SerializeField] private GameObject Blocks;
    [SerializeField] private GameObject Walls;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject GameClear;
    [SerializeField] private GameObject HomeButton;
    [SerializeField] private GameObject RetryButton;
    [SerializeField] private GameObject GameOver;
    [SerializeField] TextMeshProUGUI TimerText; // 経過時間表示用
    [SerializeField] TextMeshProUGUI BestRecordText; // ベストレコード表示用
    [SerializeField] private GameObject ClearFall;

    
    [SerializeField] private GameObject Over1;
    [SerializeField] private GameObject Over2;
    [SerializeField] private GameObject Over3;
    [SerializeField] private GameObject Over4;

    private float timer = 0f;

    private void Start()
    {
        StageImage.SetActive(true);
        // StartCoroutine(FadeIn(StageImage, 0.2f));
        StartCoroutine(FadeIn(StageText, 0.5f));
        StartCoroutine(FadeIn(BackGroundImage, 0.5f));
        StartSound.PlayOneShot(StartSound.clip);
        
        TimerText.gameObject.SetActive(false);   
        Blocks.SetActive(false);
        Walls.SetActive(false);
        Player.SetActive(false);
        Ball.SetActive(false);
        GameClear.SetActive(false);
        HomeButton.SetActive(false);
        RetryButton.SetActive(false);
        GameOver.SetActive(false);
        ClearFall.SetActive(false);
        ClearSound.SetActive(false);
        OverSound.SetActive(false);


        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeOut(StageImage, 0.5f));
        StartCoroutine(FadeOut(StageText, 0.5f));

        // Start BGM and expand the background image
        BGM.Play();
        StartCoroutine(EnlargeBackgroundImage());

        // Wait for background image expansion to complete
        yield return new WaitForSeconds(1); // Assuming it takes 2 seconds to expand

        Blocks.SetActive(true);
        Walls.SetActive(true);
        Player.SetActive(true);
        
        // Wait for 0.5 seconds then activate the ball
        yield return new WaitForSeconds(1.5f);
        Ball.SetActive(true);
        TimerText.gameObject.SetActive(true);
    }

    // ...

    private IEnumerator EnlargeBackgroundImage()
    {
        float duration = 0.5f;
        float currentTime = 0f;
        Vector3 startSize = BackGroundImage.transform.localScale; // initial scale before enlargement
        Vector3 endSize = startSize * 4/3; // target scale for full screen

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            BackGroundImage.transform.localScale = Vector3.Lerp(startSize, endSize, t);
            yield return null;
        }
    }


    private bool Cleared = false;
    private bool Overed = false;
    private void Update()
    {
        // Update the timer if the game is in play
        if (Ball.activeInHierarchy)
        {
            timer += Time.deltaTime;
            TimerText.text = "Time: " +  timer.ToString("F2");
        }

        // Check for game over conditions
        if (Ball.transform.position.y < -15 && !Overed)
        {
            Overed = true;
            StartCoroutine(GameOverSequence());
        }
        else if (Blocks.transform.childCount == 0 && !Cleared) // Assuming Blocks are children of the Blocks GameObject
        {
            Cleared = true;
            StartCoroutine(GameClearSequence());
        }
    }

    private IEnumerator GameOverSequence()
    {
        GameOver.SetActive(true);
        HomeButton.SetActive(true);
        RetryButton.SetActive(true);
        BGM.Stop();

        Ball.SetActive(false);
        Walls.SetActive(false);
        BackGroundImage.SetActive(false);
        Player.SetActive(false);
        TimerText.gameObject.SetActive(false);

        int rnd = UnityEngine.Random.Range(1, 101);
        if(rnd<=33){
            StartCoroutine(FadeIn(Over1, 1f));
        }else if(rnd<=66){
            StartCoroutine(FadeIn(Over2, 1f));
        }else if(rnd<=99){
            StartCoroutine(FadeIn(Over4, 1f));
        }else{
            StartCoroutine(FadeIn(Over4, 1f));
        }

        yield return new WaitForSeconds(1f);
        OverSound.SetActive(true);
    }
    private IEnumerator GameClearSequence()
    {
        GameClear.SetActive(true);
        HomeButton.SetActive(true);
        RetryButton.SetActive(true);
        // ClearSound.PlayOneShot(ClearSound.clip);
        ClearSound.SetActive(true);

        Ball.SetActive(false);

        // HomeSceneManager.Stagelist[HomeSceneManager.selectedvalue]=SceneManager.GetActiveScene().buildIndex;//  
        // ベストタイム記録
        string key = "BestTime_" + SceneManager.GetActiveScene().name;
        Debug.Log(key);
        if (!PlayerPrefs.HasKey(key) || PlayerPrefs.GetFloat(key) > timer)
        {
            PlayerPrefs.SetFloat(key, timer);
            // BestRecordText.SetActive(true); // ベストレコード表示
            BestRecordText.text = "Best Record ! " + timer.ToString("F2");  
        }
        PlayerPrefs.Save();

        yield return new WaitForSeconds(1);
        ClearFall.SetActive(true);


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