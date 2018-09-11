using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;

namespace Apartment_ML
{
    class Program
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

        // IrisPrediction is the result returned from prediction operations
        public class ApartmentPrediction
        {
            [ColumnName("PredictedLabel")]
            public string PredictedLabels;
        }

        public class PredictApartment
        {
            private PredictionModel<ApartmentData, ApartmentPrediction> model;

            public PredictApartment()
            {
                // STEP 2: Create a pipeline and load your data
                var pipeline = new LearningPipeline();

                // If working in Visual Studio, make sure the 'Copy to Output Directory' 
                // property of iris-data.txt is set to 'Copy always'
                string dataPath = "Apartment-Trainer.txt";
                pipeline.Add(new TextLoader(dataPath).CreateFrom<ApartmentData>(separator: ','));

                // STEP 3: Transform your data
                // Assign numeric values to text in the "Label" column, because only
                // numbers can be processed during model training
                pipeline.Add(new Dictionarizer("Label"));

                // Puts all features into a vector
                pipeline.Add(new ColumnConcatenator("Features", "ApartmentSize", "ApartmentPrice", "ApartmentFloorNumber"));

                // STEP 4: Add learner
                // Add a learning algorithm to the pipeline. 
                // This is a classification scenario (What type of iris is this?)
                pipeline.Add(new StochasticDualCoordinateAscentClassifier());

                // Convert the Label back into original text (after converting to number in step 3)
                pipeline.Add(new PredictedLabelColumnOriginalValueConverter() { PredictedLabelColumn = "PredictedLabel" });

                // STEP 5: Train your model based on the data set
                this.model = pipeline.Train<ApartmentData, ApartmentPrediction>();
            }

            public bool PredictApartmentSale(int size, int price, int floornumber)
            {
                // STEP 6: Use your model to make a prediction
                // You can change these numbers to test different predictions
                var prediction = this.model.Predict(new ApartmentData()
                {
                    ApartmentSize = size,
                    ApartmentFloorNumber = floornumber,
                    ApartmentPrice = size
                });

                return (prediction.PredictedLabels == "true");
            }
        }
    }
}