using Predictr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.Services
{
    public class PredictionHandler
    {
        private const int CORRECT_SCORE_POINTS = 3;
        private const int CORRECT_RESULT_POINTS = 1;
        private List<Prediction> _predictions;
        private Fixture _fixture;
        private String _user;

        public PredictionHandler(List<Prediction> predictions, Fixture fixture) {

            _predictions = predictions;
            _fixture = fixture;

        }

        public PredictionHandler(List<Prediction> predictions, String user) {
            _predictions = predictions;
            _user = user;
        }



        public Boolean CorrectResult(Prediction _prediction, Fixture _fixture) {
            if (_prediction.HomeScore > _prediction.AwayScore && this._fixture.HomeScore > this._fixture.AwayScore
                                || _prediction.HomeScore < _prediction.AwayScore && this._fixture.HomeScore < this._fixture.AwayScore
                                || _prediction.HomeScore == _prediction.AwayScore && this._fixture.HomeScore == this._fixture.AwayScore   // correct result: +1 pt
                            )
                return true;
            else {
                return false;
            }
        }

        public List<Prediction> updatePredictions() {

            foreach (Prediction _prediction in _predictions) {

                // correct score?
                if (CorrectScore(_prediction, _fixture))
                {
                    _prediction.Points = CORRECT_SCORE_POINTS;
                }

                // correct result
                else if (CorrectResult(_prediction, _fixture))
                {
                    _prediction.Points = CORRECT_RESULT_POINTS;
                }

                // nothing
                else 
                {
                    _prediction.Points = 0;
                }

                // joker?

                if (CorrectResult(_prediction, _fixture) && JokerPlayed(_prediction)) {
                    _prediction.Points = CORRECT_SCORE_POINTS;
                }

                // double-up
                if (CorrectResult(_prediction, _fixture) && DoubleUpPlayed(_prediction))
                {
                    _prediction.Points = _prediction.Points * 2;
                }
            }

            return _predictions;
        }

        public Boolean CorrectScore(Prediction _prediction, Fixture _fixture)
        {
            if (_prediction.HomeScore == _fixture.HomeScore && _prediction.AwayScore == _fixture.AwayScore)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public Boolean JokerPlayed(Prediction _prediction)
        {
            if (_prediction.Joker == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean DoubleUpPlayed(Prediction _prediction)
        {
            if (_prediction.DoubleUp == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CountDoublesPlayed() {
            return _predictions
                        .Where(p => p.ApplicationUserId == _user)
                        .Where(p => p.DoubleUp == true)
                        .Count();
        }

        public int CountJokersPlayed()
        {
            return _predictions
                        .Where(p => p.ApplicationUserId == _user)
                        .Where(p => p.Joker == true)
                        .Count();
        }
    }
}
