using System.Collections;
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
    private List<SpriteRenderer> activeClouds = new List<SpriteRenderer>();


    public static MovingCloudManager Singleton;
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

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
            GameObject cloud = activeClouds[i].gameObject;
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

        activeClouds.Add(cloud.GetComponent<SpriteRenderer>());
    }

    public float fadeDuration = 1f;

    public void FadeOutAll() => StartCoroutine(FadeOut());

    private IEnumerator FadeOut()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);

            foreach (var s in activeClouds)
                if (s) s.color = new Color(s.color.r, s.color.g, s.color.b, alpha);

            yield return null;
        }

        // Pastikan alpha = 0 di akhir
        foreach (var s in activeClouds)
            if (s) s.color = new Color(s.color.r, s.color.g, s.color.b, 0);
    }

    public void FadeInAll() => StartCoroutine(FadeIn());

    private IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 0.5f, t / fadeDuration);

            foreach (var s in activeClouds)
                if (s) s.color = new Color(s.color.r, s.color.g, s.color.b, alpha);

            yield return null;
        }

        foreach (var s in activeClouds)
            if (s) s.color = new Color(s.color.r, s.color.g, s.color.b, 0.5f);
    }
}
