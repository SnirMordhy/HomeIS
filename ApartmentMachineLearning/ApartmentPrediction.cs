﻿using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApartmentMachineLearning
{
    class ApartmentPrediction
    {
        [Microsoft.ML.Runtime.Api.ColumnName("PredictedLabel")]
        public string PredictedLabels = "";
    }
}
