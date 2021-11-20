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

    List<RawImage> UILives = new List<RawImage>();
    List<RawImage> UIAmmos = new List<RawImage>();

    RawImage ammoResource;
    RawImage emptyAmmoResource;
    RawImage lifeResource;
    RawImage emptyLifeResource;
    RectTransform panelResource;

    int UIAmmo;
    int UILife;

    Canvas canvas;
    RectTransform ammoPanel;
    RectTransform emptyAmmoPanel;
    RectTransform lifePanel;
    RectTransform emptyLifePanel;

    Vector2 ammoMinAnchor = new Vector2(0.75f, 0.05f);
    Vector2 ammoMaxAnchor = new Vector2(1, 0.15f);
    Vector2 lifeMinAnchor = new Vector2(0, 0.05f);
    Vector2 lifeMaxAnchor = new Vector2(0.25f, 0.15f);

    public void Init()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
        ammoResource = Resources.Load<GameObject>("Prefabs/UIAmmo").GetComponent<RawImage>();
        emptyAmmoResource = Resources.Load<GameObject>("Prefabs/UIEmptyAmmo").GetComponent<RawImage>();
        lifeResource = Resources.Load<GameObject>("Prefabs/UILife").GetComponent<RawImage>();
        emptyLifeResource = Resources.Load<GameObject>("Prefabs/UIEmptyLife").GetComponent<RawImage>();
        panelResource = Resources.Load<GameObject>("Prefabs/UIPanel").GetComponent<RectTransform>();

        InitAmmoUI();
        InitLifeUI();
    }

    public void Refresh()
    {
        if (UIAmmo != PlayerManager.Instance.Stats.ammo)
        {
            UIAmmo = PlayerManager.Instance.Stats.ammo;
            UpdateUI(UIAmmo, UIAmmos);
        }
        
        if (UILife != PlayerManager.Instance.Stats.hp)
        {
            UILife = PlayerManager.Instance.Stats.hp;
            UpdateUI(UILife, UILives);
        }
    }

    private void InitAmmoUI()
    {
        InstantiatePanel(ref ammoPanel, ammoMinAnchor, ammoMaxAnchor);
        InstantiatePanel(ref emptyAmmoPanel, ammoMinAnchor, ammoMaxAnchor);

        UIAmmo = PlayerManager.MaxAmmo;
        for (int i = 0; i < UIAmmo; i++)
        {
            UIAmmos.Add(GameObject.Instantiate(ammoResource, ammoPanel.transform));
            GameObject.Instantiate(emptyAmmoResource, emptyAmmoPanel.transform);
        }
    }

    private void InitLifeUI()
    {
        InstantiatePanel(ref lifePanel, lifeMinAnchor, lifeMaxAnchor);
        InstantiatePanel(ref emptyLifePanel, lifeMinAnchor, lifeMaxAnchor);

        UILife = PlayerManager.MaxLife;
        for (int i = 0; i < UILife; i++)
        {
            UILives.Add(GameObject.Instantiate(lifeResource, lifePanel.transform));
            GameObject.Instantiate(emptyLifeResource, emptyLifePanel.transform);
        }
    }

    private void InstantiatePanel(ref RectTransform panel, Vector2 minAnchor, Vector2 maxAnchor)
    {
        panel = GameObject.Instantiate(panelResource, canvas.transform);
        panel.anchorMin = minAnchor;
        panel.anchorMax = maxAnchor;
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
