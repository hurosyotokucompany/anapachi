using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeRecordController : MonoBehaviour
{
    private MusicManager musicManager;

    // レコードの回転速度
    private float rotationSpeed = -50f;

    void Start()
    {
        // MusicManagerのインスタンスを取得
        musicManager = MusicManager.instance;

        if (musicManager == null)
        {
            // MusicManagerが存在しない場合、このオブジェクトを非表示にする
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (musicManager != null && musicManager.isPlaying)
        {
            // isPlayingがtrueの場合、オブジェクトを表示して回転させる
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);

            // レコードを回転させる
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // isPlayingがfalseの場合、オブジェクトを非表示にする
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }

    // オブジェクトがクリックされたときにMusicシーンに遷移する
    public void OnClick()
    {
        SceneManager.LoadScene("Music");
    }
}
