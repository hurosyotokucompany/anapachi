using System.Collections;
using UnityEngine;

public class Fall_thunder : MonoBehaviour
{
    public float tiltAngle = 50.0f;
    public float fadeOutDuration = 0.5f;
    public float delayBetweenFalls = 0.2f;
    public GameObject fallAnayamaPrefab; // FallAnayamaのプレハブをInspectorから設定
    public Vector3 startPosition; // 最初のFallAnayamaの出現位置
    public float positionStep = 0.5f; // 次のFallAnayamaの出現位置のずれ

    private SpriteRenderer spriteRenderer;
    private Rigidbody rb;
    private Vector3 nextPosition;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        nextPosition = startPosition;
        StartCoroutine(FallSequence());
    }

    private IEnumerator FallSequence()
    {
        GameObject currentInstance = gameObject;

        // 1つ目のFallAnayamaを左向きに傾けて出現
        currentInstance.transform.rotation = Quaternion.Euler(0, 0, -tiltAngle);
        nextPosition.y -= positionStep; // 位置を下にずらす
        yield return new WaitForSeconds(delayBetweenFalls);

        // 2つ目のFallAnayamaを右向きに傾けて出現
        GameObject secondInstance = Instantiate(fallAnayamaPrefab, nextPosition, Quaternion.identity);
        secondInstance.transform.rotation = Quaternion.Euler(0, 0, tiltAngle);
        nextPosition.y -= positionStep; // 位置を下にずらす
        yield return new WaitForSeconds(delayBetweenFalls);

        // 3つ目のFallAnayamaを左向きに傾けて出現、1つ目をフェードアウト
        GameObject thirdInstance = Instantiate(fallAnayamaPrefab, nextPosition, Quaternion.identity);
        thirdInstance.transform.rotation = Quaternion.Euler(0, 0, -tiltAngle);
        StartCoroutine(FadeOut(currentInstance)); // 1つ目のインスタンスをフェードアウト
        currentInstance = thirdInstance; // 現在のインスタンスを更新
        nextPosition.y -= positionStep; // 位置を下にずらす
        yield return new WaitForSeconds(delayBetweenFalls);

        // 4つ目のFallAnayamaを右向きに傾けて出現、2つ目をフェードアウト
        GameObject fourthInstance = Instantiate(fallAnayamaPrefab, nextPosition, Quaternion.identity);
        fourthInstance.transform.rotation = Quaternion.Euler(0, 0, tiltAngle);
        StartCoroutine(FadeOut(secondInstance)); // 2つ目のインスタンスをフェードアウト
        currentInstance = fourthInstance; // 現在のインスタンスを更新
        nextPosition.y -= positionStep; // 位置を下にずらす
        yield return new WaitForSeconds(delayBetweenFalls);

        // 5つ目のFallAnayamaを左向きに傾けて出現、3つ目と4つ目をフェードアウト
        GameObject fifthInstance = Instantiate(fallAnayamaPrefab, nextPosition, Quaternion.identity);
        fifthInstance.transform.rotation = Quaternion.Euler(0, 0, -tiltAngle);
        StartCoroutine(FadeOut(thirdInstance)); // 3つ目のインスタンスをフェードアウト
        StartCoroutine(FadeOut(fourthInstance)); // 4つ目のインスタンスをフェードアウト

        // 5つ目のFallAnayamaが自由落下
        Rigidbody rbFifth = fifthInstance.GetComponent<Rigidbody>();
        rbFifth.useGravity = true;
        Destroy(fifthInstance, 5.0f + fadeOutDuration); // フェードアウト後に破棄
    }

    private IEnumerator FadeOut(GameObject obj)
    {
        float elapsedTime = 0;
        SpriteRenderer objSpriteRenderer = obj.GetComponent<SpriteRenderer>();
        Color color = objSpriteRenderer.color;
        while (elapsedTime < fadeOutDuration)
        {
            color.a = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeOutDuration);
            objSpriteRenderer.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(obj); // 完全に透明になったらオブジェクトを破棄
    }
}
