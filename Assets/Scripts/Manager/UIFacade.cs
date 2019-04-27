using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFacade
{
    public GameManager mGameMgr;
    public UIManager mUIMgr;
    public PlayerManager mPlayerMgr;
    public AudioManager mAudioMgr;

    public RectTransform mUIRoot;
    public GameObject mMaskGo;
    public Image mMaskImg;
    private float mSpeed = 0.3f;

    public Dictionary<string, BaseUIPanel> uiPanelClassDic = new Dictionary<string, BaseUIPanel>();
    //public Dictionary<string, BaseItem>

    public BaseSceneState mCurScene;
    public BaseSceneState mPreScene;

    public UIFacade(UIManager uiMgr) {
        mGameMgr = GameManager.Instance;
        mUIMgr = uiMgr;

        mUIRoot = GameObject.Find("UIRoot").GetComponent<RectTransform>();

        mMaskGo = InstantiateUIPrefab(FactoryType.UI, "Img_Mask");
        mMaskImg = mMaskGo.GetComponent<Image>();

        ChangeSceneState(new StartLoadSceneState(this));
    }

    public void ChangeSceneState(BaseSceneState sceneState) {
        mPreScene = mCurScene;
        if (mPreScene != null) {
            mPreScene.ExitScene();
        }
        
        mCurScene = sceneState;
        if (mCurScene != null) {
            ShowMask(); 
        }
    }

    public void ShowMask()
    {
        Tweener ter = DOTween.To(() => mMaskImg.color, x => mMaskImg.color = x, new Color(0.5f, 0.5f, 0.5f, 1f), mSpeed);
        ter.OnComplete(() => {
            HideMask();
            if (mCurScene.mSceneName != NameConfig.SceneName_StartLoad) {
                SceneManager.LoadScene(mCurScene.mSceneName);
            }
        });
    }
    public void HideMask() {
        DOTween.To(() => mMaskImg.color, x => mMaskImg.color = x, new Color(0.5f, 0.5f, 0.5f, 0), mSpeed).
            OnComplete(()=>
            {
                mCurScene.EnterScene();
            });
    }

    public void OpenPanel(string name) { 
        BaseUIPanel panel = null;
        if (uiPanelClassDic.TryGetValue(name, out panel) && panel != null)
        {
            panel.mPanelGo.gameObject.SetActive(true);
            panel.__Enter();
        }
        else {
            Debug.LogError("没有此面板: " + name);
        }
    }

    public void ClosePanel(string name) {
        BaseUIPanel panel = null;
        if (uiPanelClassDic.TryGetValue(name, out panel) && panel != null)
        {
            panel.mPanelGo.gameObject.SetActive(false);
            panel.__Close();
        }
    }

    public void InitUIPanelClassDic() {

        for (int i = 0; i < mUIMgr.panelGoDic.Count; i++)
        {
            //Debug.Log(mUIMgr.panelGoDic["GameLoadPanel"]);
            //Debug.Log(i);
        }

        foreach (string key in mUIMgr.panelGoDic.Keys)
        { 
            GameObject go = mUIMgr.panelGoDic[key];
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.transform.SetParent(mUIRoot);
            BaseUIPanel uiPanel = go.GetComponent<BaseUIPanel>(); 
            if (uiPanel)
            {
                uiPanel.__Init();
                uiPanel.__Close();
                uiPanelClassDic.Add(key, uiPanel);
            }
            else
            {
                Debug.LogError("初始化UIPanelClassDic失败");
            }
        }
    }

    /// <summary>
    /// 添加uipanel的实例化物体到mUIMgr的dic中
    /// </summary>
    public void AddUIPanelGo(string path) {
        GameObject go = InstantiateUIPrefab(FactoryType.UIPanel, path); 
        mUIMgr.AddPanel(path, go);
    }

    public void ClearUIPanelGo() {
        mUIMgr.ClearPanelGoDic();
        mUIMgr.panelGoDic.Clear();
    }

    /// <summary>
    /// 实例化prefab
    /// </summary> 
    public GameObject InstantiateUIPrefab(FactoryType type, string path) {
        GameObject inst = null;
        GameObject notInst = mGameMgr.GetGoRes(type, path);
        if (notInst != null) {
            inst = GameObject.Instantiate(notInst);
            inst.transform.SetParent(mUIRoot);
            inst.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            inst.transform.GetComponent<RectTransform>().localScale = Vector3.one;
        }
        return inst;
    }

    public GameObject GetGoRes(FactoryType type, string name)
    {
        return mGameMgr.GetGoRes(type, name);
    }

    public void PushItem(FactoryType type, string name, GameObject item)
    {
        mGameMgr.PushItem(type, name, item);
    }

    public Sprite GetSprite(string spritePath)
    {
        return mGameMgr.GetSprite(spritePath);
    }

    public AudioClip GetAudioClip(string audioPath)
    {
        return mGameMgr.GetAudioClip(audioPath);
    }

    public RuntimeAnimatorController GetRuntimeAnimCtrl(string animPath)
    {
        return mGameMgr.GetRuntimeAnimCtrl(animPath);
    }

    public void SetBgAudio(bool isOpen)
    {
        mGameMgr.audioMgr.SetBgAudio(isOpen);
    }

    public void SetEffectAudio(bool isOpen)
    {
        mGameMgr.audioMgr.SetEffectAudio(isOpen);
    }


}
