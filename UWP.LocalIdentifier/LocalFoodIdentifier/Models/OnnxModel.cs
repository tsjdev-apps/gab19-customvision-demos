using System;
using System.Threading.Tasks;
using Windows.AI.MachineLearning;
using Windows.Storage;

namespace LocalFoodIdentifier.Models
{
    public class OnnxModel
    {
        private LearningModelSession _session;

        public static async Task<OnnxModel> CreateOnnxModel(StorageFile file)
        {
            LearningModel learningModel;

            try
            {
                learningModel = await LearningModel.LoadFromStorageFileAsync(file);
            }
            catch (Exception e)
            {
                var exceptionStr = e.ToString();
                Console.WriteLine(exceptionStr);

                throw e;
            }
            return new OnnxModel()
            {
                _session = new LearningModelSession(learningModel)
            };
        }

        public async Task<OnnxModelOutput> EvaluateAsync(OnnxModelInput input)
        {
            var output = new OnnxModelOutput();

            var binding = new LearningModelBinding(_session);
            binding.Bind("data", input.Data);
            binding.Bind("classLabel", output.ClassLabel);
            binding.Bind("loss", output.Loss);

            await _session.EvaluateAsync(binding, "0");

            return output;
        }
    }
}
