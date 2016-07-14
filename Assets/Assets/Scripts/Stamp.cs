using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stamp : MonoBehaviour {

    private bool isSelect;
    private bool onPaper;
    private GameObject paper;
    private Vector3 mousePos;

    [SerializeField]
    private bool isStamp;
    [SerializeField]
    private bool approved;

    // Use this for initialization
    void Start () {
	
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Paper") && this.tag != "Paper")
        {
            onPaper = true;
            paper = coll.gameObject;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Paper") && this.tag != "Paper")
        {
            onPaper = false;
            paper = null;
        }
    }
    void OnMouseDown()
    {
        isSelect = true;
        Cursor.visible = false;
    }
    void OnMouseUp()
    {
        isSelect = false;
        Cursor.visible = true;
        if (onPaper)
        {
            if (approved)
            {
                if (!paper.transform.GetChild(1).GetComponent<Image>().enabled && !paper.transform.GetChild(2).GetComponent<Image>().enabled)
                {
                    paper.transform.GetChild(1).transform.position = this.transform.position;
                    paper.transform.GetChild(1).GetComponent<Image>().enabled = true;
                    // é aq, isso é pra mandar um contrato aprovado
                }
            }
            else
            {
                if (!paper.transform.GetChild(1).GetComponent<Image>().enabled && !paper.transform.GetChild(2).GetComponent<Image>().enabled)
                {
                    paper.transform.GetChild(2).transform.position = this.transform.position;
                    paper.transform.GetChild(2).GetComponent<Image>().enabled = true;
                    // é aq, isso é pra mandar um contrato reprovado
                }
            }
        }
            
    }
    // Update is called once per frame
    void Update () {
        if (isSelect)
        {
            mousePos = getWorldPosition(Input.mousePosition);
            this.transform.position = new Vector3(mousePos.x,mousePos.y, 0);
            if(this.tag.Equals("Paper"))
                this.transform.position = new Vector3(mousePos.x, mousePos.y, 1);
        }
    }
    // Following method calculates world's point from screen point as per camera's projection type
    public Vector3 getWorldPosition(Vector3 screenPos)
    {
        Vector3 worldPos;
        if (Camera.main.orthographic)
        {
            worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            worldPos.z = Camera.main.transform.position.z;
        }
        else
        {
            worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.transform.position.z));
            worldPos.x *= -1;
            worldPos.y *= -1;
        }
        return worldPos;
    }
}
