using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFactory : BaseResFactory<Sprite>
{
    public Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();

    public override Sprite GetRes(string name)
    {
        Sprite sprite = null;
        LoadPath += name;
        if (spriteDic.ContainsKey(name))
        {
            sprite = spriteDic[name];
        }
        else {
            sprite = Resources.Load<Sprite>(LoadPath);
            if (sprite != null) {
                spriteDic.Add(name, sprite);
            }
        }
        if (sprite == null) {
            Debug.LogError("资源加载失败，路径：" + LoadPath);
        }
        return sprite;
    }
}
