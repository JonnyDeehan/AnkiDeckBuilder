using OpenAI_API.Chat;

namespace AnkiDeckBuilder.Services
{
    public class DeckBuilder
    {
        private string _cardAnkiFormat;

        private string _apiKey;

        public DeckBuilder()
        {
            // Load text file format from file to use in OpenAI API request
            _cardAnkiFormat = File.ReadAllText("../AnkiDeckBuilder/Data/text/LanguageCardFormat.txt");

            // Fetch openAI API key from environment variables
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        }

        /// <summary>
        /// Calls OpenAI API to translate a list of words to a target language and returns the result in a format that can be imported into Anki
        /// </summary>
        /// <param name="words">List of words created by the user</param>
        /// <param name="targetLanguage">The selected target language the user wishes to learn</param>
        /// <returns>The filename of the created file</returns>
        public async Task<string> BuildDeckAsync(string[] words, string targetLanguage)
        {
            // Create chat prompt for OpenAI API
            string chatPrompt = $"Perform the following numbered steps: 1.Convert the following list of words: {string.Join(",", words)} into this language:{targetLanguage}. 2.For each word, provide the accompanying phonetic romanization equivalent of how to say said word, e.g. in Chinese Pinyin is used for this. 3.Also accompany each word with an example sentence using that word. Where possible, construct a sentence utilising other words from the list. Keep the sentences short, and practical, i.e. a sentence that would like be used in day to day conversation. Also provide the phonetic equivalent of that sentence. 4.Present the result in the same following format which Anki can then read: ${_cardAnkiFormat}, where each word, its translation, example sentence and phonetic equivalent are grouped. Simply provide this Anki format result, including the column headers Translation;Word;Phonetic;Sentence;TargetSentence;PhoneticSentence, and nothing else in the response.";

            // Use OpenAI API to translate words to target language
            OpenAiClient openAiClient = new OpenAiClient();
            ChatResult result = await openAiClient.GetChatResultAsync(_apiKey, chatPrompt);

            // Save result to text file which can be downloaded by the user
            var fileName = $"LanguageDeck_{targetLanguage}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt";
            File.WriteAllText($"../AnkiDeckBuilder/Data/results/{fileName}", result.ToString());

            return fileName;
        }
    }
}
