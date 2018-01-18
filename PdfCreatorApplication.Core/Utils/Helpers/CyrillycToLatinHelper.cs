using System.Collections.Generic;
using System.Text;

namespace PdfCreatorApplication.Core.Utils.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class CyrillycToLatinHelper
    {
        /// <summary>
        /// The rules
        /// </summary>
        private static readonly Dictionary<char, string> Rules = new Dictionary<char, string>()
        {
            {'А',"a"},
            {'Б',"b"},
            {'В',"v"},
            {'Г',"g"},
            {'Д',"d"},
            {'Е',"e"},
            {'Ё',"yo"},
            {'Ж',"zh"},
            {'З',"z"},
            {'И',"i"},
            {'Й',"j"},
            {'I',"i"},
            {'К',"k"},
            {'Л',"l"},
            {'М',"m"},
            {'Н',"n"},
            {'О',"o"},
            {'П',"p"},
            {'Р',"r"},
            {'С',"s"},
            {'Т',"t"},
            {'У',"u"},
            {'Ф',"f"},
            {'Х',"x"},
            {'Ц',"cz"},
            {'Ч',"ch"},
            {'Ш',"sh"},
            {'Щ',"shh"},
            {'Ъ',""},
            {'Ы',"y"},
            {'Ь',""},
            {'Э',"e"},
            {'Ю',"yu"},
            {'Я',"ya"}
        };

        /// <summary>
        /// Convert entry text to the latin translit.
        /// </summary>
        /// <param name="cyrillicText">The cyrillic text.</param>
        /// <returns></returns>
        public static string ToLatin(this string cyrillicText)
        {
            if (string.IsNullOrEmpty(cyrillicText))
            {
                return cyrillicText;
            }

            cyrillicText = cyrillicText.ToUpperInvariant();

            var translitText = new StringBuilder();
            for (var srcIndex = 0; srcIndex < cyrillicText.Length; srcIndex++)
            {
                string substitute;
                if (Rules.TryGetValue(cyrillicText[srcIndex], out substitute))
                {
                    translitText.Append(substitute);
                }
                else
                {
                    translitText.Append(cyrillicText[srcIndex]);
                }
            }

            return translitText.ToString();
        }
    }
}
