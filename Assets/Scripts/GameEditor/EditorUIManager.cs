using System;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using GameEditor;
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
        Monster, Orb, Switch
    }

    public RectTransform leftPanelRect;

    public UButton monsterButton;
    public UButton orbButton;
    public UButton switchButton;
    public UButton addButton;
    public UButton removeButton;

    public UButton objectSelectButtonPrefab;
    public float selectButtonWidth;

    public GameObject scrollViewContent;
    public List<UButton> scrollViewButtons;

    public HoverTileCursor hoverTileCursor;
    public SelectedTileCursor selectedTileCursor;

    public Mode editorMode = Mode.Add;
    public ObjectType currentObjectType = ObjectType.Monster;

    void Awake()
    {
        leftPanelRect = GameObject.Find("LeftPanel").GetComponent<RectTransform>();
        hoverTileCursor.gameObject.SetActive(true);
        selectedTileCursor.gameObject.SetActive(true);

        monsterButton.onClick.AddListener(() => UpdateScrollViewContent(ObjectType.Monster));
        orbButton.onClick.AddListener(() => UpdateScrollViewContent(ObjectType.Orb));
        switchButton.onClick.AddListener(() => UpdateScrollViewContent(ObjectType.Switch));
        addButton.onClick.AddListener(() => editorMode = Mode.Add);
        removeButton.onClick.AddListener(() => editorMode = Mode.Remove);
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
            Monster monster = GameStateManager.Instance.CheckMonsterPosition(selectedTileCursor.pos);
            if (monster != null)
            {
                DestroyImmediate(monster);
                GameStateManager.Instance.RemoveMonster(monster);
            }
            Orb orb = GameStateManager.Instance.CheckOrbPosition(selectedTileCursor.pos);
            if (orb != null)
            {
                DestroyImmediate(orb);
                GameStateManager.Instance.RemoveOrb(orb);
            }
            Buttons.Button button = GameStateManager.Instance.buttons.Find(b => b.pos.GetVector2i() == selectedTileCursor.pos);
            if (button != null)
            {
                DestroyImmediate(button);
                GameStateManager.Instance.buttons.Remove(button);
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
                        GameStateManager.Instance.SpawnMonster(monster,
                            selectedTileCursor.pos.x,
                            selectedTileCursor.pos.y);
                });
                button.GetComponentInChildren<Image>().sprite =
                    PrefabDictionary.Instance.monsterPrefabDictionary.GetSprite(name);
            });
        }
        else if (objectType == ObjectType.Orb)
        {
            Orb orbPrefab = PrefabDictionary.Instance.orbPrefab;
            List<TileColor> colors = new List<TileColor>() {TileColor.Black, TileColor.White, TileColor.Red, TileColor.Blue, TileColor.Yellow};

            foreach (var color in colors)
            {
                UButton button = CreateContentViewButton(() =>
                {
                    if (editorMode == Mode.Add)
                        GameStateManager.Instance.SpawnOrb(orbPrefab, color,
                            selectedTileCursor.pos.x,
                            selectedTileCursor.pos.y);
                });
                button.GetComponentInChildren<Image>().sprite =
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
                        if (editorMode == Mode.Add)
                            GameStateManager.Instance.SpawnSwitch(switchPrefab, selectedTileCursor.pos.x,
                                selectedTileCursor.pos.y);
                    });
                button.GetComponentInChildren<Image>().sprite =
                    PrefabDictionary.Instance.buttonPrefabDictionary.GetSprite(name);
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
}
