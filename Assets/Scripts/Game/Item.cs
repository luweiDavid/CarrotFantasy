using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GridPoint mGridPoint;
    public int mItemId;

    public void Init(GridPoint grid, int id) {
        mGridPoint = grid;
        mItemId = id;
    }
}
