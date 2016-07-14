using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PaperManager : MonoBehaviour {
    
    private bool approved;

    [SerializeField]
    private BoxCollider2D myCollider;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.GetChild(1).GetComponent<Image>().enabled)
        {
            approved = true;
            myCollider.enabled = false;
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 0));

        }
        else if (transform.GetChild(2).GetComponent<Image>().enabled)
        {
            approved = false;
            myCollider.enabled = false;
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 0));
        }
    }
}
