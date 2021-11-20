using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    #region Singleton
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = new UIManager();
            return instance;
        }
    }

    private static UIManager instance;

    private UIManager() { }
    #endregion

    List<RawImage> lives = new List<RawImage>();
    List<RawImage> ammos = new List<RawImage>();

    int UIAmmo;
    int UILife;
    Canvas canvas;
    GameObject UIAmmoPanel;
    GameObject UILifePanel;
    RawImage UIAmmoResource;
    RawImage UILifeResource;

    public void Init()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
        UIAmmoPanel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UIAmmoPanel"), canvas.transform);
        UILifePanel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UILifePanel"), canvas.transform);
        UIAmmoResource = Resources.Load<GameObject>("Prefabs/UIAmmo").GetComponent<RawImage>();
        UILifeResource = Resources.Load<GameObject>("Prefabs/UILife").GetComponent<RawImage>();
        InitAmmoUI();
        InitLifeUI();
    }

    public void Refresh()
    {
        if (UIAmmo != PlayerManager.Instance.Stats.ammo)
        {
            UIAmmo = PlayerManager.Instance.Stats.ammo;
            UpdateUI(UIAmmo, ammos);
        }
        
        if (UILife != PlayerManager.Instance.Stats.hp)
        {
            UIAmmo = PlayerManager.Instance.Stats.hp;
            UpdateUI(UILife, lives);
        }
    }

    private void InitAmmoUI()
    {
        UIAmmo = PlayerManager.MaxAmmo;
        for (int i = 0; i < UIAmmo; i++)
            ammos.Add(GameObject.Instantiate(UIAmmoResource, UIAmmoPanel.transform));
    }

    private void InitLifeUI()
    {
        UILife = PlayerManager.MaxLife;
        for (int i = 0; i < UILife; i++)
            lives.Add(GameObject.Instantiate(UILifeResource, UILifePanel.transform));
    }

    private void UpdateUI(int value, List<RawImage> list)
    {
        foreach (var item in list)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < value; i++)
        {
            list[i].gameObject.SetActive(true);
        }
    }
}
