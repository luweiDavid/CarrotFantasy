using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseFactory {

    GameObject GetItem(string itemName);

    void PushItem(string itemName, GameObject item);

}