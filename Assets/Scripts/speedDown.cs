using UnityEngine;

public class speedDown : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ball"))
        {
            GetComponent<AudioSource>().Play();

        }
    }
}
