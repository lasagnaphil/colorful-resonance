using UnityEngine;

namespace GameEditor
{
    public class SelectedTileCursor : MonoBehaviour
    {
        public Vector2i pos;

        public new Camera camera;

        public EditorUIManager uiManager;

        void Awake()
        {
            camera = FindObjectOfType<Camera>();
        }
        public void EditorUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                uiManager.OnSelected();
                Vector2 cursorPos = camera.ScreenToWorldPoint(Input.mousePosition);
                pos = new Vector2i((int)Mathf.Round(cursorPos.x), (int)Mathf.Round(cursorPos.y));
                transform.position = pos.ToVector3(1.0f);
            }
        }
    }
}