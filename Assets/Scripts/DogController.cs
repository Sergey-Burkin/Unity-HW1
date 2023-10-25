using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Math;
public class DogController : MonoBehaviour
{
    public List<GameObject> spawnPointList = new List<GameObject>();
    public GameObject playerToFollow;
    public float speed = 1;
    public float minDelay = 1f;
    public float maxDelay = 5f;
    private float current_velocity;
    Rigidbody2D rb;


    // Start is called before the first frame update

    private bool canSeeThePlayer() {
        return Mathf.Abs(playerToFollow.transform.position.y - transform.position.y) < 1;
    }
    private bool leftFromScreen() {
        return transform.position.y < -4;
    }
    private void turnToPlayer() {
        current_velocity = speed * Mathf.Sign(playerToFollow.transform.position.x - transform.position.x);
    }
    private void spawn() {
        GameObject currentSpawnPoint = spawnPointList[Random.Range(0, spawnPointList.Count)];
        this.transform.position = currentSpawnPoint.transform.position;
        turnToPlayer();
    }
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CallRandomFunction());
    }
    private IEnumerator CallRandomFunction() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            if (leftFromScreen ()) {
                spawn();
            }
        }
    }

    void FixedUpdate() {
        if (canSeeThePlayer()) {
            turnToPlayer();
        }
        rb.velocity = new Vector2(current_velocity, 0);
        
    }
}
