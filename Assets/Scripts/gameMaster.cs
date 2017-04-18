using UnityEngine;

public class gameMaster : MonoBehaviour
{

    #region PUBLIC

    public GameObject[] powerUps;


    #endregion

    #region PRIVATE

    //Time it takes to spawn theGoodies
    private float waitingForNextSpawn = 10f;

    private float theCountdown = 10f;
    private string text;

    // the range of X
    [Header("X Spawn Range")]
    private float xMin = -14;
    private float xMax = 14;
  
    // the range of y
    [Header("Y Spawn Range")]
    private float yMin = -14;
    private float yMax = 14;

    #endregion


    void Start()
    {
		
    }

    void Update()
    {
        // timer to spawn the next goodie Object
        theCountdown -= Time.deltaTime;
        if (theCountdown <= 0)
        {
            SpawnGoodies();
            theCountdown = waitingForNextSpawn;
        }
    }

    void SpawnGoodies()
    {
        // Defines the min and max ranges for x and y
        Vector2 pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
  
        // Choose a new goods to spawn from the array (note I specifically call it a 'prefab' to avoid confusing myself!)
        GameObject goodsPrefab = powerUps[Random.Range(0, powerUps.Length)];
 
        // Creates the random object at the random 2D position.
        Instantiate(goodsPrefab, pos, transform.rotation);
 
    }
	
}
