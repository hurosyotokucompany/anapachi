using UnityEngine;

public class FallAnayama : MonoBehaviour
{

    private void Start()
    {
        
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // RigidbodyのUse Gravityを有効にします。
            rb.useGravity = true;
            Destroy(gameObject, 5.0f);
            
        }
    }
    // private void OnCollisionEnter(Collision collision)
    // {
    //     if(collision.gameObject.CompareTag("Wall")){
    //         Destroy(gameObject);
    //     }
    //     Destroy(gameObject);
    //     Debug.Log("Effect Start!");
    // }
}
