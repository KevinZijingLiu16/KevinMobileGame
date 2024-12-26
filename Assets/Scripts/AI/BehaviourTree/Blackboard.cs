using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    Dictionary<string, object> blackboardData = new Dictionary<string, object>();

    public delegate void OnBlackboardValueChanged(string key, object value);

    public event OnBlackboardValueChanged onBlackboardValueChanged;

    public void SetOrAddData(string key, object value)
    {
        if (blackboardData.ContainsKey(key))
        {
           blackboardData[key] = value;
        }
        else
        {
            blackboardData.Add(key, value);
        }

        onBlackboardValueChanged?.Invoke(key, value);

    }

    public bool GetBlackBoardData<T>(string key, out T value)
    {
       value = default(T);

        if (blackboardData.ContainsKey(key))
        {
            value = (T)blackboardData[key];
            return true;
            Debug.Log("Blackboard data found");
        }

        return false;
    }

    public void RemoveBlackboardData(string key)
    {
        blackboardData.Remove(key);
        onBlackboardValueChanged?.Invoke(key, null);
    }

    public bool HasKey(string key)
    {
        return blackboardData.ContainsKey(key);
    }
}
