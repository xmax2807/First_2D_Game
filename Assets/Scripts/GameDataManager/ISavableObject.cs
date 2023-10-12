public interface ISavableObject{
    bool IsActive();
    void ToggleActive(bool value);
    string GetID();
    void DestroyThisObject();
    public event System.Action OnDestroyed;
}