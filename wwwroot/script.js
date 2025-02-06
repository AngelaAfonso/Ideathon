const sendMessage = document.getElementById('send-message');
const userInput = document.getElementById('user-input');
const chatIcon = document.getElementById('chat-icon');
const chatWindow = document.getElementById('chat-window');

// Função para enviar mensagens ao backend
async function sendMessageToBackend(message) {
    const response = await fetch('https://localhost:7009/api/chatbot/message', {  // Certifique-se de que a URL esteja correta
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ message: message }),
    });

    const data = await response.json();
    return data.response;
}

// Função para adicionar a mensagem no chat
function addMessage(sender, text) {
    const chatMessages = document.getElementById('chat-messages');
    const messageDiv = document.createElement('div');
    messageDiv.classList.add(sender === 'user' ? 'user-message' : 'bot-message');
    messageDiv.textContent = text;
    chatMessages.appendChild(messageDiv);
    chatMessages.scrollTop = chatMessages.scrollHeight; // Rola para a última mensagem
}

// Evento para abrir e fechar o chat
chatIcon.addEventListener("click", () => {
    chatWindow.classList.toggle("hidden");
    startChat(); // Chama a função de inicializar a conversa ao clicar no ícone
});

// Função para iniciar a conversa automaticamente ao abrir o chat
async function startChat() {
    // Verifica se a janela do chat está visível
    if (!chatWindow.classList.contains("hidden")) {
        // Envia uma mensagem vazia ao backend para obter a resposta inicial
        const initialMessage = ""; // Envia uma mensagem vazia para inicializar o chat
        const botResponse = await sendMessageToBackend(initialMessage); // Recebe a resposta inicial
        addMessage('bot', botResponse); // Exibe a resposta do bot na tela
    }
}

// Evento de clique no botão de envio de mensagem
sendMessage.addEventListener('click', async () => {
    const userMessage = userInput.value.trim();
    if (userMessage !== '') {
        addMessage('user', userMessage); // Adiciona a mensagem do usuário
        userInput.value = ''; // Limpa o campo de entrada

        // Envia a mensagem ao backend e recebe a resposta
        const botResponse = await sendMessageToBackend(userMessage);
        addMessage('bot', botResponse); // Exibe a resposta do bot
    }
});
