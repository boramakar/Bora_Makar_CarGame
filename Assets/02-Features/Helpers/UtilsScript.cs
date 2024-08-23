public struct UserInput
{
    public string action;
    public float delta;

    public UserInput(string act, float dt)
    {
        action = act;
        delta = dt;
    }
}