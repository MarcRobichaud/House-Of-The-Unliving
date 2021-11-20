using UnityEngine;

public class InputManager
{
    #region Singleton
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InputManager();
            return instance;
        }
    }

    private static InputManager instance;

    private InputManager() { }
    #endregion

    public InputInfo InputInfos { get; private set; }

    public void Init()
    {
        InputInfos = new InputInfo(false, new Vector3());
    }

    public void Refresh()
    {
        InputInfos = new InputInfo(Input.GetMouseButtonDown(0), Input.mousePosition);
    }
}

public struct InputInfo
{
    public bool shoot;
    public Vector3 mouseLocation;

    public InputInfo(bool _shoot, Vector3 _mouseLocation)
    {
        shoot = _shoot;
        mouseLocation = _mouseLocation;
    }
}
