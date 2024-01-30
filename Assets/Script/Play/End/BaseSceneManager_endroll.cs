using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class BaseSceneManager_endroll : MonoBehaviour
{
    [SerializeField] private GameObject BackGroundImage;
    [SerializeField] private GameObject BGM;    
    [SerializeField] private GameObject Blocks;
    [SerializeField] private GameObject Walls;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Balls;
    [SerializeField] private GameObject HomeButton;
    
    [SerializeField] private GameObject Anasans;
    [SerializeField] private GameObject smile;
    [SerializeField] private GameObject fashion;
    [SerializeField] private GameObject thunder;
    [SerializeField] private GameObject shun;
    [SerializeField] private GameObject bleach;
    [SerializeField] private GameObject spin;
    [SerializeField] private GameObject fuji;
    [SerializeField] private GameObject peace;
    [SerializeField] private GameObject space;
    [SerializeField] private GameObject best;
    [SerializeField] private GameObject ufo;
    [SerializeField] private GameObject coming;
    [SerializeField] private GameObject spiner;
    [SerializeField] private GameObject obake;
    [SerializeField] private GameObject message;
    [SerializeField] private GameObject clearSound;



    private void Start()
    {
        Blocks.SetActive(false);
        BackGroundImage.SetActive(false);
        Walls.SetActive(false);
        Player.SetActive(false);
        Balls.SetActive(false);
        HomeButton.SetActive(false);
        BGM.SetActive(false);

        smile.SetActive(false);
        fashion.SetActive(false);
        thunder.SetActive(false);
        shun.SetActive(false);
        bleach.SetActive(false);
        spin.SetActive(false);
        fuji.SetActive(false);
        peace.SetActive(false);
        space.SetActive(false);
        best.SetActive(false);
        ufo.SetActive(false);
        coming.SetActive(false);
        spiner.SetActive(false);
        obake.SetActive(false);
        message.SetActive(false);
        clearSound.SetActive(false);
        StartCoroutine(StartSequence());
    }

    private void Update()
    {
        // if (Blocks.transform.childCount == 0) // Assuming Blocks are children of the Blocks GameObject
        // {
        //     // StartCoroutine(EndSequence());
        // }

    }

    private IEnumerator StartSequence()
    {
        
        StartCoroutine(FadeIn(smile,1f));
        yield return new WaitForSeconds(2f);

        BGM.SetActive(true);
        StartCoroutine(FadeIn(fashion,1f));
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeIn(spin,1f));
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeIn(thunder,1f));
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeIn(peace,1f));
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeIn(shun,1f));
        StartCoroutine(FadeIn(space,1f));
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeIn(best,1f));
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeIn(ufo,1f));
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeIn(bleach,1f));
        StartCoroutine(FadeIn(coming,1f));
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeIn(spiner,1f));
        StartCoroutine(FadeIn(obake,1f));
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeIn(fuji,1f));
        yield return new WaitForSeconds(4f);

        Anasans.SetActive(false);

        Blocks.SetActive(true);
        BackGroundImage.SetActive(true);
        Walls.SetActive(true);
        Player.SetActive(true);
        Balls.SetActive(true);
        

        yield return new WaitForSeconds(5f);
        StartCoroutine(EndSequence());

    }

    private IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(4f);
        Player.SetActive(false);
        Balls.SetActive(false);
        yield return new WaitForSeconds(3f);
        
        StartCoroutine(FadeIn(message,3f));
        HomeButton.SetActive(true);

        StartCoroutine(FadeIn(message,3f));
        clearSound.SetActive(true);

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

//     private IEnumerator FadeOut(GameObject target, float duration)
//     {
//         CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();
//         if (canvasGroup != null)
//         {
//             // アルファ値を1（完全に不透明）から0（完全に透明）まで変化させる
//             for (float t = 0; t < duration; t += Time.deltaTime)
//             {
//                 canvasGroup.alpha = 1 - (t / duration);
//                 yield return null;
//             }
//             canvasGroup.alpha = 0; // 確実にアルファ値を0に設定する
//             target.SetActive(false); // 完全に透明になった後、オブジェクトを非アクティブにする
//         }
//         else
//         {
//             Debug.LogError("CanvasGroup component is not found on " + target.name);
//         }
//     }



}