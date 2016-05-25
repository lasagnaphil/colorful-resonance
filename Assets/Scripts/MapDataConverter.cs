using System;
using System.Collections.Generic;
using FullSerializer;

public class MapDataConverter : fsDirectConverter<MapData>
{
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
        SerializeMember(serialized, null, "tiles", mapData.tiles);
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
                    if ((result += CheckKey(data, "numOfTurnsToWin", out turnsData)).Failed) return result;
                    if ((result += CheckType(turnsData, fsDataType.Int64)).Failed) return result;
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
           (result += DeserializeMember(data, null, "tiles", out mapData.tiles)).Failed ||
           (result += DeserializeMember(data, null, "playerData", out mapData.playerData)).Failed ||
           (result += DeserializeMember(data, null, "monsters", out mapData.monsters)).Failed ||
           (result += DeserializeMember(data, null, "orbs", out mapData.orbs)).Failed)
        {
            return result;
        }
            
        
        return result;
    }
}