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

    public BaseSceneState mCurScene;
    public BaseSceneState mPreScene;

    public UIFacade(UIManager uiMgr) {
        mGameMgr = GameManager.Instance;
        mUIMgr = uiMgr;

        mUIRoot = GameObject.Find("UIRoot").GetComponent<RectTransform>();

        mMaskGo = GetItem(FactoryType.UI, "Img_Mask");
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
            if (mCurScene.mSceneName != NameConfig.SceneName_StartLoad) {
                try
                { 
                    SceneManager.LoadScene(mCurScene.mSceneName);
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                } 
            }
            HideMask();
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
        //Debug.Log(name);      
        BaseUIPanel panel = null;
        if (uiPanelClassDic.TryGetValue(name, out panel) && panel != null)
        { 
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
                uiPanel.mPanelGo.SetActive(false);
                if (!uiPanelClassDic.ContainsKey(key)) {
                    uiPanelClassDic.Add(key, uiPanel);
                } 
            }
            else
            {
                Debug.LogError("初始化UIPanelClassDic失败");
            }
        }
    }

    public BaseUIPanel GetUIPanelClass(string name) {
        BaseUIPanel uiPanel = null;
        if (uiPanelClassDic.TryGetValue(name, out uiPanel) && uiPanel != null) {
            return uiPanel;
        }
        return null;
    }

    /// <summary>
    /// 添加uipanel的实例化物体到mUIMgr的dic中
    /// </summary>
    public void AddUIPanelGo(string name) {
        GameObject go = GetItem(FactoryType.UIPanel, name); 
        mUIMgr.AddPanel(name, go); 
    }

    public void ClearUIPanelGo() {
        mUIMgr.ClearPanelGoDic();
        mUIMgr.panelGoDic.Clear();
    }
     
    public GameObject GetItem(FactoryType type, string name, Transform parent = null) { 
        GameObject notInst = mGameMgr.GetItem(type, name);
        if (notInst != null)
        {
            if (parent != null)
            {
                notInst.transform.SetParent(parent);
            }
            else {
                notInst.transform.SetParent(mUIRoot);
            }
            
            notInst.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            notInst.transform.GetComponent<RectTransform>().localScale = Vector3.one;
        }
        else {
            Debug.LogError("无法生成物体: " + name);
        } 
        return notInst;
    }

    public GameObject GetGameItem(FactoryType type, string name)
    {
        return mGameMgr.GetItem(type, name);
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
