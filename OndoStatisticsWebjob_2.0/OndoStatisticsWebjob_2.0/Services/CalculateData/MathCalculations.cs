namespace OndoStatisticsWebjob_2._0.Services.CalculateData
{
    class MathCalculations
    {
        /// <summary>
        /// Calculates the average number from a sum and a count
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int calculateAverage(int sum, int count)
        {
            return sum / count;
        }

        /// <summary>
        /// Returns the percentage of a totalAmount given a givenAmount. Returns 0 if totalAmount is 0
        /// </summary>
        /// <param name="givenQuantity"></param>
        /// <param name="totalAmount"></param>
        /// <returns></returns>
        public static int calculatePercentage(int givenQuantity, int totalAmount )
        {
            return (totalAmount != 0) ? (givenQuantity * 100) / totalAmount : 0;
        }
    }
}
