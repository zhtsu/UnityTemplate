public interface FB_IModable<T>
     where T : class, FB_IData
{
    void Initialize(T Data);
}
