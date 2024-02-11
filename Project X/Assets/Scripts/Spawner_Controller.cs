using System.Collections;
using UnityEngine;

public class Spawner_Controller : MonoBehaviour
{

    public GameObject asteriod;
    public GameObject home;
    private Transform playerPos;
    [SerializeField] private float spawnRate;
    [SerializeField] private float distance;
    [SerializeField] private bool isSpawning = false;


    // Update is called once per frame
    void Update()
    {
        spawnRate = 7;
        Transform homePos = home.transform;
        playerPos = GameObject.Find("Player").transform;

        if (!isSpawning)
        {
            StartCoroutine(Asteriod_Spawn_Timer(playerPos));
        }
        
        Vector2 distanceVector = playerPos.position - homePos.position;
        distance = Mathf.Pow(Mathf.Pow(Mathf.Abs(distanceVector.x), 2) + Mathf.Pow(Mathf.Abs(distanceVector.y), 2), 0.5f);

        if (spawnRate > 0.1f)
        {
            spawnRate -= Mathf.Floor(distance / 100) * 0.5f;
        }
        else
        {
            spawnRate = 0.1f;
        }



    }

    IEnumerator Asteriod_Spawn_Timer(Transform playerPos)
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnRate);
        Spawn_Asteriod(playerPos);
        isSpawning = false;
    }

    private void Spawn_Asteriod(Transform pos) {

        Vector2 v2Pos = Convert_To_V2(pos);

        Vector2 randomPos = v2Pos + UnityEngine.Random.insideUnitCircle.normalized * 22;

        GameObject asteriodInstance = Instantiate(asteriod, randomPos, Quaternion.identity);

    }

    private Vector2 Convert_To_V2(Transform pos)
    {
        Vector2 v2 = new Vector2(pos.position.x, pos.position.y);
        return v2;
    }



}
