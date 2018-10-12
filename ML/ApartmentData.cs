using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;

namespace ML
{
    public class ApartmentData
    {
        [Column("0")]
        public float ApartmentSize;

        [Column("1")]
        public float ApartmentPrice;

        [Column("2")]
        public float ApartmentFloorNumber;

        [Column("3")]
        [ColumnName("Label")]
        public string Label;
    }
}
