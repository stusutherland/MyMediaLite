// Copyright (C) 2010 Steffen Rendle, Zeno Gantner
//
// This file is part of MyMediaLite.
//
// MyMediaLite is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// MyMediaLite is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with MyMediaLite.  If not, see <http://www.gnu.org/licenses/>.

using System;


namespace MyMediaLite.item_recommender
{
    /// <summary>
    /// Weighted k-nearest neighbor user-based collaborative filtering using cosine-similarity
    /// </summary>
    /// <remarks>
    /// This engine does not support online updates.
	/// </remarks>
    public class WeightedUserKNN : UserKNN
    {
        /// <inheritdoc/>
        public override double Predict(int user_id, int item_id)
        {
            if ((user_id < 0) || (user_id > MaxUserID))
                throw new ArgumentException("user is unknown: " + user_id);
            if ((item_id < 0) || (item_id > MaxItemID))
                throw new ArgumentException("item is unknown: " + item_id);

			if (k == UInt32.MaxValue)
			{
				return correlation.SumUp(user_id, data_item[item_id]);
			}
			else
			{
				double result = 0;
				foreach (int neighbor in nearest_neighbors[user_id])
					if (data_user[neighbor, item_id])
						result += correlation[user_id, neighbor];
				return result;
			}
        }

		/// <inheritdoc/>
		public override string ToString()
		{
			return string.Format("weighted-user-kNN k={0}",
			                     k == uint.MaxValue ? "inf" : k.ToString());
		}
    }
}