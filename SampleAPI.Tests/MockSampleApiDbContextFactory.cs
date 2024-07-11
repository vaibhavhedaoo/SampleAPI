﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SampleAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.Tests
{
    /// <summary>
    /// Mock database provided for your convenience.
    /// </summary>
    internal static class MockSampleApiDbContextFactory
    {
        public static SampleApiDbContext GenerateMockContext()
        {
            var options = new DbContextOptionsBuilder<SampleApiDbContext>()
                .UseInMemoryDatabase(databaseName: "mock_SampleApiDbContext")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var context = new SampleApiDbContext(options);
            return context;
        }
    }
}
