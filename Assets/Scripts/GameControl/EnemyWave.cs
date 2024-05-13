using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave
{
    public int banditCount;
    public int wizardCount;

    public EnemyWave(int bandit, int wizard)
    {
        this.banditCount = bandit;  
        this.wizardCount = wizard;  
    }
}
