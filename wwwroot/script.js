const sendMessage = document.getElementById('send-message');
const userInput = document.getElementById('user-input');

// Função para enviar mensagens ao backend
async function sendMessageToBackend(message) {
    const response = await fetch('http://localhost:7009/api/chatbot/message', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ text: message }),
    });

    const data = await response.json();
    return data.response;
}

function addMessage(sender, text) {
    const chatMessages = document.getElementById('chat-messages');
    const messageDiv = document.createElement('div');
    messageDiv.classList.add(sender === 'user' ? 'user-message' : 'bot-message');
    messageDiv.textContent = text;
    chatMessages.appendChild(messageDiv);
    chatMessages.scrollTop = chatMessages.scrollHeight; // Rola para a última mensagem
}

// Evento para enviar mensagens
sendMessage.addEventListener('click', async () => {
    const userMessage = userInput.value.trim();
    if (userMessage !== '') {
        addMessage('user', userMessage); // Adiciona mensagem do usuário
        userInput.value = ''; // Limpa o campo de entrada

        // Envia a mensagem ao backend e recebe a resposta
        const botResponse = await sendMessageToBackend(userMessage);
        addMessage('bot', botResponse);
    }
});