namespace DoPooo.Services.IServices
{
    public interface IMail
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
