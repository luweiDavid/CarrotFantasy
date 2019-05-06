using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilder<T>
{
    GameObject GetProduct();

    //设置数据
    void SetData(T t);

    //获取特有属性
    void GetPeculiar(T t);
}
