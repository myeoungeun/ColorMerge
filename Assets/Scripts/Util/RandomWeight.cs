using System.Collections.Generic;

namespace Util
{
    public static class RandomWeight
    {
        public static int GetRandomIndex(List<float> weights)
        {
            float sum = 0;
            int index = -1;
            
            foreach(float weight in weights)
                sum += weight;
            
            float random = UnityEngine.Random.Range(0, sum);

            for (int i = 0; i < weights.Count; i++)
            {
                random -= weights[i];
                if (random <= 0)
                {
                    index = i;
                    break;
                }
            }
            
            return index;
        }
    }
}