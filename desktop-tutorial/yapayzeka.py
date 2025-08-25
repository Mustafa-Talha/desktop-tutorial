def chatbot(input_text):
    input_text = input_text.lower()
    if "merhaba" in input_text:
        return "Merhaba! Nasılsınız?"
    elif "Nasılsın" in input_text:
        return "Ben İyiyim, sen nasılsın?"
    elif "proje" in input_text:
        return "Python ile AI projeleri yapıyoruz!"
    else:
        return "Üzgünüm bunu anlamdım, başka bir şey sorabilir miziniz?"
    

while True:
    user_input = input("sen: ")
    if user_input.lower() == "çık":
        print("ChatBot: Görüşürüz! ")
        break
    print("Chatbot: ", chatbot(user_input))