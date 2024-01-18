using UnityEngine;
using UnityEngine.AI; 

public class Spawner : MonoBehaviour
{
    public GameObject[] pickupPrefabs;
    public GameObject enemyPrefab; 
    public int numberOfPickups = 5; 
    public int numberOfEnemies = 3; 
    public float safetyRadius = 10f; 
    public float pickupSafetyRadius = 10f; 



    void Start()
    {
        numberOfPickups = GameManager.instance.currentPickups;
        numberOfEnemies = GameManager.instance.currentEnemies;

        SpawnPickups();
        SpawnEnemies();
    }




    void SpawnPickups()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 

        for (int i = 0; i < numberOfPickups; i++)
        {
            Vector3 spawnPosition;

            do
            {
                spawnPosition = GetRandomPosition();

                spawnPosition = GetGroundPosition(spawnPosition); 
            } while (Vector3.Distance(spawnPosition, player.transform.position) < pickupSafetyRadius);

            int randomIndex = Random.Range(0, pickupPrefabs.Length);
            Instantiate(pickupPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
    }


    Vector3 GetGroundPosition(Vector3 originalPosition)
    {
        RaycastHit hit;

        float elevation = 1.0f; 

        
        if (Physics.Raycast(originalPosition + Vector3.up * 100, Vector3.down, out hit, Mathf.Infinity))
        {
            

            return hit.point + Vector3.up * elevation;
        }
        else
        {
           

            return originalPosition + Vector3.up * elevation;
        }
    }

       void SpawnEnemies()
       {
           GameObject player = GameObject.FindGameObjectWithTag("Player"); 


           for (int i = 0; i < numberOfEnemies; i++)
           {
               Vector3 spawnPosition;

               do
               {
                   spawnPosition = GetRandomNavMeshLocation();


               } while (Vector3.Distance(spawnPosition, player.transform.position) < safetyRadius);

               GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
               enemy.GetComponent<Enemy>().target = player.transform; 
           }
       }
    

 

    Vector3 GetRandomNavMeshLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        int t = Random.Range(0, navMeshData.indices.Length - 3);

       
        Vector3 point1 = navMeshData.vertices[navMeshData.indices[t]];

        Vector3 point2 = navMeshData.vertices[navMeshData.indices[t + 1]];


        Vector3 point3 = navMeshData.vertices[navMeshData.indices[t + 2]];

     
        float r1 = Random.value;
        float r2 = Random.value;
        Vector3 randomPoint = (1 - Mathf.Sqrt(r1)) * point1 + (Mathf.Sqrt(r1) * (1 - r2)) * point2 + (Mathf.Sqrt(r1) * r2) * point3;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            


            return Vector3.zero;
        }
    }

    Vector3 GetRandomPosition()
    {
        
        float range = 10f;

        Vector3 randomPosition = new Vector3(
            Random.Range(-range, range),
            0, 

            Random.Range(-range, range)
        );

        return GetGroundPosition(randomPosition); 
    }


}