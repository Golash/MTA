using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglandCheckers.Components
{
    /// <summary>
    /// The Coin class represent a soldier in the game (can be King or Regular)
    /// </summary>
    public class Coin
    {
        /// <summary>
        /// Create a new instance of the coin class
        /// </summary>
        /// <param name="i_Sign"></param>
        public Coin(eCoinSign i_Sign)
        {
            m_Sign = i_Sign;
            m_IsKing = false;
        }

        /// <summary>
        /// Gets the coin sign
        /// </summary>
        public eCoinSign Sign
        {
            get
            {
                return m_Sign;
            }
        }

        /// <summary>
        /// Gets or sets if the coin is a king coin
        /// </summary>
        public bool IsKing
        {
            get
            {
                return m_IsKing;
            }
            
            set
            {
                m_IsKing = value;
            }
        }

        /// <summary>
        /// Mapping function that map between regular coin sign to king coin sign
        /// The X sign get the K king sign
        /// The O sign get the U king sign
        /// </summary>
        private eKingCoinSign getKingSignByCoinSign(eCoinSign i_Sign)
        {
            eKingCoinSign kingSign = eKingCoinSign.K;
            if (i_Sign == eCoinSign.O)
            {
                kingSign = eKingCoinSign.U;
            }

            return kingSign;
        }

        /// <summary>
        /// Gets the string value that represent the current coin.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string coinStrValue = m_Sign.ToString();
            if (IsKing)
            {
                coinStrValue = getKingSignByCoinSign(m_Sign).ToString();
            }

            return coinStrValue;
        }

        private eCoinSign m_Sign;
        private bool m_IsKing;
    }
}
