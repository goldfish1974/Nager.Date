using Nager.Date.Contract;
using Nager.Date.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nager.Date.PublicHolidays
{
    /// <summary>
    /// Jersey
    /// </summary>
    public class JerseyProvider : IPublicHolidayProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// JerseyProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public JerseyProvider(ICatholicProvider catholicProvider)
        {
            this._catholicProvider = catholicProvider;
        }

        ///<inheritdoc/>
        public IEnumerable<PublicHoliday> Get(int year)
        {
            var countryCode = CountryCode.JE;

            var firstMondayInMay = DateSystem.FindDay(year, 5, DayOfWeek.Monday, 1);
            var lastMondayInMay = DateSystem.FindLastDay(year, 5, DayOfWeek.Monday);
            var lastMondayInAugust = DateSystem.FindLastDay(year, 8, DayOfWeek.Monday);

            var items = new List<PublicHoliday>();
            items.Add(new PublicHoliday(year, 1, 1, "New Year's Day", "New Year's Day", countryCode));
            items.Add(this._catholicProvider.GoodFriday("Good Friday", year, countryCode));
            items.Add(this._catholicProvider.EasterMonday("Easter Monday", year, countryCode));
            items.Add(new PublicHoliday(firstMondayInMay, "Early May Bank Holiday", "Early May Bank Holiday", countryCode));
            items.Add(new PublicHoliday(year, 5, 9, "Liberation Day", "Liberation Day", countryCode));
            items.Add(new PublicHoliday(lastMondayInMay, "Spring Bank Holiday", "Spring Bank Holiday", countryCode));
            items.Add(new PublicHoliday(lastMondayInAugust, "Summer Bank Holiday", "Summer Bank Holiday", countryCode));
            items.Add(new PublicHoliday(year, 12, 25, "Christmas Day", "Christmas Day", countryCode));
            items.Add(new PublicHoliday(year, 12, 26, "Boxing Day", "St. Stephen's Day", countryCode));

            return items.OrderBy(o => o.Date);
        }

        ///<inheritdoc/>
        public IEnumerable<string> GetSources()
        {
            return new string[]
            {
                "https://en.wikipedia.org/wiki/Public_holidays_in_Jersey"
            };
        }
    }
}
