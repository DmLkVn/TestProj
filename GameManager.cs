using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
    int coinsScore;


    private void Awake()
    {
        Coin.scoreUp.AddListener(AddCoin);

    }



    public void AddCoin()
    {
        coinsScore++;
        score.text = coinsScore.ToString();
    }
}
