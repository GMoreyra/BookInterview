﻿using System.Globalization;
using Utils;

namespace Tests
{
    public class PublishDateHelperTests
    {
        [Theory]
        [InlineData(2021, null, null, "2021-01-01 00:00:00")]
        [InlineData(2021, 8, null, "2021-08-01 00:00:00")]
        [InlineData(2021, 8, 15, "2021-08-15 00:00:00")]
        [InlineData(null, null, null, null)]
        [InlineData(null, 8, 15, null)]
        [InlineData(2021, null, 15, null)]
        [InlineData(2021, 13, 15, null)]
        [InlineData(2021, 8, 32, null)]
        public void ParseDate_ReturnsExpectedResult(int? year, int? month, int? day, string expected)
        {
            DateTime? expectedResult = expected != null ? DateTime.ParseExact(expected, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) : (DateTime?)null;
            DateTime? result = PublishDateHelper.ParseDate(year, month, day);

            Assert.Equal(expectedResult, result);
        }
    }
}
