using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;  // Spawnlanacak araç prefab'ı
    public float minSpawnInterval = 3f;  // Minimum spawn süresi
    public float maxSpawnInterval = 15f;  // Maksimum spawn süresi
    public Transform spawnPoint;  // Spawn noktası

    private float spawnInterval;  // Mevcut spawn süresi
    private float timer;  // Zamanlayıcı

    void Start()
    {
        // İlk spawn süresini belirlenen aralıkta rastgele seç
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        timer = spawnInterval;
    }

    void Update()
    {
        // Zamanlayıcıyı güncelle
        timer -= Time.deltaTime;

        // Zamanlayıcı sıfırlandığında prefab'ı spawnla
        if (timer <= 0f)
        {
            SpawnCar();
            // Her spawn işleminden sonra spawn süresini rastgele değiştir
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            timer = spawnInterval;  // Zamanlayıcıyı sıfırla
        }
    }

    void SpawnCar()
    {
        // Eğer spawn noktası belirtilmişse, oraya spawnla
        if (spawnPoint != null)
        {
            Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            // Eğer spawn noktası belirtilmemişse, CarSpawner'ın olduğu konuma spawnla
            Instantiate(carPrefab, transform.position, Quaternion.identity);
        }
    }
}