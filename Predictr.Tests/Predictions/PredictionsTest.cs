using Predictr.Models;
using Predictr.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Predictr.Tests.Predictions
{
    public class PredictionsTest
    {
        private Fixture fixture;
        private List<Prediction> predictions = new List<Prediction>();
        private PredictionHandler ph;

        public PredictionsTest() {
            fixture = new Fixture { Group = "B", FixtureDateTime = DateTime.Now, Home = "England", Away = "Belgium", HomeScore = 3, AwayScore = 2 };

        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsZeroForIncorrectResult()
        {
            // Arrange
            Prediction incorrectResult = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 1, AwayScore = 1 };
            predictions.Add(incorrectResult);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(0, incorrectResult.Points);
        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsOnePointForCorrectResult()
        {
            // Arrange
            Prediction correctResult = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 2, AwayScore = 1 };
            predictions.Add(correctResult);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(1, correctResult.Points);
        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsThreePointForCorrectScore()
        {
            // Arrange
            Prediction correctScore = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 3, AwayScore = 2 };
            predictions.Add(correctScore);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(3, correctScore.Points);
        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsZeroPointsForIncorrectResultWhenJokerPlayed()
        {
            // Arrange
            Prediction incorrectResultAndJoker = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 0, AwayScore = 0, Joker = true };
            predictions.Add(incorrectResultAndJoker);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(0, incorrectResultAndJoker.Points);
        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsThreePointForCorrectResultWhenJokerPlayed()
        {
            // Arrange
            Prediction correctResultAndJoker = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 1, AwayScore = 0, Joker = true };
            predictions.Add(correctResultAndJoker);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(3, correctResultAndJoker.Points);
        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsThreePointForCorrectScoreWhenJokerPlayed()
        {
            // Arrange
            Prediction correctScoreAndJoker = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 3, AwayScore = 2, Joker = true };
            predictions.Add(correctScoreAndJoker);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(3, correctScoreAndJoker.Points);
        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsZeroPointsForIncorrectResultWhenDoublePlayed()
        {
            // Arrange
            Prediction incorrectResultAndDouble = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 4, AwayScore = 5, DoubleUp = true };
            predictions.Add(incorrectResultAndDouble);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(0, incorrectResultAndDouble.Points);
        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsTwoPointsForCorrectResultWhenDoublePlayed()
        {
            // Arrange
            Prediction correctResultAndDouble = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 4, AwayScore = 0, DoubleUp = true };
            predictions.Add(correctResultAndDouble);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(2, correctResultAndDouble.Points);
        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsSixPointsForCorrectScoreWhenDoublePlayed()
        {
            // Arrange
            Prediction correctResultAndDouble = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 3, AwayScore = 2, DoubleUp = true };
            predictions.Add(correctResultAndDouble);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(6, correctResultAndDouble.Points);
        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsZeroPointsForIncorrectScoreWhenDoubleAndJokerPlayed()
        {
            // Arrange
            Prediction inCorrectResultAndDoubleAndJoker = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 0, AwayScore = 5, DoubleUp = true, Joker = true };
            predictions.Add(inCorrectResultAndDoubleAndJoker);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(0, inCorrectResultAndDoubleAndJoker.Points);
        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsSixPointsForCorrectResultWhenDoubleAndJokerPlayed()
        {
            // Arrange
            Prediction correctResultAndDoubleAndJoker = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 5, AwayScore = 0, DoubleUp = true, Joker = true };
            predictions.Add(correctResultAndDoubleAndJoker);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(6, correctResultAndDoubleAndJoker.Points);
        }

        [Fact]
        public void PredictionHandler_updatePredictions_awardsSixPointsForCorrectScoreWhenDoubleAndJokerPlayed()
        {
            // Arrange
            Prediction correctScoreAndDoubleAndJoker = new Prediction { Id = 1, FixtureId = fixture.Id, HomeScore = 5, AwayScore = 0, DoubleUp = true, Joker = true };
            predictions.Add(correctScoreAndDoubleAndJoker);

            ph = new PredictionHandler(predictions, fixture);

            // Act
            var result = ph.updatePredictions();

            // Assert
            Assert.Equal(6, correctScoreAndDoubleAndJoker.Points);
        }
    }
}
