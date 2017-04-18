using UnityEngine;

public class goalScript : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ball"))
        {
            GetComponent<AudioSource>().Play();

        }
    }
}
