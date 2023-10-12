public interface IMySerializable {
    public IMySerializable GetData();
    public System.Action OnLoaded(IMySerializable data);
}