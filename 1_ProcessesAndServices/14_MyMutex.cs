public class MutexInterlocked
{
    private int estado = 0;

    public void Lock()
    {
        while (Interlocked.CompareExchange(ref estado, 1, 0) != 0)
        {
            Thread.Sleep(100);
        }
    }

    public void Unlock()
    {
        Interlocked.Exchange(ref estado, 0);
    }
}
