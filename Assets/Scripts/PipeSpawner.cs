using UnityEngine;

public class PipeSpawner : MonoBehaviour {

    public GameObject pipePrefab;

    public float spawnInterval;
    private float timer;

    void Start() {
        timer = spawnInterval;
    }

    void Update() {
        if (GameManager.Instance.IsGameRunning()) {
            if (timer >= spawnInterval) {
                SpawnPipe();
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }

    private void SpawnPipe() {
        float posY = Random.Range(0, 200) / 100f;
        Instantiate(pipePrefab, new Vector3(3, posY, 0), Quaternion.identity);
    }
}
