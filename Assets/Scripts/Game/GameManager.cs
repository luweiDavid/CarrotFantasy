using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform GameObjectPoolTr;

    public PlayerManager playerMgr;
    public FactoryManager factoryMgr;
    public AudioManager audioMgr;
    public UIManager uiMgr; 

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameObjectPoolTr = transform.Find("GameObjectPool").GetComponent<Transform>();

        playerMgr = new PlayerManager();
        factoryMgr = new FactoryManager();
        audioMgr = new AudioManager();
        uiMgr = new UIManager();
    }

    private void Start()
    { 
    }

    private void Update()
    {

    }
}
