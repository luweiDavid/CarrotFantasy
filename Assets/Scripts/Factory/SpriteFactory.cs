using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFactory : BaseResFactory<Sprite>
{
    public Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();

    public SpriteFactory() {
        LoadPath = "Pictures/";
    }

    public override Sprite GetRes(string name)
    {
        Sprite sprite = null;
        string path = LoadPath + name;
        if (spriteDic.ContainsKey(name))
        {
            sprite = spriteDic[name];
        }
        else {
            sprite = Resources.Load<Sprite>(path);
            if (sprite != null) {
                spriteDic.Add(name, sprite);
            }
        }
        if (sprite == null) {
            Debug.LogError("资源加载失败，路径：" + path);
        }
        return sprite;
    }
}
