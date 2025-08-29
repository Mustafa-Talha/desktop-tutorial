using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace YourApp.Controllers
{
    public class ChatController : Controller
    {
        // Basit keyword tabanlı cevaplar
        private static readonly Dictionary<string, string> Responses = new Dictionary<string, string>()
        {
            { "merhaba", "Merhaba! Sana nasıl yardımcı olabilirim?" },
            { "selam", "Selam! Nasılsın?" },
            { "nasılsın", "Ben iyiyim, teşekkür ederim! Sen nasılsın?" },
            { "hava", "Maalesef ben hava durumunu gerçek zamanlı veremem, ama umarım güzel bir gün geçiriyorsundur!" },
            { "teşekkür", "Rica ederim! 😊" },
            { "gpt", "Evet, ben bir GPT tarzı chatbot'um!" }
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageDto req)
        {
            // typing hissi için gecikme
            await Task.Delay(600);

            // Kullanıcı mesajını al
            string userText = req?.Content?.Trim().ToLower() ?? "";

            // replyText'i başlat (CS0165 hatasını önler)
            string replyText = "";

            bool matched = false;
            foreach (var key in Responses.Keys)
            {
                if (userText.Contains(key))
                {
                    replyText = Responses[key];
                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                // keyword eşleşmezse rastgele cevap
                var defaultReplies = new List<string>
                {
                    "Bunu daha sonra öğrenmem gerekecek. 😅",
                    "Hmmm, ilginç bir soru!",
                    "Şu an bunu anlayamadım, başka bir şekilde sorabilir misin?",
                    "Geliştirme aşamasında, lütfen sabırlı ol!"
                };
                var rand = new System.Random();
                replyText = defaultReplies[rand.Next(defaultReplies.Count)];
            }

            // JSON formatında cevabı döndür
            return Json(new { role = "assistant", content = replyText });
        }
    }

    // Chat mesaj DTO
    public class ChatMessageDto
    {
        public string Role { get; set; } = "user";  // Default role = user
        public string Content { get; set; } = "";   // Default content = boş string
    }
}
