using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Legacy;
using Microsoft.ML.Legacy.Data;
using Microsoft.ML.Legacy.Trainers;
using Microsoft.ML.Legacy.Transforms;

namespace ML
{
    public class PredictApartment
    {
        private PredictionModel<ApartmentData, ApartmentPrediction> model;

        public PredictApartment()
        {
            // Creating a pipeline and loading the data
            var pipeline = new LearningPipeline();

            // Pipelining the training file
            string dataPath = System.AppDomain.CurrentDomain.BaseDirectory + @"..\debug\Apartment-Trainer.txt";
            pipeline.Add(new TextLoader(dataPath).CreateFrom<ApartmentData>(separator: ','));

            // Labeling the data
            pipeline.Add(new Dictionarizer("Label"));

            // Putting features into a vector
            pipeline.Add(new ColumnConcatenator("Features", "ApartmentSize", "ApartmentPrice", "ApartmentFloorNumber"));

            // Adding learning algorithm
            pipeline.Add(new StochasticDualCoordinateAscentClassifier());

            // Converting the Label back into original text 
            pipeline.Add(new PredictedLabelColumnOriginalValueConverter() { PredictedLabelColumn = "PredictedLabel" });

            // Train the model
            this.model = pipeline.Train<ApartmentData, ApartmentPrediction>();
        }

        public bool PredictApartmentSale(int size, int price, int floornumber)
        {
            // Make a prediction
            var prediction = this.model.Predict(new ApartmentData()
            {
                ApartmentSize = size,
                ApartmentFloorNumber = floornumber,
                ApartmentPrice = price
            });

            return (prediction.PredictedLabels == "true");
        }
    }
}
