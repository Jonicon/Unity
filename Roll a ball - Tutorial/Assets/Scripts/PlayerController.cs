using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 100f;
    public Text countText;
    public Text winText;
    public float loadDealy = 2.0f;

    private Rigidbody rb;
    public GameObject[] pickUps;
    public GameObject dLight;
    private int count = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        DontDestroyOnLoad(dLight);
        setCount(count);
        winText.text = "";
	}

    // called upon fixed interval
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 v = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(v * speed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            setCount(count += 1);
        }
    }

    private void setCount(int c) {
        count = c;
        countText.text = "Count " + count.ToString();
        checkForWin();
    }

    private void checkForWin() {
        if (count >= pickUps.Length) {
            winText.text = "You win!";
            Invoke("Reload", loadDealy);
        }
        
    }

    private void Reload(){
        Application.LoadLevel(Application.loadedLevel);
    }
}
