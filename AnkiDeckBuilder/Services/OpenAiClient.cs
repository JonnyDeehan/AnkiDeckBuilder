using OpenAI_API;
using OpenAI_API.Chat;

namespace AnkiDeckBuilder.Services
{
    public class OpenAiClient
    {
        public OpenAiClient() { }

        public async Task<ChatResult> GetChatResultAsync(string apiKey, string chatPrompt)
        {
            OpenAIAPI api = new OpenAIAPI(new APIAuthentication(apiKey));
            return await api.Chat.CreateChatCompletionAsync(chatPrompt);
        }
    }
}
