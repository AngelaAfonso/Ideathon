import google.generativeai as genai
from flask import Flask, request, jsonify
from dotenv import load_dotenv
from flask_cors import CORS

load_dotenv()

GOOGLE_GEMINI_API_KEY = "AIzaSyAMXMF6fbAEIQAK_kvjoMSXxedAmhJ_FwY"
genai.configure(api_key=GOOGLE_GEMINI_API_KEY)

# Configuração de segurança e geração
safety_settings = [
    {"category": "HATE_SPEECH", "threshold": "BLOCK_ONLY_HIGH"},
    {"category": "HARM_CATEGORY_DANGEROUS_CONTENT", "threshold": "BLOCK_ONLY_HIGH"},
]

generation_config = {
    "temperature": 0.7,
    "max_output_tokens": 100,
    "top_p": 1,
    "top_k": 40,
}

# Inicializando o modelo
model = genai.GenerativeModel(
    model_name="gemini-1.5-pro",
    safety_settings=safety_settings,
    generation_config=generation_config,
    system_instruction="You are an AI assistant that helps answer questions.",
)

# Criando a aplicação Flask
app = Flask(__name__)
CORS(app)

# Histórico de mensagens
history = []

@app.route("/api/chatbot/message", methods=["POST"])
def chat():
    data = request.get_json()
    user_message = data.get("message", "")

    if not user_message:
        return jsonify({"response": "Não entendi sua mensagem."})

    # Inicia a sessão de chat com o modelo
    chat_session = model.start_chat(history=history)
    response = chat_session.send_message(user_message)

    # Adiciona a resposta do bot ao histórico
    model_response = response.text
    history.append({"role": "user", "parts": [user_message]})
    history.append({"role": "model", "parts": [model_response]})

    return jsonify({"response": model_response})

if __name__ == "__main__":
    app.run(debug=True)
