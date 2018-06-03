using Predictr.Models;
using Predictr.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Predictr.Tests.Predictions
{
    public class PredictionsTest
    {

        [Fact]
        public async Task PredictionHandler_updatePredictions_awardsZeroForIncorrectResult()
        {
            // Arrange
            Fixture fixture = new Fixture { Group = "B", FixtureDateTime = DateTime.Now, Home = "England", Away = "Belgium", HomeScore = 3, AwayScore = 2 };

            List<Prediction> predictions = new List<Prediction>();

            Prediction incorrectResult = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 1, AwayScore = 1 };

            predictions.Add(incorrectResult);
            var predictionHandler = new PredictionHandler(predictions, fixture);

            // Act
            var result = predictionHandler.updatePredictions();

            // Assert
            Assert.Equal(0, incorrectResult.Points);
        }
    }
}
