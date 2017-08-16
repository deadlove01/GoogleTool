using NUnit.Framework;
using System;
using Translation;

namespace Translation.Tests
{
   [TestFixture] public class BabelFishTests : NUnit.Framework.Assertion
	{
      [Test] public void SimpleTranslation()
      {
         BabelFish babel = new BabelFish();
         string french = babel.Translate("en_fr", "A simple sentence.");
         AssertEquals ("foo", french);
      }

      [Test, ExpectedException(typeof(ArgumentException))] 
      public void UnsupportedTranslation()
      {
         BabelFish babel = new BabelFish();
         babel.Translate("aa_aa", "not a valid translation");
      }
	}
}
