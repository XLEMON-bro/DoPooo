namespace DoPooo.Services.IServices
{
    public interface IMailService
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
