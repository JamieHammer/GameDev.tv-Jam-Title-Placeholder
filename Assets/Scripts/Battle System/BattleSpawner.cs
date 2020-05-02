using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpawner : MonoBehaviour
{
    #region Singleton
    public static BattleSpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public GameObject battleSystemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnBattleSystem()
    {
        Instantiate(battleSystemPrefab, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
