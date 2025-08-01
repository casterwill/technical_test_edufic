using System.Collections.Generic;
using UnityEngine;

public class MovingCloudManager : MonoBehaviour
{
     [Header("Cloud Settings")]
    public GameObject[] cloudPrefabs;
    public float minY = -3f;
    public float maxY = 3f;
    public float spawnInterval = 2f;
    public int maxClouds = 5;

    [Header("Spawn Area")]
    public float spawnX = 10f; // Posisi X untuk spawn (kanan layar)
    public float despawnX = -12f; // Posisi X untuk hapus (kiri layar)

    [Header("Cloud Movement")]
    public float minSpeed = 1f;
    public float maxSpeed = 3f;

    private float timer = 10f;
    private List<GameObject> activeClouds = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        // Spawn cloud jika belum melebihi batas
        if (timer >= spawnInterval && activeClouds.Count < maxClouds)
        {
            SpawnCloud();
            timer = 0f;
        }

        // Gerakkan cloud dan hapus jika keluar layar
        for (int i = activeClouds.Count - 1; i >= 0; i--)
        {
            GameObject cloud = activeClouds[i];
            cloud.transform.position += Vector3.left * cloud.GetComponent<CloudMover>().speed * Time.deltaTime;

            if (cloud.transform.position.x < despawnX)
            {
                Destroy(cloud);
                activeClouds.RemoveAt(i);
            }
        }
    }

    void SpawnCloud()
    {
        GameObject prefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];
        float yPos = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnX, yPos, 0f);
        GameObject cloud = Instantiate(prefab, spawnPos, Quaternion.identity, transform);

        float speed = Random.Range(minSpeed, maxSpeed);
        CloudMover mover = cloud.AddComponent<CloudMover>();
        mover.speed = speed;

        activeClouds.Add(cloud);
    }
}
