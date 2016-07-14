using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class MagnifyGlass : MonoBehaviour
{
    private Camera magnifyCamera;
    private GameObject magnifyBorders;
    private LineRenderer LeftBorder, RightBorder, TopBorder, BottomBorder; // Reference for lines of magnify glass borders
    private float MGOX, MG0Y; // Magnify Glass Origin X and Y position
    private float MGWidth = Screen.width / 17, MGHeight = Screen.width / 17; // Magnify glass width and height
    public bool isSelect;
    private Vector3 mousePos;

    [SerializeField]
    private GameObject glass;
    [SerializeField]
    private Shader fish;

    void Start()
    {
        createMagnifyGlass();
}
    void Update()
    {
        if (isSelect)
        {
            // Following lines set the camera's pixelRect and camera position at mouse position
            magnifyCamera.pixelRect = new Rect(Input.mousePosition.x - MGWidth / 2.0f, Input.mousePosition.y - MGHeight / 2.0f, MGWidth, MGHeight);
            mousePos = getWorldPosition(Input.mousePosition);
            magnifyCamera.transform.position = mousePos;
            mousePos.z = 0;
            Cursor.visible = false;
        }
    }

    // Following method creates MagnifyGlass
    private void createMagnifyGlass()
    {
        GameObject camera = new GameObject("MagnifyCamera");
        MGOX = Screen.width / 2f - MGWidth / 2f;
        MG0Y = Screen.height / 2f - MGHeight / 2f;
        magnifyCamera = camera.AddComponent<Camera>();
        magnifyCamera.pixelRect = new Rect(MGOX, MG0Y, MGWidth, MGHeight);
        magnifyCamera.transform.position = new Vector3(0, 0, 0);
        isSelect = false;
        if (Camera.main.orthographic)
        {
            magnifyCamera.orthographic = true;
            magnifyCamera.orthographicSize = Camera.main.orthographicSize / 11;//+ 1.0f;
            magnifyCamera.backgroundColor = new Color(0.46f, 0.36f, 0.29f);
            magnifyCamera.gameObject.AddComponent<Fisheye>();
            magnifyCamera.gameObject.GetComponent<Fisheye>().SetMe(fish,0.5f,0.5f);
            glass.transform.SetParent(magnifyCamera.transform);
            glass.transform.localPosition = new Vector3(0, 0, 5);
        }
        Vector3 fakePosition = new Vector3(Screen.width*8  / 10 , Screen.height*7 / 10, 0);
        // Following lines set the camera's pixelRect and camera position at mouse position
        magnifyCamera.pixelRect = new Rect(fakePosition.x - MGWidth / 2.0f, fakePosition.y - MGHeight / 2.0f, MGWidth, MGHeight);
        magnifyCamera.transform.position = getWorldPosition(fakePosition);

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