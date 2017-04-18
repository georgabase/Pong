using UnityEngine;
using UnityEngine.UI;


public class rightPlayerScoreScript : MonoBehaviour
{
    #region Public

    public static int player2Score;


    #endregion

    #region Private

    private Text text;

    #endregion

    void Start()
    {
        text = GetComponent<Text>();
        text.text = player2Score.ToString();
    }

    void Update()
    {
        text.text = player2Score.ToString();

        if (player2Score < leftPlayerScoreScript.player1Score)
        {
            text.GetComponent<Text>().color = Color.red;
        }
        else if (player2Score > leftPlayerScoreScript.player1Score)
        {
            text.GetComponent<Text>().color = Color.green;
        }
        else if (player2Score == leftPlayerScoreScript.player1Score)
        {
            text.GetComponent<Text>().color = Color.white;
        }

    }
}
