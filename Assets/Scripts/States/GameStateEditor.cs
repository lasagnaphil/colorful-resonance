using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class GameStateEditor : IGameState
    {
        private bool isDragging = false;
        private Vector2 prevMousePos;
        private float dragScale = 0.01f;
        private float cameraMoveSpeed = 0.1f;

        public EditorUIManager editorUIManager;
        private List<GameObject> gmsToDisable = new List<GameObject>();

        public void Enter(GameStateManager gsm)
        {
            Debug.Log("Entering Editor...");
            Debug.Log("Editor functionality not implemented yet!!!");

            editorUIManager = gsm.editorUIManager;
            editorUIManager.gameObject.SetActive(true);

            gmsToDisable.Add(GameObject.Find("TurnNumberBackground"));
            gmsToDisable.Add(GameObject.Find("PlayerHealthImages"));

            foreach (var gm in gmsToDisable)
            {
                gm.SetActive(false);
            }

            gsm.inputManager.Enabled = false;
        }

        public void Update(GameStateManager gsm)
        {
            InputUpdate(gsm);
        }

        public void InputUpdate(GameStateManager gsm)
        {
            if (isDragging)
            {
                gsm.camera.transform.Translate(((Vector2)Input.mousePosition - prevMousePos)*dragScale);
                prevMousePos = Input.mousePosition;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gsm.ChangeState<GameStatePlay>();
            }
            if (Input.GetMouseButtonDown(1))
            {
                isDragging = true;
                prevMousePos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(1))
            {
                isDragging = false;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                gsm.camera.transform.Translate(0f, cameraMoveSpeed, 0f);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                gsm.camera.transform.Translate(0f, -cameraMoveSpeed, 0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                gsm.camera.transform.Translate(cameraMoveSpeed, 0f, 0f);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gsm.camera.transform.Translate(-cameraMoveSpeed, 0f, 0f);
            }
            if (Input.mouseScrollDelta.y != Mathf.Epsilon)
            {
                gsm.camera.orthographicSize += Input.mouseScrollDelta.y;
                if (gsm.camera.orthographicSize < 0)
                    gsm.camera.orthographicSize -= Input.mouseScrollDelta.y;
            }
        }

        public void Exit(GameStateManager gsm)
        {
            foreach (var gm in gmsToDisable)
            {
                gm.SetActive(true);
            }
            editorUIManager.gameObject.SetActive(false);

            gsm.inputManager.Enabled = true;
        }
    }
}