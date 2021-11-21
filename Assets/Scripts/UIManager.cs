using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    Button reloadButtonResource;
    RectTransform panelResource;

    int UIAmmo;
    int UILife;

    Canvas canvas;
    RectTransform ammoPanel;
    RectTransform emptyAmmoPanel;
    RectTransform lifePanel;
    RectTransform emptyLifePanel;

    Button reloadButton;
    Button deathScreen;

    Vector2 ammoMinAnchor = new Vector2(0.75f, 0.05f);
    Vector2 ammoMaxAnchor = new Vector2(1, 0.15f);
    Vector2 ammoPivot = new Vector2(1, 0);

    Vector2 lifeMinAnchor = new Vector2(0, 0.05f);
    Vector2 lifeMaxAnchor = new Vector2(0.25f, 0.15f);
    Vector2 lifePivot = Vector2.zero;

    Dictionary<int, Vector2> reloadPositions = new Dictionary<int, Vector2>()
    { 
        { 0, new Vector2(0.1f, 0.75f)},
        { 1, new Vector2(0.1f, 0.25f)},
        { 2, new Vector2(0.9f, 0.75f)},
        { 3, new Vector2(0.9f, 0.25f)}
    };

    public void Init()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
        ammoResource = Resources.Load<GameObject>("Prefabs/UIAmmo").GetComponent<RawImage>();
        emptyAmmoResource = Resources.Load<GameObject>("Prefabs/UIEmptyAmmo").GetComponent<RawImage>();
        lifeResource = Resources.Load<GameObject>("Prefabs/UILife").GetComponent<RawImage>();
        emptyLifeResource = Resources.Load<GameObject>("Prefabs/UIEmptyLife").GetComponent<RawImage>();
        panelResource = Resources.Load<GameObject>("Prefabs/UIPanel").GetComponent<RectTransform>();
        reloadButtonResource = Resources.Load<GameObject>("Prefabs/UIReloadButton").GetComponent<Button>();

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

        if (PlayerManager.Instance.Stats.ammo < PlayerManager.MaxAmmo && reloadButton == null)
        {
            reloadButton = GameObject.Instantiate(reloadButtonResource, canvas.transform);
            reloadButton.onClick.AddListener(ReloadAmmoClick);

            int randPosKey = Random.Range(0, reloadPositions.Count);
            RectTransform rectTrans = reloadButton.GetComponent<RectTransform>();
            if (reloadPositions.TryGetValue(randPosKey, out Vector2 vect2))
            {
                rectTrans.anchorMin = vect2;
                rectTrans.anchorMax = vect2;
            }
        }
    }

    private void InitAmmoUI()
    {
        InstantiatePanel(ref ammoPanel, ammoMinAnchor, ammoMaxAnchor, ammoPivot);
        InstantiatePanel(ref emptyAmmoPanel, ammoMinAnchor, ammoMaxAnchor, ammoPivot);

        UIAmmo = PlayerManager.MaxAmmo;
        for (int i = 0; i < UIAmmo; i++)
        {
            UIAmmos.Add(GameObject.Instantiate(ammoResource, ammoPanel.transform));
            GameObject.Instantiate(emptyAmmoResource, emptyAmmoPanel.transform);
        }
    }

    private void InitLifeUI()
    {
        InstantiatePanel(ref lifePanel, lifeMinAnchor, lifeMaxAnchor, lifePivot);
        InstantiatePanel(ref emptyLifePanel, lifeMinAnchor, lifeMaxAnchor, lifePivot);

        UILife = PlayerManager.MaxLife;
        for (int i = 0; i < UILife; i++)
        {
            UILives.Add(GameObject.Instantiate(lifeResource, lifePanel.transform));
            GameObject.Instantiate(emptyLifeResource, emptyLifePanel.transform);
        }
    }

    private void InstantiatePanel(ref RectTransform panel, Vector2 minAnchor, Vector2 maxAnchor, Vector2 pivot)
    {
        panel = GameObject.Instantiate(panelResource, canvas.transform);
        panel.anchorMin = minAnchor;
        panel.anchorMax = maxAnchor;
        panel.pivot = pivot;
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

    private void ReloadAmmoClick()
    {
        GameObject.Destroy(reloadButton.gameObject);
        reloadButton = null;
        PlayerManager.Instance.AddAmmo();
    }

    public void ActivateDeathScreen()
    {
        deathScreen = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UIDeathScreen"), canvas.transform).GetComponentInChildren<Button>();
        deathScreen.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        EnemyManager.Instance.Reset();
        UIAmmos.Clear();
        UILives.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
