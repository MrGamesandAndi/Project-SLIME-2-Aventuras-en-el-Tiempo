using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;

    public Texture2D menuCursor, bullseyeClear, bullseyeDetect;

    private void Awake()
    {
        instance = this;
    }

    public void ActivateMenuCursor()
    {
        Cursor.SetCursor(menuCursor, Vector2.zero, CursorMode.Auto);
    }

    public void ActivateBullseyeClear()
    {
        Cursor.SetCursor(bullseyeClear, new Vector2(bullseyeClear.width/2,bullseyeClear.height/2), CursorMode.Auto);
    }

    public void ActivateBullseyeDetect()
    {
        Cursor.SetCursor(bullseyeDetect, new Vector2(bullseyeDetect.width / 2, bullseyeDetect.height / 2), CursorMode.Auto);
    }
}
