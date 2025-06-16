document.addEventListener("DOMContentLoaded", () => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:8080/chatHub", {
            transport: signalR.HttpTransportType.WebSockets
        })
        .configureLogging(signalR.LogLevel.Information)
        .build();

    if (connection.state === "Disconnected") { // ✅ Prevent duplicate starts
        connection.start()
            .then(() => console.log("Connected to SignalR ChatHub!"))
            .catch(err => console.error("SignalR connection error:", err));
    }

    connection.on("ReceiveMessage", (user, message) => {
        const chatBox = document.getElementById("chatBox");
        const messageElement = document.createElement("p");
        messageElement.innerHTML = `<strong>${user}:</strong> ${message}`;
        chatBox.appendChild(messageElement);
        chatBox.scrollTop = chatBox.scrollHeight;
    });

    document.getElementById("sendMessageButton").addEventListener("click", () => {
        sendMessage(connection);
    });
});

function sendMessage(connection) {
    if (connection.state !== "Connected") {
        console.error("SignalR connection is not established yet.");
        return;
    }

    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;

    if (!user || !message) {
        alert("Please enter both username and message!");
        return;
    }

    connection.invoke("SendMessage", user, message)
        .catch(err => console.error("Error sending message:", err));

    document.getElementById("messageInput").value = "";
}
