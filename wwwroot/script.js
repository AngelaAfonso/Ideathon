const sendMessage = document.getElementById('send-message');
const userInput = document.getElementById('user-input');
const chatIcon = document.getElementById('chat-icon');
const chatWindow = document.getElementById('chat-window');

// Função para enviar mensagens ao backend
async function sendMessageToBackend(message) {
    const response = await fetch('http://localhost:5000/chat', {  // Certifique-se de que a URL esteja correta
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
});

// Evento para enviar mensagens ao clicar no botão
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
