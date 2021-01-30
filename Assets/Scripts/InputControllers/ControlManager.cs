class ControlManager
{
    private static readonly ControlManager instance = new ControlManager();
    private ControlManager() { }
    static ControlManager() { }
    public static ControlManager Instance { get { return instance; } }

    public IControlScheme controlScheme
    {
        get
        {
            if (DataScript.Instance.CurrentControlType == ControlType.touch)
                return TouchControls.Instance;
            else
                return KeyboardControls.Instance;
        }
    }
}