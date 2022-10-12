using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    // Start is called before the first frame update

    private float time_interval_p = 10.0f;

    [SerializeField]
    private Material _m_greenplus;
    [SerializeField]
    private Material _m_redmark;

    [SerializeField]
    private GameObject pickup1;
    [SerializeField]
    private GameObject pickup2;

    private GameObject temp1;
    private GameObject temp2;

    private IEnumerator coroutine; // To use while gaming

    public TMP_Text countText;
    public TMP_Text winText;



    bool isGaming;
    private int score;

    void Start()
    {
        //Instantiate(pickup1, Random.insideUnitSphere * 5, Quaternion.identity);
        SpawnOne();
        SpawnTwo();
        isGaming = false;
        score = 0;
        SetCountText();
        winText.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        if(GameOver())
        {
            Debug.Log("Game Over!!!");
        }
        if (isGaming)
        {
            temp2.GetComponent<Collider>().isTrigger = true;
            temp2.GetComponent<MeshRenderer>().material = _m_greenplus;
        }
        else
        {
            temp2.GetComponent<Collider>().isTrigger = false;
            temp2.GetComponent<MeshRenderer>().material = _m_redmark;
        }
    }

    public bool GamingHard() // Check if player is allowed to pick up 
    {
        return isGaming;
    }

    public void HandleBasicCollision() // Player contacted basic pickup, start timer
    {
        IncreaseScore();
        Destroy(temp1);
        if (!GamingHard())
        {
            coroutine = BuddyYouAreGaming();
            StartCoroutine(BuddyYouAreGaming());
        }
        SpawnOne();
    }
    public void HandleComplexCollision()
    {
        if(GamingHard())
        {
            IncreaseScore();
            Destroy(temp2);
            SpawnTwo();
        }
        else
        {
            Debug.Log("Game Harder");
        }
        
    }
    private void IncreaseScore()
    {
        ++score;
        Debug.Log("Score: " + score );
        SetCountText();
    }
    private void SpawnOne()
    {
        Debug.Log("Spawn One");
        temp1 = Instantiate(pickup1, new Vector3(Random.Range(-10.0f, 10.0f), 1.5f, Random.Range(-10.0f, 10.0f)), Quaternion.identity);
    }
    private void SpawnTwo()
    {
        Debug.Log("Spawn Two");
        temp2 = Instantiate(pickup2, new Vector3(Random.Range(-10.0f, 10.0f), 1.5f, Random.Range(-10.0f, 10.0f)), Quaternion.identity);
    }

    private IEnumerator BuddyYouAreGaming()
    {
        Debug.Log("You have started gaming");
        isGaming = true;

        yield return new WaitForSeconds(time_interval_p);

        isGaming = false;
        time_interval_p -= 1.0f;
        Debug.Log("You have stopped gaming");
    }

    private bool GameOver()
    {
        return time_interval_p <= 1;
    }

    private void SetCountText()
    {
        countText.text = "Score: " + score.ToString();
        if(score >= 10)
        {
            winText.text = "YOU WIN!!!";
        }
    }
}
