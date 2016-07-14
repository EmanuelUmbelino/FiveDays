﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class MagnifyGlass : MonoBehaviour
{
    private Camera magnifyCamera;
    private GameObject magnifyBorders;
    private LineRenderer LeftBorder, RightBorder, TopBorder, BottomBorder; // Reference for lines of magnify glass borders
    private float MGOX, MG0Y; // Magnify Glass Origin X and Y position
    private float MFactor = 15f;
    private float MGWidth = Screen.width / 17, MGHeight = Screen.width / 17; // Magnify glass width and height
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
        // Following lines set the camera's pixelRect and camera position at mouse position
        magnifyCamera.pixelRect = new Rect(Input.mousePosition.x - MGWidth / 2.0f, Input.mousePosition.y - MGHeight / 2.0f, MGWidth, MGHeight);
        mousePos = getWorldPosition(Input.mousePosition);
        magnifyCamera.transform.position = mousePos;
        mousePos.z = 0;
        //magnifyBorders.transform.position = mousePos;
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
        if (Camera.main.orthographic)
        {
            magnifyCamera.orthographic = true;
            magnifyCamera.orthographicSize = Camera.main.orthographicSize / 11;//+ 1.0f;
            magnifyCamera.backgroundColor = new Color(0.46f, 0.36f, 0.29f);
            magnifyCamera.gameObject.AddComponent<Fisheye>();
            magnifyCamera.gameObject.GetComponent<Fisheye>().SetMe(fish,0.5f,0.5f);
            glass.transform.SetParent(magnifyCamera.transform);
            glass.transform.localPosition = new Vector3(0, 0, 5);
            //createBordersForMagniyGlass();
        }
        else
        {
            magnifyCamera.orthographic = false;
            magnifyCamera.fieldOfView = Camera.main.fieldOfView / 10.0f;//3.0f;
        }

    }

    // Following method sets border of MagnifyGlass
    private void createBordersForMagniyGlass()
    {
        magnifyBorders = new GameObject();
        LeftBorder = getLine();
        LeftBorder.SetVertexCount(2);
        LeftBorder.SetPosition(0, new Vector3(getWorldPosition(new Vector3(MGOX, MG0Y, 0)).x, getWorldPosition(new Vector3(MGOX, MG0Y, 0)).y - 0.1f, -1));
        LeftBorder.SetPosition(1, new Vector3(getWorldPosition(new Vector3(MGOX, MG0Y + MGHeight, 0)).x, getWorldPosition(new Vector3(MGOX, MG0Y + MGHeight, 0)).y + 0.1f, -1));
        LeftBorder.transform.parent = magnifyBorders.transform;
        TopBorder = getLine();
        TopBorder.SetVertexCount(2);
        TopBorder.SetPosition(0, new Vector3(getWorldPosition(new Vector3(MGOX, MG0Y + MGHeight, 0)).x, getWorldPosition(new Vector3(MGOX, MG0Y + MGHeight, 0)).y, -1));
        TopBorder.SetPosition(1, new Vector3(getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y + MGHeight, 0)).x, getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y + MGHeight, 0)).y, -1));
        TopBorder.transform.parent = magnifyBorders.transform;
        RightBorder = getLine();
        RightBorder.SetVertexCount(2);
        RightBorder.SetPosition(0, new Vector3(getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y + MGWidth, 0)).x, getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y + MGWidth, 0)).y + 0.1f, -1));
        RightBorder.SetPosition(1, new Vector3(getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y, 0)).x, getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y, 0)).y - 0.1f, -1));
        RightBorder.transform.parent = magnifyBorders.transform;
        BottomBorder = getLine();
        BottomBorder.SetVertexCount(2);
        BottomBorder.SetPosition(0, new Vector3(getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y, 0)).x, getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y, 0)).y, -1));
        BottomBorder.SetPosition(1, new Vector3(getWorldPosition(new Vector3(MGOX, MG0Y, 0)).x, getWorldPosition(new Vector3(MGOX, MG0Y, 0)).y, -1));
        BottomBorder.transform.parent = magnifyBorders.transform;
    }

    // Following method creates new line for MagnifyGlass's border
    private LineRenderer getLine()
    {
        LineRenderer line = new GameObject("Line").AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Diffuse"));
        line.SetVertexCount(2);
        line.SetWidth(0.2f, 0.2f);
        line.SetColors(Color.black, Color.black);
        line.useWorldSpace = false;
        return line;
    }
    private void setLine(LineRenderer line)
    {
        line.material = new Material(Shader.Find("Diffuse"));
        line.SetVertexCount(2);
        line.SetWidth(0.2f, 0.2f);
        line.SetColors(Color.black, Color.black);
        line.useWorldSpace = false;
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