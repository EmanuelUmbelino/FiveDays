using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MyExtensions;

public class GameController : MonoBehaviour {

    [SerializeField]
    private Text clock;
    [SerializeField]
    private GameObject paper;
    [SerializeField]
    private GameObject screen;

    private int seconds;
    private int minutes;
    private int random;

    void Start()
    {
        seconds = 0;
        minutes = 0;
        random = 2;
    }

	void FixedUpdate () {
        seconds = Mathf.FloorToInt(Time.fixedTime);
        minutes = Mathf.FloorToInt(seconds / 60);
        seconds = seconds - (60 * minutes);
        if(seconds < 10 && minutes < 10)
            clock.text = "0" + minutes + ":0" + seconds;
        else if (minutes < 10)
            clock.text = "0" + minutes + ":" + seconds;
        else if (seconds < 10)
            clock.text = minutes + ":0" + seconds;
        else
            clock.text = minutes + ":" + seconds;

        if(seconds.Equals(random))
        {
            GameObject temp = (GameObject) Instantiate(paper);
            temp.GetComponent<RectTransform>().SetParent(screen.GetComponent<RectTransform>(), false);
            temp.GetComponent<RectTransform>().position = new Vector3(10, Random.Range(-300,300)/100, 0);
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1500, 0));
            random = Random.Range(0,59);
        }

    }
}
