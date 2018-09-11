using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;

namespace Apartment_ML
{
    // IrisPrediction is the result returned from prediction operations
    public class ApartmentPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
}
