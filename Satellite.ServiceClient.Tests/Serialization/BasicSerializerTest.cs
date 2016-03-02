using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Satellite.Data.Test;
using Satellite.ServiceClient.Serialization;

namespace Satellite.ServiceClient.Tests.Serialization
{
	[TestFixture]
	public class BasicSerializerTest
	{
		private BasicSerializer<Note> Serializer { get; set; }

		private Note CreateNote()
		{
			Note note = new Note
			{
				@from = new string('f', 5000),
				heading = new string('h', 5000),
				to = new string('t', 5000)
			};
			return note;
		}

		private string Serialze(Note note)
		{
			return Serializer.Serialze(note);
		}

		private Note DeSerialize(string data)
		{
			return Serializer.DeSerialize(data);
		}

		[SetUp]
		public void SetUp()
		{
			Serializer = new BasicSerializer<Note>();
		}

		[Test]
		public void SpecialCharactersDoNotAppearInOutput()
		{
			Note note = CreateNote();

			string result = Serialze(note);

			Assert.IsFalse(string.IsNullOrEmpty(result));
			Assert.IsFalse(result.Contains("?"));
			Assert.IsFalse(result.Contains("  "));
			Assert.IsFalse(result.Contains("\t"));
			Assert.IsFalse(result.Contains("\r"));
			Assert.IsFalse(result.Contains("\n"));
		}

		[Test]
		public void NoXmlHeader()
		{
			Note note = CreateNote();

			string result = Serialze(note);

			Match match = Regex.Match(result, "^xml$");
			Assert.AreEqual(0, match.Length);
		}

		[Test]
		public void SoapNamespaceIsRespected()
		{
			Note note = CreateNote();

			string result = Serialze(note);

			Assert.IsTrue(result.Contains("<soap:Note"));
		}

		[Test]
		public void ValidObjectIsReturned()
		{
			Note note = CreateNote();
			string data = Serialze(note);

			Note result = DeSerialize(data);

			Assert.IsNotNull(result);
			Assert.AreEqual(note.body, result.body);
			Assert.AreEqual(note.from, result.from);
			Assert.AreEqual(note.heading, result.heading);
		}

		[Test]
		public void InvalidXml()
		{
			Assert.Throws<InvalidOperationException>(() => DeSerialize("invalid xml"));
		}

		[Test]
		public void DataIsNotOfProperType()
		{
			string notNoteData = GeoCodeAddressModelSample.StringSample;
			Assert.Throws<InvalidOperationException>(() => DeSerialize(notNoteData));
		}
	}
}
