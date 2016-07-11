﻿using System;
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

    public UButton objectSelectButtonPrefab;
    public float selectButtonWidth;

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
            Destroy(button.gameObject);
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
        else if (objectType == EditorObjectType.Orb)
        {
            Orb orbPrefab = PrefabDictionary.Instance.orbPrefab;
            List<TileColor> colors = new List<TileColor>() {TileColor.Black, TileColor.White, TileColor.Red, TileColor.Blue, TileColor.Yellow};

            foreach (var color in colors)
            {
                UButton button = CreateContentViewButton(() =>
                    GameStateManager.Instance.SpawnOrb(orbPrefab, color, selectedTilePos.x, selectedTilePos.y));
            }
        }
        else if (objectType == EditorObjectType.Switch)
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
