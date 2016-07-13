using UnityEngine;

namespace GameEditor
{
    public class SelectedTileCursor : MonoBehaviour
    {
        public Vector2i pos;

        public new Camera camera;

        void Awake()
        {
            camera = FindObjectOfType<Camera>();
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 cursorPos = camera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(
                    Mathf.Round(cursorPos.x), 
                    Mathf.Round(cursorPos.y),
                    1f);
            }
        }
    }
}