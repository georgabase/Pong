using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ball : MonoBehaviour
{
    #region Public

    public float speed = 30;
    public ParticleSystem particle;
    public ParticleSystem hitParticle;
    public GameObject ballPrefab;
    [HideInInspector] 
    public bool lastPlayerRight = false;
    //    [HideInInspector]
    //  public int player1Score = 0;
    // [HideInInspector]
    //  public int player2Score = 0;

    #endregion

    #region Private


    private float y;
    private Vector2 dir;

    #endregion

    private IEnumerator Pause(int p)
    {
        Time.timeScale = 0.1f;
        float pauseEndTime = Time.realtimeSinceStartup + 1;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }

    private IEnumerator PowerUpSpeed()
    {
        speed = 45;
        GetComponent<Rigidbody2D>().velocity = dir * speed;
        yield return new WaitForSeconds(3f);
        print("powerUpSpeed ended");
        speed = 30;
    }

    private IEnumerator noPowerUp()
    {
        speed = 30;
        GetComponent<Rigidbody2D>().velocity = dir * speed;
        yield return null;

    }

    private IEnumerator PowerUpSlow()
    {
        speed = 15;
        GetComponent<Rigidbody2D>().velocity = dir * speed;
        yield return new WaitForSeconds(3f);
        print("powerUpSlow ended");
        speed = 30;
    }

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }

       

        if (leftPlayerScoreScript.player1Score == 3)
        {
            SceneManager.LoadScene("Main");
        }

        if (rightPlayerScoreScript.player2Score == 3)
        {
            SceneManager.LoadScene("Main");
        }
    }

    void Awake()
    {
        Debug.Log("Awake " + GetComponent<Rigidbody2D>().velocity);

    }

    void Start()
    {
        // Initial Velocity
        Time.timeScale = 1;
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        Debug.Log("Start " + GetComponent<Rigidbody2D>().velocity);
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                    float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "slowDown")
        {
            Destroy(col.gameObject);

            GetComponent<AudioSource>().Play();

            Debug.Log("slow down");
            StartCoroutine(PowerUpSlow());
        }
        if (col.tag == "speedUp")
        {
            Destroy(col.gameObject);

            GetComponent<AudioSource>().Play();

            Debug.Log("speed up");
            StartCoroutine(PowerUpSpeed());
        }
        if (col.tag == "goal")
        {
            StartCoroutine(noPowerUp());

            Debug.Log("goal");
            if (lastPlayerRight)
            {
                rightPlayerScoreScript.player2Score++;
            }
            if (!lastPlayerRight)
            {
                leftPlayerScoreScript.player1Score++;
            }
          
            GameObject.Find("leftPlayer").GetComponent<leftPlayer>().transform.position = new Vector2(-18.0f, 0.0f);
            GameObject.Find("rightPlayer").GetComponent<rightPlayer>().transform.position = new Vector2(18.0f, 0.0f);

            Instantiate(ballPrefab, new Vector2(1, 1), Quaternion.identity);
            Destroy(gameObject);


            //StartCoroutine(Pause(3));
            // transform.position = new Vector3(0, 0, 0);
        }
    }

    IEnumerator Shake()
    {

        float elapsed = 0.0f;
        float duration = 0.5f;
        float magnitude = 0.2f;
        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;          

            float percentComplete = elapsed / duration;         
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

        Camera.main.transform.position = originalCamPos;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
       
        hitParticle.Play();
        StartCoroutine(Shake());
        // pick a random color
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);


        // Hit the left Racket?
        if (col.gameObject.name == "leftPlayer")
        {

           

            // apply it on current object's material
            GameObject.Find("leftWall").GetComponent<SpriteRenderer>().color = newColor;
            GameObject.Find("rightWall").GetComponent<SpriteRenderer>().color = newColor;
            GameObject.Find("topWall").GetComponent<SpriteRenderer>().color = newColor;
            GameObject.Find("botWall").GetComponent<SpriteRenderer>().color = newColor;
            GameObject.Find("midLine").GetComponent<SpriteRenderer>().color = newColor;

            lastPlayerRight = false;
            // Calculate hit Factor
            y = hitFactor(transform.position,
                col.transform.position,
                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            dir = new Vector2(1, y).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        // Hit the right Racket?
        if (col.gameObject.name == "rightPlayer")
        {
            GameObject.Find("leftWall").GetComponent<SpriteRenderer>().color = newColor;
            GameObject.Find("rightWall").GetComponent<SpriteRenderer>().color = newColor;
            GameObject.Find("topWall").GetComponent<SpriteRenderer>().color = newColor;
            GameObject.Find("botWall").GetComponent<SpriteRenderer>().color = newColor;
            GameObject.Find("midLine").GetComponent<SpriteRenderer>().color = newColor;

            lastPlayerRight = true;
            // Calculate hit Factor
            y = hitFactor(transform.position,
                col.transform.position,
                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            dir = new Vector2(-1, y).normalized;
        
            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
    }
}
