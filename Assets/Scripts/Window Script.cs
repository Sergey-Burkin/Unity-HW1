using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject playerToFollow;
    public float throwForce = 300;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void Activate() {
        bullet.transform.position = transform.position;
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.velocity = new Vector2(0, 0);
        float sign = (playerToFollow.transform.position.x - transform.position.x) / 10;
        bulletBody.AddForce(new Vector2(sign * throwForce, throwForce));
        spriteRenderer.color = Color.green;
        tag = "ExitZone";
        StartCoroutine(WaitAndChangeColor());
    }

    private IEnumerator WaitAndChangeColor() {
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        spriteRenderer.color = Color.grey;
        tag = "Untagged";
    }
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.grey;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
