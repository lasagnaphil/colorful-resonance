﻿using UnityEngine;

namespace GameEditor
{
    public class HoverTileCursor : MonoBehaviour
    {
        public Vector2i pos;
        public new Camera camera;

        void Awake()
        {
            camera = FindObjectOfType<Camera>();
        }
        public void EditorUpdate()
        {
            Vector2 cursorPos = camera.ScreenToWorldPoint(Input.mousePosition);
            pos = new Vector2i((int)Mathf.Round(cursorPos.x), (int)Mathf.Round(cursorPos.y));
            transform.position = pos.ToVector3(1.0f);
        }
    }
}