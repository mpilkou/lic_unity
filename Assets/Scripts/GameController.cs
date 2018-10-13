using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Klasssa sterująca grę
public class GameController : MonoBehaviour
{
    public GameObject panel;
    public GameObject[] zombies;
    public GameObject[] bonus;
    
    public Text ScoreText;
    public GameObject WaveTextGO;
    // aaa
    private Text WaveTextT;

    private int zombieCounter = 10;


    public float waitSpawn = 0.5f;
    public float waitWave = 1f;


    private int[] zombieTypeCounter;
    
    private int zombieLen;

    private int waveCount = 0;
    private int zLevel = 0;

    private int MyScore { get; set; }

    private bool elive = true;
    
    // Inicja  
    void Start()
    {
        MyScore = 0;
        UpdateScoreText();
        WaveTextT = WaveTextGO.GetComponent<Text>();

        zombieLen = zombies.Length;
        
        zombieTypeCounter = new int[zombieLen];
        
        zombieTypeCounter[0] = zombieCounter;
        for (int i = 1; i < zombieTypeCounter.Length; i++)
        {
            zombieTypeCounter[i] = zombieCounter / (5*i);
        }
        
        StartCoroutine(Waves());
    }

    // Sterowanie "Falami"
    private IEnumerator Waves()
    {
        zombieCounter = 0;
        while (elive)
        {
            if (waveCount % 5 == 0)
            {
                zLevel++;
                for (int i = 0; i < zombieLen; i++)
                {
                    zombieTypeCounter[i] >>= i;
                    zombieCounter += zombieTypeCounter[i];
                }
            } else {
                for (int i = 0; i < zombieLen; i++)
                {
                    zombieTypeCounter[i] = (int)Mathf.Ceil
                        (zombieTypeCounter[i] * 
                        (1f + (0.1f / (i + 1))));
                    zombieCounter += zombieTypeCounter[i];
                }
            }

            StartCoroutine(UpdateWaveText());

            for (int i = 0; i < zombieLen && elive; i++){
                StartCoroutine(SpawnMonstersByType(i));
            }
            if (!elive)
                break;

            while(MyScore != zombieCounter && elive)
            {
                yield return new WaitForSeconds(waitWave);
            }
        }
        panel.SetActive(true);
    }
    
    public void Bonus_1(Vector3 position)
    {
        Instantiate(bonus[0], position, Quaternion.identity);
    }
    public void Bonus_2(Vector3 position)
    {
        Instantiate(bonus[1], position, Quaternion.identity);
    }

    // Zwiększenie rekordu
    public void Add()
    {
        MyScore ++;
        UpdateScoreText();
    }

    // Otrzymanie rekordu
    public int GetScore()
    {
        return MyScore;
    }

    // Orświeżenie rekordu
    public void UpdateScoreText()
    {
        ScoreText.text = "Score: " + MyScore;
    }

    public IEnumerator UpdateWaveText()
    {
        WaveTextT.text = "Wave: " + waveCount;
        WaveTextGO.SetActive(true);
        
        yield return new WaitForSeconds(waitWave);
        WaveTextGO.SetActive(false);
    }

    // Śmierć bochatera
    public void Die()
    {
        elive = false;
    }

    
    private IEnumerator SpawnMonstersByType(int monsterType)
    {
        for (int i = 0; i < zombieTypeCounter[monsterType]; i++)
        {       
            int r = Random.Range(-5, 1);
            Vector3 spawnPosition = new Vector3(
            (Random.Range(0, 2) == 0 ? -10f : 10f),
                r,
                    r);
            GameObject z = Instantiate(zombies[monsterType], spawnPosition, Quaternion.identity);
            z.SendMessage("Lives", zLevel);

            yield return new WaitForSeconds(waitSpawn);
        }

        waveCount++;
    }

}
