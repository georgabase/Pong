using UnityEngine;
using UnityEngine.UI;


public class leftPlayerScoreScript : MonoBehaviour
{
    #region Public

    public static int player1Score;


    #endregion

    #region Private

    private Text text;

    #endregion

    void Start()
    {
        text = GetComponent<Text>();
        text.text = player1Score.ToString();
    }

    void Update()
    {
        text.text = player1Score.ToString();

        if (player1Score > rightPlayerScoreScript.player2Score)
        {
            text.GetComponent<Text>().color = Color.green;
        }
        else if (player1Score < rightPlayerScoreScript.player2Score)
        {
            text.GetComponent<Text>().color = Color.red;
        }
        else if (player1Score == rightPlayerScoreScript.player2Score)
        {
            text.GetComponent<Text>().color = Color.white;
        }

    }
}
