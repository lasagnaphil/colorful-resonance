using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameEditor;
using States;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;
using UButton = UnityEngine.UI.Button;

public class EditorUIManager : MonoBehaviour
{
    public enum Mode
    {
        Add, Remove
    }
    public enum ObjectType
    {
        Monster, Orb, Switch, Tile
    }

    public RectTransform leftPanelRect;

#region LeftPanelFields
    public UButton monsterButton;
    public UButton orbButton;
    public UButton switchButton;
    public UButton tileButton;
    public UButton addButton;
    public UButton removeButton;

    public UButton objectSelectButtonPrefab;
    public float selectButtonWidth;

    public GameObject scrollViewContent;
    public List<UButton> scrollViewButtons;

    public HoverTileCursor hoverTileCursor;
    public SelectedTileCursor selectedTileCursor;
#endregion

#region UtilPanelFields
    public GameObject editInfoGm;
    public GameObject levelSelectGm;

    public InputField editInfoNameField;
    public InputField editInfoDescriptionField;

    public GameObject levelSelectContentGm;
    public GameObject levelSelectButtonPrefab;
    private List<UButton> levelSelectButtons = new List<UButton>();

    public UButton editInfoButton;
    public UButton loadButton;
    public UButton saveButton;
#endregion

    public Mode editorMode = Mode.Add;
    public ObjectType currentObjectType = ObjectType.Monster;

    private bool isEditingInfo;
    public bool IsEditingInfo
    {
        get { return isEditingInfo; }
        set
        {
            isEditingInfo = value;
            editInfoGm.SetActive(isEditingInfo);
        }
    }

    private bool isSelectingLevel;
    public bool IsSelectingLevel
    {
        get { return isSelectingLevel; }
        set
        {
            isSelectingLevel = value;
            levelSelectGm.SetActive(isSelectingLevel);
            if (isSelectingLevel) CreateLevelSelectButtons();
            else
            {
                levelSelectButtons.ForEach(b => Destroy(b.gameObject));
                levelSelectButtons.Clear();
            }
        }
    }

    private GameStateManager gsm;

    void Awake()
    {
        leftPanelRect = GameObject.Find("LeftPanel").GetComponent<RectTransform>();
        hoverTileCursor.gameObject.SetActive(true);
        selectedTileCursor.gameObject.SetActive(true);

        monsterButton.onClick.AddListener(() => UpdateScrollViewContent(ObjectType.Monster));
        orbButton.onClick.AddListener(() => UpdateScrollViewContent(ObjectType.Orb));
        switchButton.onClick.AddListener(() => UpdateScrollViewContent(ObjectType.Switch));
        tileButton.onClick.AddListener(() => UpdateScrollViewContent(ObjectType.Tile));
        addButton.onClick.AddListener(() => editorMode = Mode.Add);
        removeButton.onClick.AddListener(() => editorMode = Mode.Remove);

        editInfoButton.onClick.AddListener(() => IsEditingInfo = !IsEditingInfo);
        loadButton.onClick.AddListener(() => IsSelectingLevel = !IsSelectingLevel);
        IsEditingInfo = false;
        IsSelectingLevel = false;

        gsm = GameStateManager.Instance;
    }

    void Start()
    {
        editInfoNameField.text = gsm.mapLoader.MapToLoad;
        editInfoDescriptionField.text = "(currently unimplemented)";
    }

    void OnDisable()
    {
        if (hoverTileCursor != null)
            hoverTileCursor.gameObject.SetActive(false);
        if (selectedTileCursor != null)
            selectedTileCursor.gameObject.SetActive(false);
    }

    public void OnSelected()
    {
        if (editorMode == Mode.Remove)
        {
            Monster monster = gsm.CheckMonsterPosition(selectedTileCursor.pos);
            if (monster != null)
            {
                gsm.RemoveMonster(monster);
                Destroy(monster.gameObject);
            }
            Orb orb = gsm.CheckOrbPosition(selectedTileCursor.pos);
            if (orb != null)
            {
                gsm.RemoveOrb(orb);
                Destroy(orb.gameObject);
            }
            Buttons.Button button = gsm.buttons.Find(b => b.pos.GetVector2i() == selectedTileCursor.pos);
            if (button != null)
            {
                gsm.buttons.Remove(button);
                Destroy(button.gameObject);
            }
        }
    }

    void Update()
    {
        if (!IsMouseOverLeftPanel())
        {
            hoverTileCursor.EditorUpdate();
            selectedTileCursor.EditorUpdate();
        }
    }

    private bool IsMouseOverLeftPanel()
    {
        Vector3[] worldCorners = new Vector3[4];
        leftPanelRect.GetWorldCorners(worldCorners);
        Vector2 mousePosition = Input.mousePosition;
        if (mousePosition.x >= worldCorners[0].x && mousePosition.x < worldCorners[2].x
            && mousePosition.y >= worldCorners[0].y && mousePosition.y < worldCorners[2].y)
            return true;
        return false;
    }

    private void UpdateScrollViewContent(ObjectType objectType)
    {
        currentObjectType = objectType;

        foreach (var button in scrollViewButtons)
        {
            Destroy(button.gameObject);
        }
        scrollViewButtons.Clear();

        if (objectType == ObjectType.Monster)
        {
            var dict = PrefabDictionary.Instance.monsterPrefabDictionary.ToDictionary();
            dict.ForEach((name, monster) =>
            {
                UButton button = CreateContentViewButton(() =>
                {
                    if (editorMode == Mode.Add)
                    {
                        Monster existingMonster = gsm.CheckMonsterPosition(selectedTileCursor.pos);
                        if (existingMonster != null)
                        {
                            Destroy(existingMonster.gameObject);
                            gsm.RemoveMonster(existingMonster);
                        }
                        gsm.SpawnMonster(monster,
                            selectedTileCursor.pos.x,
                            selectedTileCursor.pos.y);
                    }
                });
                button.GetComponent<Image>().sprite = monster.GetComponent<SpriteRenderer>().sprite;
            });
        }
        else if (objectType == ObjectType.Orb)
        {
            Orb orbPrefab = PrefabDictionary.Instance.orbPrefab;
            List<TileColor> colors = new List<TileColor>() {TileColor.Black, TileColor.White, TileColor.Red, TileColor.Blue, TileColor.Yellow};

            foreach (var color in colors)
            {
                TileColor currentColor = color;
                UButton button = CreateContentViewButton(() =>
                {
                    if (editorMode == Mode.Add)
                    {
                        Orb existingOrb = gsm.CheckOrbPosition(selectedTileCursor.pos);
                        if (existingOrb != null)
                        {
                            Destroy(existingOrb.gameObject);
                            gsm.RemoveOrb(existingOrb);
                        }
                        gsm.SpawnOrb(orbPrefab, currentColor,
                            selectedTileCursor.pos.x,
                            selectedTileCursor.pos.y);
                    }
                });
                button.GetComponent<Image>().sprite =
                    SpriteDictionary.Instance.orbSpriteDictionary.GetSprite(color);
            }
        }
        else if (objectType == ObjectType.Switch)
        {
            var dict = PrefabDictionary.Instance.buttonPrefabDictionary.ToDictionary();
            dict.ForEach((name, switchPrefab) =>
            {
                UButton button =
                    CreateContentViewButton(() =>
                    {
                        Buttons.Button existingSwitch = gsm.buttons.Find(b => b.pos.GetVector2i() == selectedTileCursor.pos);
                        if (existingSwitch)
                        {
                            Destroy(existingSwitch.gameObject);
                            gsm.buttons.Remove(existingSwitch);
                        }
                        if (editorMode == Mode.Add)
                            gsm.SpawnSwitch(switchPrefab, selectedTileCursor.pos.x,
                                selectedTileCursor.pos.y);
                    });
                button.GetComponent<Image>().sprite = switchPrefab.GetComponent<SpriteRenderer>().sprite;
            });
        }
        else if (objectType == ObjectType.Tile)
        {
            var dict = SpriteDictionary.Instance.tileSpriteDictionary.ToDictionary();
            dict.ForEach((tileData, tileSprite) =>
            {
                UButton button = CreateContentViewButton(() =>
                {
                    if (editorMode == Mode.Add)
                        TileManager.Instance.SetTileData(selectedTileCursor.pos.x, selectedTileCursor.pos.y,
                            tileData);
                });
                button.GetComponent<Image>().sprite = tileSprite;
            });
        }
    }

    private UButton CreateContentViewButton(UnityAction action)
    {
        int x = scrollViewButtons.Count % 3;
        int y = scrollViewButtons.Count/3;
        UButton button = Instantiate(objectSelectButtonPrefab);
        button.onClick.AddListener(action);
        button.transform.SetParent(scrollViewContent.transform);
        button.transform.localPosition = new Vector2(x + 0.6f, -y - 0.6f) * selectButtonWidth;
        scrollViewButtons.Add(button);
        return button;
    }

    private void CreateLevelSelectButtons()
    {
        string[] files = Directory.GetFiles(Application.dataPath + "/Maps", "*.json", SearchOption.TopDirectoryOnly)
            .Select(str => Path.GetFileName(str)).ToArray();
        levelSelectButtons = files.Select(filename =>
        {
            string levelName = filename.Replace(".json", "");
            UButton button = Instantiate(levelSelectButtonPrefab).GetComponent<UButton>();
            button.transform.parent = levelSelectContentGm.transform;
            button.GetComponentInChildren<Text>().text = levelName;
            button.onClick.AddListener(() =>
            {
                gsm.mapLoader.MapToLoad = levelName;
                gsm.ChangeState<GameStateLoad>();
            });
            return button;
        }).ToList();
    }
}
