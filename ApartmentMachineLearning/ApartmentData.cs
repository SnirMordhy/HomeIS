using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;

namespace ApartmentMachineLearning
{
    // STEP 1: Define your data structures

    // IrisData is used to provide training data, and as 
    // input for prediction operations
    // - First 4 properties are inputs/features used to predict the label
    // - Label is what you are predicting, and is only set when training
    public class ApartmentData
    {
        [Column("0")]
        public float ApartmentSize;

        [Column("1")]
        public float ApartmentPrice;

        [Column("2")]
        public float ApartmentFloorNumber;

        [Column("4")]
        [ColumnName("Label")]
        public string Label;
    }
}
