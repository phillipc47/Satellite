using NUnit.Framework;
using Satellite.ServiceClient.Tests.Data;

namespace Satellite.ServiceClient.Tests
{
	[TestFixture]
	public class BasicSerializerTest
	{
		private BasicSerializer<Note> Serializer { get; set; }

		private Note CreateNote()
		{
			Note note = new Note
			{
				from = new string('f', 5000),
				heading = new string('h', 5000),
				to = new string('t', 5000)
			};
			return note;
		}

		private string Serialze(Note note)
		{
			return Serializer.Serialze(note);
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
	}
}
