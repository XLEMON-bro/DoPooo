using DoPooo.ViewModel;

namespace DoPooo.Services.IServices
{
    public interface IYouTubeService
    {
        public Task<YouTubeVideoViewModel> GetMostPopularByTopic(string regionCode = "cryptocurrency news today");

        public Task<YouTubeVideoViewModel> GetMostPopular(string topic = "us");
    }
}
