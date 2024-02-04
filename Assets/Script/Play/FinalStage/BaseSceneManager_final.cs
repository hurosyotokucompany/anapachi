using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class BaseSceneManager_final : MonoBehaviour
{
    [SerializeField] private GameObject StageImage;
    [SerializeField] private GameObject StageText;
    [SerializeField] private GameObject SilverBackGroundImage;
    [SerializeField] private GameObject GoldBackGroundImage;

    [SerializeField] private GameObject StartSound;
    [SerializeField] private AudioSource BGM;    
    [SerializeField] private GameObject OverSound;
    [SerializeField] private GameObject HomeButton;
    [SerializeField] private GameObject RetryButton;
    
    [SerializeField] private GameObject SilverBlocks;
    [SerializeField] private GameObject GoldBlocks;
    [SerializeField] private GameObject Walls;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject GoldBall;
    [SerializeField] private GameObject GameOver;
    [SerializeField] TextMeshProUGUI TimerText; // 経過時間表示用

    private float timer = 0f;

    private void Start()
    {   
        TimerText.gameObject.SetActive(false);   
        SilverBlocks.SetActive(false);
        GoldBlocks.SetActive(false);
        GoldBackGroundImage.SetActive(false);
        SilverBackGroundImage.SetActive(false);

        Walls.SetActive(false);
        Player.SetActive(false);
        Ball.SetActive(false);
        GoldBall.SetActive(false);

        GameOver.SetActive(false);
        RetryButton.SetActive(false);
        HomeButton.SetActive(false);
        OverSound.SetActive(false);
        StartSound.SetActive(false);
        
        StageText.SetActive(false);
        StageImage.SetActive(false);
        
        StartCoroutine(FadeIn(StageImage, 1f));
        StartCoroutine(StartSequence());
    }

    private bool SliverCleared = false;
    private bool GoldCleared = false;
    private void Update()
    {
        // Update the timer if the game is in play
        if (Ball.activeInHierarchy)
        {
            timer += Time.deltaTime;
            TimerText.text = "Time: " +  timer.ToString("F2");
        }

        // Check for game over conditions
        if (Ball.transform.position.y < -15 | GoldBall.transform.position.y < -15 )
        {  
            StartCoroutine(GameOverSequence());
        }    
        
        if (SilverBlocks.transform.childCount == 0 && !SliverCleared) // Assuming Blocks are children of the Blocks GameObject
        {
            SliverCleared = true;
            SilverBlocks.SetActive(false);
            StartCoroutine(GoldStartSequence());
        }

        if (GoldBlocks.transform.childCount == 0 && !GoldCleared) // Assuming Blocks are children of the Blocks GameObject
        {
            GoldCleared = true;
            GoldBlocks.SetActive(false);
            StartCoroutine(GameClearSequence());
        }

    }


    private IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(1.5f);
        StartSound.SetActive(true);
        StartCoroutine(FadeIn(StageText, 1.5f));
        
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(FadeOut(StageText, 0.5f));
        StartCoroutine(FadeOut(StageImage, 0.5f));

        // Start BGM and expand the background image
        BGM.Play();
        SilverBackGroundImage.SetActive(true);
        StartCoroutine(EnlargeBackgroundImage(SilverBackGroundImage));

        // Wait for background image expansion to complete
        yield return new WaitForSeconds(1.5f); // Assuming it takes 2 seconds to expand

        SilverBlocks.SetActive(true);
        Walls.SetActive(true);
        Player.SetActive(true);
        
        // Wait for 0.5 seconds then activate the ball
        yield return new WaitForSeconds(2f);
        Ball.SetActive(true);
        TimerText.gameObject.SetActive(true);
    }

    private IEnumerator GoldStartSequence()
    {
        SilverBackGroundImage.SetActive(false);     
        StartCoroutine(EnlargeBackgroundImage(GoldBackGroundImage));

        // Wait for background image expansion to complete
        yield return new WaitForSeconds(1); // Assuming it takes 2 seconds to expand

        GoldBlocks.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);
        GoldBall.SetActive(true);

    }


    private IEnumerator GameOverSequence()
    {
        GameOver.SetActive(true);

        // int rnd = Random.Range(1, 101);
        // if(rnd<=33){
        //     over1.SetActive(true);
        // }else if(rnd<=66){
        //     over2.SetActive(true);
        // }else if(rnd<=99){
        //     over3.SetActive(true);
        // }else{
        //     over4.SetActive(true);
        // }

        HomeButton.SetActive(true);
        RetryButton.SetActive(true);
        BGM.Stop();
        Ball.SetActive(false);
        GoldBall.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        OverSound.SetActive(true);
    }

    private IEnumerator GameClearSequence()
    {
        // ベストタイム記録
        string key = "BestTime_" + SceneManager.GetActiveScene().name;
        Debug.Log(key);
        if (!PlayerPrefs.HasKey(key) || PlayerPrefs.GetFloat(key) > timer)
        {
            PlayerPrefs.SetFloat(key, timer);
        }
        PlayerPrefs.Save();

        yield return new WaitForSeconds(1);
        Walls.SetActive(false);
        Player.SetActive(false);
        Ball.SetActive(false);
        BGM.Stop();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Endroll");

    }

    private IEnumerator EnlargeBackgroundImage(GameObject BackGroundImage)
    {
        BackGroundImage.SetActive(true);
        float duration = 1f;
        float currentTime = 0f;
        Vector3 startSize = BackGroundImage.transform.localScale; // initial scale before enlargement
        Vector3 endSize = startSize * 8/7; // target scale for full screen

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            BackGroundImage.transform.localScale = Vector3.Lerp(startSize, endSize, t);
            yield return null;
        }
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