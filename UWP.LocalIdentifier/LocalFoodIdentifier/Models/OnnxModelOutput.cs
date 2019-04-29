using System.Collections.Generic;
using Windows.AI.MachineLearning;

namespace LocalFoodIdentifier.Models
{
    public class OnnxModelOutput
    {
        public TensorString ClassLabel = TensorString.Create(new long[] { 1, 1 });
        public IList<IDictionary<string, float>> Loss = new List<IDictionary<string, float>>();
    }
}
