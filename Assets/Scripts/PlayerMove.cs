using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 7;
    public float jumpHeight  = 5;
    public float gravityScale = 5;
    public float distToGround = 0f;
    public LayerMask groundLayer;
    public bool wouldLikeToCling = true;
    private GameObject clingedObject = null;
    private bool wantToCling = true;
    public GameObject spawnPoint = null;
    public int currentLives = 3;    
    public UnityEvent onPlayerDeath;

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Clingable") {
            wantToCling = wouldLikeToCling;
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        // Check if the object is of the type that can be grabbed
        if (other.gameObject.tag == "Clingable" && wantToCling) {
            wantToCling = false;
            clingedObject = other.gameObject;
        }
        if (other.gameObject.tag == "DeathZone") {
            Die();
        }
        if (other.gameObject.tag == "ExitZone") {
            Win();
        }
    }
    void Win() {
        SceneManager.LoadScene("WinScreen");
    }
    private IEnumerator Wait() {
        transform.position = new Vector2(0, -1000);
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        this.transform.position = spawnPoint.transform.position;
        rb.velocity = new Vector2(0, 0);
    }
    void Die() {
        --currentLives;
        onPlayerDeath.Invoke();
        clingedObject = null;
        wantToCling = wouldLikeToCling;
        if (currentLives >= 0) {
            StartCoroutine(Wait());
        } else {
            SceneManager.LoadScene("DeathScreen");
        }
    }
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        wantToCling = wouldLikeToCling;
        
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (clingedObject == null) {
            rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
            if (IsGrounded()) {
                float dirX = Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
            }   
        } 
    }
    void Update() {
        if (clingedObject != null) {
            this.transform.position = clingedObject.transform.position;
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyDown("space") && (IsGrounded() || clingedObject != null)) {
            float dirX = Input.GetAxis("Horizontal");
            Vector2 jump = new Vector2(Mathf.Sign(dirX) / 2, 1);
            clingedObject = null;
            if (Input.GetAxis("Vertical") >= 0) {
                float jumpForce = Mathf.Sqrt(jumpHeight  * -2 * (Physics2D.gravity.y * gravityScale));
                rb.AddForce(jump * jumpForce, ForceMode2D.Impulse);

            }
        }
    }
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        ContactFilter2D contactFilter = new ContactFilter2D();
        List<RaycastHit2D> results = new List<RaycastHit2D>();

        Physics2D.Raycast(position, direction, contactFilter.NoFilter(), results, distToGround);

        foreach (RaycastHit2D hit in results)
        {
            if (hit.collider != null && hit.collider.tag == "Ground") {
                return true;
            }
        }

        return false;
    }
}
