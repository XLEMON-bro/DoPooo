using DoPooo.ViewModel;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace DoPooo.ThirdPartyApi
{
    public class YouTube
    {
        private IConfiguration MyConfiguration;
        public YouTube(IConfiguration configuration)
        {
            MyConfiguration = configuration;
        }

        public async Task<YouTubeVideoViewModel> GetMostPopular(string regionCode)
        {
            var youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = MyConfiguration.GetSection("YouTubeApi").Value
            });

            var request = youTubeService.Videos.List("snippet");
            request.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;
            request.MaxResults = 6;
            request.RegionCode = regionCode;


            var result = await request.ExecuteAsync();

            if (result.Items.Count <= 0)
                return null;

            var viewModel = new YouTubeVideoViewModel()
            {
                VideoData = new List<Models.YouTubeVideoModel>(),
                Title = "Most Popular Videos"
            };

            foreach (var videoInfo in result.Items)
            {
                viewModel.VideoData.Add(new Models.YouTubeVideoModel()
                {
                    VideoId = videoInfo.Id,
                    Description = videoInfo.Snippet.Title
                });
            }

            return viewModel;
        }

        public async Task<YouTubeVideoViewModel> GetMostPopularByTopic(string topic)
        {
            var youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = MyConfiguration.GetSection("YouTubeApi").Value
            });

            var searchListRequest = youTubeService.Search.List("snippet");
            searchListRequest.Q = topic;
            searchListRequest.MaxResults = 6;


            var result = await searchListRequest.ExecuteAsync();

            if (result.Items.Count <= 0)
                return null;

            var viewModel = new YouTubeVideoViewModel()
            {
                VideoData = new List<Models.YouTubeVideoModel>(),
                Title = "Related Video"
            };

            foreach (var searchResult in result.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        viewModel.VideoData.Add(new Models.YouTubeVideoModel()
                        {
                            VideoId = searchResult.Id.VideoId,
                            Description = searchResult.Snippet.Title
                        });
                        break;
                }

            }

            return viewModel;
        }
    }
}
