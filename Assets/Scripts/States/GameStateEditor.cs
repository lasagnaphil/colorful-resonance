using System.Collections.Generic;
using System.Linq;
using Buttons;
using UnityEngine;
using Utils;

namespace States
{
    public class GameStateEditor : IGameState
    {
        private bool isDragging = false;
        private Vector2 prevMousePos;
        private float dragScale = 0.01f;
        private float cameraMoveSpeed = 0.1f;
        private float originalCameraSize;

        public EditorUIManager editorUIManager;
        private List<GameObject> gmsToDisable = new List<GameObject>();

        private Dictionary<TileColor, string> colorDict = new Dictionary<TileColor, string>()
        {
            {TileColor.None, "none"},
            {TileColor.Black, "black"},
            {TileColor.White, "white"},
            {TileColor.Red, "red"},
            {TileColor.Blue, "blue" },
            {TileColor.Yellow, "yellow"},
            {TileColor.Green, "green"}
        };
        private Dictionary<TileData, char> tileDataDict = new Dictionary<TileData, char>
        {
            {new TileData(TileColor.None, TileType.None), '.'},
            {new TileData(TileColor.None, TileType.Normal), 'o'},
            {new TileData(TileColor.Black, TileType.Normal), 'l'},
            {new TileData(TileColor.White, TileType.Normal), 'w'},
            {new TileData(TileColor.Red, TileType.Normal), 'r'},
            {new TileData(TileColor.Blue, TileType.Normal), 'b'},
            {new TileData(TileColor.Yellow, TileType.Normal), 'y'},
            {new TileData(TileColor.Green, TileType.Normal), 'g'},
            {new TileData(TileColor.None, TileType.Wall), 'O'},
            {new TileData(TileColor.Black, TileType.Wall), 'L'},
            {new TileData(TileColor.White, TileType.Wall), 'W'},
            {new TileData(TileColor.Red, TileType.Wall), 'R'},
            {new TileData(TileColor.Blue, TileType.Wall), 'B'},
            {new TileData(TileColor.Yellow, TileType.Wall), 'Y'},
            {new TileData(TileColor.Green, TileType.Wall), 'G'}
        };

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

            originalCameraSize = gsm.camera.orthographicSize;
        }

        public void Update(GameStateManager gsm)
        {
            if (!editorUIManager.IsEditingInfo)
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

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save(gsm);
            }
        }

        public void Save(GameStateManager gsm)
        {
            TileManager tileManager = TileManager.Instance;
            int width = tileManager.width;
            int height = tileManager.height;
            MapLoader mapLoader = gsm.mapLoader;
            
            MapData mapData = new MapData();

            mapData.name = editorUIManager.editInfoNameField.text;
            // mapData.comment = editorUIManager.editInfoDescriptionText.text;
            mapData.winCondition = gsm.mapData.winCondition;
            mapData.width = width;
            mapData.height = height;
            mapData.tiles = new char[width*height];
            for (int i = 0; i < width*height; i++)
            {
                int x = i%width;
                int y = height - i/width - 1;
                mapData.tiles[i] = tileDataDict[tileManager.tiles[x, y].Data];
            }
            mapData.background = gsm.mapData.background;
            mapData.playerData = new PlayerData();
            mapData.playerData.position = gsm.player.pos.GetVector2i();
            mapData.monsters = gsm.monsters.Select(m =>
            {
                MonsterData data = new MonsterData();
                data.name = m.name.Replace("(Clone)", "");
                data.position = m.pos.GetVector2i();
                return data;
            }).ToArray();
            mapData.orbs = gsm.orbs.Select(o =>
            {
                OrbData data = new OrbData();
                data.color = colorDict[o.Color];
                data.position = o.pos.GetVector2i();
                return data;
            }).ToArray();
            mapData.buttons = gsm.buttons.Select(b =>
            {
                ButtonData data = new ButtonData();
                data.name = b.name.Replace("(Clone)", "");
                data.position = b.pos.GetVector2i();
                if (b is WallToggleButton)
                {
                    var wtButton = (WallToggleButton) b;
                    data.togglePosition = wtButton.wallTogglePos;
                    data.isWallOnButtonOff = wtButton.isWallOnButtonOff;
                }
                if (b is WallToggleLever)
                {
                    var wtLever = (WallToggleLever) b;
                    data.togglePosition = wtLever.wallTogglePos;
                    data.isWallOnButtonOff = wtLever.isWallOnButtonOff;
                }
                return data;
            }).ToArray();

            string jsonData = JsonHelper.Serialize<MapData>(mapData);
            System.IO.File.WriteAllText(Application.dataPath + "/Maps/" + mapData.name + ".json", jsonData);
            Load(gsm);
        }

        public void Load(GameStateManager gsm)
        {
            gsm.ChangeState<GameStateLoad>();
        }

        public void Exit(GameStateManager gsm)
        {
            foreach (var gm in gmsToDisable)
            {
                gm.SetActive(true);
            }
            editorUIManager.gameObject.SetActive(false);

            gsm.inputManager.Enabled = true;

            gsm.camera.orthographicSize = originalCameraSize;
        }
    }
}