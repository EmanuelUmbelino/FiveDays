using UnityEngine;
using System.Collections;

public class DetectClick : MonoBehaviour {

    [SerializeField]
    private GameObject glass;
    

    void OnMouseDown()
    {
        glass.GetComponent<MagnifyGlass>().isSelect = true;
        Cursor.visible = false;
    }
    void OnMouseUp()
    {
        glass.GetComponent<MagnifyGlass>().isSelect = false;
        Cursor.visible = true;
    }
}
