using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchQuest.Models;
using DutchQuest.Services.Abstractions;

namespace DutchQuest.Services.Mock
{
    public class AppServiceData : IDataService
    {
        private IDataService dataServiceImplementation;

        public async Task Initialize()
        {

        }

        public async Task Sync()
        {

        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            var topics = new List<Topic>();

            var colorTopic = new Topic()
            {
                Title = "Colours",
                Words = GetColours()
            };
            topics.Add(colorTopic);

            var emotionsTopic = new Topic()
            {
                Title = "Emotions"
            };
            topics.Add(emotionsTopic);

            var foodTopic = new Topic()
            {
                Title = "Food"
            };
            topics.Add(foodTopic);


            return await Task.FromResult(topics);
        }

        List<Word> GetColours()
        {
            var colours = new List<Word>();
            var white = new Models.Word()
            {
                English = "White",
                Dutch = "Wit"
            };

            var black = new Models.Word()
            {
                English = "Black",
                Dutch = "Zwart"
            };

            var grey = new Models.Word()
            {
                English = "Grey",
                Dutch = "Grijs"
            };

            var brown = new Models.Word()
            {
                English = "Brown",
                Dutch = "Bruin"
            };

            var purple = new Models.Word()
            {
                English = "Purple",
                Dutch = "Paars"
            };

            var orange = new Models.Word()
            {
                English = "Orange",
                Dutch = "Oranje"
            };

            var yellow = new Models.Word()
            {
                English = "Yellow",
                Dutch = "Geel"
            };

            var green = new Models.Word()
            {
                English = "Green",
                Dutch = "Groen"
            };

            colours.Add(white);
            colours.Add(black);
            colours.Add(grey);
            colours.Add(brown);
            colours.Add(purple);
            colours.Add(orange);
            colours.Add(yellow);
            colours.Add(green);

            return colours;
        }
    }
}
