﻿using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Marshalling.Serializers.AtomPlusXml
{
    [TestFixture]
    public class AtomPlusXmlSerializerTests
    {
        private IResourceSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            serializer = new AtomPlusXmlSerializer();
        }

        [Test]
        public void ShouldSerializeAResource()
        {
            var resource = new SomeResource { Name = "John Doe", Amount = 123.45 };
            var atom = serializer.Serialize(resource);

            Assert.That(atom.Contains("<entry"));
            Assert.That(atom.Contains("John Doe"));
            Assert.That(atom.Contains("</entry>"));
        }

        [Test]
        public void ShouldSerializeAListOfResources()
        {
            var resources = new []
                                          {
                                              new SomeResource {Amount = 123.45, Name = "John Doe"},
                                              new SomeResource {Amount = 67.89, Name = "Sally Doe"}
                                          };

            var atom = serializer.Serialize(resources);

            Assert.That(atom.Contains("<feed xmlns=\"http://www.w3.org/2005/Atom\">"));
            Assert.That(atom.Contains("John Doe"));
            Assert.That(atom.Contains("Sally Doe"));
            Assert.That(atom.Contains("</feed>"));
        }

        private IList<Relation> SomeRelations()
        {
            return new List<Relation> { new Relation("pay", "http://some/url") };
        }
    }
}
