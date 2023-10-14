using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public float SpawnTime;
    private float SpawnCounter;

    public Transform minSpawnPoint, maxSpawnPoint;
    private Transform Target;
    private float DisSpawnDistance;

    public List<GameObject> SpawnedEnemy = new List<GameObject>();
    public int CheckNumber;//每帧检查的敌人数量
    private int EnemyNum;//敌人编号

    public List<WaveInformation> waveInfo;

    private int currentWave;
    private float WaveCounter;

    void Awake()
    {
        Target = FindObjectOfType<PlayerControl>().transform;
    }
    private void Start()
    {
        SpawnCounter = SpawnTime;
        DisSpawnDistance = Vector3.Distance(transform.position, minSpawnPoint.position) + 2f;
        currentWave = -1;
        GoToNextWave();
    }

    private void Update()
    {
       /* SpawnCounter -= Time.deltaTime;
        if (SpawnCounter<=0)
        {
            SpawnCounter = SpawnTime;
            GameObject newEnemy= Instantiate(Enemy,SetSpawnPoint(),transform.rotation);
            SpawnedEnemy.Add(newEnemy);
        }*/

       if (PlayerHealth.Instance.gameObject.activeSelf)
       {
           if (currentWave<waveInfo.Count)
           {
               WaveCounter -= Time.deltaTime;
               if (WaveCounter<=0)
               {
                   GoToNextWave();
               }

               SpawnCounter -= Time.deltaTime;
               if (SpawnCounter<=0)
               {
                   SpawnCounter = waveInfo[currentWave].timeBetweenSpawn;
                   GameObject newEnemy= Instantiate(waveInfo[currentWave].Enemy,SetSpawnPoint(),transform.rotation);
                   SpawnedEnemy.Add(newEnemy);
               }
           }
       }
       
       
       
        //检查敌人是否过远
        int checkTarget = CheckNumber + EnemyNum;
        while (EnemyNum<checkTarget)
        {
            if (EnemyNum<SpawnedEnemy.Count)
            {
                if (SpawnedEnemy[EnemyNum]!=null)
                {
                    if (Vector3.Distance(SpawnedEnemy[EnemyNum].transform.position,transform.position)>=DisSpawnDistance)
                    {
                        Destroy(SpawnedEnemy[EnemyNum]);
                        SpawnedEnemy.RemoveAt(EnemyNum);
                        checkTarget -= 1;
                    }
                    else
                    {
                        EnemyNum++;
                    }
                }
                else
                {
                    SpawnedEnemy.RemoveAt(EnemyNum);//检测敌人是否死亡
                    checkTarget -= 1;
                }
            }
            else
            {
                EnemyNum = 0;
                checkTarget = 0;
            }
        }
    }
    void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x,Target.position.y,transform.position.z) ;
    }

    private Vector2 SetSpawnPoint()
    {
        Vector2 SpawnPoint=Vector2.zero;
       
        
        if (Random.Range(0f, 1f) > 0.5f)
        {
            SpawnPoint.y = Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                SpawnPoint.x = minSpawnPoint.position.x;
            }
            else
            {
                SpawnPoint.x = maxSpawnPoint.position.x;
            }
        }
        else
        {
            SpawnPoint.x = Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                SpawnPoint.y = minSpawnPoint.position.y;
            }
            else
            {
                SpawnPoint.y = maxSpawnPoint.position.y;
            }
        }
        return SpawnPoint;
    }


    private void GoToNextWave()
    {
        currentWave++;
        if (currentWave>=waveInfo.Count)
        {
            currentWave = waveInfo.Count - 1;
        }

        WaveCounter = waveInfo[currentWave].waveLength;
        SpawnCounter = waveInfo[currentWave].timeBetweenSpawn;
    }
    
}
/// <summary>
/// 敌人波次
/// </summary>
[System.Serializable]
public class WaveInformation
{
    public GameObject Enemy;
    public float waveLength = 10f;
    public float timeBetweenSpawn = 1f;
}
