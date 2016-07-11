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
            if (Input.GetMouseButtonDown(1))
            {
                int mousePosX = (int) Mathf.Round(Input.mousePosition.x + camera.transform.position.x);
                int mousePosY = (int) Mathf.Round(Input.mousePosition.y + camera.transform.position.y);
                pos = new Vector2i(mousePosX, mousePosY);

                transform.position = new Vector3(pos.x, pos.y, 1f);
            }
        }
    }
}