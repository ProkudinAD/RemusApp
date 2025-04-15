namespace Content.Application.Helper
{
    public static class IntervalHelper
    {
        /// <summary>
        /// Хелпер - проверяем пересекаются ли периоды
        /// </summary>
        /// <param name="periods"></param>
        /// <returns></returns>
        public static bool IsOverlap(DataInterval[] periods)
        {
            if (periods == null || periods.Length == 0)
            {
                return false;
            }

            for (var ii = 0; ii < periods.Length; ii++)
            {
                for (var jj = ii + 1; jj < periods.Length; jj++)
                {
                    if ((periods[ii].DataStart ?? DateTime.MinValue) <= (periods[jj].DateEnd ?? DateTime.MaxValue)
                        && (periods[ii].DateEnd ?? DateTime.MaxValue) >= (periods[jj].DataStart ?? DateTime.MinValue))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public class DataInterval
        {
            public DateTime? DataStart { get; set; }
            public DateTime? DateEnd { get; set; }
        }
    }
}