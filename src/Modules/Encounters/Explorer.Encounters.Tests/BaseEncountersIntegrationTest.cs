using Explorer.BuildingBlocks.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Tests;

internal class BaseEncountersIntegrationTest : BaseWebIntegrationTest<EncountersTestFactory>
{
    public BaseEncountersIntegrationTest(EncountersTestFactory factory) : base(factory)
    {
    }
}
