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

public enum EditorObjectType
{
    Monster, Orb, Switch
}

public class EditorUIManager : MonoBehaviour
{
    public RectTransform leftPanelRect;

    public UButton monsterButton;
    public UButton orbButton;
    public UButton switchButton;

    public UButton objectSelectButtonPrefab;
    public float selectButtonWidth;

    public GameObject scrollViewContent;
    public List<UButton> scrollViewButtons;

    public HoverTileCursor hoverTileCursor;
    public SelectedTileCursor selectedTileCursor;

    void Awake()
    {
        leftPanelRect = GameObject.Find("LeftPanel").GetComponent<RectTransform>();
        hoverTileCursor.gameObject.SetActive(true);
        selectedTileCursor.gameObject.SetActive(true);

        monsterButton.onClick.AddListener(() => UpdateScrollViewContent(EditorObjectType.Monster));
        orbButton.onClick.AddListener(() => UpdateScrollViewContent(EditorObjectType.Orb));
        switchButton.onClick.AddListener(() => UpdateScrollViewContent(EditorObjectType.Switch));
    }

    void OnDisable()
    {
        if (hoverTileCursor != null)
            hoverTileCursor.gameObject.SetActive(false);
        if (selectedTileCursor != null)
            selectedTileCursor.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!IsMouseOverleftPanel())
        {
            hoverTileCursor.EditorUpdate();
            selectedTileCursor.EditorUpdate();
        }
    }

    private bool IsMouseOverleftPanel()
    {
        Vector3[] worldCorners = new Vector3[4];
        leftPanelRect.GetWorldCorners(worldCorners);
        Vector2 mousePosition = Input.mousePosition;
        if (mousePosition.x >= worldCorners[0].x && mousePosition.x < worldCorners[2].x
            && mousePosition.y >= worldCorners[0].y && mousePosition.y < worldCorners[2].y)
            return true;
        return false;
    }

    private void UpdateScrollViewContent(EditorObjectType objectType)
    {
        foreach (var button in scrollViewButtons)
        {
            Destroy(button.gameObject);
        }
        scrollViewButtons.Clear();

        if (objectType == EditorObjectType.Monster)
        {
            var dict = PrefabDictionary.Instance.monsterPrefabDictionary.ToDictionary();
            dict.ForEach((name, monster) =>
            {
                UButton button = CreateContentViewButton(() =>
                    GameStateManager.Instance.SpawnMonster(monster, selectedTileCursor.pos.x, selectedTileCursor.pos.y));
                Monster tempMonster = Instantiate(monster);
                button.GetComponentInChildren<Image>().sprite = tempMonster.GetComponent<Sprite>();
                DestroyImmediate(tempMonster);
            });
        }
        else if (objectType == EditorObjectType.Orb)
        {
            Orb orbPrefab = PrefabDictionary.Instance.orbPrefab;
            List<TileColor> colors = new List<TileColor>() {TileColor.Black, TileColor.White, TileColor.Red, TileColor.Blue, TileColor.Yellow};

            foreach (var color in colors)
            {
                UButton button = CreateContentViewButton(() =>
                    GameStateManager.Instance.SpawnOrb(orbPrefab, color, selectedTileCursor.pos.x, selectedTileCursor.pos.y));
                button.GetComponentInChildren<Image>().sprite = orbPrefab.GetComponent<Sprite>();
                Orb tempOrb = Instantiate(orbPrefab);
                button.GetComponentInChildren<Image>().sprite = tempOrb.GetComponent<Sprite>();
                DestroyImmediate(tempOrb);
            }
        }
        else if (objectType == EditorObjectType.Switch)
        {
            var dict = PrefabDictionary.Instance.buttonPrefabDictionary.ToDictionary();
            dict.ForEach((name, switchPrefab) =>
            {
                UButton button =
                    CreateContentViewButton(
                        () => GameStateManager.Instance.SpawnSwitch(switchPrefab, selectedTileCursor.pos.x, selectedTileCursor.pos.y));
                button.GetComponentInChildren<Image>().sprite = switchPrefab.GetComponent<Sprite>();
                Buttons.Button tempSwitch = Instantiate(switchPrefab);
                button.GetComponentInChildren<Image>().sprite = tempSwitch.GetComponent<Sprite>();
                DestroyImmediate(tempSwitch);
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
