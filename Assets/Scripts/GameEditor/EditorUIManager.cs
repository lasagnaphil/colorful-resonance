using System;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UnityEngine.Events;
using Utils;
using UButton = UnityEngine.UI.Button;

public enum EditorObjectType
{
    Monster, Orb, Switch
}

public class EditorUIManager : MonoBehaviour
{
    public UButton monsterButton;
    public UButton orbButton;
    public UButton switchButton;

    public UButton createButtonPrefab;

    public GameObject scrollViewContent;
    public List<UButton> scrollViewButtons;

    public Vector2i selectedTilePos;

    void Awake()
    {
        monsterButton.onClick.AddListener(() => UpdateScrollViewContent(EditorObjectType.Monster));
        orbButton.onClick.AddListener(() => UpdateScrollViewContent(EditorObjectType.Orb));
        switchButton.onClick.AddListener(() => UpdateScrollViewContent(EditorObjectType.Switch));
    }

    private void UpdateScrollViewContent(EditorObjectType objectType)
    {
        foreach (var button in scrollViewButtons)
        {
            Destroy(button);
        }
        scrollViewButtons.Clear();

        if (objectType == EditorObjectType.Monster)
        {
            var dict = PrefabDictionary.Instance.monsterPrefabDictionary.ToDictionary();
            dict.ForEach((name, monster) =>
            {
                UButton button = CreateContentViewButton(() =>
                    GameStateManager.Instance.SpawnMonster(monster, selectedTilePos.x, selectedTilePos.y));
            });
        }
        if (objectType == EditorObjectType.Orb)
        {
            Orb orbPrefab = PrefabDictionary.Instance.orbPrefab;

            foreach (TileColor color in EnumHelper.GetValues<TileColor>())
            {
                UButton button = CreateContentViewButton(() =>
                    GameStateManager.Instance.SpawnOrb(orbPrefab, color, selectedTilePos.x, selectedTilePos.y));
            }
        }
        if (objectType == EditorObjectType.Switch)
        {
            var dict = PrefabDictionary.Instance.buttonPrefabDictionary.ToDictionary();
            dict.ForEach((name, switchPrefab) =>
            {
                UButton button =
                    CreateContentViewButton(
                        () => GameStateManager.Instance.SpawnSwitch(switchPrefab, selectedTilePos.x, selectedTilePos.y));
            });
        }
    }

    private UButton CreateContentViewButton(UnityAction action)
    {
        UButton button = Instantiate(createButtonPrefab);
        button.onClick.AddListener(action);
        button.transform.parent = scrollViewContent.transform;
        scrollViewButtons.Add(button);
        return button;
    }
}
