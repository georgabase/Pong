using UnityEngine;

public class leftPlayer : MonoBehaviour
{

    #region PUBLIC

    public float speed = 30;
    public string axis;


    #endregion

    void FixedUpdate()
    {
        float v = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;
    }

    void Start()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        if (col.gameObject.CompareTag("ball"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            GameObject.FindWithTag("ball").GetComponent<SpriteRenderer>().color = newColor;
            GetComponent<AudioSource>().Play();

        }
    }
}
