using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MusicManager : MonoBehaviour
{
    // 音楽トラックのリスト
    public static MusicManager instance;
    private List<AudioSource> audioSources;
    // 現在再生中のトラックのインデックス
    private int currentTrackIndex = 0;
    // シャッフルされたトラック順序
    private List<int> trackOrder;
    // 再生状態を示すフラグ
    private bool isPlaying = false;

    // 再生・停止ボタンのアイコンを切り替えるためのGameObject
    [SerializeField] private GameObject playIcon;  // 再生ボタンのアイコン
    [SerializeField] private GameObject pauseIcon; // 停止ボタンのアイコン

     // レコードの回転用
    [SerializeField] private GameObject record;
    // レコードの回転速度
    private float rotationSpeed = 50f;

    // 再生中の曲名を表示するTextMeshProUGUI
    [SerializeField] private TextMeshProUGUI musicNameText;

     // ボタンの参照を追加
    [SerializeField] private Button playPauseButton;
    [SerializeField] private Button nextTrackButton;
    [SerializeField] private Button previousTrackButton;
    [SerializeField] private Button returnHomeButton;
    // [SerializeField] private Button gameButton;

    void Awake()
    {
        // シングルトンの実装
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // このゲームオブジェクトをシーン遷移後も保持
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は破棄
            return;
        }
    }

    void Start()
    {   
        // このオブジェクトの子オブジェクトにあるすべてのAudioSourceを取得
        audioSources = new List<AudioSource>(GetComponentsInChildren<AudioSource>());

        // トラックをシャッフル
        ShuffleTracks();

        // 最初のトラックから開始
        currentTrackIndex = 0;

        // ボタンの初期状態を設定
        UpdatePlayPauseIcon();

        // 曲名を初期化
        UpdateMusicName();

        // ボタンのイベントを設定
        if (playPauseButton != null)
            playPauseButton.onClick.RemoveAllListeners();
            playPauseButton.onClick.AddListener(PlayPause);

        if (nextTrackButton != null)
            nextTrackButton.onClick.AddListener(NextTrack);

        if (previousTrackButton != null)
            previousTrackButton.onClick.AddListener(PreviousTrack);

        if (returnHomeButton != null)
            returnHomeButton.onClick.AddListener(ReturnToHome);

        // if (gameButton != null)
        //     gameButton.onClick.AddListener(GoToGameScene);
    }

    void OnDestroy()
    {
        
        if (playPauseButton != null)
            playPauseButton.onClick.RemoveListener(PlayPause);

        if (nextTrackButton != null)
            nextTrackButton.onClick.RemoveListener(NextTrack);

        if (previousTrackButton != null)
            previousTrackButton.onClick.RemoveListener(PreviousTrack);

        if (returnHomeButton != null)
            returnHomeButton.onClick.RemoveListener(ReturnToHome);
        
        // if (gameButton != null)
        //     gameButton.onClick.RemoveListener(GoToGameScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Music")
        {
            // 音楽プレイヤーシーンに戻ってきたらUIを再初期化
            InitializeUI();
        }
        else if (scene.name != "Home" && scene.name != "insta")
        {
            // "Home"と"Game"以外のシーンに遷移したらAudioPlayerを破棄
            DestroyAudioPlayer();
        }
    }

     void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void InitializeUI()
    {
        // ボタンやUI要素を再取得（シーンが変わると参照が失われるため）
        playPauseButton = GameObject.Find("PlayPauseButton")?.GetComponent<Button>();
        nextTrackButton = GameObject.Find("NextButton")?.GetComponent<Button>();
        previousTrackButton = GameObject.Find("BackButton")?.GetComponent<Button>();
        returnHomeButton = GameObject.Find("HomeButton")?.GetComponent<Button>();
        // gameButton = GameObject.Find("GameButton")?.GetComponent<Button>();

        playIcon = GameObject.Find("PlayIcon");
        pauseIcon = GameObject.Find("PauseIcon");
        record = GameObject.Find("Record");
        musicNameText = GameObject.Find("MusicNameText")?.GetComponent<TextMeshProUGUI>();


        if (playPauseButton != null)
            playPauseButton.onClick.AddListener(PlayPause);

        if (nextTrackButton != null)
            nextTrackButton.onClick.AddListener(NextTrack);

        if (previousTrackButton != null)
            previousTrackButton.onClick.AddListener(PreviousTrack);

        if (returnHomeButton != null)
            returnHomeButton.onClick.AddListener(ReturnToHome);
        
        // if (gameButton != null)
        //     gameButton.onClick.RemoveListener(GoToGameScene);

        // ボタンのアイコンを更新
        UpdatePlayPauseIcon();
    }

    void DestroyAudioPlayer()
    {
        // 音楽を停止し、AudioPlayerを破棄
        StopAllCoroutines();
        if (audioSources != null)
        {
            foreach (var audio in audioSources)
            {
                audio.Stop();
            }
        }
        Destroy(gameObject);
    }


    // トラックをシャッフルする関数
    void ShuffleTracks()
    {
        trackOrder = new List<int>();
        for (int i = 0; i < audioSources.Count; i++)
        {
            trackOrder.Add(i);
        }

        // ランダムに順序を入れ替える
        for (int i = 0; i < trackOrder.Count; i++)
        {
            int temp = trackOrder[i];
            int randomIndex = Random.Range(i, trackOrder.Count);
            trackOrder[i] = trackOrder[randomIndex];
            trackOrder[randomIndex] = temp;
        }
    }

    // 再生・一時停止ボタンに割り当てる関数
    public void PlayPause()
    {
        if (isPlaying)
        {
            // 再生中の場合は一時停止
            audioSources[trackOrder[currentTrackIndex]].Pause();
            isPlaying = false;
        }
        else
        {
            // 一時停止中の場合は再生
            audioSources[trackOrder[currentTrackIndex]].Play();
            isPlaying = true;
        }

        // ボタンのアイコンを更新
        UpdatePlayPauseIcon();
    }

    // 次の曲ボタンに割り当てる関数
    public void NextTrack()
    {
        // 現在のトラックを停止
        audioSources[trackOrder[currentTrackIndex]].Stop();

        // インデックスを進める
        currentTrackIndex++;
        if (currentTrackIndex >= trackOrder.Count)
        {
            currentTrackIndex = 0; // 最初のトラックに戻る
        }

        // 次のトラックを再生
        audioSources[trackOrder[currentTrackIndex]].Play();
        isPlaying = true;

        // ボタンのアイコンを更新
        UpdatePlayPauseIcon();

        // 曲名を更新
        UpdateMusicName();
    }

    // 前の曲ボタンに割り当てる関数
    public void PreviousTrack()
    {
        // 現在のトラックの再生時間を取得
        float currentTime = audioSources[trackOrder[currentTrackIndex]].time;

        if (currentTime > 3f)
        {
            // 3秒以上経過している場合は現在の曲を最初から再生
            audioSources[trackOrder[currentTrackIndex]].Stop();
            audioSources[trackOrder[currentTrackIndex]].Play();
        }
        else
        {
            // 現在のトラックを停止
            audioSources[trackOrder[currentTrackIndex]].Stop();

            // インデックスを戻す
            currentTrackIndex--;
            if (currentTrackIndex < 0)
            {
                currentTrackIndex = trackOrder.Count - 1; // 最後のトラックに移動
            }

            // 前のトラックを再生
            audioSources[trackOrder[currentTrackIndex]].Play();
        }

        isPlaying = true;

        // ボタンのアイコンを更新
        UpdatePlayPauseIcon();
        // 曲名を更新
        UpdateMusicName();
    }

    void Update()
    {
        if (isPlaying)
        {
            if (record != null)
            {
                record.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            }
            
            // 現在のトラックが再生終了したかチェック
            if (!audioSources[trackOrder[currentTrackIndex]].isPlaying)
            {
                // 自動的に次のトラックへ
                NextTrack();
            }
        }
    }

    // 再生・停止ボタンのアイコンを更新する関数
    void UpdatePlayPauseIcon()
    {
        if (playIcon != null && pauseIcon != null)
        {
            playIcon.SetActive(!isPlaying);
            pauseIcon.SetActive(isPlaying);
        }
    }

     // 曲名を更新する関数
    void UpdateMusicName()
    {
        if (musicNameText != null)
        {
            // 現在のAudioSourceのゲームオブジェクト名を取得
            string trackName = audioSources[trackOrder[currentTrackIndex]].gameObject.name;
            musicNameText.text = trackName;
        }
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene("Home"); // "Home"はHomeシーンの名前です
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("insta"); // "Game"は遷移先のシーン名
    }
}
