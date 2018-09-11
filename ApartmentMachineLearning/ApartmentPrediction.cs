using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApartmentMachineLearning
{
    class ApartmentPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
}
