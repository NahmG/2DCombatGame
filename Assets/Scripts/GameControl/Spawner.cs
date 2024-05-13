using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject banditPrefab;
    public GameObject wizardPrefab;
    public FloatSO Round;
    public int banditNumber;
    public int wizardNumber;
    public GameObject EndGameUI;

    [SerializeField] int EnemyRemaining;
    bool roundInProgress;
    readonly EnemyWave[] waves =
    {
        new(1,0),
        new(2,0),
        new(1,1)
    };

    private void Start()
    {
        StartCoroutine(StartRound());
    }

    private void Update()
    {
        if(roundInProgress && EnemyRemaining == 0)
        {
            StartCoroutine(EndRound()); 
        }
    }

    void GetSpawnNumber()
    {
        if (Round.Value < 3)
        {
            EnemyWave wave = waves[Round.Value - 1];
            banditNumber = wave.banditCount;
            wizardNumber = wave.wizardCount;
        }
        else if(Round.Value >= 3)
        {
            if(Round.Value % 2 == 0)
            {
                banditNumber = Round.Value / 2;
                wizardNumber = (Round.Value - 2) / 2 ;
            }
            else
            {
                banditNumber = (Round.Value - 1) / 2;
                wizardNumber = (Round.Value - 1) / 2;
            }
        }

    }

    void SpawnBandit()
    {
        for(int i = 0; i < banditNumber; i++)
        {
            GameObject bandit = Instantiate (banditPrefab, new Vector2(i + 3, -1), Quaternion.identity);
            bandit.GetComponent<EnemyHealth>().OnDeath += OnDeath;
        }
    }

    void SpawnWizard()
    {
        for (int i = 0; i < wizardNumber; i++)
        {
            GameObject wizard = Instantiate(wizardPrefab, new Vector2(i + 12, -1), Quaternion.identity);
            wizard.GetComponent<EnemyHealth>().OnDeath += OnDeath;  
        }
    }

    public void OnDeath()
    {
        EnemyRemaining--;
    }

    public IEnumerator StartRound()
    {
        GetSpawnNumber();

        EndGameUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        EndGameUI.SetActive(false);

        SpawnBandit();
        SpawnWizard();
        EnemyRemaining = banditNumber + wizardNumber;
        roundInProgress = true;
    }

    public IEnumerator EndRound()
    {
        Round.Value++;
        roundInProgress = false;
        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game");
    }
}
