using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursorMainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
    }
        
}
