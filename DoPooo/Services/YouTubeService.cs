using DoPooo.Services.IServices;
using DoPooo.ThirdPartyApi;
using DoPooo.ViewModel;
using Microsoft.Extensions.Caching.Memory;

namespace DoPooo.Services
{
    public class YouTubeService : IYouTubeService
    {
        private IMemoryCache _cache;
        private IConfiguration _myConfiguration;
        private YouTube _youTube;

        public YouTubeService(IMemoryCache memoryCache,IConfiguration configuration)
        {
            _cache = memoryCache;
            _myConfiguration = configuration;
            _youTube = new YouTube(configuration);
        }

        public async Task<YouTubeVideoViewModel> GetMostPopular(string regionCode)
        {
            YouTubeVideoViewModel viewModel = null;

            if (!_cache.TryGetValue($"popular {regionCode}", out viewModel))
            {
                viewModel = await _youTube.GetMostPopular(regionCode);

                if (viewModel != null)
                {
                    _cache.Set($"popular {regionCode}", viewModel,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(6)));
                }
            }

            return viewModel;
        }

        public async Task<YouTubeVideoViewModel> GetMostPopularByTopic(string topic)
        {
            YouTubeVideoViewModel viewModel = null;

            if(!_cache.TryGetValue(topic, out viewModel))
            {
                viewModel = await _youTube.GetMostPopularByTopic(topic);
                
                if(viewModel != null)
                {
                    _cache.Set(topic, viewModel,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(6)));
                }
            }

            return viewModel;
        }
    }
}
