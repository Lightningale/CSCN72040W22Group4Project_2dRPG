using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;
    private int enemyCount=0,maxEnemies=2;
    public List<Vector3> spawnPositionList;

    private List<bool> spawnPositionOccupation;
    //public static GameObject[] enemyPrefabs;
    private List<GameObject> enemyPrefabs;
    
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {   
        
       //enemyPrefabs.Add(Resources.Load("Assets/Resources/Enemies/Enemy_Goblin")as GameObject);
        spawnPositionOccupation=new List<bool>(new bool[spawnPositionList.Count]);
        enemyPrefabs=Resources.LoadAll<GameObject>("Enemies/").ToList();
        for(int i=0;i<maxEnemies;i++)
        {
            GameObject spawned=spawnEnemy();
          //  activeEnemies.Add(spawned);
        }
        EnemyManager.instance=this;
        //Random.seed = System.DateTime.Now.Millisecond;
        
        player=FindObjectOfType<Player>() as Player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static EnemyManager GetInstance()
    {
        if(EnemyManager.instance==null)
            EnemyManager.instance=new EnemyManager();
        return EnemyManager.instance;
    }
    private int RandomSpawnPosition()
    {
        //int i=Random.Range(0,spawnPositionList.Count);
        int i=(int)(1/Random.value)%spawnPositionList.Count;
        while(spawnPositionOccupation[i]==true)
        {
            if(i<(spawnPositionList.Count-1))
                i++;
            else
                i=0;
        }
        return i;
    }
    public void NotifyEnemyDeath(int positionID)
    {
        spawnPositionOccupation[positionID]=false;
        StartCoroutine("DelayedSpawn");
        player.AddExp(100);
    }
    private GameObject spawnEnemy()
    {
        GameObject spawned;
        int positionID=RandomSpawnPosition();
        int enemyType=(int)(1/Random.value)%enemyPrefabs.Count;
        spawned=Instantiate(enemyPrefabs[enemyType],spawnPositionList[positionID],new Quaternion());
        spawnPositionOccupation[positionID]=true;
        spawned.GetComponent<Enemy>().Initialize(1,positionID);
        spawned.GetComponent<Enemy>().UpdateStats();
        return spawned;
    }
    private IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(3f);
        spawnEnemy();
    }
}
