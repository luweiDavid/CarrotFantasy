using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
#if Tool
[CustomEditor(typeof(MapMaker))]
public class MapEditor : Editor
{
    private MapMaker mMapMaker;

    private List<FileInfo> fileList = new List<FileInfo>();
    private string[] fileNameArray;

    private int curSelectIndex;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (!Application.isPlaying)
            return;

        mMapMaker = MapMaker.Instance; 
        
        fileNameArray = GetFileName();

        EditorGUILayout.BeginHorizontal();
        int index = EditorGUILayout.Popup(curSelectIndex, fileNameArray);
        if (index != curSelectIndex) {
            curSelectIndex = index;

            mMapMaker.InitMap();
            LevelInfo _levelInfo = mMapMaker.LoadJson(fileNameArray[curSelectIndex]);
            mMapMaker.ReadLevelInfo(_levelInfo);
        }
        
        if (GUILayout.Button("加载关卡文件"))
        {
            LoadLevelFile();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("设置地图初始状态")) {
            mMapMaker.InitMap();
        }

        if (GUILayout.Button("清除怪物路径点")) {
            mMapMaker.ClearMonsterPath();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("保存关卡地图数据"))
        {
            mMapMaker.SaveToJson();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void LoadLevelFile() {
        fileList.Clear();
        curSelectIndex = -1;

        string[] files = Directory.GetFiles(Application.dataPath + "/Resources/Json/Level/", "*.json");
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo info = new FileInfo(files[i]);
            fileList.Add(info);
        } 
    }

    private string[] GetFileName() {
        List<string> _list = new List<string>();
        foreach (FileInfo info in fileList)
        {
            _list.Add(info.Name);
        }
        return _list.ToArray();
    }

}
#endif