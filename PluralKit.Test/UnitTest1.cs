using Autofac;
using NUnit.Framework;
using PluralKit.Core;
using System;
using System.Diagnostics;
using System.IO;

namespace PluralKit.Test
{
    public class Tests
    {
        IContainer services;

        [SetUp]
        public void Setup()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(InitUtils.BuildConfiguration(new string[0]).Build());
            builder.RegisterModule(new ConfigModule<object>());
            builder.RegisterModule(new LoggingModule("test"));
            builder.RegisterModule(new MetricsModule());
            builder.RegisterModule<DataStoreModule>();
            services = builder.Build();

            SchemaService.Initialize();

            var schema = services.Resolve<SchemaService>();

            schema.ApplyMigrations().Wait();
        }

        [Test]
        public void Test1()
        {
            Debug.WriteLine(services.Resolve<IDataStore>());
            Assert.Pass();
        }
    }
}