using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/statusSO", order = 1)]
public class SO : ScriptableObject
{
    public string status_initial;
    [NonSerialized]
    public string status;
    public void OnAfterDeserialize()
    {
        status= status_initial;
    }
    public void OnBeforeDeserialize()
    {}
}
