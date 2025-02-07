using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public Camera cam;

    public float spawnZ = 100f;
    public float spawnRate = 2f;
    public int enemyCount = 0;
    public float startingZ = 50f;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        float screenPadding = 2f; // Extra padding to ensure enemies spawn slightly outside the screen
        Vector3 spawnPoint = Vector3.zero;
        int edge = Random.Range(0, 4); // 0 = Top, 1 = Bottom, 2 = Left, 3 = Right

        switch (edge)
        {
            case 0: // Top
                spawnPoint =
                    cam.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1, spawnZ))
                    + new Vector3(0, screenPadding, 0);
                break;
            case 1: // Bottom
                spawnPoint =
                    cam.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 0, spawnZ))
                    + new Vector3(0, -screenPadding, 0);
                break;
            case 2: // Left
                spawnPoint =
                    cam.ViewportToWorldPoint(new Vector3(0, Random.Range(0f, 1f), spawnZ))
                    + new Vector3(-screenPadding, 0, 0);
                break;
            case 3: // Right
                spawnPoint =
                    cam.ViewportToWorldPoint(new Vector3(1, Random.Range(0f, 1f), spawnZ))
                    + new Vector3(screenPadding, 0, 0);
                break;
        }

        GameObject currentEnemy = Instantiate(Enemy, spawnPoint, Quaternion.identity);
        EnemyController currentEnemyController = currentEnemy.GetComponent<EnemyController>();

        currentEnemyController.targetStartingPos = cam.ViewportToWorldPoint(
            new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), startingZ)
        );
    }
}
