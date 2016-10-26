using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

namespace DutchQuest.Services.Azure
{
    public class CognitiveService
    {
        VisionServiceClient visionClient = new VisionServiceClient(Helpers.Keys.CognitiveServiceKey);

        public async Task<AnalysisResult> GetImageDescription(Stream imageStream)
        {
            try
            {
                VisualFeature[] features = { VisualFeature.Tags, VisualFeature.Categories, VisualFeature.Description };
                var results = await visionClient.AnalyzeImageAsync(imageStream, features.ToList(), null);
                return results;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }

}
