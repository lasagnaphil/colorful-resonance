﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FullSerializer;
using UnityEngine;

public class MapDataConverter : fsDirectConverter<MapData>
{
    private fsResult CheckField(Dictionary<string, fsData> data, string key, fsDataType type, out fsData subItem)
    {
        fsResult result = CheckKey(data, key, out subItem);
        result += CheckType(subItem, type);
        return result;
    }

    public override object CreateInstance(fsData data, Type storageType)
    {
        return new MapData();
    }

    protected override fsResult DoSerialize(MapData mapData, Dictionary<string, fsData> serialized)
    {
        SerializeMember(serialized, null, "name", mapData.name);

        if (mapData.winCondition is EliminationWinCondition)
            serialized["winCondition"] = new fsData("Elimination");
        else if (mapData.winCondition is SurvivalWinCondition)
        {
            serialized["winCondition"] = new fsData("Survival");
            SerializeMember(serialized, null, "numOfTurnsToWin",
                (mapData.winCondition as SurvivalWinCondition).numOfTurns);
        }

        SerializeMember(serialized, null, "width", mapData.width);
        SerializeMember(serialized, null, "height", mapData.height);
        //SerializeMember(serialized, null, "tiles", mapData.tiles);
        string[] serializedTiles = new string[mapData.height];
        StringBuilder strBuilder = new StringBuilder();
        for(int i = 0; i <= mapData.width * mapData.height; i++)
        {
            if (i%mapData.width == 0 && i != 0)
            {
                serializedTiles[i/mapData.width - 1] = strBuilder.ToString();
                if (i == mapData.width*mapData.height) break;
                strBuilder = new StringBuilder();
            }
            strBuilder.Append(mapData.tiles[i]);
            if (i%mapData.width != mapData.width - 1) strBuilder.Append(" ");
        }
        SerializeMember(serialized, null, "tiles", serializedTiles);

        SerializeMember(serialized, null, "playerData", mapData.playerData);
        SerializeMember(serialized, null, "monsters", mapData.monsters);
        SerializeMember(serialized, null, "orbs", mapData.orbs);

        return fsResult.Success;
    }

    protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref MapData mapData)
    {
        var result = fsResult.Success;

        fsData winConditionData;

        if (CheckKey(data, "winCondition", out winConditionData).Succeeded)
        {
            if ((result += CheckType(winConditionData, fsDataType.String)).Failed) return result;
            string winConditionStr = winConditionData.AsString;
            switch (winConditionStr)
            {
                case "Elimination": mapData.winCondition = new EliminationWinCondition();
                    break;
                case "Survival":
                    fsData turnsData;
                    if ((result += CheckField(data, "numOfTurnsToWin", fsDataType.Int64, out turnsData)).Failed) return result;
                    mapData.winCondition = new SurvivalWinCondition((int) turnsData.AsInt64);
                    break;
            }
        }
        else
        {
            // default map mode is elimination
            mapData.winCondition = new EliminationWinCondition();
        }
        
        if ((result += DeserializeMember(data, null, "name", out mapData.name)).Failed ||
           (result += DeserializeMember(data, null, "width", out mapData.width)).Failed ||
           (result += DeserializeMember(data, null, "height", out mapData.height)).Failed ||
           (result += DeserializeMember(data, null, "playerData", out mapData.playerData)).Failed ||
           (result += DeserializeMember(data, null, "monsters", out mapData.monsters)).Failed ||
           (result += DeserializeMember(data, null, "orbs", out mapData.orbs)).Failed)
        {
            return result;
        }
            
         // deserialization of tiles : convert a list of string into a list of chars
        fsData tilesData;
        if ((result += CheckField(data, "tiles", fsDataType.Array, out tilesData)).Failed) return result;
        List<fsData> tilesDataList = tilesData.AsList;
        StringBuilder tilesDataStr = new StringBuilder();
        foreach(var tilesDataLine in tilesDataList)
        {
            if ((result += CheckType(tilesDataLine, fsDataType.String)).Failed) return result;
            tilesDataStr.Append(tilesDataLine.AsString);
        }
        // parse the tiles (represented in a string)
        char[] tileChars = tilesDataStr.ToString().ToCharArray().Where(c => c != ' ').ToArray();
        if (tileChars.Length != mapData.width*mapData.height)
        {
            Debug.LogError("Error parsing tile string: the number of tiles does not match");
        }
        mapData.tiles = tileChars;

        return result;
    }
}