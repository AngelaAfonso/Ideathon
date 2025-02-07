const sendMessage = document.getElementById('send-message');
const userInput = document.getElementById('user-input');
const chatIcon = document.getElementById('chat-icon');
const chatWindow = document.getElementById('chat-window');
const closebutton = document.getElementById('close-chat');

// Função para enviar mensagens ao backend
async function sendMessageToBackend(message) {
    const currentUrl = window.location.href; //pega url do site que esta o chatbot
    try {
        const response = await fetch('https://localhost:7009/api/chatbot/message', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                message: message,
                url: currentUrl // enviando a url pro backend
            }),
        });
        if (!response.ok) {
            throw new Error(`Erro HTTP: ${response.status}`);
        }
        const data = await response.json();
        return data.response;
    } catch (error) {
        console.error('Erro ao enviar mensagem ao backend:', error);
        return 'Desculpe, ocorreu um erro ao processar sua mensagem.';
    }
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
    const chatMessages = document.getElementById('chat-messages');
    chatMessages.innerHTML = '';
    startChat(); // Chama a função de inicializar a conversa ao clicar no ícone
});

// Envento pra fechar chat pelo ícone X
closebutton.addEventListener("click", () => {
    chatWindow.classList.toggle("hidden");
    const chatMessages = document.getElementById('chat-messages');
    chatMessages.innerHTML = ''; // Limpando o chat pra inicializar de novo
});

// Função para iniciar a conversa automaticamente ao abrir o chat
async function startChat() {
    if (!chatWindow.classList.contains("hidden")) {
        const initialMessage = ""; // Envia uma mensagem vazia para inicializar o chat
        const botResponse = await sendMessageToBackend(initialMessage); // Recebe a resposta inicial
        addMessage('bot', botResponse); // Exibe a resposta do bot na tela
    }
}

// Função para processar o envio da mensagem
async function handleSendMessage() {
    const userMessage = userInput.value.trim();
    if (userMessage !== '') {
        addMessage('user', userMessage); // Adiciona a mensagem do usuário
        userInput.value = ''; // Limpa o campo de entrada
        try {
            const botResponse = await sendMessageToBackend(userMessage); // Envia a mensagem ao backend
            addMessage('bot', botResponse); // Exibe a resposta do bot
        } catch (error) {
            addMessage('bot', 'Ocorreu um erro ao processar sua mensagem.');
        }
    }
}

// Evento de clique no botão de envio de mensagem
sendMessage.addEventListener('click', handleSendMessage);

// Evento para capturar a tecla Enter no campo de entrada
userInput.addEventListener('keydown', async (event) => {
    if (event.key === 'Enter') { // Verifica se a tecla pressionada é o Enter
        event.preventDefault(); // Previne o comportamento padrão (como adicionar uma nova linha)
        await handleSendMessage(); // Chama a função para enviar a mensagem
    }
});