﻿using System.IO;
using NUnit.Framework;
using Pixelator.Api.Configuration;
using Pixelator.Api.Utility;

namespace Pixelator.Api.Tests.Integration
{
    [TestFixture]
    class V1DecodingCompatTest : DecodingVersionCompatTestBase
    {
        protected override DirectoryInfo InputDirectory
        {
            get { return new DirectoryInfo("./VersionCompatData/V1"); }
        }

        protected override DecodingConfiguration DecodingConfiguration
        {
            get { return new DecodingConfiguration("!!somePass!!", new MemoryStorageProvider(), 4096); }
        }
    }
}