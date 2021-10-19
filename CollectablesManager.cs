using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesManager : MonoBehaviour
{
    public int numberOfAllCoins = 0;
    public int numberOfCollectedCoins = 0;
    public Text[] scores;
    public EnvironmentManager environmentManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        numberOfAllCoins = environmentManagerScript.coins.Count;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Text score in scores)
        {
            score.text = "SCORE: " + numberOfCollectedCoins + @" / " + numberOfAllCoins;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            CollectCoin(other.gameObject);
        }
    }

    public void CollectCoin(GameObject coin)
    {
        numberOfCollectedCoins++;
        Destroy(coin.transform.parent.gameObject);
    }
}
