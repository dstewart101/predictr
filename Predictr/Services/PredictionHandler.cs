using Predictr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.Services
{
    public class PredictionHandler
    {

        private List<Prediction> _predictions;
        private Fixture _fixture; 

        public PredictionHandler(List<Prediction> predictions, Fixture fixture) {

            _predictions = predictions;
            _fixture = fixture;

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
                    _prediction.Points = 3;
                }

                // correct result
                else if (CorrectResult(_prediction, _fixture))
                {
                    _prediction.Points = 1;
                }

                // nothing
                else 
                {
                    _prediction.Points = 0;
                }

                // joker?

                if (CorrectResult(_prediction, _fixture) && JokerPlayed(_prediction)) {
                    _prediction.Points = 3;
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
    }
}
