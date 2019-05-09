using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorSentimentAnalysis.Client.Pages
{
    public class Calculator : ComponentBase
    {
        [Inject] private HttpClient _http { get; set; }

        public float CurrentHappiness = 50; // 0=worst, 100=best

        public float? Min { get; set; }
        public float? Max { get; set; }
        public float Total { get; set; }
        public int Count { get; set; }

        public void AddValue(float value)
        {
            Total += value;
            Count++;
            if (!Min.HasValue || Min > value) Min = value;
            if (!Max.HasValue || Max < value) Max = value;
        }

        public float? GetAverage()
        {
            if (Count <= 0) return null;
            return Total / Count;
        }

        public async void UpdateScoreAsync(UIChangeEventArgs evt)
        {
            string targetText = (string)evt.Value;

            //Make a real call to Sentiment service
            CurrentHappiness = await PredictSentimentAsync(targetText);

            AddValue(CurrentHappiness);

            // See issue https://github.com/aspnet/Blazor/issues/519.  This is needed otherwise the binding to happiness isn't updated after the async call for prediction.
            StateHasChanged();
        }

        private async Task<float> PredictSentimentAsync(string targetText)
        {
            string url = $"api/Sentiment/sentimentprediction?sentimentText={targetText}";

            float percentage = await _http.GetJsonAsync<float>(url);

            return percentage;
        }
    }
}