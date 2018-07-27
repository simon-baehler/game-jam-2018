using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseurManager : MonoBehaviour {


    public Texture2D cursorTexture;
    public CursorMode cursorsMode = CursorMode.Auto;
    public Vector2 HotSpot = Vector2.zero;

    // Use this for initialization
    void Start () {
        Cursor.SetCursor(cursorTexture, HotSpot, cursorsMode);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
